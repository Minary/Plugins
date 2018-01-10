namespace Minary.Plugin.Main.HttpAccounts.ManageAuthentications.TcpLib
{
  using System.Net;
  using System.Net.NetworkInformation;


  public class TcpRow
  {

    #region PROPERTIES

    public IPEndPoint LocalEndPoint { get; private set; }

    public IPEndPoint RemoteEndPoint { get; private set; }

    public TcpState State { get; private set; }

    public int ProcessId { get; private set; }

    #endregion


    #region PUBLIC

    public TcpRow(IpHelper.TcpRow tcpRow)
    {
      this.State = tcpRow.State;
      this.ProcessId = tcpRow.OwningPid;

      int localPort = (tcpRow.LocalPort1 << 8) + tcpRow.LocalPort2 + (tcpRow.LocalPort3 << 24) + (tcpRow.LocalPort4 << 16);
      long localAddress = tcpRow.LocalAddr;
      this.LocalEndPoint = new IPEndPoint(localAddress, localPort);

      int remotePort = (tcpRow.RemotePort1 << 8) + tcpRow.RemotePort2 + (tcpRow.RemotePort3 << 24) + (tcpRow.RemotePort4 << 16);
      long remoteAddress = tcpRow.RemoteAddr;
      this.RemoteEndPoint = new IPEndPoint(remoteAddress, remotePort);
    }

    #endregion

  }
}
