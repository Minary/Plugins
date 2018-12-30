namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.DnsRequest.DataTypes;
  using System;
  using System.Collections.Generic;


  public partial class Plugin_DnsRequests
  {

    #region GUI RECORDS METHODS

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dnsRequests"></param>
    private delegate void AddRecordDelegate(List<DnsRequestRecord> dnsRequests);
    private void AddRecordsToDgv(List<DnsRequestRecord> dnsRequests)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new AddRecordDelegate(this.AddRecordsToDgv), new object[] { dnsRequests });
        return;
      }

      if (dnsRequests?.Count > 0 == false)
      {
        return;
      }

      var firstVisibleRowTop = -1;
      var selectedRowIndex = -1;

      lock (this)
      {
        // Memorize DataGridView position and selection
        firstVisibleRowTop = this.dgv_DnsRequests.FirstDisplayedScrollingRowIndex;
        if (this.dgv_DnsRequests.SelectedRows.Count > 0)
        {
          selectedRowIndex = this.dgv_DnsRequests.SelectedRows[0].Index;
        }

        // Verify and insert selectedHostName rows
        foreach (DnsRequestRecord tmpReq in dnsRequests)
        {
          // Verify if Hostname and IPaddress for correctness.
          try
          {
            if (string.IsNullOrEmpty(tmpReq.SrcIP))
            {
              throw new Exception("Something is wrong with the source IP.");
            }
            else if (string.IsNullOrEmpty(tmpReq.DnsRequest))
            {
              throw new Exception("Something is wrong with the source host name.");
            }
            else if (string.IsNullOrEmpty(tmpReq.PacketType))
            {
              throw new Exception("Something is wrong with the source request type.");
            }
          }
          catch (Exception ex)
          {
            this.pluginProperties.HostApplication.LogMessage($"{Config.PluginName}: {ex.Message}");
            continue;
          }
          
          this.dnsRequests.Insert(0, tmpReq);
          if (firstVisibleRowTop > 0)
          {
            firstVisibleRowTop++;
          }

          if (selectedRowIndex > 0)
          {
            selectedRowIndex++;
          }
        }

        // Adjust and resume DataGridView
        try
        {
          while (this.dgv_DnsRequests.Rows.Count > this.maxRowNum)
          {
            this.dnsRequests.RemoveAt(this.dgv_DnsRequests.Rows.Count - 1);
          }

          if (firstVisibleRowTop >= 0)
          {
            this.dgv_DnsRequests.FirstDisplayedScrollingRowIndex = firstVisibleRowTop;
          }
        }
        catch (Exception)
        {
        }

        this.UseFilter();
      }

//      this.dgv_DnsRequests.Refresh();
      if (selectedRowIndex >= 0 &&
          this.dgv_DnsRequests.Rows.Count >= selectedRowIndex)
      {
        if (this.dgv_DnsRequests.SelectedRows.Count > 0)
        {
          this.dgv_DnsRequests.ClearSelection();
        }

        this.dgv_DnsRequests.Rows[selectedRowIndex].Selected = true;
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
        this.dgv_DnsRequests.SuspendLayout();

        try
        {
          this.dnsRequests.Clear();
        }
        catch (Exception)
        {
        }

        this.dgv_DnsRequests.ResumeLayout();
      }
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    public delegate void RemoveRecordAtDelegate(int index);
    public void RemoveRecordAt(int index)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new RemoveRecordAtDelegate(this.RemoveRecordAt), new object[] { index });
        return;
      }

      lock (this)
      {
        this.dgv_DnsRequests.SuspendLayout();

        try
        {
          this.dnsRequests.RemoveAt(index);
        }
        catch (Exception)
        {
        }

        this.dgv_DnsRequests.ResumeLayout();
      }
    }

    #endregion

  }
}