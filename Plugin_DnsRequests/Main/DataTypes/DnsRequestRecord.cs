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
    private string dnsHostname;
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
    public string DNSHostname
    {
      get
      {
        return this.dnsHostname;
      }

      set
      {
        this.dnsHostname = value;
        this.NotifyPropertyChanged("DNSHostname");
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

    #endregion


    #region PUBLIC

    public DnsRequestRecord()
    {
      this.srcMac = string.Empty;
      this.srcIp = string.Empty;
      this.timestamp = DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss");
      this.dnsHostname = string.Empty;
      this.packetType = string.Empty;
    }


    public DnsRequestRecord(string srcMac, string srcIp, string dnsHostname, string type)
    {
      this.srcMac = srcMac;
      this.srcIp = srcIp;
      this.timestamp = DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss");
      this.dnsHostname = dnsHostname;
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
