namespace Minary.Plugin.Main.HttpAccounts.DataTypes
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;


  #region DATATYPES

  public struct HttpAccountStruct
  {
    public string Username;
    public string Password;
    public string Company;
    public string CompanyURL;
  }


  [Serializable]
  public struct PluginData
  {
    public List<AccountRecord> Records;
  }

  public struct PeerSystems
  {
    public string Name { get; set; }

    public string Value { get; set; }
  }

  #endregion

  [Serializable]
  public class AccountRecord : INotifyPropertyChanged
  {

    #region MEMBERS

    private string srcMac;
    private string srcIp;
    private string dstIp;
    private string dstPort;
    private string username;
    private string password;

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


    [Browsable(false)]
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
    public string DstPort
    {
      get
      {
        return this.dstPort;
      }

      set
      {
        this.dstPort = value;
        this.NotifyPropertyChanged("DstPort");
      }
    }


    [Browsable(true)]
    public string Username
    {
      get
      {
        return this.username;
      }

      set
      {
        this.username = value;
        this.NotifyPropertyChanged("Username");
      }
    }


    [Browsable(true)]
    public string Password
    {
      get
      {
        return this.password;
      }
      
      set
      {
        this.password = value;
        this.NotifyPropertyChanged("Password");
      }
    }

    #endregion


    #region PUBLIC

    public AccountRecord()
    {
      this.srcMac = string.Empty;
      this.srcIp = string.Empty;
      this.dstIp = string.Empty;
      this.dstPort = string.Empty;
      this.username = string.Empty;
      this.password = string.Empty;
    }


    public AccountRecord(string srcMac, string srcIp, string dstIp, string dstPort, string username, string password)
    {
      this.srcMac = srcMac;
      this.srcIp = srcIp;
      this.dstIp = dstIp;
      this.dstPort = dstPort;
      this.username = username;
      this.password = password;
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
