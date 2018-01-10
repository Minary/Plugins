namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.RequestRedirect.DataTypes;
  using System;
  using System.Linq;
  using System.Text.RegularExpressions;


  public partial class Plugin_HttpRequestRedirect
  {

    #region GUI RECORDS METHODS

    /// <summary>
    ///
    /// </summary>
    /// <param name="pRecord"></param>
    private delegate void AddRecordDelegate(string redirectType, string redirectDescription, string requestedResource, string replacementResource);
    private void AddRecord(string redirectType, string redirectDescription, string requestedResource, string replacementResource)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new AddRecordDelegate(this.AddRecord), new object[] { redirectType, redirectDescription, requestedResource, replacementResource });
        return;
      }

      RequestURL requestUrl = this.ParseRequestedURLRegex(requestedResource);

      // Verify if record already exists
      foreach(RequestRedirectRecord tmpRecord in this.requestRedirectRecords)
      {
        if (tmpRecord.RequestedHostRegex == requestUrl.HostRegex &&
            tmpRecord.RequestedPathRegex == requestUrl.PathRegex)
        {
          throw new Exception("A record with this host name already exists.");
        }
      }

      if (string.IsNullOrEmpty(redirectType))
      {
        throw new Exception("The redirect type code is invalid");
      }

      if (string.IsNullOrEmpty(redirectDescription))
      {
        throw new Exception("The redirect description is invalid");
      }

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

      if (this.IsRegexPatternValid(requestUrl.HostRegex) == false)
      {
        this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: Invalid host name regex: {requestUrl.HostRegex}");
        throw new Exception("The host name regex is invalid");
      }

      if (this.IsRegexPatternValid(requestUrl.PathRegex) == false)
      {
        throw new Exception("The request path regex is invalid");
      }

      lock (this)
      {
        RequestRedirectRecord newRecord = new RequestRedirectRecord(redirectType, redirectDescription, requestUrl.HostRegex, requestUrl.PathRegex, replacementResource);

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
          this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
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


    public bool IsRegexPatternValid(string pattern)
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


    private RequestURL ParseRequestedURLRegex(string url)
    {
      RequestURL requestedUrl;
      var pathDelimiter = '/';

      if (string.IsNullOrEmpty(url) == true || 
          string.IsNullOrWhiteSpace(url) == true)
      {
        throw new Exception("The URL is invalid");
      }

      url = url.Trim();
      if(url.StartsWith("http://") == true || 
         url.StartsWith("https://") == true)
      {
        throw new Exception("The URL must not contain a scheme definition");
      }

      if (url.Contains(pathDelimiter) == false)
      {
        throw new Exception("The URL must contain a root path slash");
      }

      string[] splitter = url.Split(new char[] { pathDelimiter }, 2);

      if (splitter == null || 
          splitter.Count() != 2)
      {
        throw new Exception("The URL is invalid");
      }

      var urlPath = $"{pathDelimiter}{splitter[1]}";
      requestedUrl = new RequestURL(splitter[0], urlPath);

      return requestedUrl;
    }

    #endregion

  }
}
