﻿namespace Minary.Plugin.Main
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

      this.tb_Address.Text = this.Config.HostApplication.CurrentIP;
      this.tb_Host.Text = string.Empty;

      this.SetGuiActive();
      this.ClearRecordList();

      this.Config.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.NotRunning);
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

      if (this.dnsPoisonRecords?.Count > 0 == true)
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
            if (tmpRecord.ResponseType == DnsResponseType.A)
            {
              dnsPoisoningHosts += $"{tmpRecord.HostName},{tmpRecord.ResponseType.ToString()},{tmpRecord.IpAddress}\r\n";
            }
            else
            {
              dnsPoisoningHosts += $"{tmpRecord.HostName},{tmpRecord.ResponseType.ToString()},{tmpRecord.CName},{tmpRecord.IpAddress}\r\n";
            }
          }

          using (var outfile = new StreamWriter(poisoningHostsPath))
          {
            outfile.Write(dnsPoisoningHosts);
          }
        }

        this.SetGuiInactive();
        this.Config.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.Running);
      }
      else
      {
        this.Config.HostApplication.LogMessage($"{this.Config.PluginName}: No rule defined. Stopping the pluggin.");
        this.Config.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.NotRunning);
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

      string dnsPoisonedHostsFilePath = this.dnsPoisoningConfigFilePath;

      if (File.Exists(dnsPoisonedHostsFilePath))
      {
        File.Delete(dnsPoisonedHostsFilePath);
      }

      this.SetGuiActive();
      this.Config.HostApplication.ReportPluginSetStatus(this, Status.NotRunning);
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
      if (poisoningRecords?.Count > 0 == true)
      {
        poisoningRecords.ToList().ForEach(elem => this.dnsPoisonRecords.Add(elem));
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

      this.dnsPoisonRecords.Clear();
      this.Refresh();
    }

    #endregion

  }
}
