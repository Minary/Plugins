namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.DnsPoison.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;


  public partial class Plugin_DnsPoisoning
  {

    #region IPlugin INTERFACE MEMBERS

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

      this.pluginProperties.HostApplication.LogMessage("{0}: Searching for pattern file update", this.Config.PluginName);

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
    public delegate void OnShutDownDelegate();
    public void OnShutDown()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnShutDownDelegate(this.OnShutDown), new object[] { });
        return;
      }

      this.SetGuiActive();
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

      this.tb_Address.Text = this.pluginProperties.HostApplication.CurrentIP;
      this.tb_Host.Text = string.Empty;

      this.SetGuiActive();
      this.ClearRecordList();

      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.NotRunning);
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

      if (this.dnsPoisonRecords != null && this.dnsPoisonRecords.Count > 0)
      {
        string poisoningHostsPath = this.dnsPoisoningConfigFilePath;
        string dnsPoisoningHosts = string.Empty;

        // Write DNS poisoning host list to file
        if (!string.IsNullOrEmpty(poisoningHostsPath))
        {
          if (File.Exists(poisoningHostsPath))
          {
            File.Delete(poisoningHostsPath);
          }

          foreach (RecordDnsPoison tmpRecord in this.dnsPoisonRecords.ToList())
          {
            dnsPoisoningHosts += string.Format("{0},{1}\r\n", tmpRecord.HostName, tmpRecord.IpAddress);
          }

          using (StreamWriter outfile = new StreamWriter(poisoningHostsPath))
          {
            outfile.Write(dnsPoisoningHosts);
          }
        }

        this.SetGuiInactive();
        this.pluginProperties.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.Running);
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

      string dnsPoisonedHostsFilePath = this.dnsPoisoningConfigFilePath;

      if (File.Exists(dnsPoisonedHostsFilePath))
      {
        File.Delete(dnsPoisonedHostsFilePath);
      }

      this.SetGuiActive();
      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, Status.NotRunning);
    }


    public delegate TemplatePluginData OnGetTemplateDataDelegate();
    public TemplatePluginData OnGetTemplateData()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnGetTemplateDataDelegate(this.OnGetTemplateData), new object[] { });
        return null;
      }

      return this.infrastructureLayer.OnGetTemplateData(this.dnsPoisonRecords);
    }


    public delegate void OnLoadTemplateDataDelegate(TemplatePluginData templateData);
    public void OnLoadTemplateData(TemplatePluginData templateData)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnLoadTemplateDataDelegate(this.OnLoadTemplateData), new object[] { templateData });
        return;
      }

      this.dnsPoisonRecords.Clear();

      List<RecordDnsPoison> poisoningRecords = this.infrastructureLayer.OnLoadTemplateData(templateData);
      if (poisoningRecords != null && poisoningRecords.Count > 0)
      {
        poisoningRecords.ToList().ForEach(elem => this.dnsPoisonRecords.Add(elem));
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
