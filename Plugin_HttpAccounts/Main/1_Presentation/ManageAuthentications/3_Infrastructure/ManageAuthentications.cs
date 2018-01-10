namespace Minary.Plugin.Main.HttpAccounts.ManageAuthentications.Infrastructure
{
  using Minary.Plugin.Main.HttpAccounts.ManageAuthentications.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Runtime.Serialization.Formatters.Binary;
  using System.Text.RegularExpressions;

  public class ManageAuthentications
  {

    #region MEMBERS
    
    private PluginProperties pluginProperties;

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="ManageAuthentications"/> class.
    ///
    /// </summary>
    public ManageAuthentications(PluginProperties pluginProperties)
    {
      this.pluginProperties = pluginProperties;
    }


    /// <summary>
    ///
    /// </summary>
    public List<HttpAccountPattern> ReadAuthenticationPatterns()
    {
      string remotePatternFilesPath = Path.Combine(this.pluginProperties.PluginBaseDir, this.pluginProperties.PatternSubDir);
      List<HttpAccountPattern> allAccountPatternRecords = new List<HttpAccountPattern>();
      List<HttpAccountPattern> remoteAccountPatternRecords;
      List<HttpAccountPattern> localAccountPatternRecords;
      List<HttpAccountPattern> templateAccountPatternRecords;

      // parse all remote pattern files
      string repositoryLocalFullpath = Path.Combine(
                                                    this.pluginProperties.ApplicationBaseDir,
                                                    this.pluginProperties.PluginBaseDir,
                                                    this.pluginProperties.PatternSubDir,
                                                    Plugin.Main.HttpAccounts.DataTypes.General.PATTERN_DIR_LOCAL);
      localAccountPatternRecords = this.ParsePatternFiles(repositoryLocalFullpath, Plugin.Main.HttpAccounts.DataTypes.General.PATTERN_FILE_PATTERN, "Local");


      // Parse all local pattern files
      string repositoryRemoteFullpath = Path.Combine(
                                                     this.pluginProperties.ApplicationBaseDir,
                                                     this.pluginProperties.PluginBaseDir,
                                                     this.pluginProperties.PatternSubDir,
                                                     Plugin.Main.HttpAccounts.DataTypes.General.PATTERN_DIR_REMOTE);
      remoteAccountPatternRecords = this.ParsePatternFiles(repositoryRemoteFullpath, Plugin.Main.HttpAccounts.DataTypes.General.PATTERN_FILE_PATTERN, "Remote");

      // Parse all template pattern files
      string repositoryTemplateFullpath = Path.Combine(
                                                       this.pluginProperties.ApplicationBaseDir,
                                                       this.pluginProperties.PluginBaseDir,
                                                       this.pluginProperties.PatternSubDir,
                                                       Plugin.Main.HttpAccounts.DataTypes.General.PATTERN_DIR_TEMPLATE);
      templateAccountPatternRecords = this.ParsePatternFiles(repositoryTemplateFullpath, Plugin.Main.HttpAccounts.DataTypes.General.PATTERN_FILE_PATTERN, "Template");
      
      //// 3. Join local and remote pattern data records.
      //// lRemoteAccountPatternRecord.ForEach(elem => lAccountPatternRecords.Add(elem));
      localAccountPatternRecords.ForEach(elem => allAccountPatternRecords.Add(elem));
      remoteAccountPatternRecords.ForEach(elem => allAccountPatternRecords.Add(elem));
      templateAccountPatternRecords.ForEach(elem => allAccountPatternRecords.Add(elem));

      return allAccountPatternRecords;
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="record"></param>
    public void RemoveTemplate(HttpAccountPattern record)
    {
      if (File.Exists(record.PatternFileFullPath))
      {
        File.Delete(record.PatternFileFullPath);
      }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="record"></param>
    public void SaveTemplate(HttpAccountPattern record)
    {
      FileStream fileStream = null;
      string templateDir = Path.Combine(
                                        this.pluginProperties.ApplicationBaseDir,
                                        this.pluginProperties.PluginBaseDir,
                                        this.pluginProperties.PatternSubDir,
                                        Plugin.Main.HttpAccounts.DataTypes.General.PATTERN_DIR_TEMPLATE);
      var fileName = Regex.Replace(record.Company, @"[^\d\w\-]", "_", RegexOptions.IgnoreCase);
      fileName += Plugin.Main.HttpAccounts.DataTypes.General.PATTERN_FILE_EXTENSION;
      var newPatternFileFullPath = Path.Combine(templateDir, fileName);

      try
      {
        var formatter = new BinaryFormatter();
        fileStream = new FileStream(newPatternFileFullPath, FileMode.Create);
        formatter.Serialize(fileStream, record);
      }
      finally
      {
        if (fileStream != null)
        {
          fileStream.Close();
        }
      }
    }


    public byte[] OnGetTemplateData(List<HttpAccountPattern> httpAccountPatternRecords)
    {
      byte[] templateDataBytes;

      // Serialize the pattern data
      var stream = new MemoryStream();
      var formatter = new BinaryFormatter();
      formatter.Serialize(stream, httpAccountPatternRecords);
      stream.Seek(0, SeekOrigin.Begin);
      templateDataBytes = stream.ToArray();

      return templateDataBytes;
    }


    public List<HttpAccountPattern> OnLoadTemplateData(TemplatePluginData templateData)
    {
      List<HttpAccountPattern> httpAccountPatternRecords;

      // Deserialize the pattern data
      MemoryStream stream = new MemoryStream();
      stream.Write(templateData.PluginDataSearchPatternItems, 0, templateData.PluginDataSearchPatternItems.Length);
      stream.Seek(0, SeekOrigin.Begin);

      BinaryFormatter formatter = new BinaryFormatter();
      httpAccountPatternRecords = (List<HttpAccountPattern>)formatter.Deserialize(stream);

      return httpAccountPatternRecords;
    }

    #endregion


    #region PRIVATE

    /// <summary>
    ///
    /// </summary>
    /// <param name="path"></param>
    /// <param name="filePattern"></param>
    /// <returns></returns>
    private List<HttpAccountPattern> ParsePatternFiles(string path, string filePattern, string source)
    {
      FileStream fileStream = null;
      string[] remotePatternFiles;
      var tmpRecord = new HttpAccountPattern();
      var foundPatternFiles = new List<HttpAccountPattern>();
      var formatter = new BinaryFormatter();

      if (!Directory.Exists(path))
      {
        return foundPatternFiles;
      }

      remotePatternFiles = Directory.GetFiles(path, filePattern);

      foreach (string tmpPatternFile in remotePatternFiles)
      {
        try
        {
          fileStream = new FileStream(tmpPatternFile, FileMode.Open);
          tmpRecord = (HttpAccountPattern)formatter.Deserialize(fileStream);
          tmpRecord.Source = source;
          tmpRecord.PatternFileFullPath = tmpPatternFile;
          foundPatternFiles.Add(tmpRecord);
        }
        catch (Exception)
        {
          // hrow new Exception("The data pattern file could not be found");
          // TODO: Exceptions must be logged in the LogConsole form.  The LocConsole is not
          //       accessible from here
        }
        finally
        {
          if (fileStream != null)
          {
            fileStream.Close();
          }
        }
      }

      return foundPatternFiles;
    }

    #endregion

  }
}
