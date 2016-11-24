namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.Systems.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.IO;
  using System.Text.RegularExpressions;
  using System.Windows.Forms;
  using ManageSystems = Minary.Plugin.Main.Systems.ManageSystems;


  public partial class Plugin_Systems : UserControl, IPlugin
  {

    #region DATATYPES

    private enum EntryType
    {
      Empty,
      Half,
      Full
    }

    #endregion


    #region MEMBERS

    private const int MAX_TABLE_ROWS = 128;
    private List<Tuple<string, string, string>> targetList;
    private BindingList<SystemRecord> systemRecords;
    private List<SystemPattern> systemPatterns = new List<SystemPattern>();
    private List<string> dataBatch = new List<string>();
    private Systems.Infrastructure.Systems infrastructureLayer;
    private PluginProperties pluginProperties;
    private ManageSystems.Presentation.Form_ManageSystems manageSystemsPresentationLayer;
    private ManageSystems.Task.ManageSystems manageSystemsTaskLayer;
    private Dictionary<string, string> gitHubData = new Dictionary<string, string>()
                                                         { { "Username", string.Empty },
                                                           { "Email", string.Empty },
                                                           { "RepositoryRemote", string.Empty }
                                                         };

    #endregion


    #region PROPERTIES

    public Control PluginControl { get { return (this); } }

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="Plugin_Systems"/> class.
    /// Constructor.
    /// Instantiate the UserControl.
    /// </summary>
    public Plugin_Systems(PluginProperties pluginProperties)
    {
      this.InitializeComponent();

      this.dgv_Systems.AutoGenerateColumns = false;

      DataGridViewTextBoxColumn columnMac = new DataGridViewTextBoxColumn();
      columnMac.DataPropertyName = "SrcMac";
      columnMac.Name = "SrcMac";
      columnMac.HeaderText = "MAC address";
      columnMac.ReadOnly = true;
      columnMac.Width = 120;
      columnMac.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.dgv_Systems.Columns.Add(columnMac);

      DataGridViewTextBoxColumn columnSrcIp = new DataGridViewTextBoxColumn();
      columnSrcIp.DataPropertyName = "SrcIp";
      columnSrcIp.Name = "SrcIp";
      columnSrcIp.HeaderText = "Source IP";
      columnSrcIp.Width = 140;
      columnSrcIp.ReadOnly = true;
      columnSrcIp.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.dgv_Systems.Columns.Add(columnSrcIp);

      DataGridViewTextBoxColumn columnAppUrl = new DataGridViewTextBoxColumn();
      columnAppUrl.DataPropertyName = "OperatingSystem";
      columnAppUrl.Name = "OperatingSystem";
      columnAppUrl.HeaderText = "Operating System";
      columnAppUrl.ReadOnly = true;
      columnAppUrl.Width = 200; // 373;
      columnAppUrl.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.dgv_Systems.Columns.Add(columnAppUrl);

      DataGridViewTextBoxColumn columnHardwareVendor = new DataGridViewTextBoxColumn();
      columnHardwareVendor.DataPropertyName = "HWVendor";
      columnHardwareVendor.Name = "HWVendor";
      columnHardwareVendor.HeaderText = "Hardware vendor";
      columnHardwareVendor.ReadOnly = true;
      columnHardwareVendor.Width = 200; // 373;
      columnHardwareVendor.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.dgv_Systems.Columns.Add(columnHardwareVendor);

      DataGridViewTextBoxColumn columnLastSeen = new DataGridViewTextBoxColumn();
      columnLastSeen.DataPropertyName = "LastSeen";
      columnLastSeen.Name = "LastSeen";
      columnLastSeen.HeaderText = "Last seen";
      columnLastSeen.ReadOnly = true;
      //////columnLastSeen.Width = 120;
      columnLastSeen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      columnLastSeen.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.dgv_Systems.Columns.Add(columnLastSeen);

      this.systemRecords = new BindingList<SystemRecord>();
      this.dgv_Systems.DataSource = this.systemRecords;

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

      // Plugin configuration
      this.t_GUIUpdate.Interval = 1000;
      this.pluginProperties = pluginProperties;
      this.pluginProperties.PluginName = "Systems";
      this.pluginProperties.PluginType = "Passive";
      this.pluginProperties.PluginDescription = "Determine operating system of detected target systems";
      this.pluginProperties.Ports = new Dictionary<int, IpProtocols>() { { 80, IpProtocols.Tcp }, { 443, IpProtocols.Tcp } };

      try
      {
        string configFileFullPath = Path.Combine(this.pluginProperties.ApplicationBaseDir, this.pluginProperties.PluginBaseDir, Systems.ManageSystems.DataTypes.General.APP_CONFIG_FILE);
        Minary.PatternFileManager.GitHubPatternFileMgr.LoadParametersFromConfig(configFileFullPath, this.gitHubData);
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
      }

      this.t_GUIUpdate.Start();

      // Instantiate infrastructure layer
      this.infrastructureLayer = Systems.Infrastructure.Systems.GetInstance(this);

      // Initialize plugin environment
      this.infrastructureLayer.OnInit();

      // Load authentication pattern management GUI
      this.manageSystemsPresentationLayer = ManageSystems.Presentation.Form_ManageSystems.GetInstance(this.pluginProperties);
      this.manageSystemsTaskLayer = ManageSystems.Task.ManageSystems.GetInstance(this.pluginProperties);
      this.systemPatterns = this.manageSystemsPresentationLayer.GetActiveSystemPatterns();
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
        this.pluginProperties.HostApplication.LogMessage("Minary plugin Systems: Can't sync attack pattern files because no remote repository is defined in the configuration file");
        return;
      }

      //
      string repositoryLocalFullpath = Path.Combine(
                                                    this.pluginProperties.ApplicationBaseDir,
                                                    this.pluginProperties.PluginBaseDir,
                                                    this.pluginProperties.PatternSubDir,
                                                    Plugin.Main.Systems.DataTypes.General.PATTERN_DIR_REMOTE);

      try
      {
        Minary.PatternFileManager.GitHubPatternFileMgr.InitializeRepository(repositoryLocalFullpath, this.gitHubData["RepositoryRemote"]);
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage("Minary plugin Systems: Initializing local attack pattern directory ({0}) failed: {1}", this.gitHubData["RepositoryRemote"], ex.Message);
      }

      try
      {
        Minary.PatternFileManager.GitHubPatternFileMgr.SyncRepository(repositoryLocalFullpath, this.gitHubData["Username"], this.gitHubData["Email"]);
        this.pluginProperties.HostApplication.LogMessage("Minary plugin Systems: Attack pattern sync finished.");
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage("Minary plugin Systems: Syncing attack pattern failed: {0}", ex.Message);
      }

      lock (this)
      {
        try
        {
          this.systemPatterns = this.manageSystemsPresentationLayer.GetActiveSystemPatterns();
        }
        catch (Exception ex)
        {
          this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
        }
      }
    }

    #endregion


    #region PRIVATE

    /// <summary>
    ///
    /// </summary>
    public void ProcessEntries()
    {
      if (this.dataBatch != null && this.dataBatch.Count > 0)
      {
        List<SystemRecord> newRecords = new List<SystemRecord>();
        List<string> newData;
        Match matchUserAgent;
        EntryType entryType;
        DataGridViewRow tabelRow;
        string[] splitter;
        string proto;
        string srcMacAddress;
        string srcIpAddress;
        string srcPort;
        string dstIpAddress;
        string dstPort;
        string data;
        string operatingSystem = string.Empty;
        string userAgent = string.Empty;

        lock (this)
        {
          newData = new List<string>(this.dataBatch);
          this.dataBatch.Clear();
        }

        foreach (string tmpNewData in newData)
        {
          try
          {
            if (!string.IsNullOrEmpty(tmpNewData))
            {
              if ((splitter = Regex.Split(tmpNewData, @"\|\|")).Length == 7)
              {
                proto = splitter[0];
                srcMacAddress = splitter[1];
                srcIpAddress = splitter[2];
                srcPort = splitter[3];
                dstIpAddress = splitter[4];
                dstPort = splitter[5];
                data = splitter[6];

                srcMacAddress = Regex.Replace(srcMacAddress, @"-", ":");
                entryType = this.FullEntryExists(srcMacAddress, srcIpAddress);

                // Determine the operating system due to the HTTP User-Agent string.
                if (((matchUserAgent = Regex.Match(data, @"\.\.User-Agent\s*:\s*(.+?)\.\.", RegexOptions.IgnoreCase))).Success)
                {
                  try
                  {
                    userAgent = matchUserAgent.Groups[1].Value.ToString();
                    operatingSystem = this.GetOperatingSystem(userAgent);
                  }
                  catch (Exception ex)
                  {
                    this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
                  }

                  //
                  try
                  {
                    if (entryType != EntryType.Full && operatingSystem.Length > 0)
                    {
                      if (entryType == EntryType.Empty)
                      {
                        this.AddRecord(new SystemRecord(srcMacAddress, srcIpAddress, userAgent, string.Empty, operatingSystem, string.Empty));
                      }
                      else if (entryType == EntryType.Half)
                      {
                        this.SetOS(srcMacAddress, srcIpAddress, operatingSystem);
                      }

                      if ((tabelRow = this.GetRowByMac(srcMacAddress)) != null)
                      {
                        tabelRow.Cells["OperatingSystem"].ToolTipText = userAgent;
                      }
                    }
                    else if (srcIpAddress.Length > 0 && srcMacAddress.Length > 0)
                      this.AddRecord(new SystemRecord(srcMacAddress, srcIpAddress, userAgent, string.Empty, string.Empty, string.Empty));
                  }
                  catch (RecordException rex)
                  {
                    this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, rex.Message);
                  }
                  catch (RecordExistsException)
                  {
                  }
                  catch (Exception ex)
                  {
                    this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
                  }

                  // The operating system cant be determined.
                }
                else if (entryType == EntryType.Empty && srcIpAddress.Length > 0 && srcMacAddress.Length > 0)
                {
                  try
                  {
                    this.AddRecord(new SystemRecord(srcMacAddress, srcIpAddress, string.Empty, userAgent, string.Empty, string.Empty));
                  }
                  catch (RecordException rex)
                  {
                    this.pluginProperties.HostApplication.LogMessage("{0} 0: {1}", this.Config.PluginName, rex.Message);
                  }
                  catch (RecordExistsException)
                  {
                  }
                  catch (Exception ex)
                  {
                    this.pluginProperties.HostApplication.LogMessage("{0} 2: {1}", this.Config.PluginName, ex.Message);
                  }
                }

                // Updating LastSeen column.
                using (DataGridViewRow tmpRow = this.ListEntryExists(srcMacAddress))
                {
                  if (tmpRow != null && tmpRow.Cells["LastSeen"] != null)
                  {
                    tmpRow.Cells["LastSeen"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                  }
                }

              }
            }
          }
          catch (Exception ex)
          {
            MessageBox.Show(string.Format("{0} : {1}", this.Config.PluginName, ex.ToString()));
          }
        }
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="macAddress"></param>
    /// <returns></returns>
    private DataGridViewRow GetRowByMac(string macAddress)
    {
      DataGridViewRow retVal = null;

      if (this.dgv_Systems.RowCount <= 0)
      {
        return retVal;
      }
      
      foreach (DataGridViewRow tmpRow in this.dgv_Systems.Rows)
      {
        if (tmpRow.Cells["SrcMac"].Value.ToString() == macAddress)
        {
          retVal = tmpRow;
          break;
        }
      }

      return retVal;
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="srcMac"></param>
    /// <param name="srcIp"></param>
    /// <returns></returns>
    private EntryType FullEntryExists(string srcMac, string srcIp)
    {
      EntryType retVal = EntryType.Empty;

      if (this.systemRecords == null || this.systemRecords.Count <= 0)
      {
        return retVal;
      }

      foreach (SystemRecord tmpSystem in this.systemRecords)
      {
        string srcMacReal = srcMac.Replace('-', ':');
        string systemMacReal = tmpSystem.SrcMac.Replace('-', ':');

        if (systemMacReal == srcMacReal && tmpSystem.SrcIp == srcIp && tmpSystem.OperatingSystem.Length > 0)
        {
          retVal = EntryType.Full;
          break;
        }
        else if (systemMacReal == srcMacReal && tmpSystem.SrcIp == srcIp)
        {
          retVal = EntryType.Half;
          break;
        }
      }

      return retVal;
    }
    

    /// <summary>
    ///
    /// </summary>
    /// <param name="srcMac"></param>
    /// <param name="srcIp"></param>
    /// <param name="operatingSystem"></param>
    /// <returns></returns>
    private bool SetOS(string srcMac, string srcIp, string operatingSystem)
    {
      bool retVal = false;

      if (this.systemRecords == null || this.systemRecords.Count <= 0)
      {
        return retVal;
      }

      foreach (SystemRecord tmpSystem in this.systemRecords)
      {
        string srcMacReal = srcMac.Replace('-', ':');
        string systemMacReal = tmpSystem.SrcMac.Replace('-', ':');

        if (srcMacReal == systemMacReal && tmpSystem.SrcIp == srcIp)
        {
          tmpSystem.OperatingSystem = operatingSystem;
          retVal = true;
          break;
        }
      }

      return retVal;
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="userAgent"></param>
    /// <returns></returns>
    private string GetOperatingSystem(string userAgent)
    {
      string retVal = string.Empty;

      foreach (SystemPattern tmpSystemPattern in this.systemPatterns)
      {
        if (userAgent != null && Regex.Match(userAgent, tmpSystemPattern.SystemPatternstring, RegexOptions.IgnoreCase).Success)
        {
          retVal = tmpSystemPattern.SystemName;
          break;
        }
      }

      return retVal;
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="macAddress"></param>
    /// <returns></returns>
    private DataGridViewRow ListEntryExists(string macAddress)
    {
      DataGridViewRow retVal = null;

      foreach (DataGridViewRow tmpRow in this.dgv_Systems.Rows)
      {
        if (tmpRow.Cells["SrcMac"].Value.ToString() == macAddress)
        {
          retVal = tmpRow;
          break;
        }
      }

      return retVal;
    }

    #endregion

  }
}
