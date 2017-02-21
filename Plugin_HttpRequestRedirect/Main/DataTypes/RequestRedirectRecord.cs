namespace Minary.Plugin.Main.RequestRedirect.DataTypes
{
  using System;
  using System.ComponentModel;


  [Serializable]
  public class RequestRedirectRecord : INotifyPropertyChanged
  {

    #region MEMBERS

    private string requestedScheme;
    private string requestedHost;
    private string requestedPath;
    private string replacementResource;
    private string redirectType;
    private string redirectDescription;

    [field: NonSerialized]
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion


    #region PUBLIC

    public RequestRedirectRecord()
    {
      this.requestedScheme = string.Empty;
      this.requestedHost = string.Empty;
      this.requestedPath = string.Empty;
      this.replacementResource = string.Empty;
      this.redirectType = string.Empty;
      this.redirectDescription = string.Empty;
    }


    public RequestRedirectRecord(string requestedScheme, string requestedHost, string requestedPath, string replacementResource, string redirectType, string redirectDescription)
    {
      this.requestedScheme = requestedScheme;
      this.requestedHost = requestedHost;
      this.requestedPath = requestedPath;
      this.replacementResource = replacementResource;
      this.redirectType = redirectType;
      this.redirectDescription = redirectDescription;
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
    public string RedirectType
    {
      get
      {
        return this.redirectType;
      }

      set
      {
        this.redirectType = value;
        this.NotifyPropertyChanged("RedirectType");
      }
    }


    [Browsable(true)]
    public string RedirectDescription
    {
      get
      {
        return this.redirectDescription;
      }

      set
      {
        this.redirectDescription = value;
        this.NotifyPropertyChanged("RedirectDescription");
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