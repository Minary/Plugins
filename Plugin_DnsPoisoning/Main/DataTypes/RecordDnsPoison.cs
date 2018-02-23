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

    #endregion


    #region PUBLIC

    public RecordDnsPoison()
    {
    }


    public RecordDnsPoison(string hostName, string ipAddress, DnsResponseType responseType, string cname)
    {
      this.hostName = hostName;
      this.ipAddress = ipAddress;
      this.responseType = responseType;
      this.cname = cname;
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
