namespace Minary.Plugin.Main.HttpAccounts.ManageAuthentications.DataTypes
{
  using System;
  using System.ComponentModel;


  [Serializable]
  public class HttpAccountPattern : INotifyPropertyChanged
  {

    #region MEMBERS

//    private TemplateConfig config;
    private string httpMethod;
    private string hostPattern;
    private string pathPattern;
    private string dataPattern;
    private string company;
    private string webPage;
    private string source;
    private bool isEnabled;
    [field: NonSerialized]
    private string patternFileFullPath;

    [field: NonSerialized]
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion


    #region PROPERTIES

    [Browsable(true)]
    public string Method
    {
      get
      {
        return this.httpMethod;
      }

      set
      {
        this.httpMethod = value;
        this.NotifyPropertyChanged("Method");
      }
    }


    [Browsable(true)]
    public string HostPattern
    {
      get
      {
        return this.hostPattern;
      }

      set
      {
        this.hostPattern = value;
        this.NotifyPropertyChanged("HostPattern");
      }
    }


    [Browsable(true)]
    public string PathPattern
    {
      get
      {
        return this.pathPattern;
      }

      set
      {
        this.pathPattern = value;
        this.NotifyPropertyChanged("PathPattern");
      }
    }


    [Browsable(true)]
    public string DataPattern
    {
      get
      {
        return this.dataPattern;
      }

      set
      {
        this.dataPattern = value;
        this.NotifyPropertyChanged("DataPattern");
      }
    }


    [Browsable(true)]
    public string Company
    {
      get
      {
        return this.company;
      }

      set
      {
        this.company = value;
        this.NotifyPropertyChanged("Company");
      }
    }


    [Browsable(true)]
    public string WebPage
    {
      get
      {
        return this.webPage;
      }

      set
      {
        this.webPage = value;
        this.NotifyPropertyChanged("WebPage");
      }
    }


    [Browsable(true)]
    public string Source
    {
      get
      {
        return this.source;
      }

      set
      {
        this.source = value;
        this.NotifyPropertyChanged("Source");
      }
    }


    [Browsable(true)]
    public bool IsEnabled
    {
      get
      {
        return this.isEnabled;
      }

      set
      {
        this.isEnabled = value;
        this.NotifyPropertyChanged("IsEnabled");
      }
    }


    [Browsable(false)]
    public string PatternFileFullPath
    {
      get
      {
        return this.patternFileFullPath;
      }

      set
      {
        this.patternFileFullPath = value;
      }
    }

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpAccountPattern"/> class.
    ///
    /// </summary>
    public HttpAccountPattern()
    {
      //      this.config = new TemplateConfig();
      this.httpMethod = string.Empty;
      this.hostPattern = string.Empty;
      this.pathPattern = string.Empty;
      this.dataPattern = string.Empty;
      this.company = string.Empty;
      this.webPage = string.Empty;
      this.source = string.Empty;
      this.isEnabled = true;
      this.patternFileFullPath = string.Empty;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="method"></param>
    /// <param name="host"></param>
    /// <param name="pathPattern"></param>
    /// <param name="dataPattern"></param>
    /// <param name="company"></param>
    /// <param name="webPage"></param>
    /// <param name="source"></param>
    /// <param name="patternFileFullPath"></param>
    public HttpAccountPattern(string method, string host, string pathPattern, string dataPattern, string company, string webPage, string source, string patternFileFullPath)
    {
      //      this.config = new TemplateConfig();
      this.httpMethod = method;
      this.hostPattern = host;
      this.pathPattern = pathPattern;
      this.dataPattern = dataPattern;
      this.company = company;
      this.webPage = webPage;
      this.source = source;
      this.isEnabled = true;
      this.patternFileFullPath = patternFileFullPath;
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object compObj)
    {
      bool retVal = false;

      if (compObj is HttpAccountPattern)
      {
        HttpAccountPattern tmp = (HttpAccountPattern)compObj;
        if (tmp.Method == this.Method && tmp.HostPattern == this.HostPattern && tmp.PathPattern == this.PathPattern)
        {
          retVal = true;
        }
      }

      return retVal;
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
