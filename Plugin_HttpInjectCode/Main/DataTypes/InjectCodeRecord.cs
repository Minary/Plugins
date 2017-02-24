namespace Minary.Plugin.Main.InjectCode.DataTypes
{
  using System;
  using System.ComponentModel;


  [Serializable]
  public class InjectCodeRecord : INotifyPropertyChanged
  {

    #region MEMBERS

    private string requestedScheme;
    private string requestedHost;
    private string requestedPath;
    private string injectionCodeFile;
    private string tag;
    private string position;

    [field: NonSerialized]
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion


    #region PUBLIC

    public InjectCodeRecord()
    {
      this.requestedScheme = string.Empty;
      this.requestedHost = string.Empty;
      this.requestedPath = string.Empty;
      this.injectionCodeFile = string.Empty;
      this.tag = string.Empty;
      this.position = string.Empty;
    }


    public InjectCodeRecord(string requestedScheme, string requestedHost, string requestedPath, string replacementResource, string tag, string position)
    {
      this.requestedScheme = requestedScheme;
      this.requestedHost = requestedHost;
      this.requestedPath = requestedPath;
      this.injectionCodeFile = replacementResource;
      this.tag = tag;
      this.position = position;
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
    public string InjectionCodeFile
    {
      get
      {
        return this.injectionCodeFile;
      }

      set
      {
        this.injectionCodeFile = value;
        this.NotifyPropertyChanged("InjectionCodeFile");
      }
    }


    [Browsable(true)]
    public string Tag
    {
      get
      {
        return this.tag;
      }

      set
      {
        this.tag = value;
        this.NotifyPropertyChanged("Tag");
      }
    }


    [Browsable(true)]
    public string Position
    {
      get
      {
        return this.position;
      }

      set
      {
        this.position = value;
        this.NotifyPropertyChanged("Potision");
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