namespace Minary.Plugin.Main
{
  using MinaryLib;
  using MinaryLib.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;


  public partial class Plugin_HttpRequests
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
      this.infrastructureLayer.OnInit();
      this.pluginProperties.HostApplication.Register(this);
      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.NotRunning);
      this.t_GuiUpdate.Start();
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

      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, Status.Running);
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

      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.NotRunning);
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

      this.tb_Filter.Text = string.Empty;
      this.ClearRecordList();
      this.infrastructureLayer.OnReset();

      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.NotRunning);
    }


    /// <summary>
    /// New input data arrived
    /// </summary>
    /// <param name="data"></param>
    public delegate void OnNewDataDelegate(string data);
    public void OnNewData(string data)
    {
      if (this.dgv_HTTPRequests.InvokeRequired)
      {
        this.BeginInvoke(new OnNewDataDelegate(this.OnNewData), new object[] { data });
        return;
      }

      lock (this)
      {
        if (this.dataBatch != null && !string.IsNullOrEmpty(data))
        {
          this.dataBatch.Add(data);
        }
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

  }
}
