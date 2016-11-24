namespace Minary.Plugin.Main.IpAccounting.Infrastructure
{
  using Minary.Plugin.Main.IpAccounting.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.Globalization;
  using System.IO;
  using System.Linq;
  using System.Runtime.InteropServices;
  using System.Text.RegularExpressions;
  using System.Threading;
  using System.Xml.Linq;


  public class IpAccounting
  {

    #region IMPORTS

    [DllImport("user32.dll")]
    public static extern bool ShowWindow(IntPtr wndHandle, int commandShow);

    #endregion


    #region MEMBERS

    private static IpAccounting instance;
    private string ipAccountingBin = "IpAccounting.exe";
    private string ipAccountingProcName = "IpAccounting";
    private Process ipAccountingProc;
    private string ipAccountingPath;
    private IpAccountingConfig accountingConfig;
    private string data;
    private List<AccountingItem> accountingRecords;
    private IPlugin plugin;

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="IpAccounting"/> class.
    ///
    /// </summary>
    /// <param name="config"></param>
    private IpAccounting(IpAccountingConfig accountingConfig, IPlugin plugin, List<AccountingItem> records)
    {
      this.accountingConfig = accountingConfig;
      this.plugin = plugin;
      this.accountingRecords = records;
      this.ipAccountingPath = Path.Combine(this.accountingConfig.BasisDirectory, this.ipAccountingBin);
      this.Init(accountingConfig);
    }


    /// <summary>
    /// Create single instance
    /// </summary>
    /// <returns></returns>
    public static IpAccounting GetInstance(IpAccountingConfig config, IPlugin plugin, ref List<AccountingItem> recordList)
    {
      if (instance == null)
      {
        instance = new IpAccounting(config, plugin, recordList);
      }

      return instance;
    }

    #endregion


    #region PRIVATE

    /// <summary>
    ///
    /// </summary>
    /// <param name="config"></param>
    private void Init(IpAccountingConfig config)
    {
      if (this.accountingConfig == null)
      {
        this.accountingConfig = new IpAccountingConfig();
      }

      if (config != null)
      {
        this.accountingConfig.BasisDirectory = config.BasisDirectory != null ? config.BasisDirectory : this.accountingConfig.BasisDirectory;
        this.accountingConfig.Interface = config.Interface != null ? config.Interface : this.accountingConfig.Interface;
        this.accountingConfig.IsDebuggingOn = config.IsDebuggingOn;
        this.accountingConfig.OnIpAccountingExit = config.OnIpAccountingExit != null ? config.OnIpAccountingExit : this.accountingConfig.OnIpAccountingExit;
        this.accountingConfig.OnUpdateList = config.OnUpdateList != null ? config.OnUpdateList : this.accountingConfig.OnUpdateList;
        this.accountingConfig.StructureParameter = config.StructureParameter != null ? config.StructureParameter : this.accountingConfig.StructureParameter;
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="procName"></param>
    private void KillProcessByName(string procName)
    {
      if (string.IsNullOrEmpty(procName))
      {
        return;
      }

      foreach (Process tmpProc in Process.GetProcessesByName(procName))
      {
        try
        {
          Process.GetProcessById(tmpProc.Id).Kill();
        }
        catch (Exception)
        {
        }
      }
    }




    /// <summary>
    ///
    /// </summary>
    private void KillAllInstances()
    {
      Process[] foundProcessInstances;

      if ((foundProcessInstances = Process.GetProcessesByName(this.ipAccountingProcName)) != null && foundProcessInstances.Length <= 0)
      {
        return;
      }

      foreach (Process tmpProc in foundProcessInstances)
      {
        try
        {
          tmpProc.Kill();
        }
        catch (Exception)
        {
        }
      }
    }




    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnDataRecived(object sender, DataReceivedEventArgs e)
    {
      if (string.IsNullOrEmpty(e.Data))
      {
        return;
      }

      if (e.Data != "<EOF>")
        this.data += e.Data + "\n";
      else if (e.Data == "<EOF>")
      {
        Match trafficMatch = Regex.Match(this.data, @"(<traffic>.*?</traffic>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);

        if (trafficMatch.Success)
        {
          UpdateTextBox(trafficMatch.Groups[0].Value);
          this.data = string.Empty;
        }
        else if (Regex.Match(this.data, @"(<traffic>[\t\n\w\d]*</traffic>)", RegexOptions.IgnoreCase).Success)
        {
          this.UpdateTextBox(string.Empty);
          this.data = string.Empty;
        }
        else if (!Regex.Match(this.data, @"^\s*<traffic>", RegexOptions.IgnoreCase).Success)
        {
          this.UpdateTextBox(string.Empty);
          this.data = string.Empty;
        }
      }
    }
    

    /// <summary>
    ///
    /// </summary>
    /// <param name="data"></param>
    private void UpdateTextBox(string data)
    {
      this.accountingRecords.Clear();

      try
      {
        XDocument xmlContent = XDocument.Parse(data);
        var serviceEntries = from service in xmlContent.Descendants("entry")
                              select new
                              {
                                Basis = service.Element("basis").Value,
                                PacketCounter = service.Element("packetcounter").Value,
                                DataVolume = service.Element("datavolume").Value,
                                LastUpdate = service.Element("lastupdate").Value
                              };

        if (serviceEntries != null)
        {
          foreach (var tmpService in serviceEntries)
          {
            try
            {
              int packetCounterInt = Convert.ToInt32(tmpService.PacketCounter);
              int dataVolumeInt = Convert.ToInt32(tmpService.DataVolume);

              string packetCounterStr = packetCounterInt.ToString("#,#", CultureInfo.InvariantCulture);
              string dataVolumeStr = dataVolumeInt.ToString("#,#", CultureInfo.InvariantCulture);

              AccountingItem tmpAccountingItem = new AccountingItem(tmpService.Basis, packetCounterStr, dataVolumeStr, tmpService.LastUpdate);

              string[] splitter = Regex.Split(tmpService.Basis, @"\s+");
              if (splitter != null && splitter.Length == 2 &&
                  splitter[1].Length > 0 && splitter[1].ToLower() != "unknown")
              {
                if (!this.accountingRecords.Contains(tmpAccountingItem))
                {
                  this.accountingRecords.Add(tmpAccountingItem);
                }
              }
            }
            catch (Exception ex)
            {
              this.plugin.Config.HostApplication.LogMessage("{0} : {1}", this.plugin.Config.PluginName, ex.Message);
            }
          }
        }
      }
      catch (Exception ex)
      {
        this.plugin.Config.HostApplication.LogMessage("{0} : {1}", this.plugin.Config.PluginName, ex.Message);
      }

      // Update DGV
      if (this.accountingRecords.Count > 0 && this.accountingConfig != null && this.accountingConfig.OnUpdateList != null)
      {
        this.accountingConfig.OnUpdateList(this.accountingRecords);
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnIpAccountingExited(object sender, System.EventArgs e)
    {
      this.KillAllInstances();

      if (this.accountingConfig.OnIpAccountingExit != null)
      {
        this.accountingConfig.OnIpAccountingExit();
      }
    }
    
    #endregion


    #region TEMPLATE

    #endregion


    #region EVENTS

    /// <summary>
    ///
    /// </summary>
    public void OnInit()
    {
      this.KillAllInstances();

      List<string> pluginBasedirectories = new List<string>();

      pluginBasedirectories.Add(Path.Combine(
                                             this.plugin.Config.ApplicationBaseDir,
                                             this.plugin.Config.PluginBaseDir,
                                             this.plugin.Config.PatternSubDir,
                                             Plugin.Main.IpAccounting.DataTypes.General.PATTERN_DIR_REMOTE));

      pluginBasedirectories.Add(Path.Combine(
                                             this.plugin.Config.ApplicationBaseDir,
                                             this.plugin.Config.PluginBaseDir,
                                             this.plugin.Config.PatternSubDir,
                                             Plugin.Main.IpAccounting.DataTypes.General.PATTERN_DIR_LOCAL));

      pluginBasedirectories.Add(Path.Combine(
                                             this.plugin.Config.ApplicationBaseDir,
                                             this.plugin.Config.PluginBaseDir,
                                             this.plugin.Config.PatternSubDir,
                                             Plugin.Main.IpAccounting.DataTypes.General.PATTERN_DIR_TEMPLATE));

      pluginBasedirectories.ForEach(elem =>
      {
        try
        {
          if (!Directory.Exists(elem))
          {
            Directory.CreateDirectory(elem);
          }
        }
        catch (Exception ex)
        {
          this.plugin.Config.HostApplication.LogMessage("{0} : {1}", this.plugin.Config.PluginName, ex.Message);
        }
      });

      // Clean up template directory
      this.CleanUpTemplateDir();
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="pWebServerConfig"></param>
    public void OnReset()
    {
      this.CleanUpTemplateDir();
    }


    /// <summary>
    ///
    /// </summary>
    public void OnStartAttack(IpAccountingConfig currentAccountingConfig)
    {
      IpAccountingConfig accountingConfig = (currentAccountingConfig != null) ? currentAccountingConfig : this.accountingConfig;
      string ipAccountingPath = Path.Combine(accountingConfig.BasisDirectory, this.ipAccountingBin);

      // Reassign configuration parameters
      this.Init(accountingConfig);

      // Check binary
      if (!File.Exists(ipAccountingPath))
      {
        throw new Exception("The IpAccounting binary was not found.");
      }
      else if (string.IsNullOrEmpty(accountingConfig.Interface))
      {
        throw new Exception("There was no interface defined.");
      }

      // Create process objects
      this.accountingRecords.Clear();

      var procStartInfo = new ProcessStartInfo();
      this.ipAccountingProc = new Process();

      procStartInfo.WorkingDirectory = this.accountingConfig.BasisDirectory;
      this.ipAccountingProc.StartInfo = procStartInfo;
      this.ipAccountingProc.StartInfo.FileName = ipAccountingPath;
      this.ipAccountingProc.StartInfo.Arguments = string.Format("-i {0} {1} -x", accountingConfig.Interface, this.accountingConfig.StructureParameter);
      this.ipAccountingProc.StartInfo.UseShellExecute = false;
      this.ipAccountingProc.StartInfo.CreateNoWindow = accountingConfig.IsDebuggingOn ? false : true;
      this.ipAccountingProc.StartInfo.WindowStyle = ProcessWindowStyle.Normal;

      //// set up output redirection
      this.ipAccountingProc.StartInfo.RedirectStandardOutput = true;
      ////  ipAccountingProc.StartInfo.RedirectStandardError = true;
      this.ipAccountingProc.EnableRaisingEvents = true;

      // Set the data received handlers
      //// ipAccountingProc.ErrorDataReceived += OnDataRecived;
      this.ipAccountingProc.OutputDataReceived += this.OnDataRecived;

      // Configure the process exited event
      this.ipAccountingProc.Exited += new EventHandler(this.OnIpAccountingExited);

      this.ipAccountingProc.Start();
      //// ipAccountingProc.BeginErrorReadLine();
      this.ipAccountingProc.BeginOutputReadLine();

      Thread.Sleep(100);

      try
      {
        if (!this.accountingConfig.IsDebuggingOn)
        {
          ShowWindow(this.ipAccountingProc.MainWindowHandle, 0);
        }
      }
      catch (Exception)
      {
      }
    }

    
    /// <summary>
    ///
    /// </summary>
    public void OnStopAttack()
    {
      // Deactivate Exit event
      if (this.ipAccountingProc != null)
      {
        this.ipAccountingProc.EnableRaisingEvents = false;
        this.ipAccountingProc.Exited += null;
      }

      // Kill running IpAccounting instances.
      try
      {
        if (this.ipAccountingProc != null && !this.ipAccountingProc.HasExited)
        {
          try
          {
            this.ipAccountingProc.Kill();
          }
          catch (Exception)
          {
          }

          this.ipAccountingProc.Close();
          this.ipAccountingProc = null;
        }
      }
      catch (Exception)
      {
      }

      // Just to be safe, kill all other IP Accounting instances.
      this.KillProcessByName(this.ipAccountingProcName);
    }

    #endregion


    #region PRIVATE

    /// <summary>
    ///
    /// </summary>
    private void CleanUpTemplateDir()
    {
      string templateDir = Path.Combine(
                                        this.plugin.Config.ApplicationBaseDir,
                                        this.plugin.Config.PluginBaseDir,
                                        this.plugin.Config.PatternSubDir,
                                        Plugin.Main.IpAccounting.DataTypes.General.PATTERN_DIR_TEMPLATE);

      if (!Directory.Exists(templateDir))
      {
        return;
      }

      string[] patternFiles = Directory.GetFiles(templateDir, Plugin.Main.IpAccounting.DataTypes.General.PATTERN_FILE_PATTERN);

      foreach (string tmpFile in patternFiles)
      {
        try
        {
          File.Delete(tmpFile);
        }
        catch (Exception ex)
        {
          this.plugin.Config.HostApplication.LogMessage("{0} : {1}", this.plugin.Config.PluginName, ex.Message);
        }
      }
    }

    #endregion

  }
}