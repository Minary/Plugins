namespace Minary.Plugin.Main.InjectPayload.DataTypes
{
  using System.ComponentModel;
  

  public class TemplateInjectPayload
  {

    #region PROPERTIES
    
    public BindingList<Minary.Plugin.Main.InjectPayload.DataTypes.InjectPayloadRecord> InjectPayloadRecords
    {
      get;
      set;
    }

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="TemplateInjectPayload"/> class.
    ///
    /// </summary>
    public TemplateInjectPayload()
    {
    }

    #endregion

  }
}