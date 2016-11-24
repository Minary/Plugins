namespace Minary.Plugin.Main.InjectPayload.DataTypes
{
  using System;
  using System.ComponentModel;


  [Serializable]
  public class InjectPayloadRecord : INotifyPropertyChanged
  {

    #region MEMBERS

    private string requestedScheme;
    private string requestedHost;
    private string requestedPath;
    private string replacementResource;
    private string replacementType;

    [field: NonSerialized]
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion


    #region PUBLIC

    public InjectPayloadRecord()
    {
      this.requestedScheme = string.Empty;
      this.requestedHost = string.Empty;
      this.requestedPath = string.Empty;
      this.replacementResource = string.Empty;
      this.replacementType = string.Empty;
    }


    public InjectPayloadRecord(string requestedScheme, string requestedHost, string requestedPath, string replacementResource, string replacementType)
    {
      this.requestedScheme = requestedScheme;
      this.requestedHost = requestedHost;
      this.requestedPath = requestedPath;
      this.replacementResource = replacementResource;
      this.replacementType = replacementType;
    }

    #endregion


    #region PROPERTIES

    [Browsable(true)]
    public string RequestedScheme
    {
      get
      {
        return this.requestedScheme;
      }

      set
      {
        this.requestedScheme = value;
        this.NotifyPropertyChanged("RequestedScheme");
      }
    }


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
    public string RequestedPath
    {
      get
      {
        return this.requestedPath;
      }

      set
      {
        this.requestedPath = value;
        this.NotifyPropertyChanged("RequestedPath");
      }
    }


    [Browsable(true)]
    public string ReplacementResource
    {
      get
      {
        return this.replacementResource;
      }

      set
      {
        this.replacementResource = value;
        this.NotifyPropertyChanged("ReplacementResource");
      }
    }


    [Browsable(true)]
    public string ReplacementType
    {
      get
      {
        return this.replacementType;
      }

      set
      {
        this.replacementType = value;
        this.NotifyPropertyChanged("ReplacementType");
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