namespace Minary.Plugin.Main.HttpAccounts.ManageAuthentications.Infrastructure
{
  using Minary.Plugin.Main.HttpAccounts.ManageAuthentications.DataTypes;
  using MinaryLib;
  using System;
  using System.IO;
  using System.Runtime.Serialization.Formatters.Binary;


  public class CustomPatternAdd
  {

    #region MEMBERS

    private static CustomPatternAdd instance;
    private PluginProperties pluginProperties;

    #endregion


    #region PUBLIC

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public static CustomPatternAdd GetInstance(PluginProperties pluginProperties)
    {
      return instance ?? (instance = new CustomPatternAdd(pluginProperties));
    }


    /// <summary>
    ///
    /// </summary>
    public void SaveNewAccountPatternRecord(HttpAccountPattern newRecord)
    {
      FileStream fileStream = null;
      BinaryFormatter formatter = new BinaryFormatter();

      try
      {
        fileStream = new FileStream(newRecord.PatternFileFullPath, FileMode.Create);
        formatter.Serialize(fileStream, newRecord);
      }
      catch (Exception ex)
      {
        Console.WriteLine("SaveNewAccountPatternRecord(ex): {0}", ex.Message);
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
    /// Initializes a new instance of the <see cref="CustomPatternAdd"/> class.
    ///
    /// </summary>
    private CustomPatternAdd(PluginProperties pluginProperties)
    {
      this.pluginProperties = pluginProperties;
    }

    #endregion

  }
}
