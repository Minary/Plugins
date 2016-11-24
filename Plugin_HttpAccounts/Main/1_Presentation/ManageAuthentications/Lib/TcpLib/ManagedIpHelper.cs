namespace Minary.Plugin.Main.HttpAccounts.ManageAuthentications.TcpLib
{
  using System;
  using System.Collections.Generic;
  using System.Runtime.InteropServices;


  public static class ManagedIpHelper
  {

    #region PUBLIC

    public static TcpTable GetExtendedTcpTable(bool sorted)
    {
      List<TcpRow> tcpRows = new List<TcpRow>();

      IntPtr tcpTable = IntPtr.Zero;
      int tcpTableLength = 0;

      if (IpHelper.GetExtendedTcpTable(tcpTable, ref tcpTableLength, sorted, IpHelper.AfInet, IpHelper.TcpTableType.OwnerPidAll, 0) != 0)
      {
        try
        {
          tcpTable = Marshal.AllocHGlobal(tcpTableLength);
          if (IpHelper.GetExtendedTcpTable(tcpTable, ref tcpTableLength, true, IpHelper.AfInet, IpHelper.TcpTableType.OwnerPidAll, 0) == 0)
          {
            IpHelper.TcpTable table = (IpHelper.TcpTable)Marshal.PtrToStructure(tcpTable, typeof(IpHelper.TcpTable));

            IntPtr rowPtr = (IntPtr)((long)tcpTable + Marshal.SizeOf(table.Length));
            for (int i = 0; i < table.Length; ++i)
            {
              tcpRows.Add(new TcpRow((IpHelper.TcpRow)Marshal.PtrToStructure(rowPtr, typeof(IpHelper.TcpRow))));
              rowPtr = (IntPtr)((long)rowPtr + Marshal.SizeOf(typeof(IpHelper.TcpRow)));
            }
          }
        }
        finally
        {
          if (tcpTable != IntPtr.Zero)
          {
            Marshal.FreeHGlobal(tcpTable);
          }
        }
      }

      return new TcpTable(tcpRows);
    }
  }

    #endregion

}
