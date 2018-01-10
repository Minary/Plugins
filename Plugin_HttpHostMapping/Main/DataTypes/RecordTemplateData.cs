namespace Minary.Plugin.Main.HostMapping.DataTypes
{
  using System.ComponentModel;

  
  public class TemplateHostMapping
  {

    #region PROPERTIES
    
    public BindingList<HostMappingRecord> HostMappingRecords { get; set; }

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="TemplateHostMapping"/> class.
    ///
    /// </summary>
    public TemplateHostMapping()
    {
    }

    #endregion

  }
}
