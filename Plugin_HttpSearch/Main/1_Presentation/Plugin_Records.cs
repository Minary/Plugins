namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.HttpSearch.DataTypes;
  using System;
  using System.Text.RegularExpressions;


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

      // Verify whether a comparable record  already exists.
      foreach (RecordHttpSearch tmpRecord in this.httpSearchRecords)
      {
        if (tmpRecord.Method == newRecord.Method &&
            tmpRecord.Domain == newRecord.Domain &&
            tmpRecord.HostRegex == newRecord.HostRegex &&
            tmpRecord.PathRegex == newRecord.PathRegex &&
            tmpRecord.DataRegex == newRecord.DataRegex)
        {
          throw new Exception("This entry already exists");
        }
      }

      // Verify if regular expressions are valid.
      if (string.IsNullOrEmpty(newRecord.HostRegex) ||
          this.IsRegexPatternValid(newRecord.HostRegex) == false)
      {
        throw new Exception("The host regular expression is invalid");
      }

      if (string.IsNullOrEmpty(newRecord.PathRegex) ||
          this.IsRegexPatternValid(newRecord.PathRegex) == false)
      {
        throw new Exception("The path regular expression is invalid");
      }

      if (string.IsNullOrEmpty(newRecord.DataRegex) ||
          this.IsRegexPatternValid(newRecord.DataRegex) == false)
      {
        throw new Exception("The data regular expression is invalid");
      }

      lock (this)
      {
        // Memorize DataGridView position and selection
        firstVisibleRowTop = this.dgv_HttpSearch.FirstDisplayedScrollingRowIndex;

        // Update DataGridView
        this.dgv_HttpSearch.SuspendLayout();

        try
        {
          this.httpSearchRecords.Insert(0, newRecord);

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


    #region PRIVATE

    private bool IsRegexPatternValid(string pattern)
    {
      var isValid = false;

      try
      {
        new Regex(pattern);
        isValid = true;
      }
      catch
      {
      }

      return isValid;
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

      var isLastLine = false;
      var firstVisibleRowTopRow = -1;
      var lastRowIndex = -1;
      var selectedIndex = -1;

      lock (this)
      {

        if (this.dgv_HttpSearch?.CurrentRow == this.dgv_HttpSearch.Rows[this.dgv_HttpSearch.Rows.Count - 1])
        {
          isLastLine = true;
        }

        firstVisibleRowTopRow = this.dgv_HttpSearch.FirstDisplayedScrollingRowIndex;
        lastRowIndex = this.dgv_HttpSearch.Rows.Count - 1;

        if (this.dgv_HttpSearch.CurrentCell != null)
        {
          selectedIndex = this.dgv_HttpSearch.CurrentCell.RowIndex;
        }

        this.dgv_HttpSearch.SuspendLayout();
        this.dgv_HttpSearch.BeginEdit(true);
        this.dgv_HttpSearch.RefreshEdit();

        try
        {
          var currentIndex = this.dgv_HttpSearch.CurrentCell.RowIndex;
          this.httpSearchRecords.RemoveAt(currentIndex);
        }
        catch (Exception ex)
        {
          this.Config.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
        }

        // Selected cell/row
        try
        {
          if (selectedIndex >= 0)
          {
            this.dgv_HttpSearch.CurrentCell = this.dgv_HttpSearch.Rows[selectedIndex].Cells[0];
          }
        }
        catch (Exception)
        {
        }

        // Reset position
        try
        {
          if (firstVisibleRowTopRow >= 0)
          {
            this.dgv_HttpSearch.FirstDisplayedScrollingRowIndex = firstVisibleRowTopRow;
          }
        }
        catch (Exception)
        {
        }

        this.dgv_HttpSearch.ResumeLayout();
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
        this.dgv_HttpSearch.SuspendLayout();

        try
        {
          this.httpSearchRecords.Clear();
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
