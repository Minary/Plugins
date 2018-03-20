namespace Minary.Plugin.Main.HttpsRequest.Infrastructure
{
  using Minary.Plugin.Main.HttpsRequest.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.IO;


  public class HttpsRequest
  {

    #region MEMBERS

    private IPlugin plugin;

    #endregion


    #region PUBLIC

    public HttpsRequest(IPlugin plugin)
    {
      this.plugin = plugin;
    }

    #endregion


    #region EVENTS

    public void OnInit()
    {
      var pluginBasedirectories = new List<string>();

      pluginBasedirectories.Add(Path.Combine(
                                             this.plugin.Config.ApplicationBaseDir,
                                             this.plugin.Config.PluginBaseDir,
                                             this.plugin.Config.PatternSubDir,
                                             General.PATTERN_DIR_REMOTE));

      pluginBasedirectories.Add(Path.Combine(
                                             this.plugin.Config.ApplicationBaseDir,
                                             this.plugin.Config.PluginBaseDir,
                                             this.plugin.Config.PatternSubDir,
                                             General.PATTERN_DIR_LOCAL));

      pluginBasedirectories.Add(Path.Combine(
                                             this.plugin.Config.ApplicationBaseDir,
                                             this.plugin.Config.PluginBaseDir,
                                             this.plugin.Config.PatternSubDir,
                                             General.PATTERN_DIR_TEMPLATE));

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
          this.plugin.Config.HostApplication.LogMessage($"{this.plugin.Config.PluginName}: {ex.Message}");
        }
      });

      // Clean up template directory
      this.CleanUpTemplateDir();
    }


    public void OnReset()
    {
      this.CleanUpTemplateDir();
    }

    #endregion


    #region PRIVATE
    
    private void CleanUpTemplateDir()
    {
      var templateDir = Path.Combine(
                                     this.plugin.Config.ApplicationBaseDir,
                                     this.plugin.Config.PluginBaseDir,
                                     this.plugin.Config.PatternSubDir,
                                     Plugin.Main.HttpsRequest.DataTypes.General.PATTERN_DIR_TEMPLATE);

      if (!Directory.Exists(templateDir))
      {
        return;
      }

      string[] patternFiles = Directory.GetFiles(templateDir, Plugin.Main.HttpsRequest.DataTypes.General.PATTERN_FILE_PATTERN);
      foreach (var tmpPatternFile in patternFiles)
      {
        try
        {
          File.Delete(tmpPatternFile);
        }
        catch (Exception ex)
        {
          this.plugin.Config.HostApplication.LogMessage($"{this.plugin.Config.PluginName}: {ex.Message}");
        }
      }
    }

    #endregion

  }
}
