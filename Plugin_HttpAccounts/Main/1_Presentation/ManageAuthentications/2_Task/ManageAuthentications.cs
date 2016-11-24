namespace Minary.Plugin.Main.HttpAccounts.ManageAuthentications.Task
{
  using Minary.Plugin.Main.HttpAccounts.ManageAuthentications.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using System.Collections.Generic;


  public class ManageAuthentications : IObservable
  {

    #region MEMBERS

    private static ManageAuthentications instance;
    private PluginProperties pluginProperties;
    private Infrastructure.ManageAuthentications infrastructureLayer;
    private List<IObserver> observers;
    private List<HttpAccountPattern> accountPatterns;

    #endregion


    #region PROPERTIES

    public List<HttpAccountPattern> AccountPatterns { get { return this.accountPatterns; } }

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="ManageAuthentications"/> class.
    ///
    /// </summary>
    private ManageAuthentications(PluginProperties pluginProperties)
    {
      this.pluginProperties = pluginProperties;
      this.infrastructureLayer = Plugin.Main.HttpAccounts.ManageAuthentications.Infrastructure.ManageAuthentications.GetInstance(this.pluginProperties);
      this.accountPatterns = new List<HttpAccountPattern>();
      this.observers = new List<IObserver>();
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="pluginProperties"></param>
    /// <returns></returns>
    public static ManageAuthentications GetInstance(PluginProperties pluginProperties)
    {
      return instance ?? (instance = new ManageAuthentications(pluginProperties));
    }


    /// <summary>
    ///
    /// </summary>
    public void ReadAccountsPatterns()
    {
      this.accountPatterns = this.infrastructureLayer.ReadAuthenticationPatterns();
      this.Notify();
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="record"></param>
    public void RemoveTemplate(HttpAccountPattern record)
    {
      this.infrastructureLayer.RemoveTemplate(record);
    }


    public void SaveTemplate(HttpAccountPattern newPatterns)
    {
      this.infrastructureLayer.SaveTemplate(newPatterns);
    }


    public byte[] OnGetTemplateData(List<HttpAccountPattern> applicationPatternRecords)
    {
      return this.infrastructureLayer.OnGetTemplateData(applicationPatternRecords);
    }


    public List<HttpAccountPattern> OnLoadTemplateData(TemplatePluginData templateData)
    {
      return this.infrastructureLayer.OnLoadTemplateData(templateData);
    }

    #endregion


    #region OBSERVABLE

    public void AddObserver(IObserver observerObj)
    {
      if (observerObj != null)
      {
        this.observers.Add(observerObj);
      }
    }

    public void Notify()
    {
      foreach (IObserver tmpObserver in this.observers)
      {
        tmpObserver.Update(this.accountPatterns);
      }
    }

    #endregion

  }
}
