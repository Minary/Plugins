namespace Minary.Plugin.Main.Systems.ManageSystems.Task
{
  using Minary.Plugin.Main.Systems.DataTypes;
  using Minary.Plugin.Main.Systems.ManageSystems.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using System.Collections.Generic;


  public class ManageSystems
  {

    #region MEMBERS

    Infrastructure.ManageSystems infrastructureLayer;
    private List<IObserver> observers;
    private List<SystemPattern> systemPatterns;

    #endregion


    #region PROPERTIES

    public List<SystemPattern> SystemRecords { get { return this.systemPatterns; } }

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="ManageSystems"/> class.
    ///
    /// </summary>
    public ManageSystems(PluginProperties pluginProperties)
    {
      this.infrastructureLayer = new Infrastructure.ManageSystems(pluginProperties);
      this.systemPatterns = new List<SystemPattern>();
      this.observers = new List<IObserver>();
    }

    /// <summary>
    ///
    /// </summary>
    public void ReadSystemPatterns()
    {
      List<SystemPattern> systemPatterns = this.infrastructureLayer.ReadSystemPatterns();

      if (systemPatterns != null && systemPatterns.Count > 0)
      {
        this.systemPatterns.Clear();
        foreach (SystemPattern tmpRequest in systemPatterns)
        {
          this.systemPatterns.Add(tmpRequest);
        }
      }

      this.Notify();
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="record"></param>
    public void RemoveTemplate(SystemPattern record)
    {
      this.infrastructureLayer.RemoveTemplate(record);
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="newPatterns"></param>
    public void SaveTemplate(SystemPattern newPatterns)
    {
      this.infrastructureLayer.SaveTemplate(newPatterns);
    }


    public byte[] OnGetTemplateData(List<SystemPattern> systemPatternRecords)
    {
      return this.infrastructureLayer.OnGetTemplateData(systemPatternRecords);
    }


    public List<SystemPattern> OnLoadTemplateData(TemplatePluginData templateData)
    {
      return this.infrastructureLayer.OnLoadTemplateData(templateData);
    }


    #endregion


    #region OBSERVABLE

    public void AddObserver(IObserver observer)
    {
      if (observer != null)
      {
        this.observers.Add(observer);
      }
    }

    public void Notify()
    {
      foreach (IObserver tmpObserver in this.observers)
      {
        tmpObserver.Update(this.systemPatterns);
      }
    }

    #endregion

  }
}