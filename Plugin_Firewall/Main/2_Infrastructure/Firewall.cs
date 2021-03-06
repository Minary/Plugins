﻿namespace Minary.Plugin.Main.Firewall.Infrastructure
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
    
    private IPlugin plugin;

    #endregion


    #region PUBLIC

    public Firewall(IPlugin plugin)
    {
      this.plugin = plugin;
    }

    #endregion


    #region EVENTS

    /// <summary>
    ///
    /// </summary>
    /// <param name="pFWrules"></param>
    /// <param name="firewallRulesPath"></param>
    public void OnWriteConfigFile(BindingList<FirewallRuleRecord> firewallRulesList, string firewallRulesPath)
    {
      var firewallRulesString = string.Empty;

      if (firewallRulesList?.Count > 0 == false)
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
        firewallRulesString += $"{tmpFirewallRule.Protocol}:{tmpFirewallRule.SrcIp}:{tmpFirewallRule.SrcPortLower}:{tmpFirewallRule.SrcPortUpper}:{tmpFirewallRule.DstIp}:{tmpFirewallRule.DstPortLower}:{tmpFirewallRule.DstPortUpper}\r\n";
      }

      using (var outfile = new StreamWriter(firewallRulesPath))
      {
        outfile.Write(firewallRulesString);
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="firewallRulesPath"></param>
    public void OnRemoveConfiguration(string firewallRulesFilePath)
    {
      try
      {
        if (!string.IsNullOrEmpty(firewallRulesFilePath) && 
            File.Exists(firewallRulesFilePath))
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
      var pluginBasedirectories = new List<string>();

      pluginBasedirectories.Add(Path.Combine(
                             this.plugin.Config.ApplicationBaseDir,
                             this.plugin.Config.PluginBaseDir,
                             this.plugin.Config.PatternSubDir,
                             Config.PATTERN_DIR_REMOTE));

      pluginBasedirectories.Add(Path.Combine(
                             this.plugin.Config.ApplicationBaseDir,
                             this.plugin.Config.PluginBaseDir,
                             this.plugin.Config.PatternSubDir,
                             Config.PATTERN_DIR_LOCAL));

      pluginBasedirectories.Add(Path.Combine(
                             this.plugin.Config.ApplicationBaseDir,
                             this.plugin.Config.PluginBaseDir,
                             this.plugin.Config.PatternSubDir,
                             Config.PATTERN_DIR_TEMPLATE));

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
          this.plugin.Config.HostApplication.LogMessage($"{this.plugin.Config.PluginName} : {ex.Message}");
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
          this.plugin.Config.HostApplication.LogMessage("{this.plugin.Config.PluginName} : {ex.Message}");
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
        genericObjectList.Add(new FirewallRuleRecord(tmpRecord.Protocol, tmpRecord.SrcIp, tmpRecord.SrcPortLower, tmpRecord.SrcPortUpper, tmpRecord.DstIp, tmpRecord.DstPortLower, tmpRecord.DstPortUpper));
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