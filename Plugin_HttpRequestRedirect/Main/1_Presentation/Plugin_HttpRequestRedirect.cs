﻿namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.RequestRedirect.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.IO;
  using System.Windows.Forms;


  public partial class Plugin_HttpRequestRedirect : UserControl, IPlugin
  {

    #region MEMBERS

    private List<Tuple<string, string, string>> targetList;
    private BindingList<RequestRedirectRecord> requestRedirectRecords;
    private RequestRedirect.Infrastructure.RequestRedirect infrastructureLayer;
    private RequestRedirectConfig requestRedirectConfig;
    private PluginProperties pluginProperties;
    private string requestRedirectConfigFilePath;

    #endregion


    #region PROPERTIES

    public Control PluginControl { get { return (this); } }

    #endregion


    #region PUBLIC

    public Plugin_HttpRequestRedirect(PluginProperties pluginProperties)
    {
      this.InitializeComponent();

      // Textbox OnFocus/OnFocusLost custom implementations.
      this.tb_RequestedUrlRegex.GotFocus += this.TextBoxGotFocus;
      this.tb_RequestedUrlRegex.LostFocus += this.TextBoxLostFocus;
      this.tb_RequestedUrlRegex.Text = this.watermarkHttpRegex;
      this.tb_RequestedUrlRegex.ForeColor = System.Drawing.Color.LightGray;

      this.dgv_RequestRedirectURLs.AutoGenerateColumns = false;

      DataGridViewTextBoxColumn columnRequestedHost = new DataGridViewTextBoxColumn();
      columnRequestedHost.DataPropertyName = "RequestedHostRegex";
      columnRequestedHost.Name = "RequestedHostRegex";
      columnRequestedHost.HeaderText = "Requested host";
      columnRequestedHost.ReadOnly = true;
      columnRequestedHost.Width = 200;
      this.dgv_RequestRedirectURLs.Columns.Add(columnRequestedHost);

      DataGridViewTextBoxColumn columnRequestedPath = new DataGridViewTextBoxColumn();
      columnRequestedPath.DataPropertyName = "RequestedPathRegex";
      columnRequestedPath.Name = "RequestedPathRegex";
      columnRequestedPath.HeaderText = "Requested path";
      columnRequestedPath.ReadOnly = true;
      columnRequestedPath.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_RequestRedirectURLs.Columns.Add(columnRequestedPath);

      DataGridViewTextBoxColumn columnReplacementResource = new DataGridViewTextBoxColumn();
      columnReplacementResource.DataPropertyName = "ReplacementResource";
      columnReplacementResource.Name = "ReplacementResource";
      columnReplacementResource.HeaderText = "Replacement resource";
      columnReplacementResource.ReadOnly = true;
      columnReplacementResource.Width = 350;
      this.dgv_RequestRedirectURLs.Columns.Add(columnReplacementResource);

      DataGridViewTextBoxColumn columnRedirectType = new DataGridViewTextBoxColumn();
      columnRedirectType.DataPropertyName = "RedirectType";
      columnRedirectType.Name = "RedirectType";
      columnRedirectType.HeaderText = "Redirect type";
      columnRedirectType.ReadOnly = true;
      columnRedirectType.Width = 200;
      this.dgv_RequestRedirectURLs.Columns.Add(columnRedirectType);

      this.requestRedirectRecords = new BindingList<RequestRedirectRecord>();
      this.dgv_RequestRedirectURLs.DataSource = this.requestRedirectRecords;

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

      this.pluginProperties.PluginName = "HTTP request redirect";
      this.pluginProperties.PluginType = "Intrusive";
      this.pluginProperties.AttackServiceDependency = "HttpsReverseProxy";
      this.pluginProperties.PluginDescription = "Redirect an HTTP request to new URL";
      this.pluginProperties.Ports = new Dictionary<int, IpProtocols>();

      // Set inject file config file path
      this.requestRedirectConfigFilePath = Path.Combine(this.pluginProperties.HostApplication.HostWorkingDirectory, @"attackservices\HttpReverseProxy\plugins\requestredirect\plugin.config");

      this.requestRedirectConfig = new RequestRedirectConfig()
      {
        RequestRedirectConfigFilePath = this.requestRedirectConfigFilePath,
        IsDebuggingOn = this.Config.HostApplication.IsDebuggingOn,
        BasisDirectory = this.Config.PluginBaseDir
      };

      // Instantiate infrastructureLayer layer
      this.infrastructureLayer = new RequestRedirect.Infrastructure.RequestRedirect(this, this.requestRedirectConfig);

      // Populate redirect type combobox
      this.cb_RedirectType.Items.Add("301/Moved Permanently");
      this.cb_RedirectType.Items.Add("302/Found");
      this.cb_RedirectType.Items.Add("307/Temporary Redirect");
      this.cb_RedirectType.Items.Add("308/Permanent Redirect");
      this.cb_RedirectType.SelectedIndex = 1;
    }

    #endregion
   

    #region PRIVATE

    /// <summary>
    ///
    /// </summary>
    private void SetGuiActive()
    {
      this.tb_RequestedUrlRegex.Enabled = true;
      this.tb_RedirectURL.Enabled = true;
      this.bt_AddRecord.Enabled = true;
      this.cb_RedirectType.Enabled = true;

      this.Refresh();
    }

    /// <summary>
    ///
    /// </summary>
    private void SetGuiInactive()
    {
      this.tb_RequestedUrlRegex.Enabled = false;
      this.tb_RedirectURL.Enabled = false;
      this.bt_AddRecord.Enabled = false;
      this.cb_RedirectType.Enabled = false;

      this.Refresh();
    }

    #endregion

  }
}
