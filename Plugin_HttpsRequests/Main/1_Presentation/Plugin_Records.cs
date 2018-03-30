namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.HttpsRequest.DataTypes;
  using System;
  using System.Collections.Generic;


  public partial class Plugin_HttpsRequests
  {

    #region GUI RECORDS METHODS

    /// <summary>
    ///
    /// </summary>
    /// <param name="newRecords"></param>
    private delegate void AddRecordsDelegate(List<RecordHttpsRequest> newRecords);
    private void AddRecords(List<RecordHttpsRequest> newRecords)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new AddRecordsDelegate(this.AddRecords), new object[] { newRecords });
        return;
      }

      var firstVisibleRowTop = -1;
      lock (this)
      {
        // Memorize DataGridView position and selection
        firstVisibleRowTop = this.dgv_HttpsRequests.FirstDisplayedScrollingRowIndex;

        
        // Update DataGridView
        try
        {
          foreach (RecordHttpsRequest tmpRecord in newRecords)
          {
            this.foundHttpsRequests.Insert(0, tmpRecord);
          }

          // If the table contains more elements than defined by the MAX
          // remove elements from the bottom until MAX num. of elements is reached
          while (this.foundHttpsRequests.Count > MaxTableRows)
          {
            this.foundHttpsRequests.RemoveAt(this.dgv_HttpsRequests.Rows.Count - 1);
          }

          if (firstVisibleRowTop >= 0)
          {
            this.dgv_HttpsRequests.FirstDisplayedScrollingRowIndex = firstVisibleRowTop;
          }
        }
        catch (Exception)
        {
        }

        this.UseFilter();     
      }
    }


    /// <summary>
    ///
    /// </summary>
    private delegate void DeleteSelectedRecordDelegate();
    private void DeleteSelectedRecord()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new DeleteSelectedRecordDelegate(this.DeleteSelectedRecord), new object[] { });
        return;
      }

      var firstVisibleRowTop = -1;
      var lastRowIndex = -1;

      lock (this)
      {
        firstVisibleRowTop = this.dgv_HttpsRequests.FirstDisplayedScrollingRowIndex;
        lastRowIndex = this.dgv_HttpsRequests.Rows.Count - 1;

        this.dgv_HttpsRequests.SuspendLayout();

        try
        {
          var currentIndex = this.dgv_HttpsRequests.CurrentCell.RowIndex;
          //// string lHostName = DGV_Spoofing.Rows[currentIndex].Cells["HostName"].Value.ToString();
          //// httpRequests.RemoveAt(currentIndex);
        }
        catch (Exception ex)
        {
          this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
        }

        this.dgv_HttpsRequests.ResumeLayout();
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
        this.dgv_HttpsRequests.SuspendLayout();

        try
        {
          this.foundHttpsRequests.Clear();
        }
        catch (Exception)
        {
        }

        this.dgv_HttpsRequests.ResumeLayout();
      }

      this.Refresh();
    }

    #endregion

  }
}
