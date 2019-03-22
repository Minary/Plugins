namespace Minary.Plugin.Main.RequestRedirect.Infrastructure
{
  using Minary.Plugin.Main.RequestRedirect.DataTypes;
  using MinaryLib.DataTypes;
  using MinaryLib.Exceptions;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.IO;
  using System.Text;
  using System.Runtime.Serialization.Formatters.Binary;


  public class RequestRedirect
  {

    #region MEMBERS
    
    private IPlugin plugin;
    private RequestRedirectConfig requestRedirectConfig;

    #endregion


    #region PUBLIC

    /// <summary>
    /// 
    /// </summary>
    /// <param name="plugin"></param>
    /// <param name="requestRedirectdConfig"></param>
    public RequestRedirect(IPlugin plugin, RequestRedirectConfig requestRedirectdConfig)
    {
      this.plugin = plugin;
      this.requestRedirectConfig = requestRedirectdConfig;

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
        this.plugin.Config.HostApplication.LogMessage($"{ this.plugin.Config.PluginName}.OnReset(EXCEPTION) : {ex.Message}");
      }
    }


    public void OnWriteConfiguration(List<RequestRedirectRecord> recordList)
    {
this.plugin.Config.HostApplication.LogMessage($"{this.plugin.Config.PluginName}.Infrastructure.OnWriteConfiguration(0): recordList==NULL {recordList == null}");
this.plugin.Config.HostApplication.LogMessage($"{this.plugin.Config.PluginName}.Infrastructure.OnWriteConfiguration(1): recordList==NULL {recordList == null || recordList.Count <= 0}");
      if (recordList == null || 
          recordList.Count <= 0)
      {
        throw new MinaryWarningException("No request redirection rules defined");
      }

      // Write configuration file
      try
      {
        if (!File.Exists(this.requestRedirectConfig.RequestRedirectConfigFilePath))
        {
          File.Delete(this.requestRedirectConfig.RequestRedirectConfigFilePath);
        }
      }
      catch (Exception ex)
      {
        this.plugin.Config.HostApplication.LogMessage($"{this.plugin.Config.PluginName}.Infrastructure.OnWriteConfiguration(3) : {ex.Message}");
      }
      
      var requestRedirectConfigurationFileData = string.Empty;
      foreach (RequestRedirectRecord tmpRecord in recordList)
      {
        requestRedirectConfigurationFileData += $"{tmpRecord.RedirectType}||{tmpRecord.RedirectDescription}||{tmpRecord.RequestedHostRegex}||{tmpRecord.RequestedPathRegex}||{tmpRecord.ReplacementResource}\r\n";
      }

      requestRedirectConfigurationFileData = requestRedirectConfigurationFileData.Trim();

      try
      {
        this.plugin.Config.HostApplication.LogMessage($"{this.plugin.Config.PluginName}.Infrastructure.OnWriteConfiguration(4): Writing to config file {this.requestRedirectConfig.RequestRedirectConfigFilePath}");
        File.WriteAllText(this.requestRedirectConfig.RequestRedirectConfigFilePath, requestRedirectConfigurationFileData, Encoding.ASCII);
      }
      catch (Exception ex)
      {
        throw new Exception($"Error occurred while writing Redirect Request configuration data: {ex.Message}");
      }
    }


    public void OnRemoveConfiguration()
    {
      // Remove plugin configuration file
      try
      {
        File.Delete(this.requestRedirectConfig.RequestRedirectConfigFilePath);
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
                                     Plugin.Main.RequestRedirect.DataTypes.General.PATTERN_DIR_TEMPLATE);

      if (!Directory.Exists(templateDir))
      {
        return;
      }

      string[] patternFiles = Directory.GetFiles(templateDir, Plugin.Main.RequestRedirect.DataTypes.General.PATTERN_FILE_PATTERN);

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
    public TemplatePluginData OnGetTemplateData(BindingList<RequestRedirectRecord> requestRedirectRecords)
    {
      var templateData = new TemplatePluginData();
      var genericObjectList = new List<RequestRedirectRecord>();

      // Create new record list
      foreach (RequestRedirectRecord tmpRecord in requestRedirectRecords)
      {
        var tmpRecord2 = new RequestRedirectRecord()
        {
          RedirectType = tmpRecord.RedirectType,
          RedirectDescription = tmpRecord.RedirectDescription,
          RequestedHostRegex = tmpRecord.RequestedHostRegex,
          RequestedPathRegex = tmpRecord.RequestedPathRegex,
          ReplacementResource = tmpRecord.ReplacementResource
        };

        genericObjectList.Add(tmpRecord2);
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
    public List<RequestRedirectRecord> OnLoadTemplateData(TemplatePluginData templateData)
    {
      List<RequestRedirectRecord> poisoningRecords = null;

      if (templateData == null)
      {
        return null;
      }

      // Deserialize plugin data
      var stream = new MemoryStream();
      stream.Write(templateData.PluginConfigurationItems, 0, templateData.PluginConfigurationItems.Length);
      stream.Seek(0, SeekOrigin.Begin);

      var formatter = new BinaryFormatter();
      poisoningRecords = (List<RequestRedirectRecord>)formatter.Deserialize(stream);

      return poisoningRecords;
    }

    #endregion

  }
}
