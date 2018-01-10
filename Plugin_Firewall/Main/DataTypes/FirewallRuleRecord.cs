namespace Minary.Plugin.Main.Firewall.DataTypes
{
  using System;
  using System.ComponentModel;


  [Serializable]
  public class FirewallRuleRecord : INotifyPropertyChanged
  {

    #region MEMBERS

    private string id;

    [field: NonSerialized]
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion


    #region PROPERTIES

    [Browsable(true)]
    public string Protocol
    {
      get
      {
        return this.Protocol;
      }

      set
      {
        this.Protocol = value;
        this.NotifyPropertyChanged("Protocol");
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


    [Browsable(true)]
    public string SrcIP
    {
      get
      {
        return this.SrcIP;
      }

      set
      {
        this.SrcIP = value;
        this.NotifyPropertyChanged("SrcIP");
      }
    }


    [Browsable(true)]
    public string DstIP
    {
      get
      {
        return this.DstIP;
      }

      set
      {
        this.DstIP = value;
        this.NotifyPropertyChanged("DstIP");
      }
    }


    [Browsable(true)]
    public string SrcPortLower
    {
      get
      {
        return this.SrcPortLower;
      }

      set
      {
        this.SrcPortLower = value;
        this.NotifyPropertyChanged("SrcPortLower");
      }
    }


    [Browsable(true)]
    public string SrcPortUpper
    {
      get
      {
        return this.SrcPortUpper;
      }

      set
      {
        this.SrcPortUpper = value;
        this.NotifyPropertyChanged("SrcPortUpper");
      }
    }


    [Browsable(true)]
    public string DstPortLower
    {
      get
      {
        return this.DstPortLower;
      }

      set
      {
        this.DstPortLower = value;
        this.NotifyPropertyChanged("DstPortLower");
      }
    }


    [Browsable(true)]
    public string DstPortUpper
    {
      get
      {
        return this.DstPortUpper;
      }

      set
      {
        this.DstPortUpper = value;
        this.NotifyPropertyChanged("DstPortUpper");
      }
    }

    #endregion


    #region PUBLIC

    public FirewallRuleRecord()
    {
      this.id = string.Empty;
      this.Protocol = string.Empty;
      this.SrcIP = string.Empty;
      this.DstIP = string.Empty;
      this.SrcPortLower = string.Empty;
      this.SrcPortUpper = string.Empty;
      this.DstPortLower = string.Empty;
      this.DstPortUpper = string.Empty;
    }

    public FirewallRuleRecord(string protocol, string srcIp, string srcPortLower, string srcPortUpper, string dstIp, string dstPortLower, string dstPortUpper)
    {
      this.id = $"{protocol}{dstIp}{dstPortLower}{dstPortUpper}{srcIp}{srcPortLower}{srcPortUpper}";
      this.Protocol = protocol;
      this.SrcIP = srcIp;
      this.DstIP = dstIp;
      this.SrcPortLower = srcPortLower;
      this.SrcPortUpper = srcPortUpper;
      this.DstPortLower = dstPortLower;
      this.DstPortUpper = dstPortUpper;
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
