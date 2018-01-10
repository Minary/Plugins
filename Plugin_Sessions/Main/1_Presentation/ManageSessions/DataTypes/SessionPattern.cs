namespace Minary.Plugin.Main.Session.ManageSessions.DataTypes
{
  using System;
  using System.ComponentModel;


  [Serializable]
  public class SessionPattern : INotifyPropertyChanged
  {

    #region MEMBERS

 //   private TemplateConfig config;
    private string companyName;
    private string companyWebpage;
    private string httpHostRegex;
    private string sessionRegex;
    private string source;
    private bool isEnabled;

    [field: NonSerialized]
    private string patternFileFullPath;

    [field: NonSerialized]
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion


    #region PROPERTIES

    [Browsable(true)]
    public string CompanyName
    {
      get
      {
        return this.companyName;
      }

      set
      {
        this.companyName = value;
        this.NotifyPropertyChanged("CompanyName");
      }
    }


    [Browsable(true)]
    public string CompanyWebpage
    {
      get
      {
        return this.companyWebpage;
      }

      set
      {
        this.companyWebpage = value;
        this.NotifyPropertyChanged("CompanyWebpage");
      }
    }


    [Browsable(true)]
    public string HTTPHostRegex
    {
      get
      {
        return this.httpHostRegex;
      }

      set
      {
        this.httpHostRegex = value;
        this.NotifyPropertyChanged("HTTPHostRegex");
      }
    }


    [Browsable(true)]
    public string SessionRegex
    {
      get
      {
        return this.sessionRegex;
      }

      set
      {
        this.sessionRegex = value;
        this.NotifyPropertyChanged("SessionRegex");
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
    /// Initializes a new instance of the <see cref="SessionPattern"/> class.
    ///
    /// </summary>
    public SessionPattern()
    {
//      this.config = new TemplateConfig();
      this.companyName = string.Empty;
      this.companyWebpage = string.Empty;
      this.httpHostRegex = string.Empty;
      this.sessionRegex = string.Empty;
      this.isEnabled = true;
      this.patternFileFullPath = string.Empty;
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="SessionPattern"/> class.
    ///
    /// </summary>
    /// <param name="sessionPatternstring"></param>
    /// <param name="sessionName"></param>
    /// <param name="httpHost"></param>
    /// <param name="companyWebpage"></param>
    public SessionPattern(string sessionPatternstring, string companyName, string httpHost, string companyWebpage, string patternFileFullPath)
    {
      this.companyName = companyName;
      this.companyWebpage = companyWebpage;
      this.httpHostRegex = httpHost;
      this.sessionRegex = sessionPatternstring;
      this.isEnabled = true;
      this.patternFileFullPath = patternFileFullPath;

//      this.config = new TemplateConfig();
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
      bool retVal = false;

      if (obj is SessionPattern)
      {
        SessionPattern tmpSessionPattern = (SessionPattern)obj;
        if (tmpSessionPattern.sessionRegex == this.sessionRegex && tmpSessionPattern.httpHostRegex == this.httpHostRegex)
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
