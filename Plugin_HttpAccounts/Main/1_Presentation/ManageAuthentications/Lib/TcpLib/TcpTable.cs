namespace Minary.Plugin.Main.HttpAccounts.ManageAuthentications.TcpLib
{
  using System.Collections;
  using System.Collections.Generic;

  public class TcpTable : IEnumerable<TcpRow>
  {

    #region MEMBERS

    private IEnumerable<TcpRow> tcpRows;

    #endregion


    #region PROPERTIES

    public IEnumerable<TcpRow> Rows { get { return tcpRows; } }

    public IEnumerator<TcpRow> GetEnumerator() { return tcpRows.GetEnumerator(); }

    IEnumerator IEnumerable.GetEnumerator() { return tcpRows.GetEnumerator(); }

    #endregion


    #region PUBLIC

    public TcpTable(IEnumerable<TcpRow> tcpRows)
    {
      this.tcpRows = tcpRows;
    }

    #endregion

  }
}
