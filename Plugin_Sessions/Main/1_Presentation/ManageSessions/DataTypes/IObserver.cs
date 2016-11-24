namespace Minary.Plugin.Main.Session.ManageSessions.DataTypes
{
  using System.Collections.Generic;

  public interface IObserver
  {
    void Update(List<SessionPattern> observerObjexts);
  }
}