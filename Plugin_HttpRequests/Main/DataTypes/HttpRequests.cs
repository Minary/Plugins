namespace Minary.Plugin.Main.HttpRequest.DataTypes
{
  using System;
  using System.ComponentModel;


  public class HttpRequests : INotifyPropertyChanged
  {

    #region MEMBERS

    private string srcMac = string.Empty;
    private string srcIp = string.Empty;
    private string timestamp = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
    private string requestMethod = string.Empty;
    private string remoteHost = string.Empty;
    private string path = string.Empty;
    private string url = string.Empty;
    private string sessionCookies = string.Empty;
    private string request = string.Empty;
    private string userAgent = string.Empty;

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion


    #region PROPERTIES

    [Browsable(true)]
    public string SrcMAC
    {
      get { return this.srcMac; }
      set
      {
        this.srcMac = value;
        this.NotifyPropertyChanged("SrcMAC");
      }
    }


    [Browsable(true)]
    public string SrcIP
    {
      get { return this.srcIp; }
      set
      {
        this.srcIp = value;
        this.NotifyPropertyChanged("SrcIP");
      }
    }


    [Browsable(true)]
    public string Timestamp
    {
      get { return this.timestamp; }
      set
      {
        this.timestamp = value;
        this.NotifyPropertyChanged("Timestamp");
      }
    }


    [Browsable(true)]
    public string Method
    {
      get { return this.requestMethod; }
      set
      {
        this.requestMethod = value;
        this.NotifyPropertyChanged("Method");
      }
    }


    [Browsable(true)]
    public string RemoteHost
    {
      get { return this.remoteHost; }
      set
      {
        this.remoteHost = value;
        this.NotifyPropertyChanged("RemoteHost");
      }
    }


    [Browsable(true)]
    public string Path
    {
      get { return this.path; }
      set
      {
        this.path = value;
        this.NotifyPropertyChanged("Path");
      }
    }


    [Browsable(true)]
    public string URL
    {
      get { return this.url; }
      set
      {
        this.url = value;
        NotifyPropertyChanged("URL");
      }
    }


    [Browsable(false)]
    public string SessionCookies
    {
      get { return this.sessionCookies; }
      set
      {
        this.sessionCookies = value;
        this.NotifyPropertyChanged("SessionCookies");
      }
    }


    [Browsable(true)]
    public string Request
    {
      get { return this.request; }
      set
      {
        this.request = value;
        this.NotifyPropertyChanged("Request");
      }
    }


    [Browsable(true)]
    public string UserAgent
    {
      get { return this.userAgent; }
      set
      {
        this.userAgent = value;
        this.NotifyPropertyChanged("UserAgent");
      }
    }

    #endregion


    #region PUBLIC

    public HttpRequests()
    {
      this.srcMac = string.Empty;
      this.srcIp = string.Empty;
      this.timestamp = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
      this.requestMethod = string.Empty;
      this.remoteHost = string.Empty;
      this.path = string.Empty;
      this.url = string.Empty;
      this.sessionCookies = string.Empty;
      this.request = string.Empty;
      this.userAgent = string.Empty;
    }


    public HttpRequests(string srcMac, string srcIp, string requestMethod, string remoteHost, string remoteFile, string cookies, string request, string userAgent)
    {
      this.srcMac = srcMac;
      this.srcIp = srcIp;
      this.timestamp = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
      this.requestMethod = requestMethod;
      this.remoteHost = remoteHost;
      this.path = remoteFile;
      this.url = $"{this.remoteHost}{this.path}";
      this.sessionCookies = cookies;
      this.request = request;
      this.userAgent = userAgent;
    }

    #endregion


    #region PRIVATE

    /// <summary>
    ///
    /// </summary>
    /// <param name="propertyName"></param>
    private void NotifyPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged != null)
      {
        this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    #endregion

  }
}
