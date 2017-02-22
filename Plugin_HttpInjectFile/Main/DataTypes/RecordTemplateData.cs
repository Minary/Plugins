namespace Minary.Plugin.Main.InjectFile.DataTypes
{
  using System.ComponentModel;
  

  public class TemplateInjectPayload
  {

    #region PROPERTIES

    public BindingList<Minary.Plugin.Main.InjectFile.DataTypes.InjectFileRecord> InjectPayloadRecords
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