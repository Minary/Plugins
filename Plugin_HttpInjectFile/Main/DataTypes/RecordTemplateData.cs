namespace Minary.Plugin.Main.InjectFile.DataTypes
{
  using System.ComponentModel;
  

  public class TemplateInjectFile
  {

    #region PROPERTIES

    public BindingList<InjectFileRecord> InjectFileRecords { get; set; }

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="TemplateInjectFile"/> class.
    ///
    /// </summary>
    public TemplateInjectFile()
    {
    }

    #endregion

  }
}