namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.DnsPoisoning.DataTypes;
  using MinaryLib;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.IO;
  using System.Windows.Forms;


  public partial class Plugin_DnsPoisoning : UserControl, IPlugin
  {

    #region MEMBERS

    private readonly int maxRowNum = 256;
    private readonly string dnsPoisoningConfigFilePath;

    private List<Tuple<string, string, string>> targetList;
    private BindingList<RecordDnsPoison> dnsPoisonRecords = new BindingList<RecordDnsPoison>();
    private DnsPoisoning.Infrastructure.DnsPoisoning infrastructureLayer;

    #endregion


    #region PROPERTIES

    public Control PluginControl { get { return (this); } }

    public string TbHostname { get { return this.tb_Host.Text; } set { } }

    public string TbSpoofedIpAddress { get { return this.tb_Address.Text; } set { } }

    public string TbTtl { get { return this.tb_ttl.Text; } set { } }

    public string TbCName { get { return this.tb_CName.Text; } set { } }

    

    public BindingList<RecordDnsPoison> DnsPoisonRecords { get { return (dnsPoisonRecords); } }

    #endregion


    #region PUBLIC

    public Plugin_DnsPoisoning(PluginProperties pluginProperties)
    {
      this.InitializeComponent();

      DataGridViewTextBoxColumn columnHostName = new DataGridViewTextBoxColumn();
      columnHostName.DataPropertyName = "HostName";
      columnHostName.Name = "HostName";
      columnHostName.HeaderText = "Host name";
      columnHostName.ReadOnly = true;
      columnHostName.Width = 296;
      this.dgv_Spoofing.Columns.Add(columnHostName);

      DataGridViewTextBoxColumn columnIpAddress = new DataGridViewTextBoxColumn();
      columnIpAddress.DataPropertyName = "IPAddress";
      columnIpAddress.Name = "IPAddress";
      columnIpAddress.HeaderText = "Spoofed IP address";
      columnIpAddress.ReadOnly = true;
      columnIpAddress.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_Spoofing.Columns.Add(columnIpAddress);

      DataGridViewTextBoxColumn columnCName = new DataGridViewTextBoxColumn();
      columnCName.DataPropertyName = "CName";
      columnCName.Name = "CName";
      columnCName.HeaderText = "Canonical name";
      columnCName.ReadOnly = true;
      columnCName.Width = 296;
      this.dgv_Spoofing.Columns.Add(columnCName);

      DataGridViewTextBoxColumn columnTtl = new DataGridViewTextBoxColumn();
      columnTtl.DataPropertyName = "TTL";
      columnTtl.Name = "TTL";
      columnTtl.HeaderText = "TTL";
      columnTtl.ReadOnly = true;
      columnTtl.Width = 130;
      this.dgv_Spoofing.Columns.Add(columnTtl);

      DataGridViewTextBoxColumn columnType = new DataGridViewTextBoxColumn();
      columnType.DataPropertyName = "ResponseType";
      columnType.Name = "ResponseType";
      columnType.HeaderText = "Resp. type";
      columnType.ReadOnly = true;
      columnType.Width = 130;
      this.dgv_Spoofing.Columns.Add(columnType);
   
      this.dgv_Spoofing.DataSource = this.dnsPoisonRecords;

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
      this.Config = pluginProperties;

      this.Config.PluginName = "DNS Poisoning";
      this.Config.PluginType = "Active";
      this.Config.PluginDescription = "Poison client system DNS request and servers DNS responses.";
      this.Config.Ports = new Dictionary<int, MinaryLib.DataTypes.IpProtocols>();

      // Set DNS poisoning config file path
      this.dnsPoisoningConfigFilePath = Path.Combine(this.Config.HostApplication.HostWorkingDirectory, @"attackservices\ArpPoisoning\.dnshosts");

      // Instantiate infrastructure layer
      this.infrastructureLayer = new DnsPoisoning.Infrastructure.DnsPoisoning(this);

      // Initialize plugin environment
      this.infrastructureLayer.OnInit();

      this.SetGuiActive();
    }

    #endregion


    #region PRIVATE

    /// <summary>
    ///
    /// </summary>
    private delegate void SetGuiInactiveDelegate();
    private void SetGuiInactive()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new SetGuiInactiveDelegate(this.SetGuiInactive), new object[] { });
        return;
      }

      this.tb_Address.Enabled = false;
      this.tb_Host.Enabled = false;
      this.bt_Add.Enabled = false;
      this.cms_DnsPoison.Enabled = false;
      this.tsmi_Delete.Enabled = false;
      this.tsmi_ClearList.Enabled = false;
      this.tb_CName.Enabled = false;
      this.cb_Cname.Enabled = false;

      this.Refresh();
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

      this.tb_Address.Enabled = true;
      this.tb_Host.Enabled = true;
      this.bt_Add.Enabled = true;
      this.cms_DnsPoison.Enabled = true;
      this.tsmi_Delete.Enabled = true;
      this.tsmi_ClearList.Enabled = true;

      this.cb_Cname.Enabled = true;
      this.tb_CName.Enabled = this.cb_Cname.Checked == true ? true : false;

      this.Refresh();
    }


    /// <summary>
    /// Poisoning exited.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnDnsHijackExited(object sender, System.EventArgs e)
    {
      this.SetGuiActive();
    }

    #endregion

  }
}
