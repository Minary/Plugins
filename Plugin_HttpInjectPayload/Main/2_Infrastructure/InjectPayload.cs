namespace Minary.Plugin.Main.InjectPayload.Infrastructure
{
  using Minary.Plugin.Main.InjectPayload.DataTypes;
  using MinaryLib.DataTypes;
  using MinaryLib.Exceptions;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.IO;
  using System.Text;
  using System.Runtime.Serialization.Formatters.Binary;


  public class InjectPayload
  {

    #region MEMBERS

    private static InjectPayload instance;
    private IPlugin plugin;
    private InjectPayloadConfig injectPayloadConfig;

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="InjectPayload"/> class.
    ///
    /// </summary>
    /// <param name="plugin"></param>
    private InjectPayload(IPlugin plugin, InjectPayloadConfig injectPayloadConfig)
    {
      this.plugin = plugin;
      this.injectPayloadConfig = injectPayloadConfig;

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
    /// <param name="sslStripConfig"></param>
    /// <returns></returns>
    public static InjectPayload GetInstance(IPlugin plugin, InjectPayloadConfig injectPayloadConfig)
    {
      return instance ?? (instance = new InjectPayload(plugin, injectPayloadConfig));
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
    public void OnStart(List<InjectPayloadRecord> recordList)
    {
      if (recordList == null || recordList.Count <= 0)
      {
        throw new MinaryWarningException("No ssl stripping rules defined");
      }

      // Write configuration file
      try
      {
        if (!File.Exists(this.injectPayloadConfig.InjectPayloadConfigFilePath))
        {
          File.Delete(this.injectPayloadConfig.InjectPayloadConfigFilePath);
        }
      }
      catch (Exception ex)
      {
        this.plugin.Config.HostApplication.LogMessage("{0}.Infrastructure.OnStart(0) : {1}", this.plugin.Config.PluginName, ex.Message);
      }

      string injectPayloadConfigurationFileData = string.Empty;
      foreach (InjectPayloadRecord tmpRecord in recordList)
      {
        string requestedHost = tmpRecord.RequestedHost;
        string requestedPath = tmpRecord.RequestedPath;
        string replacementResource = tmpRecord.ReplacementResource;

        injectPayloadConfigurationFileData += string.Format("{0}:{1}:{2}\r\n", tmpRecord.RequestedHost, tmpRecord.RequestedPath, tmpRecord.ReplacementResource);
      }

      injectPayloadConfigurationFileData = injectPayloadConfigurationFileData.Trim();

      try
      {
        this.plugin.Config.HostApplication.LogMessage("{0}.Infrastructure.OnStart(0): Writing to config file {1}", this.plugin.Config.PluginName, this.injectPayloadConfig.InjectPayloadConfigFilePath);
        File.WriteAllText(this.injectPayloadConfig.InjectPayloadConfigFilePath, injectPayloadConfigurationFileData, Encoding.ASCII);
      }
      catch (Exception ex)
      {
        throw new Exception(string.Format("Errorr occurred while writing Inject Payload configuration data: {0}", ex.Message));
      }
    }
    

    /// <summary>
    ///
    /// </summary>
    public void OnStop()
    {
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
                                        Plugin.Main.InjectPayload.DataTypes.General.PATTERN_DIR_TEMPLATE);

      if (!Directory.Exists(templateDir))
      {
        return;
      }

      string[] patternFiles = Directory.GetFiles(templateDir, Plugin.Main.InjectPayload.DataTypes.General.PATTERN_FILE_PATTERN);

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
    public TemplatePluginData OnGetTemplateData(BindingList<InjectPayloadRecord> injectPayloadRecords)
    {
      TemplatePluginData templateData = new TemplatePluginData();
      List<InjectPayloadRecord> genericObjectList = new List<InjectPayloadRecord>();
      foreach (InjectPayloadRecord tmpRecord in injectPayloadRecords)
      {
        genericObjectList.Add(new InjectPayloadRecord(tmpRecord.RequestedScheme, tmpRecord.RequestedHost, tmpRecord.RequestedPath, tmpRecord.ReplacementResource));
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
    public List<InjectPayloadRecord> OnLoadTemplateData(TemplatePluginData templateData)
    {
      List<InjectPayloadRecord> poisoningRecords = null;

      if (templateData == null)
      {
        return null;
      }

      // Deserialize plugin data
      MemoryStream stream = new MemoryStream();
      stream.Write(templateData.PluginConfigurationItems, 0, templateData.PluginConfigurationItems.Length);
      stream.Seek(0, SeekOrigin.Begin);

      BinaryFormatter formatter = new BinaryFormatter();
      poisoningRecords = (List<InjectPayloadRecord>)formatter.Deserialize(stream);

      return poisoningRecords;
    }

    #endregion

  }
}
