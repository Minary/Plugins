namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.HttpSearch.DataTypes.Class;
  using Ifc = Minary.Plugin.Main.HttpSearch.DataTypes.Interface;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using System;
  using System.Collections.Generic;


  public partial class Plugin_HttpSearch : Ifc.IObserverRecordFound, Ifc.IObserverRecordDef
  {

    #region INTERFACE IPlugin Member

    public PluginProperties Config { get; set; }


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
      this.Config.HostApplication.Register(this);
      this.Config.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.NotRunning);

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

      // Get active HTTP Account patterns
      lock (this)
      {
      //  try
      //  {
      //    this.accountPatterns = this.manageHttpAccountsPresentationLayer.GetActiveAuthenticationPatterns();
      //  }
      //  catch (Exception ex)
      //  {
      //    this.Config.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
      //  }
      }

      this.Config.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.Running);
      this.SetGuiInactive();
      this.Refresh();
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

      this.Config.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.NotRunning);
      this.SetGuiActive();
      this.Refresh();
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

      lock (this)
      {
        if (this.dataBatch != null &&
            data?.Length > 0)
        {
          this.dataBatch.Add(data);
          this.Refresh();
        }
      }
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

      this.infrastructureLayer.OnStop();
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

      this.Config.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.NotRunning);
      this.ClearRecordList();
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

      TemplatePluginData newTemplateData = new TemplatePluginData();

      // Configuration items
      newTemplateData.PluginConfigurationItems = this.infrastructureLayer.OnGetTemplateData(this.httpSearchRecords);

      // Pattern items
      //newTemplateData.PluginDataSearchPatternItems = this.manageHttpAccountsPresentationLayer.OnGetTemplateData();

      return newTemplateData;
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

    public void UpdateRecordsFound(List<HttpFoundRecord> newRecords)
    {
      if (newRecords == null)
      {
        return;
      }

      this.httpFindingRedcords.Clear();

      if (newRecords.Count > 0)
      {
        newRecords.ForEach(elem => this.httpFindingRedcords.Add(elem));
      }
    }

    #endregion

  }
}
