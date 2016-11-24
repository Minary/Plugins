namespace Minary.Plugin.Main.SslStrip.DataTypes
{
  public class SslStripConfig
  {

    #region MEMBERS

    private bool isDebuggingOn;
    private OnSslStripExitDelegate onSslStripExit;
    private string basisDirectory;
    private string sslStripConfigFilePath;

    #endregion


    #region PROPERTIES

    public bool IsDebuggingOn { get { return this.isDebuggingOn; } set { this.isDebuggingOn = value; } }

    public OnSslStripExitDelegate OnSslStripExit { get { return this.onSslStripExit; } set { this.onSslStripExit = value; } }

    public string BasisDirectory { get { return this.basisDirectory; } set { this.basisDirectory = value; } }

    public string SslStripConfigFilePath { get { return this.sslStripConfigFilePath; } set { this.sslStripConfigFilePath = value; } }

    #endregion

  }
}