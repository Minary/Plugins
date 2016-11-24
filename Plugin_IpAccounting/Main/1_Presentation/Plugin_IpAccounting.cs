namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.IpAccounting.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.IO;
  using System.Windows.Forms;

  public partial class Plugin_IpAccounting : UserControl, IPlugin, IObserver
  {

    #region MEMBERS

    private List<Tuple<string, string, string>> targetList;
    private BindingList<AccountingItem> accountingRecords;
    private IpAccounting.Domain.IpAccounting domainLayer;
    private string accountingOutputType = "-p";
    private bool isUpToDate = false;
    private PluginProperties pluginProperties;
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
    /// Initializes a new instance of the <see cref="Plugin_IpAccounting"/> class.
    ///
    /// </summary>
    public Plugin_IpAccounting(PluginProperties pluginProperties)
    {
      this.InitializeComponent();

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
      this.pluginProperties = pluginProperties;
      this.pluginProperties.PluginName = "IP accounting";
      this.pluginProperties.PluginType = "Passive";
      this.pluginProperties.PluginDescription = "Create data traffic statistics";
      this.pluginProperties.Ports = new Dictionary<int, IpProtocols>();

      this.accountingRecords = new BindingList<AccountingItem>();
      this.dgv_TrafficData.DataSource = this.accountingRecords;

      IpAccountingConfig config = new IpAccountingConfig()
                                       {
                                         BasisDirectory = this.Config.PluginBaseDir,
                                         IsDebuggingOn = false, //// cPluginParams.HostApplication.IsDebuggingOn(),
                                         Interface = null, //// cPluginParams.HostApplication.GetInterface(),
                                         OnUpdateList = this.Update,
                                         OnIpAccountingExit = null
                                       };

      // Parse app.config file
      try
      {
        string configFileFullPath = Path.Combine(
                                                 this.pluginProperties.ApplicationBaseDir,
                                                 this.pluginProperties.PluginBaseDir,
                                                 Plugin.Main.IpAccounting.DataTypes.General.APP_CONFIG_FILE);
        Minary.PatternFileManager.GitHubPatternFileMgr.LoadParametersFromConfig(configFileFullPath, this.gitHubData);
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
      }

      // Instantiate infrastructure layer
      this.domainLayer = IpAccounting.Domain.IpAccounting.GetInstance(config, this);
      this.domainLayer.AddObserver(this);

      // Initialize plugin environment
      this.domainLayer.OnInit();
    }

    #endregion


    #region PRIVATE

    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private delegate void OnIpAccountingExitedDelegate();
    private void OnIpAccountingExited()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnIpAccountingExitedDelegate(this.OnIpAccountingExited), new object[] { });
        return;
      }

      this.SetGuiActive();
      this.domainLayer.OnStopAttack();
      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.Error);
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

      this.rb_Service.Enabled = true;
      this.rb_RemoteIP.Enabled = true;
      this.rb_LocalIP.Enabled = true;
    }


    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    private delegate void SetGuiInactiveDelegate();
    private void SetGuiInactive()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new SetGuiInactiveDelegate(this.SetGuiInactive), new object[] { });
        return;
      }

      this.rb_Service.Enabled = false;
      this.rb_RemoteIP.Enabled = false;
      this.rb_LocalIP.Enabled = false;
    }
    
    #endregion

  }
}
