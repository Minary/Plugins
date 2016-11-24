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
    private delegate void AddRecordDelegate(string requestedHost, string mappedHost, string scheme);
    private void AddRecord(string requestedHost, string scheme, string mappedHost)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new AddRecordDelegate(this.AddRecord), new object[] { requestedHost, mappedHost });
        return;
      }

      // Verify if requested host is correct
      if (string.IsNullOrEmpty(requestedHost) || string.IsNullOrWhiteSpace(requestedHost) || !Regex.Match(requestedHost, @"[\d\w\.\-_]+").Success)
      {
        throw new Exception("Requested host is invalid");
      }

      // Verify if mapped host is correct
      if (string.IsNullOrEmpty(mappedHost) || string.IsNullOrWhiteSpace(mappedHost) || !Regex.Match(requestedHost, @"[\d\w\.\-_]+").Success)
      {
        throw new Exception("Mapped host is invalid");
      }

      // Verify if scheme is correct
      if (string.IsNullOrEmpty(scheme) || !Regex.Match("^(http|https)$", scheme).Success)
      {
        throw new Exception("Mapped host scheme is invalid");
      }

      // Verify if mapped host has a valid URL structure
      //string requestedResource = mappedHost;
      //Uri requestedUri;
      //if (!requestedResource.StartsWith("http"))
      //{
      //  requestedResource = "http://" + requestedResource;
      //}

      //if (Uri.TryCreate(requestedResource, UriKind.Absolute, out requestedUri) == false)
      //{
      //  throw new Exception("The mapping address is invalid");
      //}

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
        HostMappingRecord newRecord = new HostMappingRecord(requestedHost, scheme, mappedHost);

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

      bool isLastLine = false;
      int firstVisibleRowTopRow = -1;
      int lastRowIndex = -1;
      int selectedIndex = -1;

      lock (this)
      {

        if (this.dgv_HostMapping.CurrentRow != null &&
            this.dgv_HostMapping.CurrentRow == this.dgv_HostMapping.Rows[this.dgv_HostMapping.Rows.Count - 1])
        {
          isLastLine = true;
        }

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
          int currentIndex = this.dgv_HostMapping.CurrentCell.RowIndex;
          this.hostMappingRecords.RemoveAt(currentIndex);
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