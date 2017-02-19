namespace Minary.Plugin.Main.RequestRedirect.DataTypes
{
  public class RequestRedirectConfig
  {

    #region MEMBERS

    private bool isDebuggingOn;
    private string basisDirectory;
    private string requestRedirectConfigFilePath;

    #endregion


    #region PROPERTIES

    public bool IsDebuggingOn { get { return this.isDebuggingOn; } set { this.isDebuggingOn = value; } }

    public string BasisDirectory { get { return this.basisDirectory; } set { this.basisDirectory = value; } }

    public string RequestRedirectConfigFilePath { get { return this.requestRedirectConfigFilePath; } set { this.requestRedirectConfigFilePath = value; } }

    #endregion

  }
}