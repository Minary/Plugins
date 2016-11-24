namespace Minary.Plugin.Main.DnsPoison.DataTypes
{
  using System;
  using System.ComponentModel;


  [Serializable]
  public class TemplateDnsPoison
  {

    #region MEMBERS

    private BindingList<RecordDnsPoison> poisonRecordList;

    #endregion


    #region PROPERTIES

    public BindingList<RecordDnsPoison> PoisonRecordList
    {
      get { return this.poisonRecordList; }
      set { this.poisonRecordList = value; }
    }

    #endregion


    #region PUBLIC

    public TemplateDnsPoison()
    {
      this.poisonRecordList = new BindingList<RecordDnsPoison>();
    }

    #endregion

  }
}
