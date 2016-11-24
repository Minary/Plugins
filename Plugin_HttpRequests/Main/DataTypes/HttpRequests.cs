namespace Minary.Plugin.Main.HttpRequest.DataTypes
{
  using System;
  using System.ComponentModel;

  public class HTTPRequests : INotifyPropertyChanged
  {

    #region MEMBERS

    private string srcMac;
    private string srcIp;
    private string timestamp;
    private string requestMethod;
    private string remoteHost;
    private string remoteFile;
    private string url;
    private string sessionCookies;
    private string request;

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion


    #region PUBLIC

    public HTTPRequests()
    {
      this.srcMac = string.Empty;
      this.srcIp = string.Empty;
      this.timestamp = DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss");
      this.requestMethod = string.Empty;
      this.remoteHost = string.Empty;
      this.remoteFile = string.Empty;
      this.url = string.Empty;
      this.sessionCookies = string.Empty;
      this.request = string.Empty;
    }


    public HTTPRequests(string srcMac, string srcIp, string requestMethod, string remoteHost, string remoteFile, string cookies, string request)
    {
      this.srcMac = srcMac;
      this.srcIp = srcIp;
      this.timestamp = DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss");
      this.requestMethod = requestMethod;
      this.remoteHost = remoteHost;
      this.remoteFile = remoteFile;
      this.url = string.Format("{0}{1}", this.remoteHost, this.remoteFile);
      this.sessionCookies = cookies;
      this.request = request;
    }

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


    [Browsable(false)]
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
    public string RemoteFile
    {
      get { return this.remoteFile; }
      set
      {
        this.remoteFile = value;
        this.NotifyPropertyChanged("RemoteFile");
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
