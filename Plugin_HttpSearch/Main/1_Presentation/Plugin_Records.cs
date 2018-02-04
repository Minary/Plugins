namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.HttpSearch.DataTypes.Class;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Text.RegularExpressions;
  using System.Windows.Forms;


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

      this.infrastructureLayer.AddSearchPatternRecord(newRecord);
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
    private delegate void DeleteSelectedRecordDelegate<T>(DataGridView dgv, BindingList<T> dataList);
    private void DeleteSelectedRecord<T>(DataGridView dgv, BindingList<T> dataList)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new DeleteSelectedRecordDelegate<T>(this.DeleteSelectedRecord), new object[] { dgv, dataList });
        return;
      }

      if (dgv.CurrentCell == null)
      {
        return;
      }

     


      var isLastLine = false;
      var firstVisibleRowTopRow = -1;
      var lastRowIndex = -1;
      var selectedIndex = dgv.CurrentCell.RowIndex;


      lock (this)
      {
        if (dgv?.CurrentRow == dgv.Rows[dgv.Rows.Count - 1])
        {
          isLastLine = true;
        }

        firstVisibleRowTopRow = dgv.FirstDisplayedScrollingRowIndex;
        lastRowIndex = dgv.Rows.Count - 1;

/*
        dgv.SuspendLayout();
        dgv.BeginEdit(true);
        dgv.RefreshEdit();

        try
        {
          var currentIndex = dgv.CurrentCell.RowIndex;
          dataList.RemoveAt(currentIndex);
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
            dgv.CurrentCell = dgv.Rows[selectedIndex].Cells[0];
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
            dgv.FirstDisplayedScrollingRowIndex = firstVisibleRowTopRow;
          }
        }
        catch (Exception)
        {
        }

        dgv.ResumeLayout();
*/
      }
    }

    #endregion

  }
}
