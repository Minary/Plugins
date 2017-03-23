namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.InjectFile.DataTypes;
  using System;
  using System.IO;
  using System.Linq;
  using System.Text.RegularExpressions;


  public partial class Plugin_HttpInjectFile
  {

    #region GUI RECORDS METHODS

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestedResource"></param>
    /// <param name="replacementResource"></param>
    private delegate void AddRecordDelegate(string requestedResource, string replacementResource);
    private void AddRecord(string requestedResource, string replacementResource)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new AddRecordDelegate(this.AddRecord), new object[] { requestedResource, replacementResource });
        return;
      }

      RequestURL requestUrl = this.ParseRequestedURLRegex(requestedResource);
      string scheme = "http://";

      // Verify if replacement file resource is valid
      if (!File.Exists(replacementResource))
      {
        throw new Exception("The injection file does not exist.");
      }

      // Verify if record already exists
      foreach (InjectFileRecord tmpRecord in this.injectFileRecords)
      {
        if (tmpRecord.RequestedHostRegex == requestUrl.HostRegex &&
            tmpRecord.RequestedPathRegex == requestUrl.PathRegex)
        {
          throw new Exception("A record with this host name already exists.");
        }
      }

      // Verify if host name is correct
      if (this.IsRegexPatternValid(requestUrl.HostRegex) == false)
      {
        this.pluginProperties.HostApplication.LogMessage("{0}: Invalid host name regex: {1}", this.Config.PluginName, requestUrl.HostRegex);
        throw new Exception("The host name regex is invalid");
      }

      if (this.IsRegexPatternValid(requestUrl.PathRegex) == false)
      {
        throw new Exception("The request path regex is invalid");
      }

      lock (this)
      {
        InjectFileRecord newRecord = new InjectFileRecord(scheme, requestUrl.HostRegex, requestUrl.PathRegex, replacementResource);

        this.dgv_InjectionTriggerURLs.SuspendLayout();
        this.injectFileRecords.Insert(0, newRecord);
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
          this.injectFileRecords.RemoveAt(currentIndex);
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
          this.injectFileRecords.RemoveAt(index);
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
          this.injectFileRecords.Clear();
        }
        catch (Exception)
        {
        }

        this.dgv_InjectionTriggerURLs.ResumeLayout();
      }
    }


    public bool IsRegexPatternValid(string pattern)
    {
      bool isValid = false;

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
      char pathDelimiter = '/';

      if(string.IsNullOrEmpty(url) == true || string.IsNullOrWhiteSpace(url) == true)
      {
        throw new Exception("The URL is invalid");
      }

      url = url.Trim();
      if(url.StartsWith("http://") == true || url.StartsWith("https://") == true)
      {
        throw new Exception("The URL must not contain a scheme definition");
      }

      if(url.Contains(pathDelimiter) == false)
      {
        throw new Exception("The URL must contain a root path slash");
      }

      string[] splitter = url.Split(new char[] { pathDelimiter }, 2);

      if(splitter == null || splitter.Count() != 2)
      {
        throw new Exception("The URL is invalid");
      }

      string urlPath = string.Format("{0}{1}", pathDelimiter, splitter[1]);
      requestedUrl = new RequestURL(splitter[0], urlPath);

      return requestedUrl;
    }


    #endregion

  }
}
