namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.SslStrip.DataTypes;
  using System.ComponentModel;
  using System.Linq;

  public class StatePlainText : IContentTypeState
  {

    #region MEMBERS

    private const string MimeType = "text/plain";
    private readonly BindingList<ComboboxItem> plainTextTagist;

    #endregion


    #region PROPERTIES

    public ContextType UsedContextType { get { return ContextType.PlainText; } }
    public string UsedContentType { get { return MimeType; } }

    #endregion


    #region PUBLIC METHODS

    /// <summary>
    /// Initializes a new instance of the <see cref="StatePlainText"/> class.
    ///
    /// </summary>
    public StatePlainText(BindingList<ComboboxItem> comboBoxContentTypeList, BindingList<ComboboxItem> comboBoxDataList)
    {
      this.plainTextTagist = new BindingList<ComboboxItem>();
      this.plainTextTagist.Add(new ComboboxItem("Base", "base"));
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="htmlTagist"></param>
    public void PopulateList(BindingList<ComboboxItem> htmlTagList)
    {
      htmlTagList.Clear();
      this.plainTextTagist.ToList().ForEach(elem => htmlTagList.Add(new ComboboxItem(elem.Text, elem.Value)));
    }

    #endregion

  }
}