namespace Minary.Plugin.Main.InjectFile.DataTypes
{
  using System;
  using System.ComponentModel;


  [Serializable]
  public class InjectFileRecord : INotifyPropertyChanged
  {

    #region MEMBERS

    private string requestedHostRegex;
    private string requestedPathRegex;
    private string replacementResource;

    [field: NonSerialized]
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion


    #region PUBLIC

    public InjectFileRecord()
    {
      this.requestedHostRegex = string.Empty;
      this.requestedPathRegex = string.Empty;
      this.replacementResource = string.Empty;
    }


    public InjectFileRecord(string requestedHost, string requestedPath, string replacementResource)
    {
      this.requestedHostRegex = requestedHost;
      this.requestedPathRegex = requestedPath;
      this.replacementResource = replacementResource;
    }

    #endregion


    #region PROPERTIES

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