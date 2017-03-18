namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.RequestRedirect.DataTypes;
  using System;
  using System.Text.RegularExpressions;


  public partial class Plugin_HttpRequestRedirect
  {

    #region GUI RECORDS METHODS

    /// <summary>
    ///
    /// </summary>
    /// <param name="pRecord"></param>
    private delegate void AddRecordDelegate(string requestedResource, string replacementResource, string redirectType, string redirectDescription);
    private void AddRecord(string requestedResource, string replacementResource, string redirectType, string redirectDescription)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new AddRecordDelegate(this.AddRecord), new object[] { requestedResource, replacementResource, redirectType, redirectDescription });
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

      if (requestedUri.Scheme != Uri.UriSchemeHttp && requestedUri.Scheme != Uri.UriSchemeHttps)
      {
        throw new Exception("The requested URL scheme is invalid.");
      }

      if (string.IsNullOrEmpty(requestedUri.Host) || string.IsNullOrWhiteSpace((requestedUri.Host)))
      {
        throw new Exception("The requested URL host is invalid.");
      }

      if (string.IsNullOrEmpty(requestedUri.PathAndQuery) || string.IsNullOrWhiteSpace((requestedUri.PathAndQuery)))
      {
        throw new Exception("The requested URL path is invalid.");
      }

      requestedScheme = requestedUri.Scheme;
      requestedHost = requestedUri.Host;
      requestedPath = requestedUri.PathAndQuery;

      // Verify if replacement URL resource is valid
      Uri replacementUri;
      bool isValidReplUri = Uri.TryCreate(replacementResource, UriKind.Absolute, out replacementUri);

      if (!isValidReplUri)
      {
        throw new Exception("The replacement resource URL is invalid");
      }

      if (replacementUri.Scheme != Uri.UriSchemeHttp && replacementUri.Scheme != Uri.UriSchemeHttps)
      {
        throw new Exception("The replacement URL scheme is invalid.");
      }

      if (string.IsNullOrEmpty(replacementUri.Host) || string.IsNullOrWhiteSpace((replacementUri.Host)))
      {
        throw new Exception("The replacement URL host is invalid.");
      }

      if (string.IsNullOrEmpty(replacementUri.PathAndQuery) || string.IsNullOrWhiteSpace((replacementUri.PathAndQuery)))
      {
        throw new Exception("The replacement URL path is invalid.");
      }

      // Verify if record already exists
      foreach (RequestRedirectRecord tmpRecord in this.requestRedirectRecords)
      {
        if (tmpRecord.RequestedHost == requestedHost && tmpRecord.RequestedPath == requestedPath)
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
        RequestRedirectRecord newRecord = new RequestRedirectRecord(requestedScheme, requestedHost, requestedPath, replacementResource, redirectType, redirectDescription);

        this.dgv_RequestRedirectURLs.SuspendLayout();
        this.requestRedirectRecords.Insert(0, newRecord);
        this.dgv_RequestRedirectURLs.ResumeLayout();
      }
    }


    /// <summary>
    ///
    /// </summary>
    private delegate void DeleteSelectedRecordDelegate();
    private void DeleteSelectedRecord()
    {
      if(this.InvokeRequired)
      {
        this.BeginInvoke(new DeleteSelectedRecordDelegate(this.DeleteSelectedRecord), new object[] { });
        return;
      }

      bool isLastLine = false;
      int firstVisibleRowTopRow = -1;
      int lastRowIndex = -1;
      int selectedIndex = -1;

      lock(this)
      {

        if(this.dgv_RequestRedirectURLs.CurrentRow != null &&
            this.dgv_RequestRedirectURLs.CurrentRow == this.dgv_RequestRedirectURLs.Rows[this.dgv_RequestRedirectURLs.Rows.Count - 1])
        {
          isLastLine = true;
        }

        firstVisibleRowTopRow = this.dgv_RequestRedirectURLs.FirstDisplayedScrollingRowIndex;
        lastRowIndex = this.dgv_RequestRedirectURLs.Rows.Count - 1;

        if(this.dgv_RequestRedirectURLs.CurrentCell != null)
        {
          selectedIndex = this.dgv_RequestRedirectURLs.CurrentCell.RowIndex;
        }

        this.dgv_RequestRedirectURLs.SuspendLayout();
        this.dgv_RequestRedirectURLs.BeginEdit(true);
        this.dgv_RequestRedirectURLs.RefreshEdit();

        try
        {
          int currentIndex = this.dgv_RequestRedirectURLs.CurrentCell.RowIndex;
          this.requestRedirectRecords.RemoveAt(currentIndex);
        }
        catch(Exception ex)
        {
          this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
        }

        // Selected cell/row
        try
        {
          if(selectedIndex >= 0)
          {
            this.dgv_RequestRedirectURLs.CurrentCell = this.dgv_RequestRedirectURLs.Rows[selectedIndex].Cells[0];
          }
        }
        catch(Exception)
        {
        }

        // Reset position
        try
        {
          if(firstVisibleRowTopRow >= 0)
          {
            this.dgv_RequestRedirectURLs.FirstDisplayedScrollingRowIndex = firstVisibleRowTopRow;
          }
        }
        catch(Exception)
        {
        }

        this.dgv_RequestRedirectURLs.ResumeLayout();
      }
    }


    private delegate void RemoveRecordAtDelegate(int index);
    private void RemoveRecordAt(int index)
    {
      if(this.InvokeRequired)
      {
        this.BeginInvoke(new RemoveRecordAtDelegate(this.RemoveRecordAt), new object[] { index });
        return;
      }

      lock(this)
      {
        this.dgv_RequestRedirectURLs.SuspendLayout();

        try
        {
          this.requestRedirectRecords.RemoveAt(index);
        }
        catch(Exception)
        {
        }

        this.dgv_RequestRedirectURLs.ResumeLayout();
      }
    }


    /// <summary>
    ///
    /// </summary>
    private delegate void ClearRecordListDelegate();
    private void ClearRecordList()
    {
      if(this.InvokeRequired)
      {
        this.BeginInvoke(new ClearRecordListDelegate(this.ClearRecordList), new object[] { });
        return;
      }

      lock(this)
      {
        this.dgv_RequestRedirectURLs.SuspendLayout();

        try
        {
          this.requestRedirectRecords.Clear();
        }
        catch(Exception)
        {
        }

        this.dgv_RequestRedirectURLs.ResumeLayout();
      }
    }

    #endregion

  }
}
