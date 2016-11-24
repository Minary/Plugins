namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.SslStrip.DataTypes;
  using System.ComponentModel;
  using System.Linq;


  public class StateXml : IContentTypeState
  {

    #region MEMBERS

    private const string MimeType = "text/xml";
    private readonly BindingList<ComboboxItem> xmlTagList;

    #endregion


    #region PROPERTIES

    public ContextType UsedContextType { get { return ContextType.XML; } }
    public string UsedContentType { get { return MimeType; } }

    #endregion


    #region PUBLIC METHODS

    /// <summary>
    /// Initializes a new instance of the <see cref="StateXml"/> class.
    ///
    /// </summary>
    public StateXml(BindingList<ComboboxItem> comboBoxContentTypeList, BindingList<ComboboxItem> comboBoxDataList)
    {
      this.xmlTagList = new BindingList<ComboboxItem>();
      this.xmlTagList.Add(new ComboboxItem("Imgage", "img"));
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="htmlTagist"></param>
    public void PopulateList(BindingList<ComboboxItem> htmlTagist)
    {
      htmlTagist.Clear();
      this.xmlTagList.ToList().ForEach(elem => htmlTagist.Add(new ComboboxItem(elem.Text, elem.Value)));
    }

    #endregion

  }
}