namespace Minary.Plugin.Main.Session.ManageSessions.Infrastructure
{
  using Minary.Plugin.Main.Session.ManageSessions.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Text.RegularExpressions;
  using System.Runtime.Serialization.Formatters.Binary;


  public class ManageSessions
  {

    #region MEMBERS
    
    private PluginProperties pluginProperties;

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="ManageSessions"/> class.
    ///
    /// </summary>
    public ManageSessions(PluginProperties pluginProperties)
    {
      this.pluginProperties = pluginProperties;
    }


    /// <summary>
    ///
    /// </summary>
    public List<SessionPattern> ReadSessionPatterns()
    {
      string remotePatternFilesPath = Path.Combine(this.pluginProperties.PluginBaseDir, this.pluginProperties.PatternSubDir);
      List<SessionPattern> allSessionPatternRecords = new List<SessionPattern>();
      List<SessionPattern> remoteSessionPatternRecords;
      List<SessionPattern> localSessionPatternRecords;
      List<SessionPattern> templateSessionPatternRecords;

      // parse all remote pattern files
      string repositoryLocalFullpath = Path.Combine(
                                                    this.pluginProperties.ApplicationBaseDir,
                                                    this.pluginProperties.PluginBaseDir,
                                                    this.pluginProperties.PatternSubDir,
                                                    Plugin.Main.Session.Config.General.PATTERN_DIR_LOCAL);
      localSessionPatternRecords = ParsePatternFiles(repositoryLocalFullpath, Plugin.Main.Session.Config.General.PATTERN_FILE_PATTERN, "Local");
      
      // Parse all local pattern files
      string repositoryRemoteFullpath = Path.Combine(
                                                     this.pluginProperties.ApplicationBaseDir,
                                                     this.pluginProperties.PluginBaseDir,
                                                     this.pluginProperties.PatternSubDir,
                                                     Plugin.Main.Session.Config.General.PATTERN_DIR_REMOTE);
      remoteSessionPatternRecords = ParsePatternFiles(repositoryRemoteFullpath, Plugin.Main.Session.Config.General.PATTERN_FILE_PATTERN, "Remote");

      // Parse all template pattern files
      string repositoryTemplateFullpath = Path.Combine(
                                                       this.pluginProperties.ApplicationBaseDir,
                                                       this.pluginProperties.PluginBaseDir,
                                                       this.pluginProperties.PatternSubDir,
                                                       Plugin.Main.Session.Config.General.PATTERN_DIR_TEMPLATE);
      templateSessionPatternRecords = ParsePatternFiles(repositoryTemplateFullpath, Plugin.Main.Session.Config.General.PATTERN_FILE_PATTERN, "Template");
      
      //// 3. Join local and remote pattern newData records.
      localSessionPatternRecords.ForEach(elem => allSessionPatternRecords.Add(elem));
      remoteSessionPatternRecords.ForEach(elem => allSessionPatternRecords.Add(elem));
      templateSessionPatternRecords.ForEach(elem => allSessionPatternRecords.Add(elem));

      return allSessionPatternRecords;
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="record"></param>
    public void RemoveTemplate(SessionPattern record)
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
    public void SaveTemplate(SessionPattern record)
    {
      FileStream fileStream = null;
      string templateDir = Path.Combine(
                                        this.pluginProperties.ApplicationBaseDir,
                                        this.pluginProperties.PluginBaseDir,
                                        this.pluginProperties.PatternSubDir,
                                        Plugin.Main.Session.Config.General.PATTERN_DIR_TEMPLATE);
      string fileName = Regex.Replace(record.CompanyName, @"[^\d\w\-]", "_", RegexOptions.IgnoreCase);
      fileName += Plugin.Main.Session.Config.General.PATTERN_FILE_EXTENSION;
      string newPatternFileFullPath = Path.Combine(templateDir, fileName);

      try
      {
        BinaryFormatter formatter = new BinaryFormatter();
        fileStream = new FileStream(newPatternFileFullPath, FileMode.Create);
        formatter.Serialize(fileStream, record);
      }
      catch (Exception)
      {
      }
      finally
      {
        if (fileStream != null)
        {
          fileStream.Close();
        }
      }
    }


    public byte[] OnGetTemplateData(List<SessionPattern> sessionPatternRecords)
    {
      byte[] templateDataBytes;

      // Serialize the pattern data
      MemoryStream stream = new MemoryStream();
      BinaryFormatter formatter = new BinaryFormatter();
      formatter.Serialize(stream, sessionPatternRecords);
      stream.Seek(0, SeekOrigin.Begin);
      templateDataBytes = stream.ToArray();

      return templateDataBytes;
    }


    public List<SessionPattern> OnLoadTemplateData(TemplatePluginData templateData)
    {
      List<SessionPattern> sessionPatternRecords;

      // Deserialize the pattern data
      MemoryStream stream = new MemoryStream();
      stream.Write(templateData.PluginDataSearchPatternItems, 0, templateData.PluginDataSearchPatternItems.Length);
      stream.Seek(0, SeekOrigin.Begin);

      BinaryFormatter formatter = new BinaryFormatter();
      sessionPatternRecords = (List<SessionPattern>)formatter.Deserialize(stream);

      return sessionPatternRecords;
    }

    #endregion


    #region PRIVATE

    /// <summary>
    ///
    /// </summary>
    /// <param name="path"></param>
    /// <param name="filePattern"></param>
    /// <returns></returns>
    private List<SessionPattern> ParsePatternFiles(string path, string filePattern, string source)
    {
      FileStream fileStream = null;
      SessionPattern tmpRecord = new SessionPattern();
      List<SessionPattern> foundPatternFiles = new List<SessionPattern>();
      string[] remotePatternFiles;
      BinaryFormatter formatter = new BinaryFormatter();

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
          tmpRecord = (SessionPattern)formatter.Deserialize(fileStream);
          tmpRecord.Source = source;
          tmpRecord.PatternFileFullPath = tmpPatternFile;
          foundPatternFiles.Add(tmpRecord);
        }
        catch (Exception)
        {
          // hrow new Exception("The newData pattern file could not be found");
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