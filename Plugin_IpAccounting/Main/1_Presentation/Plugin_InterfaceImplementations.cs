namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.IpAccounting.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.Windows.Forms;


  public partial class Plugin_IpAccounting : UserControl, IPlugin, IObserver
  {

    #region IPlugin Member

    /// <summary>
    ///
    /// </summary>
    public PluginProperties Config { get { return this.pluginProperties; } set { this.pluginProperties = value; } }


    /// <summary>
    ///
    /// </summary>
    public delegate void OnInitDelegate();
    public void OnInit()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnInitDelegate(this.OnInit), new object[] { });
        return;
      }

      // Plugin initialisation
      this.pluginProperties.HostApplication.Register(this);
      this.SetGuiActive();
      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.NotRunning);
      this.domainLayer.OnInit();

      // Activate "service" ordering/filtering as default profile.
      this.RB_Service_Click(null, null);
    }


    /// <summary>
    ///
    /// </summary>
    public delegate void OnStartUpdateDelegate();
    public void OnStartUpdate()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnStartUpdateDelegate(this.OnStartUpdate), new object[] { });
        return;
      }
    }


    /// <summary>
    ///
    /// </summary>
    public delegate void OnStartAttackDelegate();
    public void OnStartAttack()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnStartAttackDelegate(this.OnStartAttack), new object[] { });
        return;
      }

      // Start accounting application.
      try
      {
        this.domainLayer.OnInit();

        IpAccountingConfig config = new IpAccountingConfig
        {
          BasisDirectory = this.Config.PluginBaseDir,
          IsDebuggingOn = this.pluginProperties.HostApplication.IsDebuggingOn,
          OnUpdateList = this.Update,
          OnIpAccountingExit = this.OnIpAccountingExited,
          Interface = this.pluginProperties.HostApplication.CurrentInterface,
          StructureParameter = this.accountingOutputType
        };

        this.domainLayer.OnStartAttack(config);
        this.SetGuiInactive();
        this.pluginProperties.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.Running);
      }
      catch (Exception ex)
      {
        this.domainLayer.OnStopAttack();
        this.pluginProperties.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.Error);
        this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
      }
    }


    /// <summary>
    ///
    /// </summary>
    public delegate void OnStopAttackDelegate();
    public void OnStopAttack()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnStopAttackDelegate(this.OnStopAttack), new object[] { });
        return;
      }

      this.domainLayer.OnStopAttack();
      this.SetGuiActive();
      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, Status.NotRunning);
    }


    /// <summary>
    ///
    /// </summary>
    public delegate void OnShutDownDelegate();
    public void OnShutDown()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnShutDownDelegate(this.OnShutDown), new object[] { });
        return;
      }

      this.domainLayer.OnStopAttack();
    }


    /// <summary>
    /// New input data arrived (Not relevant in this plugin)
    /// </summary>
    /// <param name="data"></param>
    public delegate void OnNewDataDelegate(string data);
    public void OnNewData(string data)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnNewDataDelegate(this.OnNewData), new object[] { data });
        return;
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="targetList"></param>
    public void SetTargets(List<Tuple<string, string, string>> targetList)
    {
      this.targetList = targetList;
    }


    /// <summary>
    ///
    /// </summary>
    public delegate void OnResetPluginDelegate();
    public void OnResetPlugin()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnResetPluginDelegate(this.OnResetPlugin), new object[] { });
        return;
      }

      this.domainLayer.EmptyRecordList();
      this.SetGuiActive();
      this.domainLayer.OnReset();
      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.NotRunning);
    }


    public delegate TemplatePluginData OnGetTemplateDataDelegate();
    public TemplatePluginData OnGetTemplateData()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnGetTemplateDataDelegate(this.OnGetTemplateData), new object[] { });
        return null;
      }

      return new TemplatePluginData();
    }


    public delegate void OnLoadTemplateDataDelegate(TemplatePluginData templateData);
    public void OnLoadTemplateData(TemplatePluginData templateData)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnLoadTemplateDataDelegate(this.OnLoadTemplateData), new object[] { templateData });
        return;
      }
    }


    public delegate void OnUnloadTemplateDataDelegate();
    public void OnUnloadTemplateData()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnUnloadTemplateDataDelegate(this.OnUnloadTemplateData), new object[] { });
        return;
      }
    }

    #endregion


    #region OBSERVER INTERFACE METHODS

    private delegate void UpdateDelegate(List<AccountingItem> recordList);
    public void Update(List<AccountingItem> recordList)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new UpdateDelegate(this.Update), new object[] { recordList });
        return;
      }

      int lastPosition = -1;
      int lastRowIndex = -1;
      int selectedIndex = -1;

      /*
       * Remember last position
       */
      lastPosition = this.dgv_TrafficData.FirstDisplayedScrollingRowIndex;
      lastRowIndex = this.dgv_TrafficData.Rows.Count - 1;

      if (this.dgv_TrafficData.CurrentCell != null)
      {
        selectedIndex = this.dgv_TrafficData.CurrentCell.RowIndex;
      }

      try
      {
        this.dgv_TrafficData.SuspendLayout();
        this.accountingRecords.Clear();
        foreach (AccountingItem tmpRecord in recordList)
        {
          this.accountingRecords.Add(tmpRecord);
        }
      }
      catch (Exception)
      {
      }

      // Reset position
      try
      {
        if (lastPosition >= 0)
        {
          this.dgv_TrafficData.FirstDisplayedScrollingRowIndex = lastPosition;
        }
      }
      catch (Exception)
      {
      }

      // Selected cell/row
      try
      {
        if (selectedIndex >= 0)
        {
          this.dgv_TrafficData.CurrentCell = this.dgv_TrafficData.Rows[selectedIndex].Cells[0];
        }
      }
      catch (Exception)
      {
      }

      this.dgv_TrafficData.ResumeLayout();
      this.dgv_TrafficData.Refresh();
    }

    #endregion

  }
}
