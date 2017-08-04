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
    /// <param name="recordList"></param>
    public void OnStart(List<InjectFileRecord> recordList)
    {
      if (recordList == null || recordList.Count <= 0)
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
        this.plugin.Config.HostApplication.LogMessage("{0}.Infrastructure.OnStart(0) : {1}", this.plugin.Config.PluginName, ex.Message);
      }

      string injectFileConfigurationFileData = string.Empty;
      foreach (InjectFileRecord tmpRecord in recordList)
      {
        string requestedHost = tmpRecord.RequestedHostRegex;
        string requestedPath = tmpRecord.RequestedPathRegex;
        string replacementResource = tmpRecord.ReplacementResource;

        injectFileConfigurationFileData += string.Format("{0}||{1}||{2}\r\n", tmpRecord.RequestedHostRegex, tmpRecord.RequestedPathRegex, tmpRecord.ReplacementResource);
      }

      injectFileConfigurationFileData = injectFileConfigurationFileData.Trim();

      try
      {
        this.plugin.Config.HostApplication.LogMessage("{0}.Infrastructure.OnStart(0): Writing to config file {1}", this.plugin.Config.PluginName, this.injectFileConfig.InjectFileConfigFilePath);
        File.WriteAllText(this.injectFileConfig.InjectFileConfigFilePath, injectFileConfigurationFileData, Encoding.ASCII);
      }
      catch (Exception ex)
      {
        throw new Exception(string.Format("Errorr occurred while writing Inject File configuration data: {0}", ex.Message));
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
      string templateDir = Path.Combine(
                                        this.plugin.Config.ApplicationBaseDir,
                                        this.plugin.Config.PluginBaseDir,
                                        this.plugin.Config.PatternSubDir,
                                        Plugin.Main.InjectFile.DataTypes.General.PATTERN_DIR_TEMPLATE);

      if (!Directory.Exists(templateDir))
      {
        return;
      }

      string[] patternFiles = Directory.GetFiles(templateDir, Plugin.Main.InjectFile.DataTypes.General.PATTERN_FILE_PATTERN);

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

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public TemplatePluginData OnGetTemplateData(BindingList<InjectFileRecord> injectFileRecords)
    {
      TemplatePluginData templateData = new TemplatePluginData();
      List<InjectFileRecord> genericObjectList = new List<InjectFileRecord>();
      foreach (InjectFileRecord tmpRecord in injectFileRecords)
      {
        genericObjectList.Add(new InjectFileRecord(tmpRecord.RequestedHostRegex, tmpRecord.RequestedPathRegex, tmpRecord.ReplacementResource));
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
    public List<InjectFileRecord> OnLoadTemplateData(TemplatePluginData templateData)
    {
      List<InjectFileRecord> poisoningRecords = null;

      if (templateData == null)
      {
        return null;
      }

      // Deserialize plugin data
      MemoryStream stream = new MemoryStream();
      stream.Write(templateData.PluginConfigurationItems, 0, templateData.PluginConfigurationItems.Length);
      stream.Seek(0, SeekOrigin.Begin);

      BinaryFormatter formatter = new BinaryFormatter();
      poisoningRecords = (List<InjectFileRecord>)formatter.Deserialize(stream);

      return poisoningRecords;
    }

    #endregion

  }
}
