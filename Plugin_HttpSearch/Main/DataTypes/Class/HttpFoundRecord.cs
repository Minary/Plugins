namespace Minary.Plugin.Main.HttpSearch.DataTypes.Class
{
  using System;
  using System.ComponentModel;


  [Serializable]
  public class HttpFoundRecord : INotifyPropertyChanged
  {

    #region MEMBERS

    private string method = string.Empty;
    private string type = string.Empty;
    private string host = string.Empty;
    private string path = string.Empty;
    private string finding = string.Empty;

    [field: NonSerialized]
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion


    #region PROPERTIES

    [Browsable(true)]
    public string Method
    {
      get
      {
        return this.method;
      }

      set
      {
        this.method = value;
        this.NotifyPropertyChanged("Method");
      }
    }

    [Browsable(true)]
    public string Type
    {
      get
      {
        return this.type;
      }

      set
      {
        this.type = value;
        this.NotifyPropertyChanged("Type");
      }
    }

    [Browsable(true)]
    public string Host
    {
      get
      {
        return this.host;
      }

      set
      {
        this.host = value;
        this.NotifyPropertyChanged("Host");
      }
    }

    [Browsable(true)]
    public string Path
    {
      get
      {
        return this.path;
      }

      set
      {
        this.path = value;
        this.NotifyPropertyChanged("Path");
      }
    }

    [Browsable(true)]
    public string Finding
    {
      get
      {
        return this.finding;
      }

      set
      {
        this.finding = value;
        this.NotifyPropertyChanged("Finding");
      }
    }

    #endregion


    #region PUBLIC

    public HttpFoundRecord()
    {
    }


    public HttpFoundRecord(string method, string type, string host, string path, string finding)
    {
      this.method = method;
      this.host = host;
      this.path = path;
      this.finding = finding;
    }

    #endregion


    #region PRIVATE

    /// <summary>
    ///
    /// </summary>
    /// <param name="name"></param>
    private void NotifyPropertyChanged(string name)
    {
      if (this.PropertyChanged != null)
      {
        this.PropertyChanged(this, new PropertyChangedEventArgs(name));
      }
    }

    #endregion

  }
}
