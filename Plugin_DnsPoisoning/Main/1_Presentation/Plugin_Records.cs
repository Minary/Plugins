namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.DnsPoisoning.DataTypes;
  using System;
  using System.Collections.Generic;
  using System.Text.RegularExpressions;
  using System.Windows.Forms;


  public partial class Plugin_DnsPoisoning
  {

    #region PUBLIC

    public void VerifyInputData(RecordDnsPoison newRecord)
    {
      // Verify whether Hostname and IPaddress for correctness.
      if (this.VerifyHostNameStructure(newRecord.HostName) == false)
      {
        throw new Exception("Something is wrong with the host name");
      }

      if (this.VerifyIpAddressStructure(newRecord.IpAddress) == false)
      {
        throw new Exception("Something is wrong with the IP address");
      }

      if (newRecord.ResponseType == DnsResponseType.CNAME &&
          this.VerifyCNameStructure(newRecord.CName) == false)
      {
        throw new Exception("Something is wrong with the CName host name");
      }

      // Verify whether TTL has a valid value
      long ttl = 0;
      if (Regex.Match(this.tb_ttl.Text, @"^\d{1,10}$").Success == false ||
          long.TryParse(this.tb_ttl.Text, out ttl) == false ||
          ttl < 1 ||
          ttl > 4294967296)
      {
        throw new Exception("Something is wront with the TTL.\r\nValue must be 1-4'294'967'296");
      }

      // Ensure that host/ip combination does not exist.
      foreach (RecordDnsPoison tmpRecord in this.dnsPoisonRecords)
      {
        if (tmpRecord.HostName.ToLower() == newRecord.HostName.ToLower())
        {
          throw new Exception("An entry for this hostname already exists");
        }
      }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="hostName"></param>
    /// <returns></returns>
    public bool VerifyHostNameStructure(string hostName)
    {
      string validHostNamePattern = @"[\d\w\.-_\*]+\.[\*\w]{1,}$";

      if (string.IsNullOrEmpty(hostName))
      {
        throw new Exception("You didn't define a host name");
      }
      else if (!Regex.Match(hostName, validHostNamePattern, RegexOptions.IgnoreCase).Success)
      {
        throw new Exception("Something is wrong with the host name");
      }

      return true;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="hostName"></param>
    /// <returns></returns>
    public bool VerifyCNameStructure(string hostName)
    {
      string validHostNamePattern = @"[\d\w\.-_]+\.[\w]{2,3}$";

      if (string.IsNullOrEmpty(hostName))
      {
        throw new Exception("You didn't define a CNAME host name ");
      }
      else if (!Regex.Match(hostName, validHostNamePattern, RegexOptions.IgnoreCase).Success)
      {
        throw new Exception("Something is wrong with the CNAME host name");
      }

      return true;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="ipAddress"></param>
    /// <returns></returns>
    public bool VerifyIpAddressStructure(string ipAddress)
    {
      string ipAddressPattern = @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$";

      if (string.IsNullOrEmpty(ipAddress))
      {
        throw new Exception("You didn't define a IP address");
      }
      else if (!Regex.Match(ipAddress, ipAddressPattern, RegexOptions.IgnoreCase).Success)
      {
        throw new Exception("Something is wrong with the IP address");
      }

      return true;
    }

    #endregion


    #region GUI RECORDS METHODS

    /// <summary>
    ///
    /// </summary>
    /// <param name="newRecord"></param>
    private delegate void AddRecordDelegate(RecordDnsPoison newRecord);
    private void AddRecord(RecordDnsPoison newRecord)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new AddRecordDelegate(this.AddRecord), new object[] { newRecord });
        return;
      }

      var firstVisibleRowTop = -1;
      this.VerifyInputData(newRecord);

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
    private void DeleteSelectedRecords()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new DeleteSelectedRecordDelegate(this.DeleteSelectedRecords), new object[] { });
        return;
      }

      var isLastLine = false;
      var firstVisibleRowTopRow = -1;
      var lastRowIndex = -1;
      var selectedIndex = -1;

      lock (this)
      {
        if (this.dgv_Spoofing?.CurrentRow == this.dgv_Spoofing.Rows[this.dgv_Spoofing.Rows.Count - 1])
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
          this.RemoveSelectedItems();
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


    private void RemoveSelectedItems()
    {
      var tmpList = new List<RecordDnsPoison>();
      foreach (DataGridViewRow elem in this.dgv_Spoofing.SelectedRows)
      {
        tmpList.Add(this.dnsPoisonRecords[elem.Index]);
      }

      tmpList.ForEach(elem => this.dnsPoisonRecords.Remove(elem));
    }
    
    #endregion

  }
}
