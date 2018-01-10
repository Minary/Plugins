namespace Minary.Plugin.Main.InjectCode.DataTypes
{
  using System;
  using System.ComponentModel;


  [Serializable]
  public class InjectCodeRecord : INotifyPropertyChanged
  {

    #region MEMBERS

    private string requestedScheme;
    private string requestedHostRegex;
    private string requestedPathRegex;
    private string injectionCodeFile;
    private string tag;
    private string position;

    [field: NonSerialized]
    public event PropertyChangedEventHandler PropertyChanged;

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


    #region PUBLIC

    public InjectCodeRecord()
    {
      this.requestedScheme = string.Empty;
      this.requestedHostRegex = string.Empty;
      this.requestedPathRegex = string.Empty;
      this.injectionCodeFile = string.Empty;
      this.tag = string.Empty;
      this.position = string.Empty;
    }


    public InjectCodeRecord(string requestedScheme, string requestedHostRegex, string requestedPathRegex, string replacementResource, string tag, string position)
    {
      this.requestedScheme = requestedScheme;
      this.requestedHostRegex = requestedHostRegex;
      this.requestedPathRegex = requestedPathRegex;
      this.injectionCodeFile = replacementResource;
      this.tag = tag;
      this.position = position;
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