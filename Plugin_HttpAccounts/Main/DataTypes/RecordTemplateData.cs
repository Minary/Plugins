namespace Minary.Plugin.Main.HttpAccounts.DataTypes
{
  using Minary.Plugin.Main.HttpAccounts.ManageAuthentications.DataTypes;
  using System.ComponentModel;

  
  public class TemplateHTTPAccounts
  {

    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="TemplateHTTPAccounts"/> class.
    ///
    /// </summary>
    public TemplateHTTPAccounts()
    {
      this.AccountPatterns = new BindingList<HttpAccountPattern>();
    }

    #endregion


    #region PROPERTIES
    
    public BindingList<HttpAccountPattern> AccountPatterns
    {
      get;
      set;
    }

    #endregion

  }
}
