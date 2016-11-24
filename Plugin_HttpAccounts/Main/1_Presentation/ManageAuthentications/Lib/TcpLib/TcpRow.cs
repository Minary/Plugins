namespace Minary.Plugin.Main.HttpAccounts.ManageAuthentications.TcpLib
{
  using System.Net;
  using System.Net.NetworkInformation;

  public class TcpRow
  {

    #region MEMBERS

    private IPEndPoint localEndPoint;
    private IPEndPoint remoteEndPoint;
    private TcpState state;
    private int processId;

    #endregion


    #region PROPERTIES

    public IPEndPoint LocalEndPoint { get { return localEndPoint; } }

    public IPEndPoint RemoteEndPoint { get { return remoteEndPoint; } }

    public TcpState State { get { return state; } }

    public int ProcessId { get { return processId; } }

    #endregion


    #region PUBLIC

    public TcpRow(IpHelper.TcpRow tcpRow)
    {
      state = tcpRow.State;
      processId = tcpRow.OwningPid;

      int localPort = (tcpRow.LocalPort1 << 8) + tcpRow.LocalPort2 + (tcpRow.LocalPort3 << 24) + (tcpRow.LocalPort4 << 16);
      long localAddress = tcpRow.LocalAddr;
      localEndPoint = new IPEndPoint(localAddress, localPort);

      int remotePort = (tcpRow.RemotePort1 << 8) + tcpRow.RemotePort2 + (tcpRow.RemotePort3 << 24) + (tcpRow.RemotePort4 << 16);
      long remoteAddress = tcpRow.RemoteAddr;
      remoteEndPoint = new IPEndPoint(remoteAddress, remotePort);
    }

    #endregion

  }
}
