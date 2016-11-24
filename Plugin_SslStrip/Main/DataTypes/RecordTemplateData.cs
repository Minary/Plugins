namespace Minary.Plugin.Main.SslStrip.DataTypes
{
  using System.ComponentModel;


  public class TemplateSslStrip
  {

    #region PROPERTIES
    
    public BindingList<SslStripRecord> SslStripRecords
    {
      get;
      set;
    }

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="TemplateSslStrip"/> class.
    ///
    /// </summary>
    public TemplateSslStrip()
    {
    }

    #endregion

  }
}