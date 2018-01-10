namespace Minary.Plugin.Main.HttpAccounts.ManageAuthentications.Task
{
  using Minary.Plugin.Main.HttpAccounts.ManageAuthentications.DataTypes;
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
    
    public CustomPatternAdd(PluginProperties pluginProperties)
    {
      this.pluginProperties = pluginProperties;
      this.infrastructureLayer = new Infrastructure.CustomPatternAdd(this.pluginProperties);
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="record"></param>
    public void AddCustomPatternRecord(HttpAccountPattern record)
    {
      if (record == null)
      {
        throw new Exception("Something is wrong with the account pattern record.");
      }
      else if (record.Company?.Length > 0 == false)
      {
        throw new Exception("You didn't define a company name");
      }
      else if (record.HostPattern?.Length > 0 == false)
      {
        throw new Exception("You didn't define a host pattern");
      }
      else if (record.WebPage?.Length > 0 == false)
      {
        throw new Exception("You didn't define a company web page");
      }
      else if (record.PathPattern?.Length > 0 == false)
      {
        throw new Exception("You didn't define a path pattern");
      }
      else if (record.Method?.Length > 0 == false)
      {
        throw new Exception("You didn't define a request method");
      }
      else if (record.DataPattern?.Length > 0 == false)
      {
        throw new Exception("You didn't define a data pattern");
      }

      try
      {
        Regex.Match(string.Empty, record.HostPattern);
      }
      catch (ArgumentException)
      {
        throw new Exception("Host pattern is invalid");
      }

      try
      {
        Regex.Match(string.Empty, record.PathPattern);
      }
      catch (ArgumentException)
      {
        throw new Exception("Path pattern is invalid");
      }

      try
      {
        Regex.Match(string.Empty, record.DataPattern);
      }
      catch (ArgumentException)
      {
        throw new Exception("Data pattern is invalid");
      }

      this.infrastructureLayer.SaveNewAccountPatternRecord(record);
    }


    /// <summary>
    ///
    /// </summary>
    public void SaveAccountPatterns(HttpAccountPattern newPatternRecord)
    {
      this.infrastructureLayer.SaveNewAccountPatternRecord(newPatternRecord);
    }

    #endregion

  }
}
