namespace Minary.Plugin.Main.InjectFile.DataTypes
{

  public class RequestURL
  {

    #region PROPERTIES 

    public string HostRegex { get; set; }

    public string PathRegex { get; set; }

    #endregion


    #region PUBLIC

    public RequestURL(string hostRegex, string pathRegex)
    {
      this.HostRegex = hostRegex;
      this.PathRegex = pathRegex;
    }

    #endregion

  }
}
