namespace Minary.Plugin.Main.HttpSearch.DataTypes.Interface
{
  using Minary.Plugin.Main.HttpSearch.DataTypes.Class;
  using System.Collections.Generic;


  public interface IObserver
  {
    void Update(List<HttpFoundRecord> newRecords);
  }
}
