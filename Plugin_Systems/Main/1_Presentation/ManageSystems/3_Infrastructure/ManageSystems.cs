namespace Minary.Plugin.Main.Systems.ManageSystems.Infrastructure
{
  using Minary.Plugin.Main.Systems.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Runtime.Serialization.Formatters.Binary;
  using System.Text.RegularExpressions;


  public class ManageSystems
  {

    #region MEMBERS
    
    private PluginProperties pluginProperties;

    #endregion


    #region PROPERTIES

    public string SystemPatternFile { get; private set; }

    #endregion


    #region PUBLIC METHODS

    /// <summary>
    /// Initializes a new instance of the <see cref="ManageSystems"/> class.
    ///
    /// </summary>
    public ManageSystems(PluginProperties pluginProperties)
    {
      this.pluginProperties = pluginProperties;
      this.SystemPatternFile = Path.Combine(this.pluginProperties.PluginBaseDir, "Plugin_SystemsOS_Patterns.xml");
    }


    /// <summary>
    ///
    /// </summary>
    public void SaveSystemPatterns(List<SystemPattern> systemPatterns)
    {
      if (systemPatterns.Count <= 0)
      {
        return;
      }

      //XmlSerializer serializer;
      //FileStream fileStream = null;

      //try
      //{
      //  string patternFilePath = Path.GetDirectoryName(SystemPatternFile);
      //  if (!Directory.Exists(patternFilePath))
      //  {
      //    Directory.CreateDirectory(patternFilePath);
      //  }

      //  serializer = new XmlSerializer(systemPatterns.GetType());
      //  fileStream = new FileStream(SystemPatternFile, FileMode.Create);
      //  serializer.Serialize(fileStream, systemPatterns);
      //}
      //catch (Exception)
      //{
      //}
      //finally
      //{
      //  if (fileStream != null)
      //  {
      //    fileStream.Close();
      //  }
      //}
    }




    /// <summary>
    ///
    /// </summary>
    /// <param name="record"></param>
    public void RemoveTemplate(SystemPattern record)
    {
      if (File.Exists(record.PatternFileFullPath))
      {
        File.Delete(record.PatternFileFullPath);
      }
    }


    public byte[] OnGetTemplateData(List<SystemPattern> systemPatternRecords)
    {
      byte[] templateDataBytes;

      // Serialize the pattern data
      MemoryStream stream = new MemoryStream();
      BinaryFormatter formatter = new BinaryFormatter();
      formatter.Serialize(stream, systemPatternRecords);
      stream.Seek(0, SeekOrigin.Begin);
      templateDataBytes = stream.ToArray();

      return templateDataBytes;
    }


    public List<SystemPattern> OnLoadTemplateData(TemplatePluginData templateData)
    {
      List<SystemPattern> systemPatternRecords;

      // Deserialize the pattern data
      MemoryStream stream = new MemoryStream();
      stream.Write(templateData.PluginDataSearchPatternItems, 0, templateData.PluginDataSearchPatternItems.Length);
      stream.Seek(0, SeekOrigin.Begin);

      BinaryFormatter formatter = new BinaryFormatter();
      systemPatternRecords = (List<SystemPattern>)formatter.Deserialize(stream);

      return systemPatternRecords;
    }


    /// <summary>
    ///
    /// </summary>
    public List<SystemPattern> ReadSystemPatterns()
    {
      string remotePatternFilesPath = Path.Combine(this.pluginProperties.PluginBaseDir, this.pluginProperties.PatternSubDir);
      List<SystemPattern> allSystemPatternRecords = new List<SystemPattern>();
      List<SystemPattern> remoteSystemPatternRecords;
      List<SystemPattern> localSystemPatternRecords;
      List<SystemPattern> templateSystemPatternRecords;

      // parse all remote pattern files
      string repositoryLocalFullpath = Path.Combine(
                                                    this.pluginProperties.ApplicationBaseDir,
                                                    this.pluginProperties.PluginBaseDir,
                                                    this.pluginProperties.PatternSubDir,
                                                    Plugin.Main.Systems.DataTypes.General.PATTERN_DIR_LOCAL);
      localSystemPatternRecords = this.ParsePatternFiles(repositoryLocalFullpath, Plugin.Main.Systems.DataTypes.General.PATTERN_FILE_PATTERN, "Local");

      // Parse all local pattern files
      string repositoryRemoteFullpath = Path.Combine(
                                                     this.pluginProperties.ApplicationBaseDir,
                                                     this.pluginProperties.PluginBaseDir,
                                                     this.pluginProperties.PatternSubDir,
                                                     Plugin.Main.Systems.DataTypes.General.PATTERN_DIR_REMOTE);
      remoteSystemPatternRecords = this.ParsePatternFiles(repositoryRemoteFullpath, Plugin.Main.Systems.DataTypes.General.PATTERN_FILE_PATTERN, "Remote");

      // Parse all template pattern files
      string repositoryTemplateFullpath = Path.Combine(
                                                       this.pluginProperties.ApplicationBaseDir,
                                                       this.pluginProperties.PluginBaseDir,
                                                       this.pluginProperties.PatternSubDir,
                                                       Plugin.Main.Systems.DataTypes.General.PATTERN_DIR_TEMPLATE);
      templateSystemPatternRecords = this.ParsePatternFiles(repositoryTemplateFullpath, Plugin.Main.Systems.DataTypes.General.PATTERN_FILE_PATTERN, "Template");

      // 3. Join local and remote pattern data records.
      localSystemPatternRecords.ForEach(elem => allSystemPatternRecords.Add(elem));
      remoteSystemPatternRecords.ForEach(elem => allSystemPatternRecords.Add(elem));
      templateSystemPatternRecords.ForEach(elem => allSystemPatternRecords.Add(elem));

      return allSystemPatternRecords;
    }




    /// <summary>
    ///
    /// </summary>
    public void SaveTemplate(SystemPattern record)
    {
      FileStream fileStream = null;
      string templateDir = Path.Combine(
                                        this.pluginProperties.ApplicationBaseDir,
                                        this.pluginProperties.PluginBaseDir,
                                        this.pluginProperties.PatternSubDir,
                                        Plugin.Main.Systems.DataTypes.General.PATTERN_DIR_TEMPLATE);
      string fileName = Regex.Replace(record.SystemName, @"[^\d\w\-]", "_", RegexOptions.IgnoreCase);
      fileName += Plugin.Main.Systems.DataTypes.General.PATTERN_FILE_EXTENSION;
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

    #endregion


    #region PRIVATE

    /// <summary>
    ///
    /// </summary>
    /// <param name="patternFilePath"></param>
    /// <param name="filePattern"></param>
    /// <returns></returns>
    private List<SystemPattern> ParsePatternFiles(string patternFilePath, string filePattern, string source)
    {
      FileStream fileStream = null;
      SystemPattern tmpRecord = new SystemPattern();
      List<SystemPattern> foundPatternFiles = new List<SystemPattern>();
      string[] remotePatternFiles;
      BinaryFormatter formatter = new BinaryFormatter();

      if (!Directory.Exists(patternFilePath))
      {
        return foundPatternFiles;
      }

      remotePatternFiles = Directory.GetFiles(patternFilePath, filePattern);

      foreach (string tmpPatternFile in remotePatternFiles)
      {
        try
        {
          fileStream = new FileStream(tmpPatternFile, FileMode.Open);
          tmpRecord = (SystemPattern)formatter.Deserialize(fileStream);

          tmpRecord.Source = source;
          tmpRecord.PatternFileFullPath = tmpPatternFile;

          foundPatternFiles.Add(tmpRecord);
        }
        catch (Exception ex)
        {
          // throw new Exception("The data pattern file could not be found");
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