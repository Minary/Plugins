namespace Minary.Plugin.Main.HttpSearch.DataTypes.Interface
{
  using Minary.Plugin.Main.HttpSearch.DataTypes.Class;
  using System.Collections.Generic;


  public interface IObservableRecordFinding
  {
    void AddObserverRecordFound(IObserverRecordFinding observer);
    void NotifyFindingRecords(List<RecordHttpRequestData> newRecords);
  }
}
