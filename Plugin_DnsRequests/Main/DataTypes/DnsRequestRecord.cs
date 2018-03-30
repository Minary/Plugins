namespace Minary.Plugin.Main.DnsRequest.DataTypes
{
  using System;
  using System.ComponentModel;

  

  public class DnsRequestRecord : INotifyPropertyChanged
  {

    #region MEMBERS

    private string srcMac;
    private string srcIp;
    private string timestamp;
    private string dnsRequest;
    private string dnsReply;
    private string packetType;

    [field: NonSerialized]
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion


    #region PROPERTIES

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
    public string Timestamp
    {
      get
      {
        return this.timestamp;
      }

      set
      {
        this.timestamp = value;
        this.NotifyPropertyChanged("Timestamp");
      }
    }


    [Browsable(true)]
    public string PacketType
    {
      get
      {
        return this.packetType;
      }

      set
      {
        this.packetType = value;
        this.NotifyPropertyChanged("PacketType");
      }
    }


    [Browsable(true)]
    public string DnsRequest
    {
      get
      {
        return this.dnsRequest;
      }

      set
      {
        this.dnsRequest = value;
        this.NotifyPropertyChanged("DnsRequest");
      }
    }


    [Browsable(true)]
    public string DnsReply
    {
      get
      {
        return this.dnsReply;
      }

      set
      {
        this.dnsReply = value;
        this.NotifyPropertyChanged("DnsReply");
      }
    }

    #endregion


    #region PUBLIC

    public DnsRequestRecord()
    {
      this.srcMac = string.Empty;
      this.srcIp = string.Empty;
      this.timestamp = DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss");
      this.dnsRequest = string.Empty;
      this.dnsReply = string.Empty;
      this.packetType = string.Empty;
    }


    public DnsRequestRecord(string srcMac, string srcIp, string dnsRequestHost, string dnsReplyIps, string type)
    {
      this.srcMac = srcMac;
      this.srcIp = srcIp;
      this.timestamp = DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss");
      this.dnsRequest = dnsRequestHost;
      this.dnsReply = dnsReplyIps;
      this.packetType = type;
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
