namespace Minary.Plugin.Main.InjectCode.DataTypes
{

  public class InjectCodeConfig
  {

    #region MEMBERS

    private bool isDebuggingOn;
    private string basisDirectory;
    private string injectCodeConfigFilePath;

    #endregion


    #region PROPERTIES

    public bool IsDebuggingOn { get { return this.isDebuggingOn; } set { this.isDebuggingOn = value; } }

    public string BasisDirectory { get { return this.basisDirectory; } set { this.basisDirectory = value; } }

    public string InjectCodeConfigFilePath { get { return this.injectCodeConfigFilePath; } set { this.injectCodeConfigFilePath = value; } }

    #endregion

  }
}
