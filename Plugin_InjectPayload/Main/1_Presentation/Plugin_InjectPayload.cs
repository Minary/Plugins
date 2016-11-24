namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.InjectPayload.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.IO;
  using System.Windows.Forms;


  public partial class Plugin_InjectPayload : UserControl, IPlugin
  {

    #region MEMBERS

    private const string Label_File = "Inject file";
    private const string Label_URL = "Redirect to URL";
    private string cacheFile;
    private string cacheUrl;
    private List<Tuple<string, string, string>> targetList;
    private List<string> dataBatch;
    private BindingList<InjectPayloadRecord> injectPayloadRecords;
    private InjectPayload.Infrastructure.InjectPayload infrastructureLayer;
    private InjectPayloadConfig injectPayloadConfig;
    private bool isUpToDate = false;
    private PluginProperties pluginProperties;
    private string injectPayloadConfigFilePath;

    #endregion


    #region PROPERTIES

    public Control PluginControl { get { return (this); } }

    #endregion


    #region PUBLIC

    public Plugin_InjectPayload(PluginProperties pluginProperties)
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

      DataGridViewTextBoxColumn columnReplacementResource = new DataGridViewTextBoxColumn();
      columnReplacementResource.DataPropertyName = "ReplacementResource";
      columnReplacementResource.Name = "ReplacementResource";
      columnReplacementResource.HeaderText = "Replacement resource";
      columnReplacementResource.ReadOnly = true;
      columnReplacementResource.Width = 350;
      this.dgv_InjectionTriggerURLs.Columns.Add(columnReplacementResource);

      DataGridViewTextBoxColumn columnReplacementType = new DataGridViewTextBoxColumn();
      columnReplacementType.DataPropertyName = "ReplacementType";
      columnReplacementType.Name = "ReplacementType";
      columnReplacementType.HeaderText = "Type";
      columnReplacementType.ReadOnly = true;
      columnReplacementType.Width = 120;
      this.dgv_InjectionTriggerURLs.Columns.Add(columnReplacementType);

      this.injectPayloadRecords = new BindingList<Plugin.Main.InjectPayload.DataTypes.InjectPayloadRecord>();
      this.dgv_InjectionTriggerURLs.DataSource = this.injectPayloadRecords;

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
          !pluginProperties.HostApplication.AttackServiceList["HttpReverseProxyServer"].SubModules.ContainsKey("HttpReverseProxyServer.InjectPayload") ||
          string.IsNullOrEmpty(pluginProperties.HostApplication.AttackServiceList["HttpReverseProxyServer"].SubModules["HttpReverseProxyServer.InjectPayload"].WorkingDirectory) ||
          string.IsNullOrEmpty(pluginProperties.HostApplication.AttackServiceList["HttpReverseProxyServer"].SubModules["HttpReverseProxyServer.InjectPayload"].ConfigFilePath))
      {
        throw new Exception("Attack services parameters are invalid");
      }

      // Plugin configuration
      this.pluginProperties = pluginProperties;

      this.pluginProperties.PluginName = "Inject payload";
      this.pluginProperties.PluginType = "Intrusive";
      this.pluginProperties.PluginDescription = "Answer a HTTP request with a custom replacement URL/file";
      this.pluginProperties.Ports = new Dictionary<int, IpProtocols>();
      this.dataBatch = new List<string>();

      // Enable URL radio button
      this.ActivateUrlInjectionSettings();

      // Set inject payload config file path
      this.injectPayloadConfigFilePath = Path.Combine(
                                                 pluginProperties.HostApplication.AttackServiceList["HttpReverseProxyServer"].SubModules["HttpReverseProxyServer.InjectPayload"].WorkingDirectory,
                                                 pluginProperties.HostApplication.AttackServiceList["HttpReverseProxyServer"].SubModules["HttpReverseProxyServer.InjectPayload"].ConfigFilePath);

      this.injectPayloadConfig = new InjectPayloadConfig()
      {
         InjectPayloadConfigFilePath = this.injectPayloadConfigFilePath,
         IsDebuggingOn = this.Config.HostApplication.IsDebuggingOn,
         BasisDirectory = this.Config.PluginBaseDir
      };

      // Instantiate infrastructureLayer layer
      this.infrastructureLayer = InjectPayload.Infrastructure.InjectPayload.GetInstance(this, this.injectPayloadConfig);
    }

    #endregion


    #region PRIVATE

    /// <summary>
    ///
    /// </summary>
    private void SetGuiActive()
    {
      this.tb_RequestedURLRegex.Enabled = true;
      this.tb_ReplacementResource.Enabled = true;
      this.rb_InjectFile.Enabled = true;
      this.rb_Redirect.Enabled = true;
      this.bt_AddFile.Enabled = true;
      this.bt_AddRecord.Enabled = true;
      this.cms_InjectPayload.Enabled = true;
    }

    /// <summary>
    ///
    /// </summary>
    private void SetGuiInactive()
    {
      this.tb_RequestedURLRegex.Enabled = false;
      this.tb_ReplacementResource.Enabled = false;
      this.rb_InjectFile.Enabled = false;
      this.rb_Redirect.Enabled = false;
      this.bt_AddFile.Enabled = false;
      this.bt_AddRecord.Enabled = false;
      this.cms_InjectPayload.Enabled = false;
    }


    private void ActivateFileInjectionSettings()
    {
      this.cacheUrl = this.tb_ReplacementResource.Text;
      this.tb_ReplacementResource.Text = this.cacheFile;
      this.tb_ReplacementResource.Select(this.tb_ReplacementResource.Text.Length, 0);
      this.tb_ReplacementResource.RightToLeft = RightToLeft.No;

      this.l_ReplacementResource.Text = Label_File;
      this.tb_ReplacementResource.Width = this.tb_ReplacementResource.Width - 40;
      this.bt_AddFile.Visible = true;
    }


    private void ActivateUrlInjectionSettings()
    {
      this.cacheFile = this.tb_ReplacementResource.Text;
      this.tb_ReplacementResource.Text = this.cacheUrl;
      this.tb_ReplacementResource.Select(0, 0);
      this.tb_ReplacementResource.RightToLeft = RightToLeft.No;

      this.l_ReplacementResource.Text = Label_URL;
      this.tb_ReplacementResource.Width = this.tb_ReplacementResource.Width + 40;
      this.bt_AddFile.Visible = false;
    }

    #endregion
    
  }
}
