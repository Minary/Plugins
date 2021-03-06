﻿namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.HttpSearch.DataTypes.Class;
  using MinaryLib.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Windows.Forms;


  public partial class Plugin_HttpSearch : UserControl, IPlugin
  {

    #region MEMBERS

    private readonly int maxRowNum = 256;
    private BindingList<RecordHttpSearch> httpSearchRecords = new BindingList<RecordHttpSearch>();
    private BindingList<RecordHttpRequestData> httpFindingRedcords = new BindingList<RecordHttpRequestData>();
    private HttpSearch.Infrastructure.HttpSearch infrastructureLayer;
    private List<string> dataBatch = new List<string>();
    private List<Tuple<string, string, string>> targetList;

    #endregion


    #region PROPERTIES

    public Control PluginControl { get { return this; } }

    public BindingList<RecordHttpSearch> HttpSearchRecords { get { return this.httpSearchRecords; } }

    public BindingList<RecordHttpRequestData> HttpFindingRedcords { get { return this.httpFindingRedcords; } }

    #endregion


    #region PUBLIC

    public Plugin_HttpSearch(MinaryLib.PluginProperties pluginProperties)
    {
      InitializeComponent();

      DataGridViewTextBoxColumn columnMethod = new DataGridViewTextBoxColumn();
      columnMethod.DataPropertyName = "Method";
      columnMethod.Name = "Method";
      columnMethod.HeaderText = "Method";
      columnMethod.ReadOnly = true;
      columnMethod.Width = 100;
      this.dgv_HttpSearch.Columns.Add(columnMethod);

      DataGridViewTextBoxColumn columnHostRegex = new DataGridViewTextBoxColumn();
      columnHostRegex.DataPropertyName = "HostRegex";
      columnHostRegex.Name = "HostRegex";
      columnHostRegex.HeaderText = "HostRegex";
      columnHostRegex.ReadOnly = true;
      columnHostRegex.Width = 260;
      this.dgv_HttpSearch.Columns.Add(columnHostRegex);

      DataGridViewTextBoxColumn columnPathRegex = new DataGridViewTextBoxColumn();
      columnPathRegex.DataPropertyName = "PathRegex";
      columnPathRegex.Name = "PathRegex";
      columnPathRegex.HeaderText = "PathRegex";
      columnPathRegex.ReadOnly = true;
      columnPathRegex.Width = 260;
      this.dgv_HttpSearch.Columns.Add(columnPathRegex);

      DataGridViewTextBoxColumn columnDataRegex = new DataGridViewTextBoxColumn();
      columnDataRegex.DataPropertyName = "DataRegex";
      columnDataRegex.Name = "DataRegex";
      columnDataRegex.HeaderText = "DataRegex";
      columnDataRegex.ReadOnly = true;
      columnDataRegex.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_HttpSearch.Columns.Add(columnDataRegex);

      this.dgv_HttpSearch.DataSource = this.httpSearchRecords;
      this.dgv_HttpSearch.AutoGenerateColumns = false;



      DataGridViewTextBoxColumn columnFindingMethod = new DataGridViewTextBoxColumn();
      columnFindingMethod.DataPropertyName = "Method";
      columnFindingMethod.Name = "Method";
      columnFindingMethod.HeaderText = "Method";
      columnFindingMethod.ReadOnly = true;
      columnFindingMethod.Width = 100;
      this.dgv_Findings.Columns.Add(columnFindingMethod);

      DataGridViewTextBoxColumn columnFindingHost = new DataGridViewTextBoxColumn();
      columnFindingHost.DataPropertyName = "Host";
      columnFindingHost.Name = "Host";
      columnFindingHost.HeaderText = "Host";
      columnFindingHost.ReadOnly = true;
      columnFindingHost.Width = 300;
      this.dgv_Findings.Columns.Add(columnFindingHost);

      DataGridViewTextBoxColumn columnFindingPath = new DataGridViewTextBoxColumn();
      columnFindingPath.DataPropertyName = "Path";
      columnFindingPath.Name = "Path";
      columnFindingPath.HeaderText = "Path";
      columnFindingPath.ReadOnly = true;
      columnFindingPath.Width = 600;
      this.dgv_Findings.Columns.Add(columnFindingPath);

      DataGridViewTextBoxColumn columnFindingFinding = new DataGridViewTextBoxColumn();
      columnFindingFinding.DataPropertyName = "Data";
      columnFindingFinding.Name = "Data";
      columnFindingFinding.HeaderText = "Data";
      columnFindingFinding.ReadOnly = true;
      columnFindingFinding.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_Findings.Columns.Add(columnFindingFinding);
      
      this.dgv_Findings.AutoGenerateColumns = false;
      this.dgv_Findings.DataSource = this.httpFindingRedcords;


      this.cb_Method.SelectedIndex = 0;

      // Verify passed parameter(s)
      if (pluginProperties == null)
      {
        throw new Exception("Parameter PluginParameters is null");
      }

      if (pluginProperties?.HostApplication == null)
      {
        throw new Exception("Parameter HostApplication is null");
      }

      if (pluginProperties?.ApplicationBaseDir == null)
      {
        throw new Exception("Parameter ApplicationBaseDir is null");
      }

      if (pluginProperties?.PluginBaseDir == null)
      {
        throw new Exception("Parameter PluginBaseDir is null");
      }

      // Configure plugin
      this.Config = pluginProperties;
      this.Config.PluginName = "HTTP search";
      this.Config.PluginType = "Passive";
      this.Config.AttackServiceDependency = "HttpsReverseProxy";
      this.Config.PluginDescription = "Search data regex in HTTP header/data packets.";
      this.Config.Ports = new Dictionary<int, IpProtocols>() { { 80, IpProtocols.Tcp }, { 443, IpProtocols.Tcp } };

      // Instantiate infrastructure layer
      this.infrastructureLayer = new HttpSearch.Infrastructure.HttpSearch(this);
      this.infrastructureLayer.AddObserverRecordDef(this);
      this.infrastructureLayer.AddObserverRecordFound(this);
    }

    public List<string> DataBatch { get { return this.dataBatch; } private set { } }

    #endregion


    #region PRIVATE
    
    private delegate void SetGuiInactiveDelegate();
    private void SetGuiInactive()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new SetGuiInactiveDelegate(this.SetGuiInactive), new object[] { });
        return;
      }
      
      this.bt_Add.Enabled = false;
      this.cb_Method.Enabled = false;
      this.tb_DataRegex.Enabled = false;
      this.tb_HostRegex.Enabled = false;
      this.tb_PathRegex.Enabled = false;
      this.cms_HttpSearchPatterns.Enabled = false;

      this.Refresh();
    }

    
    private delegate void SetGuiActiveDelegate();
    private void SetGuiActive()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new SetGuiActiveDelegate(this.SetGuiActive), new object[] { });
        return;
      }

      this.bt_Add.Enabled = true;
      this.cb_Method.Enabled = true;
      this.tb_DataRegex.Enabled = true;
      this.tb_HostRegex.Enabled = true;
      this.tb_PathRegex.Enabled = true;
      this.cms_HttpSearchPatterns.Enabled = true;

      this.Refresh();
    }


    public void ProcessEntries()
    {
      List<string> newDataPackets;
      
      lock (this)
      {
        newDataPackets = new List<string>(this.dataBatch);
        this.dataBatch.Clear();
      }

      try
      {
        this.infrastructureLayer.ProcessEntries(newDataPackets);
      }
      catch (Exception ex)
      {
        this.Config.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
        return;
      }
    }

    #endregion

  }
}
