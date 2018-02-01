namespace Minary.Plugin.Main.HttpSearch.DataTypes.Interface
{
  using Minary.Plugin.Main.HttpSearch.DataTypes.Class;
  using System.Collections.Generic;


  public interface IObserverRecordFound
  {
    void UpdateRecordsFound(List<HttpFoundRecord> newRecords);
  }
}
