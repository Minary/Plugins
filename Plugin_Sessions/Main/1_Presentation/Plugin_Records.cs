namespace Minary.Plugin.Main
{
  using System;

  public partial class Plugin_Sessions
  {

    #region GUI RECORDS METHODS
    
    /// <summary>
    ///
    /// </summary>
    /// <param name="protocol"></param>
    /// <param name="srcIp"></param>
    /// <param name="dstIp"></param>
    /// <param name="srcPortLowerStr"></param>
    /// <param name="srcPortUpperStr"></param>
    /// <param name="dstPortLowerStr"></param>
    /// <param name="dstPortUpperStr"></param>
    private delegate void AddRecordDelegate(string protocol, string srcIp, string dstIp, string srcPortLowerStr, string srcPortUpperStr, string dstPortLowerStr, string dstPortUpperStr);
    public void AddRecord(string protocol, string srcIp, string dstIp, string srcPortLowerStr, string srcPortUpperStr, string dstPortLowerStr, string dstPortUpperStr)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new AddRecordDelegate(this.AddRecord), new object[] { protocol, srcIp, dstIp, srcPortLowerStr, srcPortUpperStr, dstPortLowerStr, dstPortUpperStr });
        return;
      }

      int firstVisibleRowTop = -1;

      // Add new rule to DataGridView
      lock (this)
      {
        // Memorize DataGridView position and selection
        firstVisibleRowTop = this.dgv_Sessions.FirstDisplayedScrollingRowIndex;
        this.dgv_Sessions.SuspendLayout();

        try
        {
          this.sessionRecords.Insert(0, new Session.DataTypes.TheSessionRecord(protocol, srcIp, srcPortLowerStr, srcPortUpperStr, dstIp, dstPortLowerStr, dstPortUpperStr));
        }
        catch (Exception)
        {
        }

        if (firstVisibleRowTop >= 0)
        {
          this.dgv_Sessions.FirstDisplayedScrollingRowIndex = firstVisibleRowTop;
        }

        this.dgv_Sessions.ResumeLayout();
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="index"></param>
    private delegate void RemoveRecordAtDelegate(int index);
    public void RemoveRecordAt(int index)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new RemoveRecordAtDelegate(this.RemoveRecordAt), new object[] { index });
        return;
      }

      lock (this)
      {
        this.dgv_Sessions.SuspendLayout();

        try
        {
          this.sessionRecords.RemoveAt(index);
        }
        catch (Exception)
        {
        }

        this.dgv_Sessions.ResumeLayout();
      }
    }


    /// <summary>
    ///
    /// </summary>
    private delegate void ClearRecordListDelegate();
    private void ClearRecordList()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new ClearRecordListDelegate(this.ClearRecordList), new object[] { });
        return;
      }

      lock (this)
      {
        this.dgv_Sessions.SuspendLayout();

        try
        {
          this.sessionRecords.Clear();
        }
        catch (Exception)
        {
        }

        this.dgv_Sessions.ResumeLayout();
      }
    }

    #endregion

  }
}