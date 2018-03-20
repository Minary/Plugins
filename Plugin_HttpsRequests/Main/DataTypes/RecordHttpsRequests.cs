namespace Minary.Plugin.Main.HttpsRequest.DataTypes
{
  using System;
  using System.ComponentModel;


  public class RecordHttpsRequest : INotifyPropertyChanged
  {

    #region MEMBERS

    private string srcMac = string.Empty;
    private string srcIp = string.Empty;
    private string timestamp = DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss");
    private string remoteHost = string.Empty;

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
    public string RemoteHost
    {
      get { return this.remoteHost; }
      set
      {
        this.remoteHost = value;
        this.NotifyPropertyChanged("RemoteHost");
      }
    }

    #endregion


    #region PUBLIC

    public RecordHttpsRequest()
    {
    }


    public RecordHttpsRequest(string srcMac, string srcIp, string remoteHost)
    {
      this.srcMac = srcMac;
      this.srcIp = srcIp;
      this.timestamp = DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss");
      this.remoteHost = remoteHost;
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
