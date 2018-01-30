namespace Minary.Plugin.Main.HttpSearch.DataTypes.Interface
{
  using Minary.Plugin.Main.HttpSearch.DataTypes.Class;
  using System.Collections.Generic;


  public interface IObservable
  {
    void AddObserver(IObserver observer);
    void Notify(List<HttpFoundRecord> findings);
  }
}
