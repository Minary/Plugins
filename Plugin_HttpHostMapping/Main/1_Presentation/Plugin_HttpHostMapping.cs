namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.HostMapping.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.IO;
  using System.Windows.Forms;


  public partial class Plugin_HttpHostMapping : UserControl, IPlugin
  {

    #region MEMBERS
    
    private PluginProperties pluginProperties;
    private string hostMappingConfigFilePath;
    private HostMappingConfig hostMappingConfig;
    private HostMapping.Infrastructure.HostMapping infrastructureLayer;
    private BindingList<HostMappingRecord> hostMappingRecords;

    #endregion


    #region PROPERTIES

    public Control PluginControl { get { return (this); } }

    #endregion


    #region PUBLIC

    public Plugin_HttpHostMapping(PluginProperties pluginProperties)
    {
      this.InitializeComponent();

      this.dgv_HostMapping.AutoGenerateColumns = false;

      DataGridViewTextBoxColumn columnRequestedHost = new DataGridViewTextBoxColumn();
      columnRequestedHost.DataPropertyName = "RequestedHost";
      columnRequestedHost.Name = "RequestedHost";
      columnRequestedHost.HeaderText = "Requested host";
      columnRequestedHost.ReadOnly = true;
      columnRequestedHost.Width = 500;
      this.dgv_HostMapping.Columns.Add(columnRequestedHost);

      DataGridViewTextBoxColumn columnMappedHostScheme = new DataGridViewTextBoxColumn();
      columnMappedHostScheme.DataPropertyName = "MappedHostScheme";
      columnMappedHostScheme.Name = "MappedHostScheme";
      columnMappedHostScheme.HeaderText = "Scheme";
      columnMappedHostScheme.ReadOnly = true;
      columnMappedHostScheme.Width = 100;
      this.dgv_HostMapping.Columns.Add(columnMappedHostScheme);

      DataGridViewTextBoxColumn columnMappedHost = new DataGridViewTextBoxColumn();
      columnMappedHost.DataPropertyName = "MappedHost";
      columnMappedHost.Name = "MappedHost";
      columnMappedHost.HeaderText = "Mapped host";
      columnMappedHost.ReadOnly = true;
      columnMappedHost.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_HostMapping.Columns.Add(columnMappedHost);

      this.hostMappingRecords = new BindingList<HostMappingRecord>();
      this.dgv_HostMapping.DataSource = this.hostMappingRecords;

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
          !pluginProperties.HostApplication.AttackServiceList["HttpReverseProxyServer"].SubModules.ContainsKey("HttpReverseProxyServer.HostMapping") ||
          string.IsNullOrEmpty(pluginProperties.HostApplication.AttackServiceList["HttpReverseProxyServer"].SubModules["HttpReverseProxyServer.HostMapping"].WorkingDirectory) ||
          string.IsNullOrEmpty(pluginProperties.HostApplication.AttackServiceList["HttpReverseProxyServer"].SubModules["HttpReverseProxyServer.HostMapping"].ConfigFilePath))
      {
        throw new Exception("Attack services parameters are invalid");
      }

      // Plugin configuration
      this.pluginProperties = pluginProperties;

      this.pluginProperties.PluginName = "HTTP host mapping";
      this.pluginProperties.PluginType = "Active";
      this.pluginProperties.PluginDescription = "Map HTTP request to an other server";
      this.pluginProperties.Ports = new Dictionary<int, IpProtocols>();
      //this.dataBatch = new List<string>();
      
      // Set inject payload config file path
      this.hostMappingConfigFilePath = Path.Combine(
                                                 pluginProperties.HostApplication.AttackServiceList["HttpReverseProxyServer"].SubModules["HttpReverseProxyServer.HostMapping"].WorkingDirectory,
                                                 pluginProperties.HostApplication.AttackServiceList["HttpReverseProxyServer"].SubModules["HttpReverseProxyServer.HostMapping"].ConfigFilePath);

      this.hostMappingConfig = new HostMappingConfig()
      {
        HostMappingConfigFilePath = this.hostMappingConfigFilePath,
        IsDebuggingOn = this.Config.HostApplication.IsDebuggingOn,
        BasisDirectory = this.Config.PluginBaseDir
      };

      // Instantiate infrastructureLayer layer
      this.infrastructureLayer = HostMapping.Infrastructure.HostMapping.GetInstance(this, this.hostMappingConfig);
    }

    #endregion


    #region PRIVATE
    
    /// <summary>
    ///
    /// </summary>
    private void SetGuiActive()
    {
      this.tb_MappedHost.Enabled = true;
      this.tb_RequestedHost.Enabled = true;
      this.bt_AddRecord.Enabled = true;
      this.cms_HostMapping.Enabled = true;
    }

    /// <summary>
    ///
    /// </summary>
    private void SetGuiInactive()
    {
      this.tb_MappedHost.Enabled = false;
      this.tb_RequestedHost.Enabled = false;
      this.bt_AddRecord.Enabled = false;
      this.cms_HostMapping.Enabled = false;
    }

    #endregion

  }
}
