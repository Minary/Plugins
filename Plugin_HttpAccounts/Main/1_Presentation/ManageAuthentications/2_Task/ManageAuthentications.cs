namespace Minary.Plugin.Main.HttpAccounts.ManageAuthentications.Task
{
  using Minary.Plugin.Main.HttpAccounts.ManageAuthentications.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using System.Collections.Generic;


  public class ManageAuthentications : IObservable
  {

    #region MEMBERS
    
    private PluginProperties pluginProperties;
    private Infrastructure.ManageAuthentications infrastructureLayer;
    private List<IObserver> observers = new List<IObserver>();
    private List<HttpAccountPattern> accountPatterns = new List<HttpAccountPattern>();

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="ManageAuthentications"/> class.
    ///
    /// </summary>
    public ManageAuthentications(PluginProperties pluginProperties)
    {
      this.pluginProperties = pluginProperties;
      this.infrastructureLayer = new Plugin.Main.HttpAccounts.ManageAuthentications.Infrastructure.ManageAuthentications(this.pluginProperties);
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
