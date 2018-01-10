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
    
    private PluginProperties pluginProperties;

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomPatternAdd"/> class.
    ///
    /// </summary>
    public CustomPatternAdd(PluginProperties pluginProperties)
    {
      this.pluginProperties = pluginProperties;
    }


    /// <summary>
    ///
    /// </summary>
    public void SaveNewAccountPatternRecord(HttpAccountPattern newRecord)
    {
      FileStream fileStream = null;
      var formatter = new BinaryFormatter();

      try
      {
        fileStream = new FileStream(newRecord.PatternFileFullPath, FileMode.Create);
        formatter.Serialize(fileStream, newRecord);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"SaveNewAccountPatternRecord(ex): {ex.Message}");
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

  }
}
