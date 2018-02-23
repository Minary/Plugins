namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.HttpSearch.DataTypes.Class;
  using Ifc = Minary.Plugin.Main.HttpSearch.DataTypes.Interface;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;


  public partial class Plugin_HttpSearch : Ifc.IObserverRecordFinding, Ifc.IObserverRecordDef
  {

    #region INTERFACE IPlugin Member

    public PluginProperties Config { get; set; }


    public delegate void OnInitDelegate();
    public void OnInit()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnInitDelegate(this.OnInit), new object[] { });
        return;
      }

      // Plugin initialisation
      this.Config.HostApplication.Register(this);
      this.Config.HostApplication.ReportPluginSetStatus(this, Status.NotRunning);

      this.SetGuiActive();
      this.t_GuiUpdate.Start();

      try
      {
        this.infrastructureLayer.OnInit();
      }
      catch (Exception ex)
      {
        this.Config.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
      }
    }


    public delegate void OnStartAttackDelegate();
    public void OnStartAttack()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnStartAttackDelegate(this.OnStartAttack), new object[] { });
        return;
      }
      
      this.Config.HostApplication.ReportPluginSetStatus(this, Status.Running);
      this.SetGuiInactive();
      this.Refresh();
    }


    public delegate void OnStopAttackDelegate();
    public void OnStopAttack()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnStopAttackDelegate(this.OnStopAttack), new object[] { });
        return;
      }

      this.Config.HostApplication.ReportPluginSetStatus(this, Status.NotRunning);
      this.SetGuiActive();
      this.Refresh();
    }


    public delegate void OnNewDataDelegate(string data);
    public void OnNewData(string data)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnNewDataDelegate(this.OnNewData), new object[] { data });
        return;
      }

      lock (this)
      {
        if (this.dataBatch != null &&
            data?.Length > 0)
        {
          this.dataBatch.Add(data);
        }
      }
    }


    public delegate void OnShutDownDelegate();
    public void OnShutDown()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnShutDownDelegate(this.OnShutDown), new object[] { });
        return;
      }

      this.infrastructureLayer.OnStop();
    }


    public void SetTargets(List<Tuple<string, string, string>> targetList)
    {
      this.targetList = targetList;
    }


    public delegate void OnResetPluginDelegate();
    public void OnResetPlugin()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnResetPluginDelegate(this.OnResetPlugin), new object[] { });
        return;
      }

      this.Config.HostApplication.ReportPluginSetStatus(this, Status.NotRunning);
      this.infrastructureLayer.ClearSearchPatternRecordList();
      try
      {
        this.httpFindingRedcords.Clear();
      }
      catch
      {
      }

      this.SetGuiActive();
      this.Refresh();
      this.infrastructureLayer.OnReset();
    }


    public delegate TemplatePluginData OnGetTemplateDataDelegate();
    public TemplatePluginData OnGetTemplateData()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnGetTemplateDataDelegate(this.OnGetTemplateData), new object[] { });
        return null;
      }

      return this.infrastructureLayer.OnGetTemplateData(this.httpSearchRecords); 
    }


    public delegate void OnLoadTemplateDataDelegate(TemplatePluginData templateData);
    public void OnLoadTemplateData(TemplatePluginData templateData)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnLoadTemplateDataDelegate(this.OnLoadTemplateData), new object[] { templateData });
        return;
      }

      // Configuration items
      this.httpSearchRecords.Clear();
      List<RecordHttpSearch> loadedApplicationRecords = this.infrastructureLayer.OnLoadTemplateData(templateData);
      loadedApplicationRecords.ForEach(elem => this.httpSearchRecords.Add(elem));

      // Pattern items
      // this.manageHttpAccountsPresentationLayer.OnLoadTemplateData(templateData);
      this.Refresh();
    }


    public delegate void OnUnloadTemplateDataDelegate();
    public void OnUnloadTemplateData()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnUnloadTemplateDataDelegate(this.OnUnloadTemplateData), new object[] { });
        return;
      }

      // Remove all template data
      this.infrastructureLayer.OnUnoadTemplateData();

      // Reload local and remote pattern files
      //this.manageHttpAccountsPresentationLayer.LocalPatternsEnabled = true;
      //this.manageHttpAccountsPresentationLayer.RemotePatternsEnabled = true;
      //this.manageHttpAccountsTaskLayer.ReadAccountsPatterns();

      this.httpSearchRecords.Clear();
      this.Refresh();
    }

    #endregion


    #region INTERFACE IObserverRecordDef

    public void UpdateRecordDef(List<RecordHttpSearch> newRecords)
    {
      if (newRecords == null)
      {
        return;
      }

      this.httpSearchRecords.Clear();
      if (newRecords.Count > 0)
      {
        newRecords.ForEach(elem => this.httpSearchRecords.Add(elem));
      }
    }

    #endregion


    #region INTERFACE IObserverRecordFound

    public void UpdateRecordsFound(List<RecordHttpRequestData> newRecords)
    {
      if (newRecords == null ||
          newRecords?.Count <= 0)
      {
        return;
      }

      var firstVisibleRowTop = -1;
      lock (this)
      {
        // Memorize DataGridView position and selection        
        firstVisibleRowTop = this.dgv_Findings.FirstDisplayedScrollingRowIndex;

        foreach (var tmpReq in newRecords)
        {
          this.httpFindingRedcords.Insert(0, tmpReq);
        }

        try
        {
          while (this.dgv_Findings.Rows.Count > this.maxRowNum)
          {
            this.httpFindingRedcords.RemoveAt(this.dgv_Findings.Rows.Count - 1);
          }

          if (firstVisibleRowTop >= 0)
          {
            this.dgv_Findings.FirstDisplayedScrollingRowIndex = firstVisibleRowTop;
          }
        }
        catch (Exception)
        {
        }
      }
    }

    #endregion

  }
}
