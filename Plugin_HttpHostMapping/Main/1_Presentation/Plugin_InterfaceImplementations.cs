namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.HostMapping.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using MinaryLib.Exceptions;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.Linq;


  public partial class Plugin_HttpHostMapping
  {

    #region IPLUGIN MEMBERS

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

      this.infrastructureLayer.OnInit();
      this.SetGuiActive();

      // Plugin initialisation
      this.pluginProperties.HostApplication.Register(this);
      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.NotRunning);
      this.Refresh();
    }


    /// <summary>
    /// 
    /// </summary>
    public delegate List<object> OnPrepareAttackDelegate();
    public List<object> OnPrepareAttack()
    {

      if (this.hostMappingRecords?.Count <= 0)
      {
        return null;
      }

      try
      {
        this.pluginProperties.HostApplication.ReportPluginSetStatus(this, Status.Running);
        this.hostMappingConfig.IsDebuggingOn = this.pluginProperties.HostApplication.IsDebuggingOn;
        this.infrastructureLayer.OnWriteConfiguration(this.hostMappingRecords.ToList());
      }
      catch (MinaryWarningException ex)
      {
        this.infrastructureLayer.OnRemoveConfiguration();
        this.pluginProperties.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.NotRunning);
        this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
      }
      catch (Exception ex)
      {
        this.infrastructureLayer.OnRemoveConfiguration();
        this.pluginProperties.HostApplication.ReportPluginSetStatus(this, Status.Error);
        this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
      }

      return null;
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

      if (this.hostMappingRecords?.Count > 0)
      {
        this.SetGuiInactive();
      }
      else
      {
        this.SetGuiInactive();
      }

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

      this.SetGuiActive();
      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.NotRunning);
      
      this.infrastructureLayer.OnRemoveConfiguration();
      this.Refresh();
    }


    /// <summary>
    ///
    /// </summary>
    public void OnShutDown()
    {
      this.infrastructureLayer.OnRemoveConfiguration();
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

      this.tb_RequestedHost.Text = string.Empty;
      this.tb_MappedHost.Text = string.Empty;

      this.ClearRecordList();
      this.SetGuiActive();
      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.NotRunning);

      this.infrastructureLayer.OnReset();
      this.Refresh();
    }


    /// <summary>
    /// New input data arrived
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
    }


    public delegate TemplatePluginData OnGetTemplateDataDelegate();
    public TemplatePluginData OnGetTemplateData()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnGetTemplateDataDelegate(this.OnGetTemplateData), new object[] { });
        return null;
      }

      return this.infrastructureLayer.OnGetTemplateData(this.hostMappingRecords);
    }


    public delegate void OnLoadTemplateDataDelegate(TemplatePluginData templateData);
    public void OnLoadTemplateData(TemplatePluginData templateData)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnLoadTemplateDataDelegate(this.OnLoadTemplateData), new object[] { templateData });
        return;
      }

      this.hostMappingRecords.Clear();

      List<HostMappingRecord> tmpHostMappingRecords = this.infrastructureLayer.OnLoadTemplateData(templateData);
      if (tmpHostMappingRecords?.Count > 0 == true)
      {
        tmpHostMappingRecords.ToList().ForEach(elem => this.hostMappingRecords.Add(elem));
      }

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

      this.hostMappingRecords.Clear();
      this.Refresh();
    }

    #endregion

  }
}
