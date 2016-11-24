namespace Minary.Plugin.Main.IpAccounting.DataTypes
{
  public interface IObservable
  {
    void AddObserver(IObserver o);
    void Notify();
  }
}
