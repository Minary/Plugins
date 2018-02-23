namespace Minary.Plugin.Main.DnsPoisoning.DataTypes
{
  using System;
  using System.ComponentModel;


  [Serializable]
  public class TemplateDnsPoison
  {

    #region PROPERTIES

    public BindingList<RecordDnsPoison> PoisonRecordList { get; set; } = new BindingList<RecordDnsPoison>();

    #endregion

  }
}
