namespace Minary.Plugin.Main.HostMapping.DataTypes
{
  using System;
  using System.ComponentModel;


  [Serializable]
  public class HostMappingRecord : INotifyPropertyChanged
  {

    #region MEMBERS

    private string requestedHost;
    private string mappedHost;

    [field: NonSerialized]
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion


    #region PROPERTIES

    [Browsable(true)]
    public string RequestedHost
    {
      get
      {
        return this.requestedHost;
      }

      set
      {
        this.requestedHost = value;
        this.NotifyPropertyChanged("RequestedHost");
      }
    }


    [Browsable(true)]
    public string MappedHost
    {
      get
      {
        return this.mappedHost;
      }

      set
      {
        this.mappedHost = value;
        this.NotifyPropertyChanged("MappedHost");
      }
    }

    #endregion


    #region PUBLIC

    public HostMappingRecord()
    {
      this.requestedHost = string.Empty;
      this.mappedHost = string.Empty;
    }


    public HostMappingRecord(string requestedHost, string mappedHost)
    {
      this.requestedHost = requestedHost;
      this.mappedHost = mappedHost;
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
