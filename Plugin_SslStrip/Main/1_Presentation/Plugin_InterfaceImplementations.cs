namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.SslStrip.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using MinaryLib.Exceptions;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.Linq;


  public partial class Plugin_SslStrip
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

      // Start SslStrip application.
      try
      {
        this.SetGuiInactive();
        this.pluginProperties.HostApplication.ReportPluginSetStatus(this, Status.Running);
        this.sslStripConfig.IsDebuggingOn = this.pluginProperties.HostApplication.IsDebuggingOn;
        this.infrastructureLayer.OnStart(this.sslStripRecords.ToList());
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

      this.infrastructureLayer.OnStop();
      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, Status.NotRunning);
      this.SetGuiActive();
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

      this.tb_HostName.Text = string.Empty;
      //// cb_ContentType.SelectedIndex = 1;
      //// cb_HtmlTag.SelectedIndex = 1;
      this.ClearRecordList();
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

      return this.infrastructureLayer.OnGetTemplateData(this.sslStripRecords);
    }


    public delegate void OnLoadTemplateDataDelegate(TemplatePluginData templateData);
    public void OnLoadTemplateData(TemplatePluginData templateData)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnLoadTemplateDataDelegate(this.OnLoadTemplateData), new object[] { templateData });
        return;
      }

      this.sslStripRecords.Clear();

      List<SslStripRecord> tmpSslStripRecords = this.infrastructureLayer.OnLoadTemplateData(templateData);
      if (tmpSslStripRecords != null && tmpSslStripRecords.Count > 0)
      {
        tmpSslStripRecords.ToList().ForEach(elem => this.sslStripRecords.Add(elem));
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

      this.sslStripRecords.Clear();
      this.Refresh();
    }

    #endregion

  }
}