﻿namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.InjectFile.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.IO;
  using System.Windows.Forms;


  public partial class Plugin_HttpInjectFile : UserControl, IPlugin
  {

    #region MEMBERS

private const string Label_File = "Inject file";
private const string Label_URL = "Redirect to URL";
    private string cacheFile;
    private string cacheUrl;
    private List<Tuple<string, string, string>> targetList;
    private BindingList<InjectFileRecord> injectFileRecords;
    private InjectFile.Infrastructure.HttpInjectFile infrastructureLayer;
    private InjectFileConfig injectFileConfig;
    private bool isUpToDate = false;
    private PluginProperties pluginProperties;
    private string injectFileConfigFilePath;

    #endregion


    #region PROPERTIES

    public Control PluginControl { get { return this; } }

    #endregion


    #region PUBLIC

    public Plugin_HttpInjectFile(PluginProperties pluginProperties)
    {
      this.InitializeComponent();

      // Textbox OnFocus/OnFocusLost custom implementations.
      this.tb_RequestedUrlRegex.GotFocus += this.TextBoxGotFocus;
      this.tb_RequestedUrlRegex.LostFocus += this.TextBoxLostFocus;
      this.tb_RequestedUrlRegex.Text = this.watermarkHttpRegex;
      this.tb_RequestedUrlRegex.ForeColor = System.Drawing.Color.LightGray;

      this.dgv_InjectionTriggerURLs.AutoGenerateColumns = false;

      DataGridViewTextBoxColumn columnRequestedHost = new DataGridViewTextBoxColumn();
      columnRequestedHost.DataPropertyName = "RequestedHostRegex";
      columnRequestedHost.Name = "RequestedHostRegex";
      columnRequestedHost.HeaderText = "Requested host";
      columnRequestedHost.ReadOnly = true;
      columnRequestedHost.Width = 200;
      this.dgv_InjectionTriggerURLs.Columns.Add(columnRequestedHost);

      DataGridViewTextBoxColumn columnRequestedPath = new DataGridViewTextBoxColumn();
      columnRequestedPath.DataPropertyName = "RequestedPathRegex";
      columnRequestedPath.Name = "RequestedPathRegex";
      columnRequestedPath.HeaderText = "Requested path";
      columnRequestedPath.ReadOnly = true;
      columnRequestedPath.Width = 200;
      this.dgv_InjectionTriggerURLs.Columns.Add(columnRequestedPath);

      DataGridViewTextBoxColumn columnReplacementResource = new DataGridViewTextBoxColumn();
      columnReplacementResource.DataPropertyName = "ReplacementResource";
      columnReplacementResource.Name = "ReplacementResource";
      columnReplacementResource.HeaderText = "Replacement resource";
      columnReplacementResource.ReadOnly = true;
      columnReplacementResource.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_InjectionTriggerURLs.Columns.Add(columnReplacementResource);

      this.injectFileRecords = new BindingList<InjectFileRecord>();
      this.dgv_InjectionTriggerURLs.DataSource = this.injectFileRecords;

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

      this.pluginProperties.PluginName = "HTTP inject file";
      this.pluginProperties.PluginType = "Intrusive";
      this.pluginProperties.AttackServiceDependency = "HttpsReverseProxy";
      this.pluginProperties.PluginDescription = "Answer an HTTP request by injecting a custom replacement file";
      this.pluginProperties.Ports = new Dictionary<int, IpProtocols>();

      // Set inject file config file path
      this.injectFileConfigFilePath = Path.Combine(this.pluginProperties.HostApplication.HostWorkingDirectory, @"attackservices\HttpReverseProxy\plugins\injectfile\plugin.config");

      this.injectFileConfig = new InjectFileConfig()
      {
         InjectFileConfigFilePath = this.injectFileConfigFilePath,
         IsDebuggingOn = this.Config.HostApplication.IsDebuggingOn,
         BasisDirectory = this.Config.PluginBaseDir
      };

      // Instantiate infrastructureLayer layer
      this.infrastructureLayer = new InjectFile.Infrastructure.HttpInjectFile(this, this.injectFileConfig);
    }

    #endregion


    #region PRIVATE

    /// <summary>
    ///
    /// </summary>
    private void SetGuiActive()
    {
      this.tb_RequestedUrlRegex.Enabled = true;
      this.tb_ReplacementResource.Enabled = true;
      this.bt_AddFile.Enabled = true;
      this.bt_AddRecord.Enabled = true;
      this.cms_InjectFile.Enabled = true;

      this.Refresh();
    }

    /// <summary>
    ///
    /// </summary>
    private void SetGuiInactive()
    {
      this.tb_RequestedUrlRegex.Enabled = false;
      this.tb_ReplacementResource.Enabled = false;
      this.bt_AddFile.Enabled = false;
      this.bt_AddRecord.Enabled = false;
      this.cms_InjectFile.Enabled = false;

      this.Refresh();
    }

    #endregion

  }
}
