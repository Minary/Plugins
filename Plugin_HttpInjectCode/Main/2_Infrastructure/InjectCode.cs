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

    private static HttpInjectCode instance;
    private IPlugin plugin;
    private InjectCodeConfig InjectCodeConfig;

    #endregion


    #region PUBLIC

    /// <summary>
    /// 
    /// </summary>
    /// <param name="plugin"></param>
    /// <param name="InjectCodeConfig"></param>
    private HttpInjectCode(IPlugin plugin, InjectCodeConfig InjectCodeConfig)
    {
      this.plugin = plugin;
      this.InjectCodeConfig = InjectCodeConfig;

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
    public static HttpInjectCode GetInstance(IPlugin plugin, InjectCodeConfig InjectCodeConfig)
    {
      return instance ?? (instance = new HttpInjectCode(plugin, InjectCodeConfig));
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
    public void OnStart(List<InjectCodeRecord> recordList)
    {
      if (recordList == null || recordList.Count <= 0)
      {
        throw new MinaryWarningException("No file injection rules defined");
      }

      // Write configuration file
      try
      {
        if (!File.Exists(this.InjectCodeConfig.InjectCodeConfigFilePath))
        {
          File.Delete(this.InjectCodeConfig.InjectCodeConfigFilePath);
        }
      }
      catch (Exception ex)
      {
        this.plugin.Config.HostApplication.LogMessage("{0}.Infrastructure.OnStart(0) : {1}", this.plugin.Config.PluginName, ex.Message);
      }

      string injectCodeConfigurationFileData = string.Empty;
      foreach (InjectCodeRecord tmpRecord in recordList)
      {
        string requestedHost = tmpRecord.RequestedHost;
        string requestedPath = tmpRecord.RequestedPath;
        string replacementResource = tmpRecord.InjectionCodeFile;

        injectCodeConfigurationFileData += string.Format("{0}||{1}||{2}||{3}||{4}\r\n", tmpRecord.Tag, tmpRecord.Position, tmpRecord.InjectionCodeFile, tmpRecord.RequestedHost, tmpRecord.RequestedPath);
      }

      injectCodeConfigurationFileData = injectCodeConfigurationFileData.Trim();

      try
      {
        this.plugin.Config.HostApplication.LogMessage("{0}.Infrastructure.OnStart(0): Writing to config file {1}", this.plugin.Config.PluginName, this.InjectCodeConfig.InjectCodeConfigFilePath);
        File.WriteAllText(this.InjectCodeConfig.InjectCodeConfigFilePath, injectCodeConfigurationFileData, Encoding.ASCII);
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
        File.Delete(this.InjectCodeConfig.InjectCodeConfigFilePath);
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
                                        Plugin.Main.InjectCode.DataTypes.General.PATTERN_DIR_TEMPLATE);

      if (!Directory.Exists(templateDir))
      {
        return;
      }

      string[] patternFiles = Directory.GetFiles(templateDir, Plugin.Main.InjectCode.DataTypes.General.PATTERN_FILE_PATTERN);

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
    /// <param name="InjectCodeRecords"></param>
    /// <returns></returns>
    public TemplatePluginData OnGetTemplateData(BindingList<InjectCodeRecord> InjectCodeRecords)
    {
      TemplatePluginData templateData = new TemplatePluginData();
      List<InjectCodeRecord> genericObjectList = new List<InjectCodeRecord>();
      foreach (InjectCodeRecord tmpRecord in InjectCodeRecords)
      {
        genericObjectList.Add(new InjectCodeRecord(tmpRecord.RequestedScheme, tmpRecord.RequestedHost, tmpRecord.RequestedPath, tmpRecord.InjectionCodeFile, tmpRecord.Tag, tmpRecord.Position));
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
      MemoryStream stream = new MemoryStream();
      stream.Write(templateData.PluginConfigurationItems, 0, templateData.PluginConfigurationItems.Length);
      stream.Seek(0, SeekOrigin.Begin);

      BinaryFormatter formatter = new BinaryFormatter();
      poisoningRecords = (List<InjectCodeRecord>)formatter.Deserialize(stream);

      return poisoningRecords;
    }

    #endregion

  }
}
