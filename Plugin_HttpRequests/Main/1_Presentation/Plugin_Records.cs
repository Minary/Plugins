namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.HttpRequest.DataTypes;
  using System;
  using System.Collections.Generic;


  public partial class Plugin_HttpRequests
  {

    #region GUI RECORDS METHODS

    /// <summary>
    ///
    /// </summary>
    /// <param name="newRecords"></param>
    private delegate void AddRecordsDelegate(List<HttpRequests> newRecords);
    private void AddRecords(List<HttpRequests> newRecords)
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
        firstVisibleRowTop = this.dgv_HttpRequests.FirstDisplayedScrollingRowIndex;

        // Update DataGridView
        this.dgv_HttpRequests.SuspendLayout();

        try
        {
          foreach (HttpRequests tmpRecord in newRecords)
          {
            this.foundHttpRequests.Insert(0, tmpRecord);
          }

          // If the table contains more elements than defined by the MAX
          // remove elements from the bottom until MAX num. of elements is reached
          while (this.foundHttpRequests.Count > MaxTableRows)
          {
            this.foundHttpRequests.RemoveAt(this.dgv_HttpRequests.Rows.Count - 1);
          }

          if (firstVisibleRowTop >= 0)
          {
            this.dgv_HttpRequests.FirstDisplayedScrollingRowIndex = firstVisibleRowTop;
          }
        }
        catch (Exception)
        {
        }

        this.UseFilter();
        this.dgv_HttpRequests.ResumeLayout();
        this.dgv_HttpRequests.Refresh();
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
        firstVisibleRowTop = this.dgv_HttpRequests.FirstDisplayedScrollingRowIndex;
        lastRowIndex = this.dgv_HttpRequests.Rows.Count - 1;

        this.dgv_HttpRequests.SuspendLayout();

        try
        {
          var currentIndex = this.dgv_HttpRequests.CurrentCell.RowIndex;
          //// string lHostName = DGV_Spoofing.Rows[currentIndex].Cells["HostName"].Value.ToString();
          //// httpRequests.RemoveAt(currentIndex);
        }
        catch (Exception ex)
        {
          this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
        }

        this.dgv_HttpRequests.ResumeLayout();
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
        this.dgv_HttpRequests.SuspendLayout();

        try
        {
          this.foundHttpRequests.Clear();
        }
        catch (Exception)
        {
        }

        this.dgv_HttpRequests.ResumeLayout();
      }

      this.Refresh();
    }

    #endregion

  }
}