namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.SslStrip.DataTypes;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Linq;

  #region DATA TYPES

  public enum ContextType
  {
    HTML,
    XML,
    JavaScript,
    CSS,
    PlainText
  }

  #endregion


  public class StateMachine
  {

    #region MEMBERS

    private BindingList<ComboboxItem> comboBoxDataList;
    private BindingList<ComboboxItem> comboBoxContentTypeList;
    private Dictionary<ContextType, IContentTypeState> contentTypes;

    #endregion


    #region PUBLIC METHODS

    /// <summary>
    /// Initializes a new instance of the <see cref="StateMachine"/> class.
    ///
    /// </summary>
    public StateMachine(BindingList<ComboboxItem> comboBoxContentTypeList)
    {
      this.comboBoxContentTypeList = comboBoxContentTypeList;

      // Populate Main_GUI Combobox Content Types
      this.contentTypes = new Dictionary<ContextType, IContentTypeState>()
                                {
                                  { ContextType.HTML, new StateHtml(comboBoxContentTypeList, comboBoxDataList) }
                                  //// { ContextType.XML, new StateXml(comboBoxContentTypeList, comboBoxDataList) },
                                  //// { ContextType.JavaScript, new StateJavaScript(comboBoxContentTypeList, comboBoxDataList) },
                                  //// { ContextType.CSS, new StateCss(comboBoxContentTypeList, comboBoxDataList) },
                                  //// { ContextType.PlainText, new StatePlainText(comboBoxContentTypeList, comboBoxDataList) }
                                };

      this.contentTypes.ToList().ForEach(elem =>
      {
        ComboboxItem tmpComboboxItem = new ComboboxItem(elem.Key.ToString(), elem.Value);
        tmpComboboxItem.Selectable = false;

        this.comboBoxContentTypeList.Add(tmpComboboxItem);

      });

      this.comboBoxDataList = comboBoxDataList;
    }



    /// <summary>
    ///
    /// </summary>
    /// <param name="newState"></param>
    //// public void ChangeContentTypeState(IContentTypeState contextContentType)
    //// {
    ////   this.contentTypes[contextContentType.UsedContextType].PopulateList(this.comboBoxDataList);
    //// }

    #endregion

  }
}