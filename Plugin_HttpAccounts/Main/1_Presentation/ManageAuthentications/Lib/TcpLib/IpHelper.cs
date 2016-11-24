namespace Minary.Plugin.Main.HttpAccounts.ManageAuthentications.TcpLib
{
  using System;
  using System.Net.NetworkInformation;
  using System.Runtime.InteropServices;

  public static class IpHelper
  {

    #region MEMBERS

    public const string DllName = "iphlpapi.dll";
    public const int AfInet = 2;

    #endregion


    #region IMPORTS

    [DllImport(IpHelper.DllName, SetLastError = true)]
    public static extern uint GetExtendedTcpTable(IntPtr tcpTable, ref int tcpTableLength, bool sort, int ipVersion, TcpTableType tcpTableType, int reserved);

    #endregion


    #region DATATYPES

    public enum TcpTableType
    {
      BasicListener,
      BasicConnections,
      BasicAll,
      OwnerPidListener,
      OwnerPidConnections,
      OwnerPidAll,
      OwnerModuleListener,
      OwnerModuleConnections,
      OwnerModuleAll,
    }

    #endregion


    #region PUBLIC

    [StructLayout(LayoutKind.Sequential)]
    public struct TcpTable
    {
      public uint Length;
      public TcpRow Row;
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct TcpRow
    {
      public TcpState State;
      public uint LocalAddr;
      public byte LocalPort1;
      public byte LocalPort2;
      public byte LocalPort3;
      public byte LocalPort4;
      public uint RemoteAddr;
      public byte RemotePort1;
      public byte RemotePort2;
      public byte RemotePort3;
      public byte RemotePort4;
      public int OwningPid;
    }

    #endregion

  }
}
