namespace Minary.Plugin.Main.HttpAccounts.ManageAuthentications.DataTypes
{
  public interface IObservable
  {
    void AddObserver(IObserver o);
    void Notify();
  }
}
