namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.SslStrip.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.IO;
  using System.Windows.Forms;


  public partial class Plugin_SslStrip : UserControl, IPlugin
  {

    #region MEMBERS

    private List<Tuple<string, string, string>> targetList;
    private List<string> dataBatch;
    private BindingList<SslStripRecord> sslStripRecords;
    private SslStrip.Infrastructure.SslStrip infrastructureLayer;
    private SslStripConfig sslStripConfig;
    private BindingList<ComboboxItem> contentTypes;
    private bool isUpToDate = false;
    private StateMachine dataCombobox;
    private PluginProperties pluginProperties;
    private string sslStripConfigFilePath;

    #endregion


    #region PROPERTIES

    public Control PluginControl { get { return (this); } }

    #endregion


    #region PUBLIC

    public Plugin_SslStrip(PluginProperties pluginProperties)
    {
      this.InitializeComponent();

      DataGridViewTextBoxColumn columnHostName = new DataGridViewTextBoxColumn();
      columnHostName.DataPropertyName = "HostName";
      columnHostName.Name = "HostName";
      columnHostName.HeaderText = "Host name";
      columnHostName.ReadOnly = true;
      columnHostName.Width = 296;
      this.dgv_SslStrippingTargets.Columns.Add(columnHostName);

      DataGridViewTextBoxColumn columnContentType = new DataGridViewTextBoxColumn();
      columnContentType.DataPropertyName = "ContentType";
      columnContentType.Name = "ContentType";
      columnContentType.HeaderText = "Content type";
      columnContentType.ReadOnly = true;
      columnContentType.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_SslStrippingTargets.Columns.Add(columnContentType);

      this.sslStripRecords = new BindingList<SslStripRecord>();
      this.dgv_SslStrippingTargets.DataSource = this.sslStripRecords;

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

      if (pluginProperties.HostApplication == null ||
          pluginProperties.HostApplication.AttackServiceList == null ||
          !pluginProperties.HostApplication.AttackServiceList.ContainsKey("HttpReverseProxyServer") ||
          pluginProperties.HostApplication.AttackServiceList["HttpReverseProxyServer"].SubModules == null ||
          !pluginProperties.HostApplication.AttackServiceList["HttpReverseProxyServer"].SubModules.ContainsKey("HttpReverseProxyServer.SslStrip") ||
          string.IsNullOrEmpty(pluginProperties.HostApplication.AttackServiceList["HttpReverseProxyServer"].SubModules["HttpReverseProxyServer.SslStrip"].WorkingDirectory) ||
          string.IsNullOrEmpty(pluginProperties.HostApplication.AttackServiceList["HttpReverseProxyServer"].SubModules["HttpReverseProxyServer.SslStrip"].ConfigFilePath))
      {
        throw new Exception("Attack services parameters are invalid");
      }

      // Plugin configuration
      this.pluginProperties = pluginProperties;

      this.pluginProperties.PluginName = "SslStrip";
      this.pluginProperties.PluginType = "Intrusive";
      this.pluginProperties.PluginDescription = "SSL strip tags from HTTP server responses";
      this.pluginProperties.Ports = new Dictionary<int, IpProtocols>();
      this.dataBatch = new List<string>();
 
      // Set SslStrip config file path
      this.sslStripConfigFilePath = Path.Combine(
                                                 pluginProperties.HostApplication.AttackServiceList["HttpReverseProxyServer"].SubModules["HttpReverseProxyServer.SslStrip"].WorkingDirectory,
                                                 pluginProperties.HostApplication.AttackServiceList["HttpReverseProxyServer"].SubModules["HttpReverseProxyServer.SslStrip"].ConfigFilePath);

      this.sslStripConfig = new SslStripConfig()
      {
        BasisDirectory = this.Config.PluginBaseDir,
        IsDebuggingOn = this.pluginProperties.HostApplication.IsDebuggingOn,
        OnSslStripExit = this.OnSslStripExited,
        SslStripConfigFilePath = this.sslStripConfigFilePath
      };

      // Instantiate infrastructure layer
      this.infrastructureLayer = SslStrip.Infrastructure.SslStrip.GetInstance(this, this.sslStripConfig);

      // Initialize plugin environment
      this.infrastructureLayer.OnInit();

      // Initialize ComboBoxes ContentType and Tags/Content
      this.contentTypes = new BindingList<ComboboxItem>();
      this.dataCombobox = new StateMachine(this.contentTypes);
      this.cb_ContentType.DataSource = this.contentTypes;
    }

    #endregion


    #region PRIVATE

    /// <summary>
    ///
    /// </summary>
    private void SetGuiActive()
    {
      this.tb_HostName.Enabled = true;
      this.cb_ContentType.Enabled = true;
      this.bt_Add.Enabled = true;
    }

    /// <summary>
    ///
    /// </summary>
    private void SetGuiInactive()
    {
      this.tb_HostName.Enabled = false;
      this.cb_ContentType.Enabled = false;
      this.bt_Add.Enabled = false;
    }


    /// <summary>
    /// 
    /// </summary>
    private delegate void OnSslStripExitedDelegate();
    private void OnSslStripExited()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnSslStripExitedDelegate(this.OnSslStripExited), new object[] { });
        return;
      }

      this.SetGuiActive();
      this.infrastructureLayer.OnStop();
      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.Error);
    }

    #endregion

  }
}