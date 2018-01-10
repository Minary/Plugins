namespace Minary.Plugin.Main.Session.DataTypes
{
  using System;
  using System.ComponentModel;


  [Serializable]
  public class TheSessionRecord : INotifyPropertyChanged
  {

    #region MEMBERS

    private string timeStamp;
    private string srcMac;
    private string srcIp;
    private string url;
    private string dstPort;
    private string sessionCookies;
    private string browser;
    private string group;
    private string id;

    [field: NonSerialized]
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion


    #region PROPERTIES

    [Browsable(true)]
    public string TimeStamp
    {
      get
      {
        return this.timeStamp;
      }

      set
      {
        this.timeStamp = value;
        this.NotifyPropertyChanged("TimeStamp");
      }
    }


    [Browsable(true)]
    public string SrcMAC
    {
      get
      {
        return this.srcMac;
      }

      set
      {
        this.srcMac = value;
        this.NotifyPropertyChanged("SrcMAC");
      }
    }


    [Browsable(true)]
    public string SrcIP
    {
      get
      {
        return this.srcIp;
      }

      set
      {
        this.srcIp = value;
        this.NotifyPropertyChanged("SrcIP");
      }
    }


    [Browsable(true)]
    public string URL
    {
      get
      {
        return this.url;
      }

      set
      {
        this.url = value;
        this.NotifyPropertyChanged("URL");
      }
    }


    [Browsable(false)]
    public string DstPort
    {
      get
      {
        return this.dstPort;
      }

      set
      {
        this.dstPort = value;
        this.NotifyPropertyChanged("DstPort");
      }
    }


    [Browsable(false)]
    public string SessionCookies
    {
      get
      {
        return this.sessionCookies;
      }

      set
      {
        this.sessionCookies = value;
        this.NotifyPropertyChanged("SessionCookies");
      }
    }


    [Browsable(false)]
    public string Browser
    {
      get
      {
        return this.browser;
      }

      set
      {
        this.browser = value;
        this.NotifyPropertyChanged("Browser");
      }
    }


    [Browsable(false)]
    public string Group
    {
      get
      {
        return this.group;
      }

      set
      {
        this.group = value;
        this.NotifyPropertyChanged("Group");
      }
    }


    [Browsable(false)]
    public string ID
    {
      get
      {
        return this.id;
      }

      set
      {
        this.id = value;
        this.NotifyPropertyChanged("ID");
      }
    }

    #endregion


    #region PUBLIC

    public TheSessionRecord()
    {
      this.timeStamp = string.Empty;
      this.srcMac = string.Empty;
      this.srcIp = string.Empty;
      this.url = string.Empty;
      this.dstPort = string.Empty;
      this.sessionCookies = string.Empty;
      this.browser = string.Empty;
      this.group = string.Empty;
      this.id = string.Empty;
    }

    public TheSessionRecord(string srcMac, string srcIp, string url, string dstPort, string sessionCookies, string browser, string group)
    {
      this.id = $"{url.Trim()}{dstPort.Trim()}{sessionCookies.Trim()}";
      this.timeStamp = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
      this.srcMac = srcMac;
      this.srcIp = srcIp;
      this.url = url;
      this.dstPort = dstPort;
      this.sessionCookies = sessionCookies;
      this.browser = browser;
      this.group = group;
    }

    #endregion


    #region PRIVATE

    /// <summary>
    ///
    /// </summary>
    /// <param name="name"></param>
    private void NotifyPropertyChanged(string name)
    {
      if (this.PropertyChanged != null)
      {
        this.PropertyChanged(this, new PropertyChangedEventArgs(name));
      }
    }

    #endregion

  }
}