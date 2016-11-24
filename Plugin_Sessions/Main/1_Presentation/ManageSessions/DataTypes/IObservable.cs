namespace Minary.Plugin.Main.Session.ManageSessions.DataTypes
{
  public interface IObservable
  {
    void AddObserver(IObserver o);
    void Notify();
  }
}