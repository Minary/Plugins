namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.SslStrip.DataTypes;
  using System.ComponentModel;
  using System.Linq;

  public class StateCss : IContentTypeState
  {

    #region MEMBERS
    
    private readonly BindingList<ComboboxItem> cssTagList;

    #endregion


    #region PROPERTIES

    public ContextType UsedContextType { get { return ContextType.CSS; } }
    public string UsedContentType { get; private set; } = "text/css";

    #endregion


    #region PUBLIC METHODS

    /// <summary>
    /// Initializes a new instance of the <see cref="StateCss"/> class.
    ///
    /// </summary>
    public StateCss(BindingList<ComboboxItem> comboBoxContentTypeList, BindingList<ComboboxItem> comboBoxDataList)
    {
      this.cssTagList = new BindingList<ComboboxItem>();
      this.cssTagList.Add(new ComboboxItem("Link", "link"));
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="htmlTagist"></param>
    public void PopulateList(BindingList<ComboboxItem> htmlTagist)
    {
      htmlTagist.Clear();
      this.cssTagList.ToList().ForEach(elem => htmlTagist.Add(new ComboboxItem(elem.Text, elem.Value)));
    }

    #endregion

  }
}