namespace Minary.Plugin.Main.Systems.DataTypes
{
  using System;
  using System.ComponentModel;
  using System.Text.RegularExpressions;

  public class SystemRecord : INotifyPropertyChanged
  {

    #region MEMBERS

    private string srcMacAddress;
    private string srcIpAddress;
    private string userAgent;
    private string operatingSystem;
    private string hardwareVendor;
    private string lastSeen;
    private string id;
    
    [field: NonSerialized]
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion


    #region PROPERTIES

    [Browsable(true)]
    public string SrcMac
    {
      get
      {
        return this.srcMacAddress;
      }

      set
      {
        this.srcMacAddress = value;
        this.NotifyPropertyChanged("SrcMac");
      }
    }


    [Browsable(true)]
    public string SrcIp
    {
      get
      {
        return this.srcIpAddress;
      }

      set
      {
        this.srcIpAddress = value;
        this.NotifyPropertyChanged("SrcIp");
      }
    }


    [Browsable(true)]
    public string OperatingSystem
    {
      get
      {
        return this.operatingSystem;
      }

      set
      {
        this.operatingSystem = value;
        this.NotifyPropertyChanged("OperatingSystem");
      }
    }


    [Browsable(true)]
    public string UserAgent
    {
      get
      {
        return this.userAgent;
      }

      set
      {
        this.userAgent = value;
        this.NotifyPropertyChanged("UserAgent");
      }
    }


    [Browsable(true)]
    public string LastSeen
    {
      get
      {
        return this.lastSeen;
      }

      set
      {
        this.lastSeen = value;
        this.NotifyPropertyChanged("LastSeen");
      }
    }


    [Browsable(true)]
    public string HWVendor
    {
      get
      {
        return this.hardwareVendor;
      }

      set
      {
        this.hardwareVendor = value;
        this.NotifyPropertyChanged("HWVendor");
      }
    }


    [Browsable(true)]
    public string Id
    {
      get
      {
        return this.id;
      }

      set
      {
        this.id = value;
        this.NotifyPropertyChanged("Id");
      }
    }

    #endregion


    #region PUBLIC

    public SystemRecord()
    {
      this.srcMacAddress = string.Empty;
      this.srcIpAddress = string.Empty;
      this.userAgent = string.Empty;
      this.operatingSystem = string.Empty;
      this.hardwareVendor = string.Empty;
      this.lastSeen = string.Empty;
      this.id = string.Empty;
    }

    public SystemRecord(string srcMacAddress, string srcIpAddress, string userAgentString, string hardwareVendor, string operatingSystem, string lastSeen)
    {
      this.srcMacAddress = srcMacAddress;
      this.srcIpAddress = srcIpAddress;
      this.userAgent = userAgentString;
      this.operatingSystem = operatingSystem;
      this.hardwareVendor = hardwareVendor;
      this.lastSeen = !string.IsNullOrEmpty(lastSeen) ? lastSeen : DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
      this.id = srcMacAddress;
      this.srcMacAddress = Regex.Replace(this.srcMacAddress, @"-", ":");
      this.id = $"{this.srcMacAddress.ToLower()}{this.srcIpAddress}";
    }

    #endregion


    #region PRIVATE

    /// <summary>
    ///
    /// </summary>
    /// <param name="name"></param>
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
