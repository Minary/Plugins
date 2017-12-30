namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.HttpAccounts.DataTypes;
  using Minary.Plugin.Main.HttpAccounts.ManageAuthentications.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Configuration;
  using System.IO;
  using System.Text.RegularExpressions;
  using System.Windows.Forms;
  using MngAuthentications = Minary.Plugin.Main.HttpAccounts.ManageAuthentications;


  public partial class Plugin_HttpAccounts : UserControl, IPlugin
  {

    #region MEMBERS

    private const int MaxTableRows = 128;
    private List<HttpAccountPattern> accountPatterns;
    private List<Tuple<string, string, string>> targetList;
    private List<PeerSystems> peersDataSource;
    private BindingList<AccountRecord> accountRecords;
    private List<string> dataBatch = new List<string>();
    private HttpAccounts.Infrastructure.HttpAccounts infrastructureLayer;
    private PluginProperties pluginProperties;
    private MngAuthentications.Presentation.Form_ManageAuthentications manageHttpAccountsPresentationLayer;
    private MngAuthentications.Task.ManageAuthentications manageHttpAccountsTaskLayer;
    private Dictionary<string, string> gitHubData = new Dictionary<string, string>()
                                                         { { "Username", string.Empty },
                                                           { "Email", string.Empty },
                                                           { "RepositoryRemote", string.Empty }
                                                         };
//private ManageAuthentications.Presentation.Form_ManageAuthentications manageHttpAccountsPresentationLayer;

    #endregion


    #region PROPERTIES

    public Control PluginControl { get { return this; } }

    public List<HttpAccountPattern> AccountPatterns { get { return this.accountPatterns; } set { this.accountPatterns = value; } }

    #endregion


    #region PUBLIC METHODS

    /// <summary>
    /// Initializes a new instance of the <see cref="Plugin_HttpAccounts"/> class.
    ///
    /// </summary>
    /// <param name="pluginProperties"></param>
    public Plugin_HttpAccounts(PluginProperties pluginProperties)
    {
      this.InitializeComponent();

      this.dgv_Accounts.AutoGenerateColumns = false;

      DataGridViewTextBoxColumn columnMac = new DataGridViewTextBoxColumn();
      columnMac.DataPropertyName = "SrcMAC";
      columnMac.Name = "SrcMAC";
      columnMac.HeaderText = "MAC address";
      columnMac.ReadOnly = true;
      columnMac.Width = 180;
      this.dgv_Accounts.Columns.Add(columnMac);

      DataGridViewTextBoxColumn columnSrcIp = new DataGridViewTextBoxColumn();
      columnSrcIp.DataPropertyName = "SrcIP";
      columnSrcIp.Name = "SrcIP";
      columnSrcIp.HeaderText = "Source IP";
      columnSrcIp.Visible = false;
      columnSrcIp.ReadOnly = true;
      columnSrcIp.Width = 120;
      this.dgv_Accounts.Columns.Add(columnSrcIp);

      DataGridViewTextBoxColumn columnDstIp = new DataGridViewTextBoxColumn();
      columnDstIp.DataPropertyName = "DstIP";
      columnDstIp.Name = "DstIP";
      columnDstIp.HeaderText = "Destination";
      columnDstIp.ReadOnly = true;
      columnDstIp.Width = 200;
      this.dgv_Accounts.Columns.Add(columnDstIp);

      DataGridViewTextBoxColumn columnDestPort = new DataGridViewTextBoxColumn();
      columnDestPort.DataPropertyName = "DstPort";
      columnDestPort.Name = "DstPort";
      columnDestPort.HeaderText = "Service";
      columnDestPort.ReadOnly = true;
      columnDestPort.Width = 60;
      this.dgv_Accounts.Columns.Add(columnDestPort);

      DataGridViewTextBoxColumn columnUser = new DataGridViewTextBoxColumn();
      columnUser.DataPropertyName = "Username";
      columnUser.Name = "Username";
      columnUser.HeaderText = "Username";
      columnUser.ReadOnly = true;
      columnUser.Width = 150;
      this.dgv_Accounts.Columns.Add(columnUser);

      DataGridViewTextBoxColumn columnPass = new DataGridViewTextBoxColumn();
      columnPass.DataPropertyName = "Password";
      columnPass.Name = "Password";
      columnPass.HeaderText = "Password";
      columnPass.ReadOnly = true;
      columnPass.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_Accounts.Columns.Add(columnPass);

      this.accountRecords = new BindingList<AccountRecord>();
      this.dgv_Accounts.DataSource = this.accountRecords;

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

      // Configure plugin
      this.pluginProperties = pluginProperties;

      this.peersDataSource = new List<PeerSystems>();
      this.accountPatterns = new List<HttpAccountPattern>();

      this.pluginProperties.PluginName = "HTTP accounts";
      this.pluginProperties.PluginType = "Passive";
      this.pluginProperties.PluginDescription = "Eavesdrop account information from HTTP data packets.";
      this.pluginProperties.Ports = new Dictionary<int, IpProtocols>() { { 80, IpProtocols.Tcp }, { 443, IpProtocols.Tcp } };

      // Instantiate and initialize infrastructure layer
      this.infrastructureLayer = new HttpAccounts.Infrastructure.HttpAccounts(this);
      this.infrastructureLayer.OnInit();

      // Load authentication pattern management GUI
      this.manageHttpAccountsPresentationLayer = new MngAuthentications.Presentation.Form_ManageAuthentications(this.pluginProperties);
      this.manageHttpAccountsTaskLayer = new MngAuthentications.Task.ManageAuthentications(this.pluginProperties);
      this.accountPatterns = this.manageHttpAccountsPresentationLayer.GetActiveAuthenticationPatterns();

      // Start DataGridView Update thredat_GuiUpdate
      this.t_GuiUpdate.Interval = 1000;
      this.t_GuiUpdate.Start();
    }

    #endregion


    #region PRIVATE METHODS

    /// <summary>
    ///
    /// </summary>
    private void ProcessEntries()
    {
      if (this.dataBatch == null || this.dataBatch.Count <= 0)
      {
        return;
      }

      List<AccountRecord> newRecords = new List<AccountRecord>();
      List<string> newData;
      int dstPortInt;
      string[] splitter;
      string protocol;
      string srcMac;
      string srcIp;
      string srcPort;
      string dstIp;
      string dstPort;
      string data;

      lock (this)
      {
        newData = new List<string>(this.dataBatch);
        this.dataBatch.Clear();
      }

      foreach (string tmpRecord in newData)
      {
        if (string.IsNullOrEmpty(tmpRecord))
        {
          continue;
        }

        splitter = Regex.Split(tmpRecord, @"\|\|");
        if (splitter.Length != 7)
        {
          continue;
        }

        protocol = splitter[0];
        srcMac = splitter[1];
        srcIp = splitter[2];
        srcPort = splitter[3];
        dstIp = splitter[4];
        dstPort = splitter[5];
        data = splitter[6];

        // HTML GET authentication strings
        HttpAccountStruct authData = new HttpAccountStruct();

        try
        {
          authData = this.FindAuthString(data);
        }
        catch (Exception ex)
        {
          this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
          return;
        }
        
        if (authData.CompanyURL.Length > 0 && authData.Username.Length > 0 && authData.Password.Length > 0)
        {
          if (!int.TryParse(dstPort, out dstPortInt))
          {
            this.pluginProperties.HostApplication.LogMessage("{0}: Something is wrong with the remote port \"{1}\"", this.Config.PluginName, dstPort);
          }
          else if (!Regex.Match(dstIp, @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$").Success && !Regex.Match(dstIp, @"\.[\d\w]+").Success)
          {
            this.pluginProperties.HostApplication.LogMessage("{0}: Something is wrong with the remote system \"{1}\"", this.Config.PluginName, dstIp);
          }

          try
          {
            newRecords.Add(new AccountRecord(srcMac, srcIp, authData.CompanyURL, dstPort, authData.Username, authData.Password));
          }
          catch (Exception ex)
          {
            this.pluginProperties.HostApplication.LogMessage("{0}: The following error occurred while adding account credentials : {1}", this.Config.PluginName, ex.Message);
          }
        }
      }

      try
      {
        if (newRecords.Count > 0)
        {
          this.AddRecords(newRecords);
        }
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage("{0}: The following error occurred while adding account credentials : {1}", this.Config.PluginName, ex.Message);
      }
    }

    /// <summary>
    ///
    /// </summary>
    private delegate void SetGUIInactiveDelegate();
    private void SetGuiInactive()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new SetGUIInactiveDelegate(this.SetGuiInactive), new object[] { });
        return;
      }

      this.tsmi_DeleteEntry.Enabled = false;
      this.tsmi_Clear.Enabled = false;

      this.Refresh();
    }


    /// <summary>
    ///
    /// </summary>
    private delegate void SetGuiActiveDelegate();
    private void SetGuiActive()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new SetGuiActiveDelegate(this.SetGuiActive), new object[] { });
        return;
      }

      this.tsmi_DeleteEntry.Enabled = true;
      this.tsmi_Clear.Enabled = true;

      this.Refresh();
    }


    /// <summary>
    /// Find authentication pattern
    /// </summary>
    /// <param name="inputHttpData"></param>
    /// <returns></returns>
    private HttpAccountStruct FindAuthString(string inputHttpData)
    {
      HttpAccountStruct retVal = new HttpAccountStruct();

      retVal.Username = string.Empty;
      retVal.Password = string.Empty;
      retVal.Company = string.Empty;
      retVal.CompanyURL = string.Empty;

      if (this.accountPatterns == null || this.accountPatterns.Count <= 0)
      {
        return retVal;
      }

      Match matchHost;
      Match matchMethod;
      Match matchURI;
      Match matchCreds;
      string reqHost = string.Empty;
      string reqMethod = string.Empty;
      string reqUri = string.Empty;
      string username = string.Empty;
      string password = string.Empty;

      foreach (HttpAccountPattern tmpAccount in this.accountPatterns)
      {
        if ((matchMethod = Regex.Match(inputHttpData, @"\s*(GET|POST)\s+")).Success == false)
        {
          continue;
        }
        
        if ((matchURI = Regex.Match(inputHttpData, @"\s*(GET|POST)\s+([^\s]+)\s+", RegexOptions.IgnoreCase)).Success == false)
        {
          continue;
        }
        
        if ((matchHost = Regex.Match(inputHttpData, @"\.\.Host\s*:\s*([\w\-_\d\.]+?)\.\.", RegexOptions.IgnoreCase)).Success == false)
        {
          continue;
        }

        if ((matchCreds = Regex.Match(inputHttpData, tmpAccount.DataPattern)).Success == false)
        {
          continue;
        }

        reqMethod = matchMethod.Groups[1].Value.ToString();
        reqUri = matchURI.Groups[2].Value.ToString();
        reqHost = matchHost.Groups[1].Value.ToString();
        username = matchCreds.Groups[1].Value.ToString();
        password = matchCreds.Groups[2].Value.ToString();

        if (tmpAccount.Method.Trim().ToLower() == reqMethod.Trim().ToLower() &&
            Regex.Match(reqHost, tmpAccount.HostPattern).Success &&
            Regex.Match(reqUri, tmpAccount.PathPattern).Success &&
            Regex.Match(inputHttpData, tmpAccount.DataPattern).Success)
        {
          retVal.Company = tmpAccount.Company;
          retVal.CompanyURL = tmpAccount.WebPage + "   (http://" + reqHost + reqUri + ")";
          retVal.Username = username;
          retVal.Password = password;

          break;
        }
      }

      return retVal;
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
        this.pluginProperties.HostApplication.LogMessage("Minary plugin HTTP accounts: Can't sync attack pattern files because no remote repository is defined in the configuration file");
        return;
      }

      //
      string repositoryLocalFullpath = Path.Combine(
                                                    this.pluginProperties.ApplicationBaseDir,
                                                    this.pluginProperties.PluginBaseDir,
                                                    this.pluginProperties.PatternSubDir,
                                                    Plugin.Main.HttpAccounts.DataTypes.General.PATTERN_DIR_REMOTE);
    }

    #endregion

  }
}
