namespace Minary.Plugin.Main.Systems.ManageSystems.Infrastructure
{
  using Minary.Plugin.Main.Systems.DataTypes;
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
    /// <param name="pluginProperties"></param>
    public CustomPatternAdd(PluginProperties pluginProperties)
    {
      this.pluginProperties = pluginProperties;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="record"></param>
    public void SaveNewAccountPatternRecord(SystemPattern record)
    {
      FileStream fileStream = null;
      BinaryFormatter formatter = new BinaryFormatter();

      try
      {
        formatter = new BinaryFormatter();
        fileStream = new FileStream(record.PatternFileFullPath, FileMode.Create);
        formatter.Serialize(fileStream, record);
      }
      catch (Exception ex)
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

  }
}