namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.HttpSearch.DataTypes;
  using System;
  using System.Windows.Forms;


  public partial class Plugin_HttpSearch
  {

    #region GUI RECORDS METHODS

    /// <summary>
    ///
    /// </summary>
    /// <param name="newRecord"></param>
    private delegate void AddRecordDelegate(RecordHttpSearch newRecord);
    private void AddRecord(RecordHttpSearch newRecord)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new AddRecordDelegate(this.AddRecord), new object[] { newRecord });
        return;
      }

      var firstVisibleRowTop = -1;

      // Verify if Hostname and IPaddress for correctness.


      // Ensure if host/ip combination does not exist.
      foreach (RecordHttpSearch tmpRecord in this.dnsPoisonRecords)
      {
        //if (tmpRecord.HostName.ToLower() == newRecord.HostName.ToLower())
        //{
        //  throw new Exception("An entry for this hostname already exists");
        //}
      }

      lock (this)
      {
        // Memorize DataGridView position and selection
        firstVisibleRowTop = this.dgv_HttpSearch.FirstDisplayedScrollingRowIndex;

        // Update DataGridView
        this.dgv_HttpSearch.SuspendLayout();

        try
        {
 //         this.dnsPoisonRecords.Insert(0, newRecord);

          while (this.dgv_HttpSearch.Rows.Count > this.maxRowNum)
          {
 //           this.dnsPoisonRecords.RemoveAt(this.dgv_HttpSearch.Rows.Count - 1);
          }

          if (firstVisibleRowTop >= 0)
          {
            this.dgv_HttpSearch.FirstDisplayedScrollingRowIndex = firstVisibleRowTop;
          }
        }
        catch (Exception)
        {
        }

        this.dgv_HttpSearch.ResumeLayout();
      }
    }

    #endregion

  }
}



