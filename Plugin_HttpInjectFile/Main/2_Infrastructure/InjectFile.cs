namespace Minary.Plugin.Main.InjectFile.Infrastructure
{
  using Minary.Plugin.Main.InjectFile.DataTypes;
  using MinaryLib.DataTypes;
  using MinaryLib.Exceptions;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.IO;
  using System.Text;
  using System.Runtime.Serialization.Formatters.Binary;


  public class HttpInjectFile
  {

    #region MEMBERS
    
    private IPlugin plugin;
    private InjectFileConfig injectFileConfig;

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpInjectFile"/> class.
    ///
    /// </summary>
    /// <param name="plugin"></param>
    public HttpInjectFile(IPlugin plugin, InjectFileConfig injectFileConfig)
    {
      this.plugin = plugin;
      this.injectFileConfig = injectFileConfig;

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


    #region PUBLIC

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


    public void OnWriteConfiguration(List<InjectFileRecord> recordList)
    {
      if (recordList == null || 
          recordList.Count <= 0)
      {
        throw new MinaryWarningException("No file injection rules defined");
      }

      // Write configuration file
      try
      {
        if (!File.Exists(this.injectFileConfig.InjectFileConfigFilePath))
        {
          File.Delete(this.injectFileConfig.InjectFileConfigFilePath);
        }
      }
      catch (Exception ex)
      {
        this.plugin.Config.HostApplication.LogMessage($"{this.plugin.Config.PluginName}.Infrastructure.OnStart(0) : {ex.Message}");
      }

      var injectFileConfigurationFileData = string.Empty;
      foreach (InjectFileRecord tmpRecord in recordList)
      {
        var requestedHost = tmpRecord.RequestedHostRegex;
        var requestedPath = tmpRecord.RequestedPathRegex;
        var replacementResource = tmpRecord.ReplacementResource;

        injectFileConfigurationFileData += $"{tmpRecord.RequestedHostRegex}||{tmpRecord.RequestedPathRegex}||{tmpRecord.ReplacementResource}\r\n";
      }

      injectFileConfigurationFileData = injectFileConfigurationFileData.Trim();

      try
      {
        this.plugin.Config.HostApplication.LogMessage($"{this.plugin.Config.PluginName}.Infrastructure.OnStart(0): Writing to config file {this.injectFileConfig.InjectFileConfigFilePath}");
        File.WriteAllText(this.injectFileConfig.InjectFileConfigFilePath, injectFileConfigurationFileData, Encoding.ASCII);
      }
      catch (Exception ex)
      {
        throw new Exception($"Error occurred while writing Inject File configuration data: {ex.Message}");
      }
    }
 
    
    public void OnRemoveConfiguration()
    {
      // Remove plugin configuration file
      try
      {
        File.Delete(this.injectFileConfig.InjectFileConfigFilePath);
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
                                     Plugin.Main.InjectFile.DataTypes.General.PATTERN_DIR_TEMPLATE);

      if (!Directory.Exists(templateDir))
      {
        return;
      }

      string[] patternFiles = Directory.GetFiles(templateDir, Plugin.Main.InjectFile.DataTypes.General.PATTERN_FILE_PATTERN);
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

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public TemplatePluginData OnGetTemplateData(BindingList<InjectFileRecord> injectFileRecords)
    {
      var templateData = new TemplatePluginData();
      var genericObjectList = new List<InjectFileRecord>();
      foreach (InjectFileRecord tmpRecord in injectFileRecords)
      {
        genericObjectList.Add(new InjectFileRecord(tmpRecord.RequestedHostRegex, tmpRecord.RequestedPathRegex, tmpRecord.ReplacementResource));
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


    /// <summary>
    ///
    /// </summary>
    /// <param name="pluginData"></param>
    public List<InjectFileRecord> OnLoadTemplateData(TemplatePluginData templateData)
    {
      List<InjectFileRecord> poisoningRecords = null;

      if (templateData == null)
      {
        return null;
      }

      // Deserialize plugin data
      var stream = new MemoryStream();
      stream.Write(templateData.PluginConfigurationItems, 0, templateData.PluginConfigurationItems.Length);
      stream.Seek(0, SeekOrigin.Begin);

      var formatter = new BinaryFormatter();
      poisoningRecords = (List<InjectFileRecord>)formatter.Deserialize(stream);

      return poisoningRecords;
    }

    #endregion

  }
}
