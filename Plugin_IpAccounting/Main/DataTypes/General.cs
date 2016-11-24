namespace Minary.Plugin.Main.IpAccounting.DataTypes
{
  using System.Collections.Generic;


  public class General
  {

    #region MEMBERS

    public static readonly string APP_CONFIG_FILE = "app.config";
    public static readonly string PATTERN_DIR_LOCAL = "local";
    public static readonly string PATTERN_DIR_REMOTE = "remote";
    public static readonly string PATTERN_DIR_TEMPLATE = "template";
    public static readonly string PATTERN_FILE_PATTERN = "*.spa";

    #endregion

  }


  #region TYPE DEFINITION

  public delegate void OnAccountingExitDelegate();
  public delegate void OnUpdateListDelegate(List<AccountingItem> records);

  #endregion

}
