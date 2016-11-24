namespace Minary.Plugin.Main.IpAccounting.DataTypes
{
  public class IpAccountingConfig
  {

    #region MEMBERS

    private bool isDebuggingOn;
    private OnAccountingExitDelegate onIpAccountingExit;
    private OnUpdateListDelegate onUpdateList;
    private string basisDirectory;
    private string networkInterface;
    private string structureParameter;

    #endregion

    public bool IsDebuggingOn { get { return isDebuggingOn; } set { isDebuggingOn = value; } }
    public OnAccountingExitDelegate OnIpAccountingExit { get { return onIpAccountingExit; } set { onIpAccountingExit = value; } }
    public OnUpdateListDelegate OnUpdateList { get { return onUpdateList; } set { onUpdateList = value; } }
    public string BasisDirectory { get { return basisDirectory; } set { basisDirectory = value; } }
    public string Interface { get { return networkInterface; } set { networkInterface = value; } }
    public string StructureParameter { get { return structureParameter; } set { structureParameter = value; } }

    #region PROPERTIES



    #endregion

  }
}
