namespace Minary.Plugin.Main.HostMapping.DataTypes
{
  public class General
  {

    #region MEMBERS

    public static readonly string PATTERN_DIR_LOCAL = "local";
    public static readonly string PATTERN_DIR_REMOTE = "remote";
    public static readonly string PATTERN_DIR_TEMPLATE = "template";
    public static readonly string PATTERN_FILE_PATTERN = "*.spa";

    #endregion

  }

  #region TYPE DEFINITION

  public delegate void OnInjectPayloadExitDelegate();

  #endregion

}
