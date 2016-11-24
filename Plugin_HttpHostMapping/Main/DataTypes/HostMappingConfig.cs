namespace Minary.Plugin.Main.HostMapping.DataTypes
{
  public class HostMappingConfig
  {

    #region MEMBERS

    private bool isDebuggingOn;
    private string basisDirectory;
    private string hostMappingConfigFilePath;

    #endregion


    #region PROPERTIES

    public bool IsDebuggingOn { get { return this.isDebuggingOn; } set { this.isDebuggingOn = value; } }

    public string BasisDirectory { get { return this.basisDirectory; } set { this.basisDirectory = value; } }

    public string HostMappingConfigFilePath { get { return this.hostMappingConfigFilePath; } set { this.hostMappingConfigFilePath = value; } }

    #endregion

  }
}