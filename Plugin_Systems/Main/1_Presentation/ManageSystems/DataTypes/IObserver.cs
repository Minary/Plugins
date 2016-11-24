namespace Minary.Plugin.Main.Systems.ManageSystems.DataTypes
{
  using Minary.Plugin.Main.Systems.DataTypes;
  using System.Collections.Generic;

  public interface IObserver
  {
    void Update(List<SystemPattern> observers);
  }
}
