namespace Minary.Plugin.Main.Firewall.Infrastructure
{
  using Minary.Plugin.Main.Firewall.DataTypes;
  using MinaryLib.DataTypes;
  using MinaryLib.Exceptions;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.IO;
  using System.Runtime.Serialization.Formatters.Binary;


  public class Firewall
  {

    #region MEMBERS

    private static Firewall instance;
    private IPlugin plugin;

    #endregion


    #region PUBLIC

    private Firewall(IPlugin plugin)
    {
      this.plugin = plugin;
    }


    /// <summary>
    /// Create single instance
    /// </summary>
    /// <returns></returns>
    public static Firewall GetInstance(IPlugin plugin)
    {
      return instance ?? (instance = new Firewall(plugin));
    }

    #endregion


    #region EVENTS

    /// <summary>
    ///
    /// </summary>
    /// <param name="pFWrules"></param>
    /// <param name="firewallRulesPath"></param>
    public void OnStart(BindingList<FirewallRuleRecord> firewallRulesList, string firewallRulesPath)
    {
      string firewallRulesString = string.Empty;

      if (firewallRulesList == null || firewallRulesList.Count <= 0)
      {
        throw new MinaryWarningException("No firewall rules defined");
      }

      if (string.IsNullOrEmpty(firewallRulesPath))
      {
        return;
      }

      // Write APE firewall rules file
      if (File.Exists(firewallRulesPath))
      {
        File.Delete(firewallRulesPath);
      }

      foreach (FirewallRuleRecord tmpFirewallRule in firewallRulesList)
      {
        firewallRulesString += string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}\r\n", tmpFirewallRule.Protocol, tmpFirewallRule.SrcIP, tmpFirewallRule.SrcPortLower, tmpFirewallRule.SrcPortUpper, tmpFirewallRule.DstIP, tmpFirewallRule.DstPortLower, tmpFirewallRule.DstPortUpper);
      }

      using (StreamWriter outfile = new StreamWriter(firewallRulesPath))
      {
        outfile.Write(firewallRulesString);
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="firewallRulesPath"></param>
    public void OnStop(string firewallRulesFilePath)
    {
      try
      {
        if (!string.IsNullOrEmpty(firewallRulesFilePath) && File.Exists(firewallRulesFilePath))
        {
          File.Delete(firewallRulesFilePath);
        }
      }
      catch (Exception)
      {
      }
    }



    /// <summary>
    ///
    /// </summary>
    /// <param name="pWebServerConfig"></param>
    public void OnInit()
    {
      List<string> pluginBasedirectories = new List<string>();

      pluginBasedirectories.Add(Path.Combine(
                             this.plugin.Config.ApplicationBaseDir,
                             this.plugin.Config.PluginBaseDir,
                             this.plugin.Config.PatternSubDir,
                             Plugin.Main.Firewall.DataTypes.Config.PATTERN_DIR_REMOTE));

      pluginBasedirectories.Add(Path.Combine(
                             this.plugin.Config.ApplicationBaseDir,
                             this.plugin.Config.PluginBaseDir,
                             this.plugin.Config.PatternSubDir,
                             Plugin.Main.Firewall.DataTypes.Config.PATTERN_DIR_LOCAL));

      pluginBasedirectories.Add(Path.Combine(
                             this.plugin.Config.ApplicationBaseDir,
                             this.plugin.Config.PluginBaseDir,
                             this.plugin.Config.PatternSubDir,
                             Plugin.Main.Firewall.DataTypes.Config.PATTERN_DIR_TEMPLATE));

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
                                        Plugin.Main.Firewall.DataTypes.Config.PATTERN_DIR_TEMPLATE);

      if (!Directory.Exists(templateDir))
      {
        return;
      }

      string[] patternFiles = Directory.GetFiles(templateDir, Plugin.Main.Firewall.DataTypes.Config.PATTERN_FILE_PATTERN);

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


    #region TEMPLATE

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public TemplatePluginData OnGetTemplateData(BindingList<FirewallRuleRecord> firewallRules)
    {
      TemplatePluginData templateData = new TemplatePluginData();
      List<FirewallRuleRecord> genericObjectList = new List<FirewallRuleRecord>();

      // Replace current configuration parameter with placeholder values
      foreach (FirewallRuleRecord tmpRecord in firewallRules)
      {
        genericObjectList.Add(new FirewallRuleRecord(tmpRecord.Protocol, tmpRecord.SrcIP, tmpRecord.SrcPortLower, tmpRecord.SrcPortUpper, tmpRecord.DstIP, tmpRecord.DstPortLower, tmpRecord.DstPortUpper));
      }

      // Serialize the list
      MemoryStream stream = new MemoryStream();
      BinaryFormatter formatter = new BinaryFormatter();
      formatter.Serialize(stream, genericObjectList);
      stream.Seek(0, SeekOrigin.Begin);

      // Assign plugin data to "Plugin Template DTO"
      templateData.PluginConfigurationItems = stream.ToArray();

      return templateData;
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="pluginData"></param>
    public List<FirewallRuleRecord> OnLoadTemplateData(TemplatePluginData pluginData)
    {
      List<FirewallRuleRecord> poisoningRecords = null;

      if (pluginData == null)
      {
        return null;
      }

      // Deserialize plugin data
      MemoryStream stream = new MemoryStream();
      stream.Write(pluginData.PluginConfigurationItems, 0, pluginData.PluginConfigurationItems.Length);
      stream.Seek(0, SeekOrigin.Begin);

      BinaryFormatter formatter = new BinaryFormatter();
      poisoningRecords = (List<FirewallRuleRecord>)formatter.Deserialize(stream);

      return poisoningRecords;
    }

    #endregion

  }
}