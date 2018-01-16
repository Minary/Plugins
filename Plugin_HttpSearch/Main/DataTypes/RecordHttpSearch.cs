namespace Minary.Plugin.Main.HttpSearch.DataTypes
{
  using System;
  using System.ComponentModel;


  [Serializable]
  public class RecordHttpSearch : INotifyPropertyChanged
  {

    #region MEMBERS

    private string method = string.Empty;
    private string type = string.Empty;
    private string dataRegex = string.Empty;
    private string hostRegex = string.Empty;
    private string pathRegex = string.Empty;
    private string domain = string.Empty;

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
    public string Domain
    {
      get
      {
        return this.domain;
      }

      set
      {
        this.domain = value;
        this.NotifyPropertyChanged("Domain");
      }
    }


    [Browsable(true)]
    public string DataRegex
    {
      get
      {
        return this.dataRegex;
      }

      set
      {
        this.dataRegex = value;
        this.NotifyPropertyChanged("DataRegex");
      }
    }


    [Browsable(true)]
    public string HostRegex
    {
      get
      {
        return this.hostRegex;
      }

      set
      {
        this.hostRegex = value;
        this.NotifyPropertyChanged("HostRegex");
      }
    }


    [Browsable(true)]
    public string PathRegex
    {
      get
      {
        return this.pathRegex;
      }

      set
      {
        this.pathRegex = value;
        this.NotifyPropertyChanged("PathRegex");
      }
    }

    #endregion


    #region PUBLIC

    public RecordHttpSearch()
    {
    }


    public RecordHttpSearch(string method, string type, string domain, string hostRegex, string pathRegex, string dataRegex)
    {
      this.method = method;
      this.type = type;
      this.dataRegex = dataRegex;
      this.hostRegex = hostRegex;
      this.pathRegex = pathRegex;
      this.domain = domain;
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
