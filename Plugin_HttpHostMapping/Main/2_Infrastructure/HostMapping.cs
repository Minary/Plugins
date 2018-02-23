namespace Minary.Plugin.Main.HostMapping.Infrastructure
{
  using Minary.Plugin.Main.HostMapping.DataTypes;
  using MinaryLib.DataTypes;
  using MinaryLib.Exceptions;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.IO;
  using System.Runtime.Serialization.Formatters.Binary;
  using System.Text;


  public class HostMapping
  {

    #region MEMBERS

    private IPlugin plugin;
    private HostMappingConfig hostMappingConfig;

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="InjectFile"/> class.
    ///
    /// </summary>
    /// <param name="plugin"></param>
    public HostMapping(IPlugin plugin, HostMappingConfig hostMappingConfig)
    {
      this.plugin = plugin;
      this.hostMappingConfig = hostMappingConfig;

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

    public void OnReset()
    {
      try
      {
        this.CleanUpTemplateDir();
      }
      catch (Exception ex)
      {
        this.plugin.Config.HostApplication.LogMessage($"{this.plugin.Config.PluginName}.OnReset(EXCEPTION) : {ex.Message}");
      }
    }

    
    public void OnStart(List<HostMappingRecord> recordList)
    {
      if (recordList?.Count > 0 == false)
      {
        throw new MinaryWarningException("No host mapping rules defined");
      }

      var hostMappingConfigurationFileData = string.Empty;
      foreach (HostMappingRecord tmpRecord in recordList)
      {
        var requestedHost = tmpRecord.RequestedHost;
        var mappedHost = tmpRecord.MappedHost;

        hostMappingConfigurationFileData += $"{tmpRecord.RequestedHost}||{tmpRecord.MappedHost}\r\n";
      }

      hostMappingConfigurationFileData = hostMappingConfigurationFileData.Trim();

      try
      {
        this.plugin.Config.HostApplication.LogMessage($"{this.plugin.Config.PluginName}.Infrastructure.OnStart(0): Writing to config file {this.hostMappingConfig.HostMappingConfigFilePath}");
        File.WriteAllText(this.hostMappingConfig.HostMappingConfigFilePath, hostMappingConfigurationFileData, Encoding.ASCII);
      }
      catch (Exception ex)
      {
        throw new Exception($"Errorr occurred while writing HostMapping configuration data: {ex.Message}");
      }
    }

    
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

      try
      {
        this.CleanUpTemplateDir();
      }
      catch (Exception ex)
      {
        this.plugin.Config.HostApplication.LogMessage($"{this.plugin.Config.PluginName} : {ex.Message}");
      }
    }
    
    
    public void OnStop()
    {
      // Remove plugin configuration file
      try
      {
        File.Delete(this.hostMappingConfig.HostMappingConfigFilePath);
      }
      catch
      {
      }
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
                                     Plugin.Main.HostMapping.DataTypes.General.PATTERN_DIR_TEMPLATE);

      if (!Directory.Exists(templateDir))
      {
        return;
      }

      string[] patternFiles = Directory.GetFiles(templateDir, Plugin.Main.HostMapping.DataTypes.General.PATTERN_FILE_PATTERN);
      foreach (var tmpFile in patternFiles)
      {
        try
        {
          File.Delete(tmpFile);
        }
        catch (Exception ex)
        {
          this.plugin.Config.HostApplication.LogMessage($"{this.plugin.Config.PluginName}.CleanUpTemplateDir() : {ex.Message}");
        }
      }
    }

    #endregion


    #region TEMPLATE

    public TemplatePluginData OnGetTemplateData(BindingList<HostMappingRecord> hostMappingRecords)
    {
      var templateData = new TemplatePluginData();
      var genericObjectList = new List<HostMappingRecord>();

      // Replace current configuration parameter with placeholder values
      foreach (HostMappingRecord tmpRecord in hostMappingRecords)
      {
        genericObjectList.Add(new HostMappingRecord(tmpRecord.RequestedHost, tmpRecord.MappedHost));
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


    public List<HostMappingRecord> OnLoadTemplateData(TemplatePluginData pluginData)
    {
      List<HostMappingRecord> poisoningRecords = null;

      if (pluginData == null)
      {
        return null;
      }

      // Deserialize plugin data
      var stream = new MemoryStream();
      stream.Write(pluginData.PluginConfigurationItems, 0, pluginData.PluginConfigurationItems.Length);
      stream.Seek(0, SeekOrigin.Begin);

      var formatter = new BinaryFormatter();
      poisoningRecords = (List<HostMappingRecord>)formatter.Deserialize(stream);
      
      return poisoningRecords;
    }

    #endregion

  }
}
