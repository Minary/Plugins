namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.Session.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Drawing;
  using System.IO;
  using System.Text.RegularExpressions;
  using System.Windows.Forms;
  using MngSessions = Session.ManageSessions;
  using MngSessionsConfig = Session.ManageSessions.DataTypes;


  public partial class Plugin_Sessions : UserControl, IPlugin
  {

    #region MEMBERS

    private const int MaxTableRows = 128;
    private string iconsDir = @"\Icons";
    private List<Tuple<string, string, string>> targetList;
    private BindingList<TheSessionRecord> sessionRecords;
    private List<MngSessionsConfig.SessionPattern> sessionPatterns = new List<MngSessionsConfig.SessionPattern>();
    private List<string> dataBatch;
    private Session.Infrastructure.Session infrastructureLayer;
    private PluginProperties pluginProperties;
    private TreeNode filterNode;
    private MngSessions.Task.ManageSessions manageSessionsTaskLayer;
    private Dictionary<string, string> gitHubData = new Dictionary<string, string>()
                                                         { { "Username", string.Empty },
                                                           { "Email", string.Empty },
                                                           { "RepositoryRemote", string.Empty }
                                                         };

    private MngSessions.Presentation.Form_ManageSessions manageSessionsPresentationLayer;

    #endregion


    #region PROPERTIES

    public Control PluginControl { get { return (this); } }

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="Plugin_Sessions"/> class.
    ///
    /// </summary>
    public Plugin_Sessions(PluginProperties pluginProperties)
    {
      this.InitializeComponent();
      this.tv_Sessions.ExpandAll();

      this.dgv_Sessions.AutoGenerateColumns = false;

      DataGridViewTextBoxColumn columnSrcMac = new DataGridViewTextBoxColumn();
      columnSrcMac.DataPropertyName = "SrcMAC";
      columnSrcMac.Name = "SrcMAC";
      columnSrcMac.HeaderText = "Source MAC";
      columnSrcMac.Width = 180;
      this.dgv_Sessions.Columns.Add(columnSrcMac);

      DataGridViewTextBoxColumn columnSrcIp = new DataGridViewTextBoxColumn();
      columnSrcIp.DataPropertyName = "SrcIP";
      columnSrcIp.Name = "SrcIP";
      columnSrcIp.HeaderText = "Source IP";
      columnSrcIp.Width = 120;
      this.dgv_Sessions.Columns.Add(columnSrcIp);

      DataGridViewTextBoxColumn columnServiceUrl = new DataGridViewTextBoxColumn();
      columnServiceUrl.DataPropertyName = "URL";
      columnServiceUrl.Name = "URL";
      columnServiceUrl.HeaderText = "URL";
      columnServiceUrl.ReadOnly = true;
      columnServiceUrl.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      columnServiceUrl.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_Sessions.Columns.Add(columnServiceUrl);

      DataGridViewTextBoxColumn columnDestPort = new DataGridViewTextBoxColumn();
      columnDestPort.DataPropertyName = "DstPort";
      columnDestPort.Name = "DstPort";
      columnDestPort.HeaderText = "Service";
      columnDestPort.Visible = false;
      columnDestPort.ReadOnly = true;
      this.dgv_Sessions.Columns.Add(columnDestPort);

      DataGridViewTextBoxColumn columnCookies = new DataGridViewTextBoxColumn();
      columnCookies.DataPropertyName = "SessionCookies";
      columnCookies.Name = "SessionCookies";
      columnCookies.HeaderText = "Cookies";
      columnCookies.Visible = false;
      this.dgv_Sessions.Columns.Add(columnCookies);

      DataGridViewTextBoxColumn columnBrowser = new DataGridViewTextBoxColumn();
      columnBrowser.DataPropertyName = "Browser";
      columnBrowser.Name = "Browser";
      columnBrowser.HeaderText = "Browser";
      columnBrowser.Visible = false;
      columnBrowser.Width = 120;
      this.dgv_Sessions.Columns.Add(columnBrowser);

      DataGridViewTextBoxColumn columnGroup = new DataGridViewTextBoxColumn();
      columnGroup.DataPropertyName = "Group";
      columnGroup.Name = "Group";
      columnGroup.HeaderText = "Group";
      columnGroup.Visible = false;
      columnGroup.Width = 0;
      this.dgv_Sessions.Columns.Add(columnGroup);

      this.sessionRecords = new BindingList<Session.DataTypes.TheSessionRecord>();
      this.dgv_Sessions.DataSource = this.sessionRecords;

      // Verify passed parameter(s)
      if (pluginProperties == null)
      {
        throw new Exception("Parameter PluginParameters is null");
      }

      if (pluginProperties.HostApplication == null)
      {
        throw new Exception("Parameter HostApplication is null");
      }

      if (pluginProperties.ApplicationBaseDir == null)
      {
        throw new Exception("Parameter ApplicationBaseDir is null");
      }

      if (pluginProperties.PluginBaseDir == null)
      {
        throw new Exception("Parameter PluginBaseDir is null");
      }

      //// Plugin configuration
      this.t_GuiUpdate.Interval = 1000;
      this.pluginProperties = pluginProperties;
      this.pluginProperties.PluginName = "Sessions";
      this.pluginProperties.PluginType = "Passive";
      this.pluginProperties.PluginDescription = "Eavesdrop HTTP session information from HTTP data packets";
      this.pluginProperties.Ports = new Dictionary<int, IpProtocols>() { { 80, IpProtocols.Tcp }, { 443, IpProtocols.Tcp } };
      this.dataBatch = new List<string>();

      try
      {
        string configFileFullPath = Path.Combine(this.pluginProperties.ApplicationBaseDir, this.pluginProperties.PluginBaseDir, MngSessionsConfig.General.APP_CONFIG_FILE);
        Minary.PatternFileManager.GitHubPatternFileMgr.LoadParametersFromConfig(configFileFullPath, this.gitHubData);
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
      }

      // Instantiate infrastructure layer
      this.infrastructureLayer = new Session.Infrastructure.Session(this);

      // Initialize plugin environment
      this.infrastructureLayer.OnInit();

      // Load session pattern management GUI
      this.manageSessionsPresentationLayer = new MngSessions.Presentation.Form_ManageSessions(this.pluginProperties);
      this.manageSessionsTaskLayer = new MngSessions.Task.ManageSessions(this.pluginProperties);
    }


    /// <summary>
    ///
    /// </summary>
    public delegate void SyncPatternFileFromServerDelegate();
    public void SyncPatternFileFromServer()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new SyncPatternFileFromServerDelegate(this.SyncPatternFileFromServer), new object[] { });
        return;
      }

      if (string.IsNullOrEmpty(this.gitHubData["RepositoryRemote"]))
      {
        this.pluginProperties.HostApplication.LogMessage("Minary plugin Sessions: Can't sync attack pattern files because no remote repository is defined in the configuration file");
        return;
      }

      string repositoryLocalFullpath = Path.Combine(
                                                    this.pluginProperties.ApplicationBaseDir,
                                                    this.pluginProperties.PluginBaseDir,
                                                    this.pluginProperties.PatternSubDir,
                                                    Plugin.Main.Session.Config.General.PATTERN_DIR_REMOTE);

      try
      {
        Minary.PatternFileManager.GitHubPatternFileMgr.InitializeRepository(repositoryLocalFullpath, this.gitHubData["RepositoryRemote"]);
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage("Minary plugin Sessions: Initializing local attack pattern directory ({0}) failed: {1}", this.gitHubData["RepositoryRemote"], ex.Message);
      }

      try
      {
        Minary.PatternFileManager.GitHubPatternFileMgr.SyncRepository(repositoryLocalFullpath, this.gitHubData["Username"], this.gitHubData["Email"]);
        this.pluginProperties.HostApplication.LogMessage("Minary plugin Sessions: Attack pattern sync finished.");
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage("Minary plugin Sessions: Syncing attack pattern failed: {0}", ex.Message);
      }

      lock (this)
      {
        try
        {
          this.sessionPatterns = this.manageSessionsPresentationLayer.GetActiveSessionPatterns();
        }
        catch (Exception ex)
        {
          this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
        }
      }
    }


    /// <summary>
    ///
    /// </summary>
    public void ProcessEntries()
    {
      if (this.dataBatch != null && this.dataBatch.Count > 0)
      {
        List<string> newData;
        string[] splitter;
        string proto;
        string srcMacAddress;
        string srcIpAddress;
        string srcPort;
        string dstIpAddress;
        string dstPort;
        string data;

        lock (this)
        {
          newData = new List<string>(this.dataBatch);
          this.dataBatch.Clear();
        }

        foreach (string tmpDataRecord in newData)
        {
          try
          {
            if (string.IsNullOrEmpty(tmpDataRecord))
            {
              continue;
            }

            if ((splitter = Regex.Split(tmpDataRecord, @"\|\|")).Length != 7)
            {
              continue;
            }

            proto = splitter[0];
            srcMacAddress = splitter[1];
            srcIpAddress = splitter[2];
            srcPort = splitter[3];
            dstIpAddress = splitter[4];
            dstPort = splitter[5];
            data = splitter[6];

            foreach (MngSessionsConfig.SessionPattern tmpSessionPattern in this.sessionPatterns)
            {
              string host = string.Format(@"\.\.Host\s*:\s*{0}\.\.", tmpSessionPattern.HTTPHostRegex);
              if (Regex.Match(data, host, RegexOptions.IgnoreCase).Success &&
                  Regex.Match(data, tmpSessionPattern.SessionRegex, RegexOptions.IgnoreCase).Success)
              {
                this.EvaluateSession(data, srcMacAddress, srcIpAddress, tmpSessionPattern.CompanyWebpage, tmpSessionPattern.CompanyName.ToLower());
                break;
              }
            }
          }
          catch (Exception ex)
          {
            MessageBox.Show(string.Format("{0} : {1}", this.Config.PluginName, ex.Message));
          }
        }
      }
    }

    #endregion


    #region PRIVATE

    /// <summary>
    ///
    /// </summary>
    private void InitSessionPatterns()
    {
      // Clear and repopulate ImageList
      this.il_Sessions.Images.Clear();
      string imgDir = string.Format("{0}{1}", this.Config.PluginBaseDir, this.iconsDir);
      string[] fileEntries = Directory.GetFiles(imgDir);

      foreach (string tmpFileName in fileEntries)
      {
        Image icon = Image.FromFile(tmpFileName);
        FileInfo fileInfo = new FileInfo(tmpFileName);
        string iconKey = Path.GetFileNameWithoutExtension(fileInfo.Name).ToLower();

        this.il_Sessions.Images.Add(iconKey, icon);
      }

      // Clear and repopulate Treeview.
      try
      {
        if (this.tv_Sessions != null && this.tv_Sessions.Nodes.Count > 0)
        {
          foreach (TreeNode tmpNode in this.tv_Sessions.Nodes)
          {
            tmpNode.Nodes.Clear();
          }
        }
      }
      catch (Exception)
      {
      }

      this.filterNode = this.tv_Sessions.Nodes[0];
      foreach (MngSessionsConfig.SessionPattern tmpSessionPatterns in this.sessionPatterns)
      {
        TreeNode childNode = new TreeNode(tmpSessionPatterns.CompanyName);
        string sessionName = tmpSessionPatterns.CompanyName.ToLower();

        childNode.ImageIndex = this.il_Sessions.Images.IndexOfKey(sessionName);
        childNode.SelectedImageIndex = this.il_Sessions.Images.IndexOfKey(sessionName);
        this.filterNode.Nodes.Add(childNode);
      }

      // Set root node properties
      this.tv_Sessions.Nodes[0].ImageKey = "default";
      this.tv_Sessions.Nodes[0].SelectedImageKey = "default";
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="newData"></param>
    /// <param name="srcMacAddress"></param>
    /// <param name="srcIp"></param>
    /// <param name="url"></param>
    /// <param name="sessionName"></param>
    /// <returns></returns>
    private bool EvaluateSession(string data, string srcMacAddress, string srcIp, string url, string sessionName)
    {
      bool retVal = false;
      string cookies = string.Empty;
      string browser = string.Empty;
      string host = string.Empty;
      string uri = string.Empty;
      Match matchCookies;
      Match matchBrowser;
      Match matchUri;
      Match matchHost;
      Match matchReferer;

      matchBrowser = Regex.Match(data, @"\.\.User-Agent\s*:\s*(.*?)(\.\.|$)", RegexOptions.IgnoreCase);
      matchCookies = Regex.Match(data, @"\.\.Cookie\s*:\s*(.*?)(\.\.|$)", RegexOptions.IgnoreCase);
      matchHost = Regex.Match(data, @"\.\.Host\s*:\s*(.*?)(\.\.|$)", RegexOptions.IgnoreCase);
      matchReferer = Regex.Match(data, @"\.\.Referer\s*:\s*(.*?)(\.\.|$)", RegexOptions.IgnoreCase);
      matchUri = Regex.Match(data, @"GET|POST\s+([^\s]+)\s+", RegexOptions.IgnoreCase);

      if (!matchHost.Success && matchReferer.Success)
      {
        matchHost = matchReferer;
      }

      if (!matchCookies.Success || !matchUri.Success || !matchHost.Success)
      {
        return false;
      }

      // Define connection newData.
      cookies = matchCookies.Groups[1].Value.ToString();
      browser = matchBrowser.Success ? matchBrowser.Groups[1].Value.ToString() : string.Empty;

      if (url.Length > 0)
      {
        host = url;
      }
      else
      {
        uri = matchUri.Groups[1].Value.ToString();
        host = "http://" + matchHost.Groups[1].Value.ToString() + uri;
      }

      if (cookies.Length > 0 && browser.Length > 0 && host.Length > 0)
      {
        if (this.IsInDGV(srcIp, browser, cookies) == false)
        {
          try
          {
            this.AddRecord(srcMacAddress, srcIp, host, "80", cookies, browser, sessionName);
            this.AddNode(sessionName, srcIp, this.il_Sessions.Images.IndexOfKey(sessionName));
          }
          catch (Exception ex)
          {
            this.pluginProperties.HostApplication.LogMessage("{0} : {1}", this.Config.PluginName, ex.Message);
          }
        }
      }

      return retVal;
    }
    

    /// <summary>
    ///
    /// </summary>
    /// <param name="treeNode"></param>
    /// <param name="nodeName"></param>
    /// <returns></returns>
    private bool NodeExists(TreeNode treeNode, string nodeName)
    {
      bool retVal = false;
      foreach (TreeNode tmpNode in treeNode.Nodes)
      {
        if (tmpNode.Text == nodeName)
        {
          retVal = true;
          break;
        }
      }

      return retVal;
    }


    /// <summary>
    /// Add new victim node
    /// </summary>
    /// <param name="parentName"></param>
    /// <param name="srcIp"></param>
    /// <param name="imgIndex"></param>
    /// <returns></returns>
    private bool AddNode(string parentName, string srcIp, int imgIndex)
    {
      bool retVal = false;
      TreeNode parentNode;
      TreeNode tempNode;

      try
      {
        if (this.tv_Sessions.Nodes.Count > 0 && this.tv_Sessions.Nodes[0].Nodes.Count > 0 && (parentNode = this.GetNodeByName(parentName)) != null)
        {
          if (this.NodeExists(parentNode, srcIp) == false)
          {
            tempNode = parentNode.Nodes.Add(srcIp);
            tempNode.ImageIndex = imgIndex;
            tempNode.SelectedImageIndex = imgIndex;
            parentNode.ExpandAll();
            retVal = true;
          }
        }
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
      }

      return retVal;
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="nodeName"></param>
    /// <returns></returns>
    private TreeNode GetNodeByName(string nodeName)
    {
      TreeNode retVal = null;

      if (!string.IsNullOrEmpty(nodeName))
      {
        foreach (TreeNode tmpNode in this.tv_Sessions.Nodes[0].Nodes)
        {
          if (tmpNode.Text.ToLower().Contains(nodeName.ToLower()))
          {
            retVal = tmpNode;
            break;
          }
        }
      }

      return retVal;
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="ipAddress"></param>
    /// <param name="browser"></param>
    /// <param name="cookie"></param>
    /// <returns></returns>
    private bool IsInDGV(string ipAddress, string browser, string cookie)
    {
      bool retVal = false;
      Session.DataTypes.TheSessionRecord session;
      IEnumerator sessionEnum = this.sessionRecords.GetEnumerator();

      sessionEnum.Reset();
      while (sessionEnum.MoveNext())
      {
        if ((session = (Session.DataTypes.TheSessionRecord)sessionEnum.Current) != null)
        {
          if (session.SrcIP == ipAddress &&
////            session.Browser == browser &&
              session.SessionCookies == cookie)
          {
            retVal = true;
            break;
          }
        }
      }

      return retVal;
    }


    /// <summary>
    ///
    /// </summary>
    private void DgvFilter()
    {
      try
      {
        // Filter by IP
        if (Regex.Match(this.filterNode.Text, @"^\d+\.\d+\.\d+\.\d+$").Success)
        {
          this.FilterByIP(this.filterNode.Text);

        // Remove all filters -> "Sessions" was clicked
        }
        else if (Regex.Match(this.filterNode.Text, "sessions", RegexOptions.IgnoreCase).Success)
        {
          this.DisableFilter();

        // Filter by group
        }
        else if (this.filterNode.Text.Length > 0)
        {
          this.FilterByGroup(this.filterNode.Text);
        }
        else
        {
          this.DisableFilter();
        }
      }
      catch (Exception)
      {
      }

      this.dgv_Sessions.Refresh();
    }


    /// <summary>
    /// Set filter by IP
    /// </summary>
    /// <param name="ipAddress"></param>
    private void FilterByIP(string ipAddress)
    {
      CurrencyManager cm = (CurrencyManager)BindingContext[this.dgv_Sessions.DataSource];
      cm.SuspendBinding();

      foreach (DataGridViewRow tmpRow in this.dgv_Sessions.Rows)
      {
        if (tmpRow.Cells[1].Value.ToString() == ipAddress)
        {
          tmpRow.Visible = true;
        }
        else
        {
          tmpRow.Visible = false;
        }
      }

      cm.ResumeBinding();
    }


    /// <summary>
    /// Set filter by Group
    /// </summary>
    /// <param name="group"></param>
    private void FilterByGroup(string group)
    {
      CurrencyManager cm = (CurrencyManager)this.BindingContext[this.dgv_Sessions.DataSource];
      cm.SuspendBinding();

      foreach (DataGridViewRow tmpRow in this.dgv_Sessions.Rows)
      {
        if (Regex.Match(tmpRow.Cells[6].Value.ToString().ToLower(), group, RegexOptions.IgnoreCase).Success)
        {
          tmpRow.Visible = true;
        }
        else
        {
          tmpRow.Visible = false;
        }
      }

      cm.ResumeBinding();
    }


    /// <summary>
    /// Disable all filters
    /// </summary>
    private void DisableFilter()
    {
      CurrencyManager cm = (CurrencyManager)this.BindingContext[this.dgv_Sessions.DataSource];
      cm.SuspendBinding();

      foreach (DataGridViewRow tmpRow in this.dgv_Sessions.Rows)
      {
        tmpRow.Visible = true;
      }

      cm.ResumeBinding();
    }

    #endregion
    
  }
}