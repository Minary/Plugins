namespace Minary.Plugin.Main.Systems.ManageSystems.DataTypes
{
  public interface IObservable
  {
    void AddObserver(IObserver o);
    void Notify();
  }
}