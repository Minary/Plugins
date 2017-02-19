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

    private static RequestRedirect instance;
    private IPlugin plugin;
    private RequestRedirectConfig requestRedirectConfig;

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="InjectPayload"/> class.
    ///
    /// </summary>
    /// <param name="plugin"></param>
    private RequestRedirect(IPlugin plugin, RequestRedirectConfig requestRedirectdConfig)
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

    /// <summary>
    ///
    /// </summary>
    /// <param name="plugin"></param>
    /// <param name="sslStripConfig"></param>
    /// <returns></returns>
    public static RequestRedirect GetInstance(IPlugin plugin, RequestRedirectConfig injectPayloadConfig)
    {
      return instance ?? (instance = new RequestRedirect(plugin, injectPayloadConfig));
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
    public void OnStart(List<RequestRedirectRecord> recordList)
    {
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
                                        Plugin.Main.RequestRedirect.DataTypes.General.PATTERN_DIR_TEMPLATE);

      if (!Directory.Exists(templateDir))
      {
        return;
      }

      string[] patternFiles = Directory.GetFiles(templateDir, Plugin.Main.RequestRedirect.DataTypes.General.PATTERN_FILE_PATTERN);

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
    public TemplatePluginData OnGetTemplateData(BindingList<RequestRedirectRecord> requestRedirectRecords)
    {
      TemplatePluginData templateData = new TemplatePluginData();
      List<RequestRedirectRecord> genericObjectList = new List<RequestRedirectRecord>();
      foreach (RequestRedirectRecord tmpRecord in requestRedirectRecords)
      {
        genericObjectList.Add(new RequestRedirectRecord(tmpRecord.RequestedScheme, tmpRecord.RequestedHost, tmpRecord.RequestedPath, tmpRecord.ReplacementResource));
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
    public List<RequestRedirectRecord> OnLoadTemplateData(TemplatePluginData templateData)
    {
      List<RequestRedirectRecord> poisoningRecords = null;

      if (templateData == null)
      {
        return null;
      }

      // Deserialize plugin data
      MemoryStream stream = new MemoryStream();
      stream.Write(templateData.PluginConfigurationItems, 0, templateData.PluginConfigurationItems.Length);
      stream.Seek(0, SeekOrigin.Begin);

      BinaryFormatter formatter = new BinaryFormatter();
      poisoningRecords = (List<RequestRedirectRecord>)formatter.Deserialize(stream);

      return poisoningRecords;
    }

    #endregion

  }
}
