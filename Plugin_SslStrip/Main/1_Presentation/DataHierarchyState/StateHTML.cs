namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.SslStrip.DataTypes;
  using System.ComponentModel;

  public class StateHtml : IContentTypeState
  {

    #region MEMBERS

    private const string MimeType = "text/html";
    private readonly BindingList<ComboboxItem> htmlTagListData;

    #endregion


    #region PROPERTIES

    public ContextType UsedContextType { get { return ContextType.HTML; } }
    public string UsedContentType { get { return MimeType; } }

    #endregion


    #region PUBLIC METHODS

    /// <summary>
    /// Initializes a new instance of the <see cref="StateHtml"/> class.
    ///
    /// </summary>
    public StateHtml(BindingList<ComboboxItem> comboBoxContentTypeList, BindingList<ComboboxItem> comboBoxDataList)
    {
      this.htmlTagListData = new BindingList<ComboboxItem>();
      this.htmlTagListData.Add(new ComboboxItem("Anchor", "a"));
      this.htmlTagListData.Add(new ComboboxItem("Base", "base"));
      this.htmlTagListData.Add(new ComboboxItem("Form", "form"));
      this.htmlTagListData.Add(new ComboboxItem("Image", "img"));
      this.htmlTagListData.Add(new ComboboxItem("Link", "link"));
      this.htmlTagListData.Add(new ComboboxItem("Script", "script"));
    }

    #endregion

  }
}