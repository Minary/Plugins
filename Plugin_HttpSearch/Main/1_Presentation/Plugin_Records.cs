namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.HttpSearch.DataTypes.Class;
  using System;
  using System.Collections.Generic;
  using System.Text.RegularExpressions;


  public partial class Plugin_HttpSearch
  {

    #region GUI RECORDS METHODS

    /// <summary>
    ///
    /// </summary>
    /// <param name="record"></param>
    private void AddRecords(List<RecordHttpSearch> records)
    {
      var firstVisibleRowTop = -1;

      if (records?.Count > 0 == false)
      {
        return;
      }

      // Memorize DataGridView position and selection
      firstVisibleRowTop = this.dgv_HttpSearch.FirstDisplayedScrollingRowIndex;

      // Update DataGridView
      this.dgv_HttpSearch.SuspendLayout();

      lock (this)
      {
        foreach (RecordHttpSearch newAccount in records)
        {
          this.httpSearchRecords.Insert(0, newAccount);
          //if (this.httpSearchRecords.Select(elem => elem.Username == newAccount.Username && elem.Password == newAccount.Password && newAccount.DstIP == elem.DstIP && elem.DstPort == newAccount.DstPort).Count() > 0)
          //{
          //  this.Config.HostApplication.LogMessage($"{Config.PluginName}: The account record for user \"{newAccount.Username}\" and password \"{newAccount.Password}\" already exists");
          //}
          //else
          //{
          //  this.httpSearchRecords.Insert(0, newAccount);
          //}
        }

        // Resize the DGV to the defined maximum size.
        while (this.httpSearchRecords.Count > maxRowNum)
        {
          this.httpSearchRecords.RemoveAt(this.dgv_HttpSearch.Rows.Count - 1);
        }

        this.dgv_HttpSearch.ResumeLayout();
      }
    }


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

      this.infrastructureLayer.AddRecord(newRecord);
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
