namespace Minary.Plugin.Main.SslStrip.DataTypes
{
  public class ComboboxItem
  {

    #region PROPERTIES

    public string Text { get; set; }
    public object Value { get; set; }
    public bool Selectable { get; set; }

    #endregion

    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="ComboboxItem"/> class.
    ///
    /// </summary>
    /// <param name="text"></param>
    /// <param name="value"></param>
    public ComboboxItem(string text, object value)
    {
      Text = text;
      Value = value;
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return Text;
    }

    #endregion

  }
}