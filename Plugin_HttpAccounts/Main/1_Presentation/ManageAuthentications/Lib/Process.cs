namespace Minary.Plugin.Main.HttpAccounts.ManageAuthentications.Lib
{
  using Minary.Plugin.Main.HttpAccounts.ManageAuthentications.TcpLib;

  public static class Process
  {

    #region PUBLIC

    /// <summary>
    ///
    /// </summary>
    /// <param name="port"></param>
    /// <returns></returns>
    public static string FindProc(int port)
    {
      string retVal = string.Empty;

      if (port <= 0 || port >= 65536)
      {
        return retVal;
      }

      try
      {
        TcpTable tcpTable = ManagedIpHelper.GetExtendedTcpTable(true);
        foreach (TcpRow tcpRow in tcpTable)
        {
          if (tcpRow.LocalEndPoint.Port != port)
          {
            continue;
          }

          try
          {
            System.Diagnostics.Process proc = System.Diagnostics.Process.GetProcessById(tcpRow.ProcessId);
            retVal = proc.ProcessName;
            break;
          }
          catch
          {
            break;
          }
        }
      }
      catch
      {
      }

      return retVal;
    }

    #endregion

  }
}
