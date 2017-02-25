namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.InjectCode.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.IO;
  using System.Windows.Forms;


  public partial class Plugin_HttpInjectCode : UserControl, IPlugin
  {

    #region MEMBERS

private const string Label_File = "Inject code";
private const string Label_URL = "Redirect to URL";
    private string cacheFile;
    private string cacheUrl;
    private List<Tuple<string, string, string>> targetList;
    private BindingList<InjectCodeRecord> InjectCodeRecords;
    private InjectCode.Infrastructure.HttpInjectCode infrastructureLayer;
    private InjectCodeConfig InjectCodeConfig;
    private PluginProperties pluginProperties;
    private string InjectCodeConfigFilePath;

    #endregion


    #region PROPERTIES

    public Control PluginControl { get { return this; } }

    #endregion


    #region PUBLIC

    public Plugin_HttpInjectCode(PluginProperties pluginProperties)
    {
      this.InitializeComponent();

      this.dgv_InjectionTriggerURLs.AutoGenerateColumns = false;

      DataGridViewTextBoxColumn columnRequestedScheme = new DataGridViewTextBoxColumn();
      columnRequestedScheme.DataPropertyName = "RequestedScheme";
      columnRequestedScheme.Name = "RequestedScheme";
      columnRequestedScheme.HeaderText = "Scheme";
      columnRequestedScheme.ReadOnly = true;
      columnRequestedScheme.Width = 50;
      this.dgv_InjectionTriggerURLs.Columns.Add(columnRequestedScheme);

      DataGridViewTextBoxColumn columnRequestedHost = new DataGridViewTextBoxColumn();
      columnRequestedHost.DataPropertyName = "RequestedHost";
      columnRequestedHost.Name = "RequestedHost";
      columnRequestedHost.HeaderText = "Requested host";
      columnRequestedHost.ReadOnly = true;
      columnRequestedHost.Width = 200;
      this.dgv_InjectionTriggerURLs.Columns.Add(columnRequestedHost);

      DataGridViewTextBoxColumn columnRequestedPath = new DataGridViewTextBoxColumn();
      columnRequestedPath.DataPropertyName = "RequestedPath";
      columnRequestedPath.Name = "RequestedPath";
      columnRequestedPath.HeaderText = "Requested path";
      columnRequestedPath.ReadOnly = true;
      columnRequestedPath.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_InjectionTriggerURLs.Columns.Add(columnRequestedPath);

      DataGridViewTextBoxColumn columnTag = new DataGridViewTextBoxColumn();
      columnTag.DataPropertyName = "Tag";
      columnTag.Name = "Tag";
      columnTag.HeaderText = "Tag";
      columnTag.ReadOnly = true;
      columnTag.Width = 100;
      this.dgv_InjectionTriggerURLs.Columns.Add(columnTag);

      DataGridViewTextBoxColumn columnPosition = new DataGridViewTextBoxColumn();
      columnPosition.DataPropertyName = "Position";
      columnPosition.Name = "Position";
      columnPosition.HeaderText = "Position";
      columnPosition.ReadOnly = true;
      columnPosition.Width = 100;
      this.dgv_InjectionTriggerURLs.Columns.Add(columnPosition);

      DataGridViewTextBoxColumn columnReplacementResource = new DataGridViewTextBoxColumn();
      columnReplacementResource.DataPropertyName = "InjectionCodeFile";
      columnReplacementResource.Name = "InjectionCodeFile";
      columnReplacementResource.HeaderText = "Injection code file";
      columnReplacementResource.ReadOnly = true;
      columnReplacementResource.Width = 350;
      this.dgv_InjectionTriggerURLs.Columns.Add(columnReplacementResource);

      this.InjectCodeRecords = new BindingList<InjectCodeRecord>();
      this.dgv_InjectionTriggerURLs.DataSource = this.InjectCodeRecords;

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
          !pluginProperties.HostApplication.AttackServiceList["HttpReverseProxyServer"].SubModules.ContainsKey("HttpReverseProxyServer.InjectCode") ||
          string.IsNullOrEmpty(pluginProperties.HostApplication.AttackServiceList["HttpReverseProxyServer"].SubModules["HttpReverseProxyServer.InjectCode"].WorkingDirectory) ||
          string.IsNullOrEmpty(pluginProperties.HostApplication.AttackServiceList["HttpReverseProxyServer"].SubModules["HttpReverseProxyServer.InjectCode"].ConfigFilePath))
      {
        throw new Exception("Attack services parameters are invalid");
      }

      // Plugin configuration
      this.pluginProperties = pluginProperties;

      this.pluginProperties.PluginName = "HTTP inject code";
      this.pluginProperties.PluginType = "Intrusive";
      this.pluginProperties.PluginDescription = "Inject custom code into server response";
      this.pluginProperties.Ports = new Dictionary<int, IpProtocols>();

      // Set inject code config file path
      this.InjectCodeConfigFilePath = Path.Combine(
                                                 pluginProperties.HostApplication.AttackServiceList["HttpReverseProxyServer"].SubModules["HttpReverseProxyServer.InjectCode"].WorkingDirectory,
                                                 pluginProperties.HostApplication.AttackServiceList["HttpReverseProxyServer"].SubModules["HttpReverseProxyServer.InjectCode"].ConfigFilePath);

      this.InjectCodeConfig = new InjectCodeConfig()
      {
         InjectCodeConfigFilePath = this.InjectCodeConfigFilePath,
         IsDebuggingOn = this.Config.HostApplication.IsDebuggingOn,
         BasisDirectory = this.Config.PluginBaseDir
      };

      // Populate position combobox
      this.cb_injectPosition.Items.Add("<html>");
      this.cb_injectPosition.Items.Add("</html>");
      this.cb_injectPosition.Items.Add("<head>");
      this.cb_injectPosition.Items.Add("</head>");
      this.cb_injectPosition.Items.Add("<body>");
      this.cb_injectPosition.Items.Add("</body>");
      this.cb_injectPosition.SelectedIndex = 0;

      // Instantiate infrastructureLayer layer
      this.infrastructureLayer = InjectCode.Infrastructure.HttpInjectCode.GetInstance(this, this.InjectCodeConfig);


    }

    #endregion


    #region PRIVATE

    /// <summary>
    ///
    /// </summary>
    private void SetGuiActive()
    {
      this.tb_RequestedURLRegex.Enabled = true;
      this.tb_InjectioinContentFile.Enabled = true;
      this.bt_AddFile.Enabled = true;
      this.bt_AddRecord.Enabled = true;
      this.cms_InjectCode.Enabled = true;
      this.cb_injectPosition.Enabled = true;
      this.rb_After.Enabled = true;
      this.rb_Before.Enabled = true;
    }

    /// <summary>
    ///
    /// </summary>
    private void SetGuiInactive()
    {
      this.tb_RequestedURLRegex.Enabled = false;
      this.tb_InjectioinContentFile.Enabled = false;
      this.bt_AddFile.Enabled = false;
      this.bt_AddRecord.Enabled = false;
      this.cms_InjectCode.Enabled = false;
      this.cb_injectPosition.Enabled = false;
      this.rb_After.Enabled = false;
      this.rb_Before.Enabled = false;
    }

    #endregion

  }
}
