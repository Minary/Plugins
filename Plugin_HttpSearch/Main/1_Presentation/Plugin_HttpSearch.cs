namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.HttpSearch.DataTypes;
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
    private BindingList<RecordHttpSearch> dnsPoisonRecords = new BindingList<RecordHttpSearch>();

    #endregion


    #region PROPERTIES

    public Control PluginControl { get { return this; } }

    #endregion


    #region PUBLIC

    public Plugin_HttpSearch(MinaryLib.PluginProperties pluginProperties)
    {
      InitializeComponent();

      this.dgv_HttpSearch.AutoGenerateColumns = false;
      this.CB_Method.SelectedIndex = 0;
      this.CB_Type.SelectedIndex = 0;

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
      this.Config.PluginDescription = "Search data regex in HTTP header/data packets.";
      this.Config.Ports = new Dictionary<int, IpProtocols>() { { 80, IpProtocols.Tcp }, { 443, IpProtocols.Tcp } };

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
      
      this.bt_Add.Enabled = false;
      this.CB_Method.Enabled = false;
      this.CB_Type.Enabled = false;
      this.TB_DataRegex.Enabled = false;
      this.TB_HostRegex.Enabled = false;
      this.TB_PathRegex.Enabled = false;
      this.RB_Body.Enabled = false;
      this.RB_Header.Enabled = false;

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

      this.bt_Add.Enabled = true;
      this.CB_Method.Enabled = true;
      this.CB_Type.Enabled = true;
      this.TB_DataRegex.Enabled = true;
      this.TB_HostRegex.Enabled = true;
      this.TB_PathRegex.Enabled = true;

      this.RB_Body.Enabled = true;
      this.RB_Header.Enabled = true;

      this.Refresh();
    }

    #endregion

  }
}
