namespace Minary.Plugin.Main.DnsPoisoning.DataTypes
{
  using System;
  using System.ComponentModel;


  [Serializable]
  public class RecordDnsPoison : INotifyPropertyChanged
  {

    #region MEMBERS

    private string hostName = string.Empty;
    private string ipAddress = string.Empty;
    private DnsResponseType responseType;
    private string cname = string.Empty;
    private long ttl = -1;
    private bool mustMatch = true;

    [field: NonSerialized]
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion


    #region PROPERTIES

    [Browsable(true)]
    public string HostName
    {
      get
      {
        return this.hostName;
      }

      set
      {
        this.hostName = value;
        this.NotifyPropertyChanged("HostName");
      }
    }


    [Browsable(true)]
    public string IpAddress
    {
      get
      {
        return this.ipAddress;
      }

      set
      {
        this.ipAddress = value;
        this.NotifyPropertyChanged("IPAddress");
      }
    }


    [Browsable(true)]
    public DnsResponseType ResponseType
    {
      get
      {
        return this.responseType;
      }

      set
      {
        this.responseType = value;
        this.NotifyPropertyChanged("ResponseType");
      }
    }


    [Browsable(true)]
    public string CName
    {
      get
      {
        return this.cname;
      }

      set
      {
        this.cname = value;
        this.NotifyPropertyChanged("CName");
      }
    }


    [Browsable(true)]
    public long TTL
    {
      get
      {
        return this.ttl;
      }

      set
      {
        this.ttl = value;
        this.NotifyPropertyChanged("TTL");
      }
    }


    [Browsable(true)]
    public bool MustMatch
    {
      get
      {
        return this.mustMatch;
      }

      set
      {
        this.mustMatch = value;
        this.NotifyPropertyChanged("MustMatch");
      }
    }

    #endregion


    #region PUBLIC

    public RecordDnsPoison()
    {
    }


    public RecordDnsPoison(string hostName, string ipAddress, DnsResponseType responseType, string cname, long ttl, bool mustMatch)
    {
      this.hostName = hostName;
      this.ipAddress = ipAddress;
      this.responseType = responseType;
      this.cname = cname;
      this.ttl = ttl;
      this.mustMatch = mustMatch;
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
