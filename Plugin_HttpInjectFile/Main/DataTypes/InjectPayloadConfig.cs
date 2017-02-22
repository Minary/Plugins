namespace Minary.Plugin.Main.InjectFile.DataTypes
{

  public class InjectFileConfig
  {

    #region MEMBERS

    private bool isDebuggingOn;
    private string basisDirectory;
    private string injectPayloadConfigFilePath;

    #endregion


    #region PROPERTIES

    public bool IsDebuggingOn { get { return this.isDebuggingOn; } set { this.isDebuggingOn = value; } }

    public string BasisDirectory { get { return this.basisDirectory; } set { this.basisDirectory = value; } }

    public string InjectFileConfigFilePath { get { return this.injectPayloadConfigFilePath; } set { this.injectPayloadConfigFilePath = value; } }

    #endregion

  }
}
