namespace Minary.Plugin.Main.InjectCode.DataTypes
{

  public class ComboboxItem
  {

    #region PROPERTIES

    public string Text { get; set; }

    public object Value { get; set; }

    #endregion


    #region PUBLIC

    public ComboboxItem(string text, string value)
    {
      this.Text = text;
      this.Value = value;
    }


    public override string ToString()
    {
      return this.Text;
    }

    #endregion

  }
}
