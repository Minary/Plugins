namespace Minary.Plugin.Main.Session.ManageSessions.Task
{
  using Minary.Plugin.Main.Session.ManageSessions.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using System.Collections.Generic;

  public class ManageSessions : IObservable
  {

    #region MEMBERS

    private static ManageSessions instance;
    private Plugin.Main.Session.ManageSessions.Infrastructure.ManageSessions infrastructureLayer;
    private List<SessionPattern> sessionPatterns;
    private List<IObserver> observers;

    #endregion


    #region PROPERTIES

    public List<SessionPattern> SessionPatterns { get { return this.sessionPatterns; } private set { } }

    #endregion


    #region PUBLIC

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pluginParams"></param>
    private ManageSessions(PluginProperties pluginParams)
    {
      this.infrastructureLayer = Plugin.Main.Session.ManageSessions.Infrastructure.ManageSessions.GetInstance(pluginParams);
      this.sessionPatterns = new List<SessionPattern>();
      this.observers = new List<IObserver>();
    }


    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public static ManageSessions GetInstance(PluginProperties pluginProperties)
    {
      return instance ?? (instance = new ManageSessions(pluginProperties));
    }


    /// <summary>
    ///
    /// </summary>
    public void ReadSessionPatterns()
    {
      this.sessionPatterns = this.infrastructureLayer.ReadSessionPatterns();
      this.Notify();
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="record"></param>
    public void RemoveTemplate(SessionPattern record)
    {
      this.infrastructureLayer.RemoveTemplate(record);
    }


    public void SaveTemplate(SessionPattern newPatterns)
    {
      this.infrastructureLayer.SaveTemplate(newPatterns);
    }


    public byte[] OnGetTemplateData(List<SessionPattern> sessionPatternRecords)
    {
      return this.infrastructureLayer.OnGetTemplateData(sessionPatternRecords);
    }


    public List<SessionPattern> OnLoadTemplateData(TemplatePluginData templateData)
    {
      return this.infrastructureLayer.OnLoadTemplateData(templateData);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="record"></param>
    public void RemoveRecord(SessionPattern record)
    {
      this.sessionPatterns.Remove(record);
      this.Notify();
    }

    #endregion


    #region PRIVATE

    /// <summary>
    ///
    /// </summary>
    /// <param name="sessionName"></param>
    /// <param name="hostPattern"></param>
    /// <param name="cookiesPattern"></param>
    /// <returns></returns>
    private bool PatternExists(string sessionName, string hostPattern, string cookiesPattern)
    {
      bool retVal = false;

      foreach (SessionPattern tmpPattern in this.sessionPatterns)
      {
        if (tmpPattern.CompanyName.ToLower() == sessionName.ToLower())
        {
          retVal = true;
          break;
        }
        else if (!string.IsNullOrEmpty(hostPattern) && !string.IsNullOrEmpty(cookiesPattern) &&
                 tmpPattern.HTTPHostRegex.ToLower() == hostPattern.ToLower() && tmpPattern.SessionRegex.ToLower() == cookiesPattern.ToLower())
        {
          retVal = true;
          break;
        }
      }

      return (retVal);
    }

    #endregion


    #region Observable

    /// <summary>
    ///
    /// </summary>
    /// <param name="o"></param>
    public void AddObserver(IObserver observerObject)
    {
      if (observerObject != null)
      {
        this.observers.Add(observerObject);
      }
    }


    /// <summary>
    ///
    /// </summary>
    public void Notify()
    {
      foreach (IObserver tmpObserver in this.observers)
      {
        tmpObserver.Update(this.sessionPatterns);
      }
    }

    #endregion

  }
}