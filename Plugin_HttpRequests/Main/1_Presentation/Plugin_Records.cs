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
    private delegate void AddRecordsDelegate(List<HTTPRequests> newRecords);
    private void AddRecords(List<HTTPRequests> newRecords)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new AddRecordsDelegate(this.AddRecords), new object[] { newRecords });
        return;
      }

      this.pluginProperties.HostApplication.LogMessage("Plugin_HttpRequests.AddRecords(): {0}", newRecords.Count);
      int firstVisibleRowTop = -1;

      lock (this)
      {
        // Memorize DataGridView position and selection
        firstVisibleRowTop = this.dgv_HTTPRequests.FirstDisplayedScrollingRowIndex;

        // Update DataGridView
        this.dgv_HTTPRequests.SuspendLayout();

        try
        {
          foreach (HTTPRequests tmpRecord in newRecords)
          {
            this.httpRequests.Insert(0, tmpRecord);
          }

          while (this.httpRequests.Count > MaxTableRows)
          {
            this.httpRequests.RemoveAt(dgv_HTTPRequests.Rows.Count - 1);
          }

          if (firstVisibleRowTop >= 0)
          {
            this.dgv_HTTPRequests.FirstDisplayedScrollingRowIndex = firstVisibleRowTop;
          }
        }
        catch (Exception)
        {
        }

        this.dgv_HTTPRequests.ResumeLayout();
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

      int firstVisibleRowTop = -1;
      int lastRowIndex = -1;


      lock (this)
      {
        firstVisibleRowTop = this.dgv_HTTPRequests.FirstDisplayedScrollingRowIndex;
        lastRowIndex = this.dgv_HTTPRequests.Rows.Count - 1;

        this.dgv_HTTPRequests.SuspendLayout();

        try
        {
          int currentIndex = this.dgv_HTTPRequests.CurrentCell.RowIndex;
          //// string lHostName = DGV_Spoofing.Rows[currentIndex].Cells["HostName"].Value.ToString();
          //// httpRequests.RemoveAt(currentIndex);
        }
        catch (Exception ex)
        {
          this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
        }

        // Reset position
        try
        {
        }
        catch (Exception ex)
        {
          this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
        }

        this.dgv_HTTPRequests.ResumeLayout();
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
        this.dgv_HTTPRequests.SuspendLayout();

        try
        {
          this.httpRequests.Clear();
        }
        catch (Exception)
        {
        }

        this.dgv_HTTPRequests.ResumeLayout();
      }
    }

    #endregion

  }
}