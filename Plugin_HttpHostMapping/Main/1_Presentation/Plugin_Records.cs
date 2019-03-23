namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.HostMapping.DataTypes;
  using System;
  using System.Text.RegularExpressions;
  using System.Windows.Forms;


  public partial class Plugin_HttpHostMapping : UserControl
  {

    #region GUI RECORDS METHODS

    /// <summary>
    ///
    /// </summary>
    /// <param name="pRecord"></param>
    private delegate void AddRecordDelegate(string requestedHost, string mappedHost);
    private void AddRecord(string requestedHost, string mappedHost)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new AddRecordDelegate(this.AddRecord), new object[] { requestedHost, mappedHost });
        return;
      }

      // Verify if requested host is correct
      if (string.IsNullOrEmpty(requestedHost) || 
          string.IsNullOrWhiteSpace(requestedHost) || 
          !Regex.Match(requestedHost, @"[\d\w\.\-_]+").Success)
      {
        throw new Exception("Requested host is invalid");
      }

      // Verify if mapped host is correct
      if (string.IsNullOrEmpty(mappedHost) || 
          string.IsNullOrWhiteSpace(mappedHost) || 
          !Regex.Match(requestedHost, @"\*?[\d\w\.\-_]+\*?").Success)
      {
        throw new Exception("Mapped host is invalid");
      }

      // Verify if requested host does not exist yet
      foreach (HostMappingRecord tmpRecord in this.hostMappingRecords)
      {
        if (tmpRecord.RequestedHost == requestedHost.Trim())
        {
          throw new Exception("A record for this requested host already exists");
        }
      }

      // Create new record in the mapping list
      lock (this)
      {
        requestedHost = requestedHost.Trim();
        mappedHost = mappedHost.Trim();
        HostMappingRecord newRecord = new HostMappingRecord(requestedHost, mappedHost);

        this.dgv_HostMapping.SuspendLayout();
        this.hostMappingRecords.Insert(0, newRecord);
        this.dgv_HostMapping.ResumeLayout();
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
      
      var firstVisibleRowTopRow = -1;
      var lastRowIndex = -1;
      var selectedIndex = -1;

      lock (this)
      {
        firstVisibleRowTopRow = this.dgv_HostMapping.FirstDisplayedScrollingRowIndex;
        lastRowIndex = this.dgv_HostMapping.Rows.Count - 1;

        if (this.dgv_HostMapping.CurrentCell != null)
        {
          selectedIndex = this.dgv_HostMapping.CurrentCell.RowIndex;
        }

        this.dgv_HostMapping.SuspendLayout();
        this.dgv_HostMapping.BeginEdit(true);
        this.dgv_HostMapping.RefreshEdit();

        try
        {
          var currentIndex = this.dgv_HostMapping.CurrentCell.RowIndex;
          this.hostMappingRecords.RemoveAt(currentIndex);
        }
        catch (Exception ex)
        {
          this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
        }

        // Selected cell/row
        try
        {
          if (selectedIndex >= 0)
          {
            this.dgv_HostMapping.CurrentCell = this.dgv_HostMapping.Rows[selectedIndex].Cells[0];
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
            this.dgv_HostMapping.FirstDisplayedScrollingRowIndex = firstVisibleRowTopRow;
          }
        }
        catch (Exception)
        {
        }

        this.dgv_HostMapping.ResumeLayout();
      }
    }


    private delegate void RemoveRecordAtDelegate(int index);
    private void RemoveRecordAt(int index)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new RemoveRecordAtDelegate(this.RemoveRecordAt), new object[] { index });
        return;
      }

      lock (this)
      {
        this.dgv_HostMapping.SuspendLayout();

        try
        {
          this.hostMappingRecords.RemoveAt(index);
        }
        catch (Exception)
        {
        }

        this.dgv_HostMapping.ResumeLayout();
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
        this.dgv_HostMapping.SuspendLayout();

        try
        {
          this.hostMappingRecords.Clear();
        }
        catch (Exception)
        {
        }

        this.dgv_HostMapping.ResumeLayout();
      }
    }

    #endregion

  }
}