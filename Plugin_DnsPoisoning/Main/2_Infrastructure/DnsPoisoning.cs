namespace Minary.Plugin.Main.DnsPoisoning.Infrastructure
{
  using Minary.Plugin.Main.DnsPoisoning.DataTypes;
  using MinaryLib.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.IO;
  using System.Runtime.Serialization.Formatters.Binary;

  
  public class DnsPoisoning
  {

    #region MEMBERS
    
    private IPlugin plugin;

    #endregion


    #region PUBLIC

    public DnsPoisoning(IPlugin plugin)
    {
      this.plugin = plugin;

      // Verifying plugin parameters
      if (plugin == null)
      {
        throw new Exception("Plugin configuration is invalid");
      }

      if (plugin.Config == null)
      {
        throw new Exception("Plugin configuration is invalid");
      }

      if (plugin.Config.PluginBaseDir == null)
      {
        throw new Exception("Plugin.Config.ApplicationBaseDir is invalid");
      }
    }

    #endregion


    #region EVENTS

    /// <summary>
    ///
    /// </summary>
    public void OnInit()
    {
      var pluginBasedirectories = new List<string>();

      pluginBasedirectories.Add(Path.Combine(
                             this.plugin.Config.ApplicationBaseDir,
                             this.plugin.Config.PluginBaseDir,
                             this.plugin.Config.PatternSubDir,
                             General.PATTERN_DIR_REMOTE));

      pluginBasedirectories.Add(Path.Combine(
                             this.plugin.Config.ApplicationBaseDir,
                             this.plugin.Config.PluginBaseDir,
                             this.plugin.Config.PatternSubDir,
                             General.PATTERN_DIR_LOCAL));

      pluginBasedirectories.Add(Path.Combine(
                             this.plugin.Config.ApplicationBaseDir,
                             this.plugin.Config.PluginBaseDir,
                             this.plugin.Config.PatternSubDir,
                             General.PATTERN_DIR_TEMPLATE));

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
      var templateDir = Path.Combine(
                                     this.plugin.Config.ApplicationBaseDir,
                                     this.plugin.Config.PluginBaseDir,
                                     this.plugin.Config.PatternSubDir,
                                     Plugin.Main.DnsPoisoning.DataTypes.General.PATTERN_DIR_TEMPLATE);

      if (!Directory.Exists(templateDir))
      {
        return;
      }

      string[] patternFiles = Directory.GetFiles(templateDir, Plugin.Main.DnsPoisoning.DataTypes.General.PATTERN_FILE_PATTERN);
      foreach (var tmpFile in patternFiles)
      {
        try
        {
          File.Delete(tmpFile);
        }
        catch (Exception ex)
        {
          this.plugin.Config.HostApplication.LogMessage($"{this.plugin.Config.PluginName} : {ex.Message}");
        }
      }
    }

    #endregion

   
    #region TEMPLATE

    public TemplatePluginData OnGetTemplateData(BindingList<RecordDnsPoison> dnsPoisonRecords)
    {
      var templateData = new TemplatePluginData();
      var genericObjectList = new List<RecordDnsPoison>();

      // Where necessary replace current configuration parameter
      // with placeholder values
      foreach (RecordDnsPoison tmpRecord in dnsPoisonRecords)
      {
        var realRecord = new RecordDnsPoison(tmpRecord.HostName, tmpRecord.IpAddress, tmpRecord.ResponseType, tmpRecord.CName, tmpRecord.TTL, tmpRecord.MustMatch);
        if (tmpRecord.IpAddress == this.plugin.Config.HostApplication.CurrentIP)
        {
          realRecord.IpAddress = MinaryLib.DSL.Config.CONSTANT_LOCAL_IP;
        }

        genericObjectList.Add(realRecord);
      }

      // Serialize the list
      var stream = new MemoryStream();
      var formatter = new BinaryFormatter();
      formatter.Serialize(stream, genericObjectList);
      stream.Seek(0, SeekOrigin.Begin);

      // Assign plugin data to "Plugin Template DTO"
      templateData.PluginConfigurationItems = stream.ToArray();

      return templateData;
    }


    public List<RecordDnsPoison> OnLoadTemplateData(TemplatePluginData pluginData)
    {
      List<RecordDnsPoison> poisoningRecords = null;

      if (pluginData == null)
      {
        return null;
      }

      // Deserialize plugin data
      var stream = new MemoryStream();
      stream.Write(pluginData.PluginConfigurationItems, 0, pluginData.PluginConfigurationItems.Length);
      stream.Seek(0, SeekOrigin.Begin);

      var formatter = new BinaryFormatter();
      poisoningRecords = (List<RecordDnsPoison>)formatter.Deserialize(stream);

      // Replace place holders by current configuration values
      poisoningRecords.ForEach(elem => {
        if (elem.IpAddress == MinaryLib.DSL.Config.CONSTANT_LOCAL_IP)
        {
          elem.IpAddress = this.plugin.Config.HostApplication.CurrentIP;
        }
      });

      return poisoningRecords;
    }

    #endregion

  }
}
