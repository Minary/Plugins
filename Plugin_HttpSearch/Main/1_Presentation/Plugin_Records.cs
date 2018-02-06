namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.HttpSearch.DataTypes.Class;
  using System;
  using System.Collections.Generic;
  using System.Text.RegularExpressions;


  public partial class Plugin_HttpSearch
  {

    #region GUI RECORDS METHODS

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
    
    #endregion

  }
}
