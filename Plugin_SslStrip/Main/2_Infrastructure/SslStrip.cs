namespace Minary.Plugin.Main.SslStrip.Infrastructure
{
  using Minary.Plugin.Main.SslStrip.DataTypes;
  using MinaryLib.DataTypes;
  using MinaryLib.Exceptions;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.IO;
  using System.Text;
  using System.Runtime.Serialization.Formatters.Binary;


  public class SslStrip
  {

    #region MEMBERS
    
    private IPlugin plugin;
    private SslStripConfig sslStripConfig;

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="SslStrip"/> class.
    ///
    /// </summary>
    /// <param name="plugin"></param>
    public SslStrip(IPlugin plugin, SslStripConfig sslStripConfig)
    {
      this.plugin = plugin;
      this.sslStripConfig = sslStripConfig;

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
    public void OnStart(List<SslStripRecord> recordList)
    {
      if (recordList == null || recordList.Count <= 0)
      {
        throw new MinaryWarningException("No ssl stripping rules defined");
      }

      // Write configuration file
      try
      {
        if (File.Exists(this.sslStripConfig.SslStripConfigFilePath))
        {
          File.Delete(this.sslStripConfig.SslStripConfigFilePath);
        }
      }
      catch (Exception ex)
      {
        this.plugin.Config.HostApplication.LogMessage("{0}.Infrastructure.OnStart(0): {1}", this.plugin.Config.PluginName, ex.Message);
      }

      string sslStripConfigurationFileData = string.Empty;
      foreach (SslStripRecord tmpRecord in recordList)
      {
        sslStripConfigurationFileData += string.Format("{0}:{1}\r\n", tmpRecord.HostName, tmpRecord.ContentType);
      }

      sslStripConfigurationFileData = sslStripConfigurationFileData.Trim();

      try
      {
        this.plugin.Config.HostApplication.LogMessage("{0}.Infrastructure.OnStart(0): Writing to config file {1}", this.plugin.Config.PluginName, this.sslStripConfig.SslStripConfigFilePath);
        File.WriteAllText(this.sslStripConfig.SslStripConfigFilePath, sslStripConfigurationFileData, Encoding.ASCII);
      }
      catch (Exception ex)
      {
        throw new Exception(string.Format("Errorr occurred while writing SslStrip configuration data: {0}", ex.Message));
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
                                        Plugin.Main.SslStrip.DataTypes.General.PATTERN_DIR_TEMPLATE);

      if (!Directory.Exists(templateDir))
      {
        return;
      }

      string[] patternFiles = Directory.GetFiles(templateDir, Plugin.Main.SslStrip.DataTypes.General.PATTERN_FILE_PATTERN);

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


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSslStripExited(object sender, System.EventArgs e)
    {
      if (this.sslStripConfig.OnSslStripExit != null)
      {
        this.sslStripConfig.OnSslStripExit();
      }
    }

    #endregion


    #region EVENTS

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
                             Plugin.Main.SslStrip.DataTypes.General.PATTERN_DIR_REMOTE));

      pluginBasedirectories.Add(Path.Combine(
                             this.plugin.Config.ApplicationBaseDir,
                             this.plugin.Config.PluginBaseDir,
                             this.plugin.Config.PatternSubDir,
                             Plugin.Main.SslStrip.DataTypes.General.PATTERN_DIR_LOCAL));

      pluginBasedirectories.Add(Path.Combine(
                             this.plugin.Config.ApplicationBaseDir,
                             this.plugin.Config.PluginBaseDir,
                             this.plugin.Config.PatternSubDir,
                             Plugin.Main.SslStrip.DataTypes.General.PATTERN_DIR_TEMPLATE));

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

    #endregion


    #region TEMPLATE

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public TemplatePluginData OnGetTemplateData(BindingList<SslStripRecord> sslStripRules)
    {
      TemplatePluginData templateData = new TemplatePluginData();
      List<SslStripRecord> genericObjectList = new List<SslStripRecord>();

      // Replace current configuration parameter with placeholder values
      foreach (SslStripRecord tmpRecord in sslStripRules)
      {
        genericObjectList.Add(new SslStripRecord(tmpRecord.HostName, tmpRecord.ContentType));
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
    public List<SslStripRecord> OnLoadTemplateData(TemplatePluginData pluginData)
    {
      List<SslStripRecord> poisoningRecords = null;

      if (pluginData == null)
      {
        return null;
      }

      // Deserialize plugin data
      MemoryStream stream = new MemoryStream();
      stream.Write(pluginData.PluginConfigurationItems, 0, pluginData.PluginConfigurationItems.Length);
      stream.Seek(0, SeekOrigin.Begin);

      BinaryFormatter formatter = new BinaryFormatter();
      poisoningRecords = (List<SslStripRecord>)formatter.Deserialize(stream);

      return poisoningRecords;
    }

    #endregion

  }
}