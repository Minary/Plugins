namespace Minary.Plugin.Main.HttpsRequest.DataTypes
{
  using System;
  using System.ComponentModel;


  public class RecordHttpsRequest : INotifyPropertyChanged
  {

    #region MEMBERS

    private string srcMAC = string.Empty;
    private string srcIP = string.Empty;
    private string dstIP = string.Empty;
    private string timestamp = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
    private string remoteHost = string.Empty;

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion


    #region PROPERTIES

    [Browsable(true)]
    public string SrcMAC
    {
      get { return this.srcMAC; }
      set
      {
        this.srcMAC = value;
        this.NotifyPropertyChanged("SrcMAC");
      }
    }


    [Browsable(true)]
    public string SrcIP
    {
      get { return this.srcIP; }
      set
      {
        this.srcIP = value;
        this.NotifyPropertyChanged("SrcIP");
      }
    }


    [Browsable(true)]
    public string DstIP
    {
      get { return this.dstIP; }
      set
      {
        this.dstIP = value;
        this.NotifyPropertyChanged("DstIP");
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
      this.srcMAC = string.Empty;
      this.srcIP = string.Empty;
      this.dstIP = string.Empty;
      this.timestamp = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
      this.remoteHost = string.Empty;
    }


    public RecordHttpsRequest(string srcMac, string srcIp, string dstIp, string remoteHost)
    {
      this.srcMAC = srcMac;
      this.srcIP = srcIp;
      this.dstIP = dstIp;
      this.timestamp = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
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
