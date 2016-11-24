namespace Minary.Plugin.Main.IpAccounting.DataTypes
{
  using System;
  using System.ComponentModel;


  public class AccountingItem : INotifyPropertyChanged, IEquatable<AccountingItem>
  {

    #region MEMBERS

    private string basis;
    private string packetCounter;
    private string dataVolume;
    private string lastUpdate;

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion


    #region PUBLIC

    public AccountingItem()
    {
      this.basis = string.Empty;
      this.packetCounter = string.Empty;
      this.dataVolume = string.Empty;
      this.lastUpdate = string.Empty;
    }

    public AccountingItem(string serviceName, string packetCounter, string dataVolume, string lastUpdate)
    {
      this.basis = serviceName;
      this.packetCounter = packetCounter;
      this.dataVolume = dataVolume;
      this.lastUpdate = lastUpdate;
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="accountingObj"></param>
    /// <returns></returns>
    public bool Equals(AccountingItem accountingObj)
    {
      if (accountingObj != null && this.basis != null && this.basis == accountingObj.Basis)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    #endregion


    #region PROPERTIES

    [Browsable(true)]
    public string Basis
    {
      get { return this.basis; }
      set
      {
        this.basis = value;
        this.NotifyPropertyChanged("Basis");
      }
    }

    [Browsable(true)]
    public string PacketCounter
    {
      get { return this.packetCounter; }
      set
      {
        this.packetCounter = value;
        this.NotifyPropertyChanged("PacketCounter");
      }
    }

    [Browsable(true)]
    public string DataVolume
    {
      get { return this.dataVolume; }
      set
      {
        this.dataVolume = value;
        this.NotifyPropertyChanged("DataVolume");
      }
    }


    [Browsable(true)]
    public string LastUpdate
    {
      get { return this.lastUpdate; }
      set
      {
        this.lastUpdate = value;
        this.NotifyPropertyChanged("LastUpdate");
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
