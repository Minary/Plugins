namespace Minary.Plugin.Main.IpAccounting.DataTypes
{
  using System.Collections.Generic;

  public interface IObserver
  {
    void Update(List<AccountingItem> observerList);
  }
}
