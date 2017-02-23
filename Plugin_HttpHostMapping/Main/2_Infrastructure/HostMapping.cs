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

    private static HostMapping instance;
    private IPlugin plugin;
    private HostMappingConfig hostMappingConfig;

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="InjectPayload"/> class.
    ///
    /// </summary>
    /// <param name="plugin"></param>
    private HostMapping(IPlugin plugin, HostMappingConfig hostMappingConfig)
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="plugin"></param>
    /// <param name="hostMappingConfig"></param>
    /// <returns></returns>
    public static HostMapping GetInstance(IPlugin plugin, HostMappingConfig hostMappingConfig)
    {
      return instance ?? (instance = new HostMapping(plugin, hostMappingConfig));
    }


    /// <summary>
    ///
    /// </summary>
    public void OnReset()
    {
      try
      {
        this.CleanUpTemplateDir();
      }
      catch (Exception ex)
      {
        this.plugin.Config.HostApplication.LogMessage("{0}.OnReset(EXCEPTION) : {1}", this.plugin.Config.PluginName, ex.Message);
      }
    }


    /// <summary>
    ///
    /// </summary>
    public void OnStart(List<HostMappingRecord> recordList)
    {
      if (recordList == null || recordList.Count <= 0)
      {
        throw new MinaryWarningException("No host mapping rules defined");
      }

      string hostMappingConfigurationFileData = string.Empty;
      foreach (HostMappingRecord tmpRecord in recordList)
      {
        string requestedHost = tmpRecord.RequestedHost;
        string mappedHost = tmpRecord.MappedHost;

        hostMappingConfigurationFileData += string.Format("{0}:{1}\r\n", tmpRecord.RequestedHost, tmpRecord.MappedHost);
      }

      hostMappingConfigurationFileData = hostMappingConfigurationFileData.Trim();

      try
      {
        this.plugin.Config.HostApplication.LogMessage("{0}.Infrastructure.OnStart(0): Writing to config file {1}", this.plugin.Config.PluginName, this.hostMappingConfig.HostMappingConfigFilePath);
        File.WriteAllText(this.hostMappingConfig.HostMappingConfigFilePath, hostMappingConfigurationFileData, Encoding.ASCII);
      }
      catch (Exception ex)
      {
        throw new Exception(string.Format("Errorr occurred while writing HostMapping configuration data: {0}", ex.Message));
      }
    }


    /// <summary>
    ///
    /// </summary>
    public void OnInit()
    {
      List<string> pluginBasedirectories = new List<string>();

      pluginBasedirectories.Add(Path.Combine(
                             this.plugin.Config.ApplicationBaseDir,
                             this.plugin.Config.PluginBaseDir,
                             this.plugin.Config.PatternSubDir,
                             Plugin.Main.HostMapping.DataTypes.General.PATTERN_DIR_REMOTE));

      pluginBasedirectories.Add(Path.Combine(
                             this.plugin.Config.ApplicationBaseDir,
                             this.plugin.Config.PluginBaseDir,
                             this.plugin.Config.PatternSubDir,
                             Plugin.Main.HostMapping.DataTypes.General.PATTERN_DIR_LOCAL));

      pluginBasedirectories.Add(Path.Combine(
                             this.plugin.Config.ApplicationBaseDir,
                             this.plugin.Config.PluginBaseDir,
                             this.plugin.Config.PatternSubDir,
                             Plugin.Main.HostMapping.DataTypes.General.PATTERN_DIR_TEMPLATE));

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

      try
      {
        this.CleanUpTemplateDir();
      }
      catch (Exception ex)
      {
        this.plugin.Config.HostApplication.LogMessage("{0} : {1}", this.plugin.Config.PluginName, ex.Message);
      }
    }



    /// <summary>
    ///
    /// </summary>
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
      string templateDir = Path.Combine(
                                        this.plugin.Config.ApplicationBaseDir,
                                        this.plugin.Config.PluginBaseDir,
                                        this.plugin.Config.PatternSubDir,
                                        Plugin.Main.HostMapping.DataTypes.General.PATTERN_DIR_TEMPLATE);

      if (!Directory.Exists(templateDir))
      {
        return;
      }

      string[] patternFiles = Directory.GetFiles(templateDir, Plugin.Main.HostMapping.DataTypes.General.PATTERN_FILE_PATTERN);

      foreach (string tmpFile in patternFiles)
      {
        try
        {
          File.Delete(tmpFile);
        }
        catch (Exception ex)
        {
          this.plugin.Config.HostApplication.LogMessage("{0}.CleanUpTemplateDir() : {1}", this.plugin.Config.PluginName, ex.Message);
        }
      }
    }

    #endregion


    #region TEMPLATE

    public TemplatePluginData OnGetTemplateData(BindingList<HostMappingRecord> hostMappingRecords)
    {
      TemplatePluginData templateData = new TemplatePluginData();
      List<HostMappingRecord> genericObjectList = new List<HostMappingRecord>();

      // Replace current configuration parameter with placeholder values
      foreach (HostMappingRecord tmpRecord in hostMappingRecords)
      {
        genericObjectList.Add(new HostMappingRecord(tmpRecord.RequestedHost, tmpRecord.MappedHost));
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


    public List<HostMappingRecord> OnLoadTemplateData(TemplatePluginData pluginData)
    {
      List<HostMappingRecord> poisoningRecords = null;

      if (pluginData == null)
      {
        return null;
      }

      // Deserialize plugin data
      MemoryStream stream = new MemoryStream();
      stream.Write(pluginData.PluginConfigurationItems, 0, pluginData.PluginConfigurationItems.Length);
      stream.Seek(0, SeekOrigin.Begin);

      BinaryFormatter formatter = new BinaryFormatter();
      poisoningRecords = (List<HostMappingRecord>)formatter.Deserialize(stream);
      
      return poisoningRecords;
    }

    #endregion

  }
}