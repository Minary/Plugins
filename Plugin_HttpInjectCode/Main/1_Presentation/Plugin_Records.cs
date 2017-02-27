namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.InjectCode.DataTypes;
  using System;
  using System.IO;
  using System.Text.RegularExpressions;


  public partial class Plugin_HttpInjectCode
  {

    #region GUI RECORDS METHODS

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestedResource"></param>
    /// <param name="replacementResource"></param>
    /// <param name="tag"></param>
    /// <param name="position"></param>
    private delegate void AddRecordDelegate(string requestedResource, string replacementResource, string tag, string position);
    private void AddRecord(string requestedResource, string replacementResource, string tag, string position)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new AddRecordDelegate(this.AddRecord), new object[] { requestedResource, replacementResource, tag, position});
        return;
      }

      string requestedScheme = string.Empty;
      string requestedHost = string.Empty;
      string requestedPath = string.Empty;

      // Verify if requested URL resource is valid
      Uri requestedUri;
      bool isValidUri = Uri.TryCreate(requestedResource, UriKind.Absolute, out requestedUri);

      if (!isValidUri)
      {
        throw new Exception("The requested resource URL is invalid");
      }

      if (string.IsNullOrEmpty(requestedUri.Host) || string.IsNullOrWhiteSpace(requestedUri.Host))
      {
        throw new Exception("The requested URL host is invalid.");
      }

      if (string.IsNullOrEmpty(requestedUri.PathAndQuery) || string.IsNullOrWhiteSpace(requestedUri.PathAndQuery))
      {
        throw new Exception("The requested URL path is invalid.");
      }

      requestedScheme = requestedUri.Scheme;
      requestedHost = requestedUri.Host;
      requestedPath = requestedUri.PathAndQuery;

      // Verify if replacement file resource is valid
      if (!File.Exists(replacementResource))
      {
        throw new Exception("The injection file does not exist.");
      }

      // Verify if record already exists
      foreach (InjectCodeRecord tmpRecord in this.injectCodeRecords)
      {
        if (tmpRecord.RequestedHost == requestedHost &&
            tmpRecord.RequestedPath == requestedPath &&
            tmpRecord.Tag == tag)
        {
          throw new Exception("A record with this host name already exists.");
        }
      }

      // Verify if host name is correct
      if (!Regex.Match(requestedHost, @"^[\w\d\-_\.]+\.[a-z]{2,10}$", RegexOptions.IgnoreCase).Success)
      {
        throw new Exception("Something is wrong with the host name.");
      }

      // Verify if path is correct
      if (!Regex.Match(requestedPath, @"^/[^\s]*$", RegexOptions.IgnoreCase).Success)
      {
        throw new Exception("Something is wrong with the path.");
      }

      lock (this)
      {
        InjectCodeRecord newRecord = new InjectCodeRecord(requestedScheme, requestedHost, requestedPath, replacementResource, tag, position);

        this.dgv_InjectionTriggerURLs.SuspendLayout();
        this.injectCodeRecords.Insert(0, newRecord);
        this.dgv_InjectionTriggerURLs.ResumeLayout();
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

        if (this.dgv_InjectionTriggerURLs.CurrentRow != null && 
            this.dgv_InjectionTriggerURLs.CurrentRow == this.dgv_InjectionTriggerURLs.Rows[this.dgv_InjectionTriggerURLs.Rows.Count - 1])
        {
          isLastLine = true;
        }

        firstVisibleRowTopRow = this.dgv_InjectionTriggerURLs.FirstDisplayedScrollingRowIndex;
        lastRowIndex = this.dgv_InjectionTriggerURLs.Rows.Count - 1;

        if (this.dgv_InjectionTriggerURLs.CurrentCell != null)
        {
          selectedIndex = this.dgv_InjectionTriggerURLs.CurrentCell.RowIndex;
        }

        this.dgv_InjectionTriggerURLs.SuspendLayout();
        this.dgv_InjectionTriggerURLs.BeginEdit(true);
        this.dgv_InjectionTriggerURLs.RefreshEdit();

        try
        {
          int currentIndex = this.dgv_InjectionTriggerURLs.CurrentCell.RowIndex;
          this.injectCodeRecords.RemoveAt(currentIndex);
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
            this.dgv_InjectionTriggerURLs.CurrentCell = this.dgv_InjectionTriggerURLs.Rows[selectedIndex].Cells[0];
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
            this.dgv_InjectionTriggerURLs.FirstDisplayedScrollingRowIndex = firstVisibleRowTopRow;
          }
        }
        catch (Exception)
        {
        }

        this.dgv_InjectionTriggerURLs.ResumeLayout();
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
        this.dgv_InjectionTriggerURLs.SuspendLayout();

        try
        {
          this.injectCodeRecords.RemoveAt(index);
        }
        catch (Exception)
        {
        }

        this.dgv_InjectionTriggerURLs.ResumeLayout();
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
        this.dgv_InjectionTriggerURLs.SuspendLayout();

        try
        {
          this.injectCodeRecords.Clear();
        }
        catch (Exception)
        {
        }

        this.dgv_InjectionTriggerURLs.ResumeLayout();
      }
    }

    #endregion

  }
}
