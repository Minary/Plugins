namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.Firewall.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.IO;
  using System.Windows.Forms;


  public partial class Plugin_Firewall : UserControl, IPlugin
  {

    #region MEMBERS

    private readonly string firewallConfigFilePath;
    private List<string> srcTargetList = new List<string>();
    private List<string> dstTargetList = new List<string>();
    private BindingList<FirewallRuleRecord> firewallRules;
    private Firewall.Infrastructure.Firewall infrastructureLayer;
    private bool isUpToDate = false;
    private PluginProperties pluginProperties;

    #endregion


    #region PROPERTIES

    public Control PluginControl { get { return (this); } }

    #endregion


    #region PUBLIC

    public Plugin_Firewall(PluginProperties pluginProperties)
    {
      this.InitializeComponent();

      DataGridViewTextBoxColumn columnId = new DataGridViewTextBoxColumn();
      columnId.DataPropertyName = "ID";
      columnId.Name = "ID";
      columnId.HeaderText = "ID";
      columnId.ReadOnly = true;
      columnId.Width = 0;
      columnId.Visible = false;
      this.dgv_FWRules.Columns.Add(columnId);

      DataGridViewTextBoxColumn columnProtocol = new DataGridViewTextBoxColumn();
      columnProtocol.DataPropertyName = "Protocol";
      columnProtocol.Name = "Protocol";
      columnProtocol.HeaderText = "Prot.";
      columnProtocol.ReadOnly = true;
      columnProtocol.Width = 50;
      this.dgv_FWRules.Columns.Add(columnProtocol);

      DataGridViewTextBoxColumn columnSrcIp = new DataGridViewTextBoxColumn();
      columnSrcIp.DataPropertyName = "SrcIP";
      columnSrcIp.Name = "SrcIP";
      columnSrcIp.HeaderText = "Source IP";
      columnSrcIp.ReadOnly = true;
      columnSrcIp.Width = 120;
      this.dgv_FWRules.Columns.Add(columnSrcIp);

      DataGridViewTextBoxColumn columnSrcPortLower = new DataGridViewTextBoxColumn();
      columnSrcPortLower.DataPropertyName = "SrcPortLower";
      columnSrcPortLower.Name = "SrcPortLower";
      columnSrcPortLower.HeaderText = "Src. port (lower)";
      columnSrcPortLower.ReadOnly = true;
      columnSrcPortLower.Width = 180;
      this.dgv_FWRules.Columns.Add(columnSrcPortLower);

      DataGridViewTextBoxColumn columnSrcPortUpper = new DataGridViewTextBoxColumn();
      columnSrcPortUpper.DataPropertyName = "SrcPortUpper";
      columnSrcPortUpper.Name = "SrcPortUpper";
      columnSrcPortUpper.HeaderText = "Src. port (upper)";
      columnSrcPortUpper.ReadOnly = true;
      columnSrcPortUpper.Width = 180;
      this.dgv_FWRules.Columns.Add(columnSrcPortUpper);

      DataGridViewTextBoxColumn columnDstIP = new DataGridViewTextBoxColumn();
      columnDstIP.DataPropertyName = "DstIP";
      columnDstIP.Name = "DstIP";
      columnDstIP.HeaderText = "Dest. IP";
      columnDstIP.ReadOnly = true;
      columnDstIP.Width = 120;
      this.dgv_FWRules.Columns.Add(columnDstIP);

      DataGridViewTextBoxColumn columnDstPortLower = new DataGridViewTextBoxColumn();
      columnDstPortLower.DataPropertyName = "DstPortLower";
      columnDstPortLower.Name = "DstPortLower";
      columnDstPortLower.HeaderText = "Dst. port (lower)";
      columnDstPortLower.ReadOnly = true;
      columnDstPortLower.Width = 180;
      this.dgv_FWRules.Columns.Add(columnDstPortLower);

      DataGridViewTextBoxColumn columnDstPortUpper = new DataGridViewTextBoxColumn();
      columnDstPortUpper.DataPropertyName = "DstPortUpper";
      columnDstPortUpper.Name = "DstPortUpper";
      columnDstPortUpper.HeaderText = "Dst. port (upper)";
      columnDstPortUpper.ReadOnly = true;
      columnDstPortUpper.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_FWRules.Columns.Add(columnDstPortUpper);

      this.firewallRules = new BindingList<FirewallRuleRecord>();
      this.dgv_FWRules.DataSource = this.firewallRules;

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

      this.pluginProperties.PluginName = "Firewall";
      this.pluginProperties.PluginType = "Active";
      this.pluginProperties.PluginDescription = "Control data packet flow between client and server systems";
      this.pluginProperties.Ports = new Dictionary<int, IpProtocols>();

      // Set Firewall config file path
      this.firewallConfigFilePath = Path.Combine(this.pluginProperties.HostApplication.HostWorkingDirectory, @"attackservices\ArpPoisoning\.fwrules");


      // Populate Protocol combobox
      this.cb_Protocol.Items.Add("TCP");
      this.cb_Protocol.Items.Add("UDP");
      this.cb_Protocol.SelectedIndex = 0;

      // Instantiate infrastructureLayer layer
      this.infrastructureLayer = new Firewall.Infrastructure.Firewall(this);

      // Initialize plugin environment
      this.infrastructureLayer.OnInit();
    }

    #endregion


    #region PRIVATE

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    private delegate void SetGuiActiveDelegate();
    private void SetGuiActive()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new SetGuiActiveDelegate(this.SetGuiActive), new object[] { });
        return;
      }

      this.cb_Protocol.Enabled = true;
      this.cb_SrcIP.Enabled = true;
      this.tb_SrcPortLower.Enabled = true;
      this.tb_SrcPortUpper.Enabled = true;
      this.cb_DstIP.Enabled = true;
      this.tb_DstPortLower.Enabled = true;
      this.tb_DstPortUpper.Enabled = true;
      this.bt_Add.Enabled = true;
      this.cms_DataGrid_RightMouseButton.Enabled = true;

      this.Refresh();
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

      this.cb_Protocol.Enabled = false;
      this.cb_SrcIP.Enabled = false;
      this.tb_SrcPortLower.Enabled = false;
      this.tb_SrcPortUpper.Enabled = false;
      this.cb_DstIP.Enabled = false;
      this.tb_DstPortLower.Enabled = false;
      this.tb_DstPortUpper.Enabled = false;
      this.bt_Add.Enabled = false;
      this.cms_DataGrid_RightMouseButton.Enabled = false;

      this.Refresh();
    }

    #endregion


  }
}
