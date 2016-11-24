namespace Minary.Plugin.Main.Systems.ManageSystems.Task
{
  using Minary.Plugin.Main.Systems.DataTypes;
  using MinaryLib;
  using System;
  using System.Text.RegularExpressions;

  public class CustomPatternAdd
  {

    #region MEMBERS

    private static Plugin.Main.Systems.ManageSystems.Task.CustomPatternAdd instance;
    private Plugin.Main.Systems.ManageSystems.Infrastructure.CustomPatternAdd infrastructureLayer;
    private PluginProperties pluginProperties;

    #endregion


    #region PUBLIC

    /// <summary>
    ///
    /// </summary>
    /// <param name="pluginProperties"></param>
    /// <returns></returns>
    public static CustomPatternAdd GetInstance(PluginProperties pluginProperties)
    {
      return instance ?? (instance = new CustomPatternAdd(pluginProperties));
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="record"></param>
    public void AddCustomPatternRecord(SystemPattern record)
    {
      if (string.IsNullOrEmpty(record.SystemName))
      {
        throw new Exception("You didn't define a system name");
      }
      else if (string.IsNullOrEmpty(record.SystemPatternstring))
      {
        throw new Exception("You didn't define a system pattern");
      }

      // Check system pattern regex
      try
      {
        Regex.Match(string.Empty, record.SystemPatternstring);
      }
      catch (ArgumentException)
      {
        throw new Exception("System pattern is invalid");
      }

      this.infrastructureLayer.SaveNewAccountPatternRecord(record);
    }

    #endregion


    #region PRIVATE

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomPatternAdd"/> class.
    ///
    /// </summary>
    /// <param name="pluginProperties"></param>
    private CustomPatternAdd(PluginProperties pluginProperties)
    {
      this.pluginProperties = pluginProperties;
      this.infrastructureLayer = Plugin.Main.Systems.ManageSystems.Infrastructure.CustomPatternAdd.GetInstance(pluginProperties);
    }

    #endregion


    #region EVENTS


    #endregion

  }
}