namespace Minary.Plugin.Main
{
  using MinaryLib;
  using MinaryLib.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;


  public partial class Plugin_DnsRequests
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
      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.NotRunning);
      this.infrastructureLayer.OnInit();

      this.t_GuiUpdate.Start();
      this.Refresh();
    }


    /// <summary>
    /// 
    /// </summary>
    public delegate List<object> OnPrepareAttackDelegate();
    public List<object> OnPrepareAttack()
    {
      return null;
    }


    public delegate void OnStartAttackDelegate();
    public void OnStartAttack()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnStartAttackDelegate(this.OnStartAttack), new object[] { });
        return;
      }

      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, Status.Running);
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

      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, Status.NotRunning);
      this.Refresh();
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

      this.tb_Filter.Text = string.Empty;

      this.ClearRecordList();
      this.infrastructureLayer.OnReset();

      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, Status.NotRunning);
      this.Refresh();
    }


    public void OnShutDown()
    {
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
          this.Refresh();
        }
      }
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

      this.infrastructureLayer.OnReset();
      this.dataBatch.Clear();
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

      this.infrastructureLayer.OnReset();
      this.dataBatch.Clear();
      this.Refresh();
    }

    #endregion

  }
}
