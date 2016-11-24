namespace Minary.Plugin.Main.Session.DataTypes
{
  using Minary.Plugin.Main.Session.ManageSessions.DataTypes;
  using System.Collections.Generic;


  public class TemplateSessions
  {

    #region PROPERTIES
    
    public List<SessionPattern> SessionPatterns { get; set; }

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="TemplateSessions"/> class.
    ///
    /// </summary>
    public TemplateSessions()
    {
      SessionPatterns = new List<SessionPattern>();
    }

    #endregion

  }
}