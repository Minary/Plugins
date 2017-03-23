namespace Minary.Plugin.Main.RequestRedirect.DataTypes
{
  using System;
  using System.ComponentModel;


  [Serializable]
  public class RequestRedirectRecord : INotifyPropertyChanged
  {

    #region MEMBERS

    private string redirectType;
    private string redirectDescription;
    private string requestedHostRegex;
    private string requestedPathRegex;
    private string replacementResource;

    [field: NonSerialized]
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion


    #region PUBLIC

    public RequestRedirectRecord()
    {
      this.redirectType = string.Empty;
      this.redirectDescription = string.Empty;
      this.requestedHostRegex = string.Empty;
      this.requestedPathRegex = string.Empty;
      this.replacementResource = string.Empty;
    }


    public RequestRedirectRecord(string redirectType, string redirectDescription, string requestedHostRegex, string requestedPathRegex, string replacementResource)
    {
      this.redirectType = redirectType;
      this.redirectDescription = redirectDescription;
      this.requestedHostRegex = requestedHostRegex;
      this.requestedPathRegex = requestedPathRegex;
      this.replacementResource = replacementResource;
    }

    #endregion


    #region PROPERTIES

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


    [Browsable(true)]
    public string RequestedHostRegex
    {
      get
      {
        return this.requestedHostRegex;
      }

      set
      {
        this.requestedHostRegex = value;
        this.NotifyPropertyChanged("RequestedHostRegex");
      }
    }


    [Browsable(true)]
    public string RequestedPathRegex
    {
      get
      {
        return this.requestedPathRegex;
      }

      set
      {
        this.requestedPathRegex = value;
        this.NotifyPropertyChanged("RequestedPathRegex");
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