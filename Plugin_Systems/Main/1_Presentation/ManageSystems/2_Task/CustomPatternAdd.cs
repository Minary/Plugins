﻿namespace Minary.Plugin.Main.Systems.ManageSystems.Task
{
  using Minary.Plugin.Main.Systems.DataTypes;
  using MinaryLib;
  using System;
  using System.Text.RegularExpressions;


  public class CustomPatternAdd
  {

    #region MEMBERS
    
    private Infrastructure.CustomPatternAdd infrastructureLayer;
    private PluginProperties pluginProperties;

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomPatternAdd"/> class.
    ///
    /// </summary>
    /// <param name="pluginProperties"></param>
    public CustomPatternAdd(PluginProperties pluginProperties)
    {
      this.pluginProperties = pluginProperties;
      this.infrastructureLayer = new Infrastructure.CustomPatternAdd(pluginProperties);
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


    #region EVENTS


    #endregion

  }
}