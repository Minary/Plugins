namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.InjectCode.DataTypes;
  using System;
  using System.Linq;
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

      RequestURL requestUrl = this.ParseRequestedURLRegex(requestedResource);
      var scheme = "http://";

      // Verify if replacement file resource is valid
      if (!File.Exists(replacementResource))
      {
        throw new Exception("The injection file does not exist.");
      }

      // Verify if record already exists
      foreach (InjectCodeRecord tmpRecord in this.injectCodeRecords)
      {
        if (tmpRecord.RequestedHostRegex == requestUrl.HostRegex &&
            tmpRecord.RequestedPathRegex == requestUrl.PathRegex &&
            tmpRecord.Tag == tag)
        {
          throw new Exception("A record with this host name already exists.");
        }
      }

      // Verify if host name is correct
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
        InjectCodeRecord newRecord = new InjectCodeRecord(scheme, requestUrl.HostRegex, requestUrl.PathRegex, replacementResource, tag, position);

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

      var isLastLine = false;
      var firstVisibleRowTopRow = -1;
      var lastRowIndex = -1;
      var selectedIndex = -1;

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
          this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
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
      if (url.StartsWith("http://") == true || 
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

      string urlPath = $"{pathDelimiter}{splitter[1]}";
      requestedUrl = new RequestURL(splitter[0], urlPath);

      return requestedUrl;
    }

    #endregion

  }
}
