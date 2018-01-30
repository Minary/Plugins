namespace Minary.Plugin.Main.HttpAccounts.ManageAuthentications
{
  using Minary.Plugin.Main.HttpAccounts.ManageAuthentications.DataTypes;
  using System.Collections.Generic;


  public interface IObserver
  {
    void Update(List<HttpAccountPattern> observers);
  }
}
