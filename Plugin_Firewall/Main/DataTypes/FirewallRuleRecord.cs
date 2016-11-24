namespace Minary.Plugin.Main.Firewall.DataTypes
{
  using System;
  using System.ComponentModel;


  [Serializable]
  public class FirewallRuleRecord : INotifyPropertyChanged
  {

    #region MEMBERS

    private string id;
    private string protocol;
    private string srcIp;
    private string dstIp;
    private string srcPortLower;
    private string srcPortUpper;
    private string dstPortLower;
    private string dstPortUpper;

    [field: NonSerialized]
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion


    #region PUBLIC

    public FirewallRuleRecord()
    {
      this.id = string.Empty;
      this.protocol = string.Empty;
      this.srcIp = string.Empty;
      this.dstIp = string.Empty;
      this.srcPortLower = string.Empty;
      this.srcPortUpper = string.Empty;
      this.dstPortLower = string.Empty;
      this.dstPortUpper = string.Empty;
    }

    public FirewallRuleRecord(string protocol, string srcIp, string srcPortLower, string srcPortUpper, string dstIp, string dstPortLower, string dstPortUpper)
    {
      this.id = string.Format("{0}{1}{2}{3}{4}{5}{6}", protocol, dstIp, dstPortLower, dstPortUpper, srcIp, srcPortLower, srcPortUpper);
      this.protocol = protocol;
      this.srcIp = srcIp;
      this.dstIp = dstIp;
      this.srcPortLower = srcPortLower;
      this.srcPortUpper = srcPortUpper;
      this.dstPortLower = dstPortLower;
      this.dstPortUpper = dstPortUpper;
    }

    #endregion


    #region PROPERTIES

    [Browsable(true)]
    public string Protocol
    {
      get
      {
        return this.protocol;
      }

      set
      {
        this.protocol = value;
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
        return this.srcIp;
      }

      set
      {
        this.srcIp = value;
        this.NotifyPropertyChanged("SrcIP");
      }
    }


    [Browsable(true)]
    public string DstIP
    {
      get
      {
        return this.dstIp;
      }

      set
      {
        this.dstIp = value;
        this.NotifyPropertyChanged("DstIP");
      }
    }


    [Browsable(true)]
    public string SrcPortLower
    {
      get
      {
        return this.srcPortLower;
      }

      set
      {
        this.srcPortLower = value;
        this.NotifyPropertyChanged("SrcPortLower");
      }
    }


    [Browsable(true)]
    public string SrcPortUpper
    {
      get
      {
        return this.srcPortUpper;
      }

      set
      {
        this.srcPortUpper = value;
        this.NotifyPropertyChanged("SrcPortUpper");
      }
    }


    [Browsable(true)]
    public string DstPortLower
    {
      get
      {
        return this.dstPortLower;
      }

      set
      {
        this.dstPortLower = value;
        this.NotifyPropertyChanged("DstPortLower");
      }
    }


    [Browsable(true)]
    public string DstPortUpper
    {
      get
      {
        return this.dstPortUpper;
      }

      set
      {
        this.dstPortUpper = value;
        this.NotifyPropertyChanged("DstPortUpper");
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
