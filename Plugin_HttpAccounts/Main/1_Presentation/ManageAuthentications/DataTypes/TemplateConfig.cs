namespace Minary.Plugin.Main.HttpAccounts.ManageAuthentications.DataTypes
{

  public class TemplateConfig
  {

    #region MEMBERS

    private string name;
    private string description;
    private string timestamp;
    private string author;
    private string reference;

    #endregion


    #region PROPERTIES

    public string Name { get { return this.name; } set { this.name = value; } }


    public string Description { get { return this.description; } set { this.description = value; } }


    public string Timestamp { get { return this.timestamp; } set { this.timestamp = value; } }


    public string Author { get { return this.author; } set { this.author = value; } }


    public string Reference { get { return this.reference; } set { this.reference = value; } }

    #endregion

  }

}
