namespace Minary.Plugin.Main.HttpSearch.DataTypes.Interface
{

  public interface IObservableRecordFinding
  {
    void AddObserverRecordFound(IObserverRecordFinding observer);
    void NotifyFindingRecords();
  }
}
