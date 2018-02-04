namespace PluginTest
{
  using MinaryLib.AttackService.Interface;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Windows.Forms;


  #region INTERFACE: IPluginHost

  public partial class PluginTest_MainForm : IPluginHost
  {
    public bool IsDebuggingOn => true;


    public string CurrentInterface => "1-2-3-4-5-6";


    public string StartIP => "192.168.0.1";


    public string StopIP => "192.168.0.255";


    public string CurrentIP => "192.168.0.100";


    public bool AttackStarted { get; set; }


    public List<Tuple<string, string, string>> ReachableSystemsList => new List<Tuple<string, string, string>>();


    public string HostWorkingDirectory => Directory.GetCurrentDirectory();


    public Dictionary<string, IAttackService> AttackServiceList => new Dictionary<string, IAttackService>();


    public Form MainWindowForm => this;


    public void LogMessage(string message, params object[] formatArgs)
    {
      var msg = string.Format(message.Trim(), formatArgs);
      this.tb_Logs.Text += $"{msg}\r\n";
    }


    public void Register(IPlugin ipi)
    {
      this.loadedPlugin = ipi;
      this.loadedPlugin.OnResetPlugin();
      this.LogMessage($"{this.loadedPlugin.Config.PluginName} : Plugin is calling back for registration");
    }

    public delegate void PluginSetStatusDelegate(object callingPluginObj, MinaryLib.Plugin.Status status);
    public void ReportPluginSetStatus(object callingPluginObj, Status status)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new PluginSetStatusDelegate(this.ReportPluginSetStatus), new object[] { callingPluginObj, status});
        return;
      }

      if (callingPluginObj == null)
      {
        return;
      }

      IPlugin tmpPlugin = (IPlugin)callingPluginObj;

      var tmpNewPluginStatus = (int)status;
      tmpNewPluginStatus = (status >= 0) ? (int)status : (int)MinaryLib.Plugin.Status.NotRunning;
      this.LogMessage($"{tmpPlugin.Config.PluginName}: Changed to state {tmpNewPluginStatus}/{status}");
    }

    #endregion

  }
}
