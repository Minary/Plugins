namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.Firewall.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using MinaryLib.Exceptions;
  using System;
  using System.Collections.Generic;
  using System.Linq;


  public partial class Plugin_Firewall
  {

    #region IPLUGIN MEMBERS

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
      this.Refresh();
    }


    /// <summary>
    /// 
    /// </summary>
    public delegate object OnPrepareAttackDelegate();
    public object OnPrepareAttack()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnPrepareAttackDelegate(this.OnPrepareAttack), new object[] { });
        return null;
      }

      this.Config.HostApplication.LogMessage($"FIREWALL PATH:{this.firewallConfigFilePath}");

      try
      {
        string firewallRulesPath = this.firewallConfigFilePath;
        this.infrastructureLayer.OnWriteConfigFile(this.firewallRules, firewallRulesPath);
      }
      catch (MinaryWarningException ex)
      {
        this.Config.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.NotRunning);
        this.Config.HostApplication.LogMessage($"{Config.PluginName}: {ex.Message}");
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

      this.SetGuiInactive();
      this.Config.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.Running);
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
      this.Config.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.NotRunning);
      this.Refresh();

      // Delete firewall rules file
      this.infrastructureLayer.OnRemoveConfiguration(this.firewallConfigFilePath);
    }


    /// <summary>
    ///
    /// </summary>
    public void OnShutDown()
    {
      this.infrastructureLayer.OnRemoveConfiguration(this.firewallConfigFilePath);
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

      this.tb_DstPortLower.Text = string.Empty;
      this.tb_DstPortUpper.Text = string.Empty;
      this.tb_SrcPortLower.Text = string.Empty;
      this.tb_SrcPortUpper.Text = string.Empty;
      this.cb_DstIP.Text = string.Empty;
      this.cb_SrcIP.Text = string.Empty;

      this.ClearRecordList();
      this.SetGuiActive();
      this.Config.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.NotRunning);

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
      if (targetList?.Count > 0)
      {
        foreach (Tuple<string, string, string> tmpTuple in targetList)
        {
          this.srcTargetList.Add(tmpTuple.Item1);
          this.dstTargetList.Add(tmpTuple.Item1);
        }

        this.cb_SrcIP.DataSource = this.srcTargetList;
        this.cb_DstIP.DataSource = this.dstTargetList;
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

      return this.infrastructureLayer.OnGetTemplateData(this.firewallRules);
    }


    public delegate void OnLoadTemplateDataDelegate(TemplatePluginData templateData);
    public void OnLoadTemplateData(TemplatePluginData templateData)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnLoadTemplateDataDelegate(this.OnLoadTemplateData), new object[] { templateData });
        return;
      }

      this.firewallRules.Clear();

      List<FirewallRuleRecord> poisoningRecords = this.infrastructureLayer.OnLoadTemplateData(templateData);
      if (poisoningRecords?.Count > 0 == true)
      {
        poisoningRecords.ToList().ForEach(elem => this.firewallRules.Add(elem));
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
      
      this.firewallRules.Clear();
      this.Refresh();
    }

    #endregion

  }
}
