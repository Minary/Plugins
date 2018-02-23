namespace Minary.Plugin.Main.InjectCode.Infrastructure
{
  using Minary.Plugin.Main.InjectCode.DataTypes;
  using MinaryLib.DataTypes;
  using MinaryLib.Exceptions;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.IO;
  using System.Text;
  using System.Runtime.Serialization.Formatters.Binary;


  public class HttpInjectCode
  {

    #region MEMBERS
    
    private IPlugin plugin;
    private InjectCodeConfig injectCodeConfig;

    #endregion


    #region PUBLIC

    /// <summary>
    /// 
    /// </summary>
    /// <param name="plugin"></param>
    /// <param name="InjectCodeConfig"></param>
    public HttpInjectCode(IPlugin plugin, InjectCodeConfig injectCodeConfig)
    {
      this.plugin = plugin;
      this.injectCodeConfig = injectCodeConfig;

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

    
    public void OnStart(List<InjectCodeRecord> recordList)
    {
      if (recordList == null || 
          recordList.Count <= 0)
      {
        throw new MinaryWarningException("No file injection rules defined");
      }

      // Write configuration file
      try
      {
        if (!File.Exists(this.injectCodeConfig.InjectCodeConfigFilePath))
        {
          File.Delete(this.injectCodeConfig.InjectCodeConfigFilePath);
        }
      }
      catch (Exception ex)
      {
        this.plugin.Config.HostApplication.LogMessage($"{this.plugin.Config.PluginName}.Infrastructure.OnStart(0) : {ex.Message}");
      }

      var injectCodeConfigurationFileData = string.Empty;
      foreach (InjectCodeRecord tmpRecord in recordList)
      {
        var requestedHost = tmpRecord.RequestedHostRegex;
        var requestedPath = tmpRecord.RequestedPathRegex;
        var replacementResource = tmpRecord.InjectionCodeFile;

        injectCodeConfigurationFileData += $"{tmpRecord.Tag}||{tmpRecord.Position}||{tmpRecord.InjectionCodeFile}||{tmpRecord.RequestedHostRegex}||{tmpRecord.RequestedPathRegex}\r\n";
      }

      injectCodeConfigurationFileData = injectCodeConfigurationFileData.Trim();

      try
      {
        this.plugin.Config.HostApplication.LogMessage($"{this.plugin.Config.PluginName}.Infrastructure.OnStart(0): Writing to config file {this.injectCodeConfig.InjectCodeConfigFilePath}");
        File.WriteAllText(this.injectCodeConfig.InjectCodeConfigFilePath, injectCodeConfigurationFileData, Encoding.ASCII);
      }
      catch (Exception ex)
      {
        throw new Exception($"Errorr occurred while writing Inject File configuration data: {ex.Message}");
      }
    }
 
    
    public void OnStop()
    {
      // Remove plugin configuration file
      try
      {
        File.Delete(this.injectCodeConfig.InjectCodeConfigFilePath);
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
                                     Plugin.Main.InjectCode.DataTypes.General.PATTERN_DIR_TEMPLATE);

      if (!Directory.Exists(templateDir))
      {
        return;
      }

      string[] patternFiles = Directory.GetFiles(templateDir, Plugin.Main.InjectCode.DataTypes.General.PATTERN_FILE_PATTERN);

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
    /// <param name="InjectCodeRecords"></param>
    /// <returns></returns>
    public TemplatePluginData OnGetTemplateData(BindingList<InjectCodeRecord> InjectCodeRecords)
    {
      var templateData = new TemplatePluginData();
      var genericObjectList = new List<InjectCodeRecord>();
      foreach (InjectCodeRecord tmpRecord in InjectCodeRecords)
      {
        genericObjectList.Add(new InjectCodeRecord(tmpRecord.RequestedScheme, tmpRecord.RequestedHostRegex, tmpRecord.RequestedPathRegex, tmpRecord.InjectionCodeFile, tmpRecord.Tag, tmpRecord.Position));
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
    /// <param name="templateData"></param>
    /// <returns></returns>
    public List<InjectCodeRecord> OnLoadTemplateData(TemplatePluginData templateData)
    {
      List<InjectCodeRecord> poisoningRecords = null;

      if (templateData == null)
      {
        return null;
      }

      // Deserialize plugin data
      var stream = new MemoryStream();
      stream.Write(templateData.PluginConfigurationItems, 0, templateData.PluginConfigurationItems.Length);
      stream.Seek(0, SeekOrigin.Begin);

      BinaryFormatter formatter = new BinaryFormatter();
      poisoningRecords = (List<InjectCodeRecord>)formatter.Deserialize(stream);

      return poisoningRecords;
    }

    #endregion

  }
}
