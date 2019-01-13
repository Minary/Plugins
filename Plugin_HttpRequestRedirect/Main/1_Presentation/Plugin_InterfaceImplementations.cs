namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.RequestRedirect.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using MinaryLib.Exceptions;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.Linq;


  public partial class Plugin_HttpRequestRedirect
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
      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, Status.NotRunning);
      this.SetGuiActive();
      this.Refresh();
    }


    /// <summary>
    /// 
    /// </summary>
    public delegate void OnPrepareAttackDelegate();
    public void OnPrepareAttack()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnPrepareAttackDelegate(this.OnPrepareAttack), new object[] { });
        return;
      }

      if (this.requestRedirectRecords?.Count > 0 == true)
      {
        return;
      }

      try
      {
        this.pluginProperties.HostApplication.ReportPluginSetStatus(this, Status.Running);
        this.requestRedirectConfig.IsDebuggingOn = this.pluginProperties.HostApplication.IsDebuggingOn;
        this.infrastructureLayer.OnWriteConfiguration(this.requestRedirectRecords.ToList());
      }
      catch (MinaryWarningException ex)
      {
        this.infrastructureLayer.OnRemoveConfiguration();
        this.pluginProperties.HostApplication.ReportPluginSetStatus(this, Status.NotRunning);
        this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
      }
      catch (Exception ex)
      {
        this.infrastructureLayer.OnRemoveConfiguration();
        this.pluginProperties.HostApplication.ReportPluginSetStatus(this, Status.Error);
        this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
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

      if (this.requestRedirectRecords?.Count > 0 == true)
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

      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, Status.NotRunning);
      this.SetGuiActive();
      this.infrastructureLayer.OnRemoveConfiguration();
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
      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, Status.NotRunning);
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

      return this.infrastructureLayer.OnGetTemplateData(this.requestRedirectRecords);
    }


    public delegate void OnLoadTemplateDataDelegate(TemplatePluginData templateData);
    public void OnLoadTemplateData(TemplatePluginData templateData)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnLoadTemplateDataDelegate(this.OnLoadTemplateData), new object[] { templateData });
        return;
      }

      this.requestRedirectRecords.Clear();

      List<RequestRedirectRecord> tmpRequestRedirectRecords = this.infrastructureLayer.OnLoadTemplateData(templateData);
      if (tmpRequestRedirectRecords != null && tmpRequestRedirectRecords.Count > 0)
      {
        tmpRequestRedirectRecords.ToList().ForEach(elem => this.requestRedirectRecords.Add(elem));
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

      this.requestRedirectRecords.Clear();
      this.Refresh();
    }

    #endregion

  }
}
