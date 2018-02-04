namespace Minary.Plugin.Main.HttpSearch.Infrastructure
{
  using Minary.Plugin.Main.HttpSearch.DataTypes.Class;
  using Ifc = Minary.Plugin.Main.HttpSearch.DataTypes.Interface;
  using MinaryLib.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.ComponentModel;
  using System.Collections.Generic;
  using System.IO;
  using System.Text.RegularExpressions;


  public class HttpSearch : Ifc.IObservableRecordFinding, Ifc.IObservableRecordDef
  {

    #region MEMBERS

    private IPlugin plugin;
    private List<Ifc.IObserverRecordFinding> observersRecFound = new List<Ifc.IObserverRecordFinding>();
    private List<Ifc.IObserverRecordDef> observersRecDef = new List<Ifc.IObserverRecordDef>();
    private List<RecordHttpSearch> httpSearchRecords = new List<RecordHttpSearch>();
    private List<HttpFindingRecord> httpFindingRecords = new List<HttpFindingRecord>();

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpAccounts"/> class.
    ///
    /// </summary>
    /// <param name="plugin"></param>
    public HttpSearch(IPlugin plugin)
    {
      this.plugin = plugin;
    }


    public void AddSearchPatternRecord(RecordHttpSearch newRecord)
    {
      this.httpSearchRecords.Add(newRecord);
      this.NotifyRecordDef();
    }


    public void DeleteSearchPatternRecordAt(int index)
    {
      this.httpSearchRecords.RemoveAt(index);
      this.NotifyRecordDef();
    }


    public void ClearSearchPatternRecordList()
    {
      if (this.httpSearchRecords.Count <= 0)
      {
        return;
      }

      lock (this)
      {
        try
        {
          this.httpSearchRecords.Clear();
        }
        catch
        {
        }

        this.NotifyRecordDef();
      }
    }





    public void AddFindingRecord(RecordHttpSearch newRecord)
    {
      this.httpSearchRecords.Add(newRecord);
      this.NotifyFindingRecords();
    }
    

    public void DeleteFindingRecordAt(int index)
    {
      this.httpFindingRecords.RemoveAt(index);
      this.NotifyFindingRecords();
    }


    public void ClearFindingRecordList()
    {
      if (this.httpFindingRecords.Count <= 0)
      {
        return;
      }

      lock (this)
      {
        try
        {
          this.httpFindingRecords.Clear();
        }
        catch
        {
        }

        this.NotifyFindingRecords();
      }
    }


    /// <summary>
    ///
    /// </summary>
    public void ProcessEntries(List<string> newData)
    {
      var newRecords = new List<HttpFindingRecord>();
      int dstPortInt;
      string[] splitter;
      string protocol;
      string srcMac;
      string srcIp;
      string srcPort;
      string dstIp;
      string dstPort;
      string data;

      foreach (var tmpRecord in newData)
      {
        if (string.IsNullOrEmpty(tmpRecord))
        {
          continue;
        }

        splitter = Regex.Split(tmpRecord, @"\|\|");
        if (splitter.Length != 7)
        {
          continue;
        }

        protocol = splitter[0];
        srcMac = splitter[1];
        srcIp = splitter[2];
        srcPort = splitter[3];
        dstIp = splitter[4];
        dstPort = splitter[5];
        data = splitter[6];

        // HTML GET authentication strings
        var searchData = this.FindHttpSearchString(data);

        if (searchData.Finding?.Length > 0 == false)
        {
          continue;
        }

        if (!int.TryParse(dstPort, out dstPortInt))
        {
          this.plugin.Config.HostApplication.LogMessage($"{this.plugin.Config.PluginName}: Something is wrong with the remote port \"{dstPort}\"");
          continue;
        }
        else if (!Regex.Match(dstIp, @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$").Success &&
                 !Regex.Match(dstIp, @"\.[\d\w]+").Success)
        {
          this.plugin.Config.HostApplication.LogMessage($"{this.plugin.Config.PluginName}: Something is wrong with the remote system \"{dstIp}\"");
        }
    
        newRecords.Add(searchData); 
      }

      // Notify all observers about new
      // findings
      if (newRecords.Count > 0)
      {
        this.httpFindingRecords.AddRange(newRecords);
        this.NotifyFindingRecords();
      }
    }

    #endregion


    #region EVENTS

    /// <summary>
    /// 
    /// </summary>
    public void OnInit()
    {
      List<string> pluginBasedirectories = new List<string>();

      pluginBasedirectories.Add(Path.Combine(
                             this.plugin.Config.ApplicationBaseDir,
                             this.plugin.Config.PluginBaseDir,
                             this.plugin.Config.PatternSubDir,
                             Plugin.Main.HttpSearch.DataTypes.Class.General.PATTERN_DIR_REMOTE));

      pluginBasedirectories.Add(Path.Combine(
                             this.plugin.Config.ApplicationBaseDir,
                             this.plugin.Config.PluginBaseDir,
                             this.plugin.Config.PatternSubDir,
                             Plugin.Main.HttpSearch.DataTypes.Class.General.PATTERN_DIR_LOCAL));

      pluginBasedirectories.Add(Path.Combine(
                             this.plugin.Config.ApplicationBaseDir,
                             this.plugin.Config.PluginBaseDir,
                             this.plugin.Config.PatternSubDir,
                             Plugin.Main.HttpSearch.DataTypes.Class.General.PATTERN_DIR_TEMPLATE));

      pluginBasedirectories.ForEach(elem =>
      {
        try
        {
          if (!Directory.Exists(elem))
          {
            Directory.CreateDirectory(elem);
          }
        }
        catch (Exception ex)
        {
          this.plugin.Config.HostApplication.LogMessage($"{this.plugin.Config.PluginName}: {ex.Message}");
        }
      });

      // Clean up template directory
      this.CleanUpTemplateDir();
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="pWebServerConfig"></param>
    public void OnReset()
    {
      this.CleanUpTemplateDir();
    }


    /// <summary>
    ///
    /// </summary>
    public void OnStop()
    {
    }

    #endregion


    #region PRIVATE

    private HttpFindingRecord FindHttpSearchString(string inputHttpData)
    {
      var retVal = new HttpFindingRecord();
      //var retVal = new HttpAccountStruct()
      //{
      //  Username = string.Empty,
      //  Password = string.Empty,
      //  Company = string.Empty,
      //  CompanyURL = string.Empty
      //};

      //if (this.accountPatterns?.Count > 0 == false)
      //{
      //  return retVal;
      //}

      Match matchHost;
      Match matchMethod;
      Match matchURI;
//Match matchCreds;
      var reqHost = string.Empty;
      var reqMethod = string.Empty;
      var reqUri = string.Empty;
      var username = string.Empty;
      var password = string.Empty;

      foreach (var tmpRecord in this.httpSearchRecords)
      {
        if ((matchMethod = Regex.Match(inputHttpData, @"\s*(GET|POST|PUT|DELETE|HEAD)\s+")).Success == false)
        {
          continue;
        }

        if ((matchURI = Regex.Match(inputHttpData, @"\s*(GET|POST|PUT|DELETE|HEAD)\s+([^\s]+)\s+", RegexOptions.IgnoreCase)).Success == false)
        {
          continue;
        }

        if ((matchHost = Regex.Match(inputHttpData, @"\.\.Host\s*:\s*([\w\-_\d\.]+?)\.\.", RegexOptions.IgnoreCase)).Success == false)
        {
          continue;
        }

        reqMethod = matchMethod.Groups[1].Value.ToString();
        reqUri = matchURI.Groups[2].Value.ToString();
        reqHost = matchHost.Groups[1].Value.ToString();
        //username = matchCreds.Groups[1].Value.ToString();
        //password = matchCreds.Groups[2].Value.ToString();

        if (tmpRecord.Method.Trim().ToLower() == reqMethod.Trim().ToLower() &&
            Regex.Match(reqHost, tmpRecord.HostRegex).Success &&
            Regex.Match(reqUri, tmpRecord.PathRegex).Success &&
            Regex.Match(inputHttpData, tmpRecord.DataRegex).Success)
        {
          retVal.Method = reqMethod;
          retVal.Host = reqHost;
          retVal.Path = reqUri;
          retVal.Finding = "FINDING";
          //retVal.CompanyURL = $"{tmpRecord.WebPage}   (http://{reqHost}{reqUri})";
          //retVal.Username = username;
          //retVal.Password = password;

          break;
        }
      }

      return retVal;
    }


    /// <summary>
    ///
    /// </summary>
    private void CleanUpTemplateDir()
    {
      var templateDir = Path.Combine(
                                     this.plugin.Config.ApplicationBaseDir,
                                     this.plugin.Config.PluginBaseDir,
                                     this.plugin.Config.PatternSubDir,
                                     Plugin.Main.HttpSearch.DataTypes.Class.General.PATTERN_DIR_TEMPLATE);

      if (!Directory.Exists(templateDir))
      {
        return;
      }

      string[] patternFiles = Directory.GetFiles(templateDir, Plugin.Main.HttpSearch.DataTypes.Class.General.PATTERN_FILE_PATTERN);
      foreach (var tmpFile in patternFiles)
      {
        try
        {
          File.Delete(tmpFile);
        }
        catch (Exception ex)
        {
          this.plugin.Config.HostApplication.LogMessage($"{this.plugin.Config.PluginName}: {ex.Message}");
        }
      }
    }

    #endregion


    #region TEMPLATE

    public byte[] OnGetTemplateData(BindingList<RecordHttpSearch> applicationPatternRecords)
    {
      return null;
    }


    public List<RecordHttpSearch> OnLoadTemplateData(TemplatePluginData pluginData)
    {
      var applicatoinPatternRecords = new List<RecordHttpSearch>();

      return applicatoinPatternRecords;
    }


    public void OnUnoadTemplateData()
    {
      this.CleanUpTemplateDir();
    }

    #endregion


    #region INTERFACE: IObservableRecordDef


    public void AddObserverRecordDef(Ifc.IObserverRecordDef observer)
    {
      this.observersRecDef.Add(observer);
    }


    public void NotifyRecordDef()
    {
      foreach (var observer in this.observersRecDef)
      {
        observer.UpdateRecordDef(this.httpSearchRecords);
      }
    }

    #endregion


    #region INTERFACE: IObservableRecordFound

    public void AddObserverRecordFound(Ifc.IObserverRecordFinding observer)
    {
      this.observersRecFound.Add(observer);
    }


    public void NotifyFindingRecords()
    {
      foreach (var observer in this.observersRecFound)
      {
        observer.UpdateRecordsFound(this.httpFindingRecords);
      }
    }

    #endregion

  }
}
