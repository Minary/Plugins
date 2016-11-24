namespace Minary.Plugin.Main.Systems.DataTypes
{
  using Minary.Plugin.Main.Systems.ManageSystems.DataTypes;
  using System;
  using System.ComponentModel;


  [Serializable]
  public class SystemPattern : INotifyPropertyChanged
  {

    #region MEMBERS

//    private TemplateConfig config;
    private string systemPatternstring;
    private string systemName;
    private string source;
    private bool isEnabled;

    [field: NonSerialized]
    private string patternFileFullPath;

    [field: NonSerialized]
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion


    #region PUBLIC METHODS

    /// <summary>
    /// Initializes a new instance of the <see cref="SystemPattern"/> class.
    ///
    /// </summary>
    public SystemPattern()
    {
      this.systemPatternstring = string.Empty;
      this.systemName = string.Empty;
      this.source = string.Empty;
      this.isEnabled = true;
      this.patternFileFullPath = string.Empty;
//      this.config = new TemplateConfig();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SystemPattern"/> class.
    ///
    /// </summary>
    /// <param name="pSystemPatternstring"></param>
    /// <param name="pSystemName"></param>
    /// <param name="source"></param>
    public SystemPattern(string systemPatternstring, string systemName, string source, string patternFileFullPath)
    {
      this.systemPatternstring = systemPatternstring;
      this.systemName = systemName;
      this.source = source;
      this.isEnabled = true;
      this.patternFileFullPath = patternFileFullPath;
 //     this.config = new TemplateConfig();
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
      bool retVal = false;

      if (obj is SystemPattern)
      {
        if (((SystemPattern)obj).SystemPatternstring.ToLower() == SystemPatternstring.ToLower())
        {
          retVal = true;
        }
      }

      return retVal;
    }

    #endregion


    #region PROPERTIES

    //[Browsable(false)]    
    //public TemplateConfig Config
    //{
    //  get { return this.config; }
    //  set
    //  {
    //    this.config = value;
    //    this.NotifyPropertyChanged("TemplateConfig");
    //  }
    //}


    [Browsable(true)]
    public string SystemName
    {
      get
      {
        return this.systemName;
      }

      set
      {
        this.systemName = value;
        this.NotifyPropertyChanged("SystemName");
      }
    }


    [Browsable(true)]
    public string SystemPatternstring
    {
      get
      {
        return this.systemPatternstring;
      }

      set
      {
        this.systemPatternstring = value;
        this.NotifyPropertyChanged("SystemPatternstring");
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
        this.NotifyPropertyChanged("PatternFileFullPath");
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
