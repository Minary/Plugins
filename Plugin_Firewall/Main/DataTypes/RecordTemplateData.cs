namespace Minary.Plugin.Main.Firewall.DataTypes
{
  using System.ComponentModel;
  

  public class TemplateFirewall
  {

    #region PROPERTIES

    public BindingList<FirewallRuleRecord> FirewallRules { get; set; }

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="TemplateFirewall"/> class.
    ///
    /// </summary>
    public TemplateFirewall()
    {
    }

    #endregion

  }
}
