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


  public class HttpSearch : Ifc.IObservable
  {

    #region MEMBERS

    private IPlugin plugin;
    private List<Ifc.IObserver> observers = new List<Ifc.IObserver>();

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


    /// <summary>
    ///
    /// </summary>
    public void ProcessEntries(List<string> newData)
    {
      var newRecords = new List<HttpFoundRecord>();
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

        if (searchData.Finding.Length <= 0)
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
        this.Notify(newRecords);
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

    private HttpFoundRecord FindHttpSearchString(string inputHttpData)
    {
      var retVal = new HttpFoundRecord();
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

      //foreach (var tmpRecord in this .httpSearchRecords)
      {
      /*
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

        //if ((matchCreds = Regex.Match(inputHttpData, tmpRecord.)).Success == false)
        //{
        //  continue;
        //}

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
          //retVal.Company = tmpRecord.Company;
          //retVal.CompanyURL = $"{tmpRecord.WebPage}   (http://{reqHost}{reqUri})";
          //retVal.Username = username;
          //retVal.Password = password;

          break;
        }
        */
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


    #region INTERFACE: IObservable

    public void AddObserver(Ifc.IObserver observer)
    {
      if (observer != null)
      {
        this.observers.Add(observer);
      }
    }


    public void Notify(List<HttpFoundRecord> findings)
    {
      foreach (var observer in this.observers)
      {
        observer.Update(findings);
      }
    }

    #endregion

  }
}
