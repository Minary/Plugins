namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.SslStrip.DataTypes;
  using System.ComponentModel;
  using System.Linq;


  public class StateJavaScript : IContentTypeState
  {

    #region MEMBERS

    private const string MimeType = "application/javascript";
    private readonly BindingList<ComboboxItem> javaScriptTagList;

    #endregion


    #region PROPERTIES

    public ContextType UsedContextType { get { return ContextType.JavaScript; } }
    public string UsedContentType { get { return MimeType; } }

    #endregion


    #region PUBLIC METHODS

    /// <summary>
    /// Initializes a new instance of the <see cref="StateJavaScript"/> class.
    ///
    /// </summary>
    public StateJavaScript(BindingList<ComboboxItem> comboBoxContentTypeList, BindingList<ComboboxItem> comboBoxDataList)
    {
      this.javaScriptTagList = new BindingList<ComboboxItem>();
      this.javaScriptTagList.Add(new ComboboxItem("Form", "form"));
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="htmlTagist"></param>
    public void PopulateList(BindingList<ComboboxItem> htmlTagList)
    {
      htmlTagList.Clear();
      this.javaScriptTagList.ToList().ForEach(elem => htmlTagList.Add(new ComboboxItem(elem.Text, elem.Value)));
    }

    #endregion

  }
}