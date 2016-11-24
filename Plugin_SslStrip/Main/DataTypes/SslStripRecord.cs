namespace Minary.Plugin.Main.SslStrip.DataTypes
{
  using System;
  using System.ComponentModel;


  [Serializable]
  public class SslStripRecord : INotifyPropertyChanged
  {

    #region MEMBERS

    private string hostName;
    private string contentType;

    [field: NonSerialized]
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion


    #region PUBLIC

    public SslStripRecord()
    {
      this.hostName = string.Empty;
      this.contentType = string.Empty;
    }


    public SslStripRecord(string hostName, string contentType)
    {
      this.hostName = hostName;
      this.contentType = contentType;
    }

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

    public string ContentType
    {
      get
      {
        return this.contentType;
      }

      set
      {
        this.contentType = value;
        this.NotifyPropertyChanged("ContentType");
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