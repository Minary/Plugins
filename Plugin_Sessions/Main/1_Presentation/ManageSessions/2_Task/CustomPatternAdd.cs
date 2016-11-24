namespace Minary.Plugin.Main.Session.ManageSessions.Task
{
  using Minary.Plugin.Main.Session.ManageSessions.DataTypes;
  using MinaryLib;
  using System;
  using System.Text.RegularExpressions;

  public class CustomPatternAdd
  {

    #region MEMBERS

    private static CustomPatternAdd instance;
    private PluginProperties pluginProperties;
    private Infrastructure.CustomPatternAdd infrastructureLayer;

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
    public void AddCustomPatternRecord(SessionPattern record)
    {
      if (record == null)
      {
        throw new Exception("Something is wrong with the record");
      }
      else if (string.IsNullOrEmpty(record.CompanyName))
      {
        throw new Exception("You didn't define a company name");
      }
      else if (string.IsNullOrEmpty(record.CompanyWebpage))
      {
        throw new Exception("You didn't define a company web page");
      }
      else if (string.IsNullOrEmpty(record.HTTPHostRegex))
      {
        throw new Exception("You didn't define a HTTP Host regex");
      }
      else if (string.IsNullOrEmpty(record.SessionRegex))
      {
        throw new Exception("You didn't define a session cookie regex");
      }
      //else if (string.IsNullOrEmpty(record.Config.Name))
      //{
      //  throw new Exception("You didn't define a pattern name");
      //}
      //else if (string.IsNullOrEmpty(record.Config.Description))
      //{
      //  throw new Exception("You didn't define a pattern description");
      //}

      // Check session cookie regex
      try
      {
        new Regex(record.SessionRegex);
      }
      catch (ArgumentException)
      {
        throw new Exception("Session cookies regex is invalid");
      }

      // Check host regex
      try
      {
        new Regex(record.HTTPHostRegex);
      }
      catch (ArgumentException)
      {
        throw new Exception("HTTP host regex is invalid");
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
      this.infrastructureLayer = Infrastructure.CustomPatternAdd.GetInstance(this.pluginProperties);
    }

    #endregion

  }
}
