namespace Minary.Plugin.Main.HttpAccounts.Infrastructure
{
  using Minary.Plugin.Main.HttpAccounts.DataTypes;
  using MinaryLib.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.ComponentModel;
  using System.Collections.Generic;
  using System.IO;


  public class HttpAccounts
  {

    #region MEMBERS

    private static HttpAccounts instance;
    private IPlugin plugin;

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpAccounts"/> class.
    ///
    /// </summary>
    /// <param name="plugin"></param>
    private HttpAccounts(IPlugin plugin)
    {
      this.plugin = plugin;
    }


    /// <summary>
    /// Create single instance
    /// </summary>
    /// <param name="pWebServerConfig"></param>
    /// <returns></returns>
    public static HttpAccounts GetInstance(IPlugin plugin)
    {
      return instance ?? (instance = new HttpAccounts(plugin));
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
                               Plugin.Main.HttpAccounts.DataTypes.General.PATTERN_DIR_REMOTE));

        pluginBasedirectories.Add(Path.Combine(
                               this.plugin.Config.ApplicationBaseDir,
                               this.plugin.Config.PluginBaseDir,
                               this.plugin.Config.PatternSubDir,
                               Plugin.Main.HttpAccounts.DataTypes.General.PATTERN_DIR_LOCAL));

        pluginBasedirectories.Add(Path.Combine(
                               this.plugin.Config.ApplicationBaseDir,
                               this.plugin.Config.PluginBaseDir,
                               this.plugin.Config.PatternSubDir,
                               Plugin.Main.HttpAccounts.DataTypes.General.PATTERN_DIR_TEMPLATE));

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

      // Clean up template directory
      this.CleanUpTemplateDir();
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="pWebServerConfig"></param>
    public void OnReset()
    {
      this.CleanUpTemplateDir();
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
                                        Plugin.Main.HttpAccounts.DataTypes.General.PATTERN_DIR_TEMPLATE);

      if (!Directory.Exists(templateDir))
      {
        return;
      }

      string[] patternFiles = Directory.GetFiles(templateDir, Plugin.Main.HttpAccounts.DataTypes.General.PATTERN_FILE_PATTERN);

      foreach (string tmpFile in patternFiles)
      {
        try
        {
          File.Delete(tmpFile);
        }
        catch (Exception ex)
        {
          this.plugin.Config.HostApplication.LogMessage("{0} : {1}", this.plugin.Config.PluginName, ex.Message);
        }
      }
    }

    #endregion


    #region TEMPLATE

    public byte[] OnGetTemplateData(BindingList<AccountRecord> applicationPatternRecords)
    {
      return null;
    }


    public List<AccountRecord> OnLoadTemplateData(TemplatePluginData pluginData)
    {
      List<AccountRecord> applicatoinPatternRecords = new List<AccountRecord>();

      return applicatoinPatternRecords;
    }


    public void OnUnoadTemplateData()
    {
      this.CleanUpTemplateDir();
    }

    #endregion
  }
}