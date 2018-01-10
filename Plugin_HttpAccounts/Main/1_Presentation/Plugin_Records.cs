namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.HttpAccounts.DataTypes;
  using System;
  using System.Collections.Generic;
  using System.Linq;

  public partial class Plugin_HttpAccounts
  {

    #region GUI RECORDS METHODS

    /// <summary>
    ///
    /// </summary>
    /// <param name="record"></param>
    private void AddRecords(List<AccountRecord> records)
    {
      var firstVisibleRowTop = -1;

      if (records?.Count > 0 == false)
      {
        return;
      }

      // Memorize DataGridView position and selection
      firstVisibleRowTop = this.dgv_Accounts.FirstDisplayedScrollingRowIndex;

      // Update DataGridView
      this.dgv_Accounts.SuspendLayout();

      lock (this)
      {
        foreach (AccountRecord newAccount in records)
        {
          if (this.accountRecords.Select(elem => elem.Username == newAccount.Username && elem.Password == newAccount.Password && newAccount.DstIP == elem.DstIP && elem.DstPort == newAccount.DstPort).Count() > 0)
          {
            this.Config.HostApplication.LogMessage($"{Config.PluginName}: The account record for user \"{newAccount.Username}\" and password \"{newAccount.Password}\" already exists");
          }
          else
          {
            this.accountRecords.Insert(0, newAccount);
          }
        }

        // Resize the DGV to the defined maximum size.
        while (this.accountRecords.Count > MaxTableRows)
        {
          this.accountRecords.RemoveAt(this.dgv_Accounts.Rows.Count - 1);
        }

        this.dgv_Accounts.ResumeLayout();
      }
    }



    /// <summary>
    ///
    /// </summary>
    /// <param name="recordIndex"></param>
    private void RemoveRecordAt(int recordIndex)
    {
      lock (this)
      {
        this.dgv_Accounts.SuspendLayout();

        try
        {
          this.accountRecords.RemoveAt(recordIndex);
        }
        catch (Exception)
        {
        }

        this.dgv_Accounts.ResumeLayout();
      }
    }


    /// <summary>
    ///
    /// </summary>
    private void ClearRecordList()
    {
      lock (this)
      {
        this.dgv_Accounts.SuspendLayout();

        try
        {
          this.accountRecords.Clear();
        }
        catch (Exception)
        {
        }

        this.dgv_Accounts.ResumeLayout();
      }
    }

    #endregion

  }
}