namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.DnsPoison.DataTypes;
  using System;
  using System.Text.RegularExpressions;

 
  public partial class Plugin_DnsPoisoning
  {

    #region GUI RECORDS METHODS

    /// <summary>
    ///
    /// </summary>
    /// <param name="newRecord"></param>
    private delegate void AddRecordDelegate(RecordDnsPoison newRecord);
    private void AddRecord(Minary.Plugin.Main.DnsPoison.DataTypes.RecordDnsPoison newRecord)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new AddRecordDelegate(this.AddRecord), new object[] { newRecord });
        return;
      }

      int firstVisibleRowTop = -1;
      string hostNamePattern = @"[\d\w\.-_]+\.[\w]{2,3}$";
      string ipAddressPattern = @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$";

      // Verify if Hostname and IPaddress for correctness.
      if (string.IsNullOrEmpty(newRecord.HostName))
      {
        throw new Exception("You didn't define a host name");
      }
      else if (string.IsNullOrEmpty(newRecord.IpAddress))
      {
        throw new Exception("You didn't define a IP address");
      }
      else if (!Regex.Match(newRecord.HostName, hostNamePattern, RegexOptions.IgnoreCase).Success)
      {
        throw new Exception("Something is wrong with the host name");
      }
      else if (!Regex.Match(newRecord.IpAddress, ipAddressPattern, RegexOptions.IgnoreCase).Success)
      {
        throw new Exception("Something is wrong with the IP address");
      }

      // Ensure if host/ip combination does not exist.
      foreach (RecordDnsPoison tmpRecord in this.dnsPoisonRecords)
      {
        if (tmpRecord.HostName.ToLower() == newRecord.HostName.ToLower())
        {
          throw new Exception(string.Format("An entry with this hostname already exists"));
        }
      }

      lock (this)
      {
        // Memorize DataGridView position and selection
        firstVisibleRowTop = this.dgv_Spoofing.FirstDisplayedScrollingRowIndex;

        // Update DataGridView
        this.dgv_Spoofing.SuspendLayout();

        try
        {
          this.dnsPoisonRecords.Insert(0, newRecord);

          while (this.dgv_Spoofing.Rows.Count > this.maxRowNum)
          {
            this.dnsPoisonRecords.RemoveAt(this.dgv_Spoofing.Rows.Count - 1);
          }

          if (firstVisibleRowTop >= 0)
          {
            this.dgv_Spoofing.FirstDisplayedScrollingRowIndex = firstVisibleRowTop;
          }
        }
        catch (Exception)
        {
        }

        this.dgv_Spoofing.ResumeLayout();
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

      bool isLastLine = false;
      int firstVisibleRowTopRow = -1;
      int lastRowIndex = -1;
      int selectedIndex = -1;

      lock (this)
      {

        if (this.dgv_Spoofing.CurrentRow != null && this.dgv_Spoofing.CurrentRow == this.dgv_Spoofing.Rows[this.dgv_Spoofing.Rows.Count - 1])
        {
          isLastLine = true;
        }

        firstVisibleRowTopRow = this.dgv_Spoofing.FirstDisplayedScrollingRowIndex;
        lastRowIndex = this.dgv_Spoofing.Rows.Count - 1;

        if (this.dgv_Spoofing.CurrentCell != null)
        {
          selectedIndex = this.dgv_Spoofing.CurrentCell.RowIndex;
        }

        this.dgv_Spoofing.SuspendLayout();
        this.dgv_Spoofing.BeginEdit(true);
        this.dgv_Spoofing.RefreshEdit();

        try
        {
          int currentIndex = this.dgv_Spoofing.CurrentCell.RowIndex;
          this.dnsPoisonRecords.RemoveAt(currentIndex);
        }
        catch (Exception ex)
        {
          this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
        }

        // Selected cell/row
        try
        {
          if (selectedIndex >= 0)
          {
            this.dgv_Spoofing.CurrentCell = this.dgv_Spoofing.Rows[selectedIndex].Cells[0];
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
            this.dgv_Spoofing.FirstDisplayedScrollingRowIndex = firstVisibleRowTopRow;
          }
        }
        catch (Exception)
        {
        }

        this.dgv_Spoofing.ResumeLayout();
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
        this.dgv_Spoofing.SuspendLayout();

        try
        {
          this.dnsPoisonRecords.Clear();
        }
        catch (Exception)
        {
        }

        this.dgv_Spoofing.ResumeLayout();
      }
    }

    #endregion

  }
}
