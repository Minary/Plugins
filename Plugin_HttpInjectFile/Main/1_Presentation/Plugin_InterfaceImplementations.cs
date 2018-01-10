namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.InjectFile.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using MinaryLib.Exceptions;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.Linq;


  public partial class Plugin_HttpInjectFile
  {

    #region IPlugin Member

    public PluginProperties Config { get { return this.pluginProperties; } set { this.pluginProperties = value; } }


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
      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.NotRunning);
      this.SetGuiActive();
      this.Refresh();
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

      if (this.injectFileRecords?.Count > 0)
      {
        try
        {
          this.SetGuiInactive();
          this.pluginProperties.HostApplication.ReportPluginSetStatus(this, Status.Running);
          this.injectFileConfig.IsDebuggingOn = this.pluginProperties.HostApplication.IsDebuggingOn;
          this.infrastructureLayer.OnStart(this.injectFileRecords.ToList());
        }
        catch (MinaryWarningException ex)
        {
          this.infrastructureLayer.OnStop();
          this.pluginProperties.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.NotRunning);
          this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
        }
        catch (Exception ex)
        {
          this.infrastructureLayer.OnStop();
          this.pluginProperties.HostApplication.ReportPluginSetStatus(this, Status.Error);
          this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
        }
      }
      else
      {
        this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: No rule defined. Stopping the pluggin.");
        this.pluginProperties.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.NotRunning);
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

      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, Status.NotRunning);
      this.SetGuiActive();
      this.infrastructureLayer.OnStop();
      this.Refresh();
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
    }


    /// <summary>
    /// New input data arrived (Not relevant in this plugin)
    /// </summary>
    /// <param name="data"></param>
    public delegate void OnNewDataDelegate(string data);
    public void OnNewData(string data)
    {
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="pTargetList"></param>
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

      this.infrastructureLayer.OnReset();
      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.NotRunning);
      this.Refresh();
    }


    public delegate TemplatePluginData OnGetTemplateDataDelegate();
    public TemplatePluginData OnGetTemplateData()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnGetTemplateDataDelegate(this.OnGetTemplateData), new object[] { });
        return null;
      }

      return this.infrastructureLayer.OnGetTemplateData(this.injectFileRecords);
    }


    public delegate void OnLoadTemplateDataDelegate(TemplatePluginData templateData);
    public void OnLoadTemplateData(TemplatePluginData templateData)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnLoadTemplateDataDelegate(this.OnLoadTemplateData), new object[] { templateData });
        return;
      }

      this.injectFileRecords.Clear();
      List<InjectFileRecord> tmpInjectFileRecords = this.infrastructureLayer.OnLoadTemplateData(templateData);
      if (tmpInjectFileRecords != null && tmpInjectFileRecords.Count > 0)
      {
        tmpInjectFileRecords.ToList().ForEach(elem => this.injectFileRecords.Add(elem));
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

      this.injectFileRecords.Clear();
      this.Refresh();
    }

    #endregion

  }
}
