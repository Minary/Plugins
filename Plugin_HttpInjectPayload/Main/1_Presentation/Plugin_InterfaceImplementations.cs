namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.InjectPayload.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using MinaryLib.Exceptions;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.Linq;


  public partial class Plugin_HttpInjectPayload
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

      this.pluginProperties.HostApplication.LogMessage("{0}: Downloading new pattern file(s)", this.Config.PluginName);

      // Stop update procedure if updates were already requested before.
      if (this.isUpToDate)
      {
        this.pluginProperties.HostApplication.LogMessage("{0}: The update procedure is stopped because the plugin was already updated before.", this.Config.PluginName);
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

      if (this.injectPayloadRecords != null && this.injectPayloadRecords.Count > 0)
      {
        try
        {
          this.SetGuiInactive();
          this.pluginProperties.HostApplication.ReportPluginSetStatus(this, Status.Running);
          this.injectPayloadConfig.IsDebuggingOn = this.pluginProperties.HostApplication.IsDebuggingOn;
          this.infrastructureLayer.OnStart(this.injectPayloadRecords.ToList());
        }
        catch (MinaryWarningException ex)
        {
          this.infrastructureLayer.OnStop();
          this.pluginProperties.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.NotRunning);
          this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
        }
        catch (Exception ex)
        {
          this.infrastructureLayer.OnStop();
          this.pluginProperties.HostApplication.ReportPluginSetStatus(this, Status.Error);
          this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
        }
      }
      else
      {
        this.pluginProperties.HostApplication.LogMessage("{0}: No rule defined. Stopping the pluggin.", this.Config.PluginName);
        this.pluginProperties.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.NotRunning);
        this.SetGuiInactive();
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

      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, Status.NotRunning);
      this.SetGuiActive();
      this.infrastructureLayer.OnStop();
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

      //this.tb_HostName.Text = string.Empty;
      //this.ClearRecordList();
      this.infrastructureLayer.OnReset();
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

      return this.infrastructureLayer.OnGetTemplateData(this.injectPayloadRecords);
    }


    public delegate void OnLoadTemplateDataDelegate(TemplatePluginData templateData);
    public void OnLoadTemplateData(TemplatePluginData templateData)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnLoadTemplateDataDelegate(this.OnLoadTemplateData), new object[] { templateData });
        return;
      }

      this.injectPayloadRecords.Clear();

      List<InjectPayloadRecord> tmpInjectPayloadRecords = this.infrastructureLayer.OnLoadTemplateData(templateData);
      if (tmpInjectPayloadRecords != null && tmpInjectPayloadRecords.Count > 0)
      {
        tmpInjectPayloadRecords.ToList().ForEach(elem => this.injectPayloadRecords.Add(elem));
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

  }
}
