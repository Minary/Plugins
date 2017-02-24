namespace Minary.Plugin.Main.InjectCode.DataTypes
{
  using System.ComponentModel;
  

  public class TemplateInjectPayload
  {

    #region PROPERTIES

    public BindingList<Minary.Plugin.Main.InjectCode.DataTypes.InjectCodeRecord> InjectPayloadRecords
    {
      get;
      set;
    }

    #endregion


    #region PUBLIC

    /// <summary>
    /// 
    /// </summary>
    public TemplateInjectPayload()
    {
    }

    #endregion

  }
}