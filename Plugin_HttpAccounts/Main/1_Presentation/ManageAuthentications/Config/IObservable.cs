using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plugin.Main.HTTPAccounts.ManageAuthentications
{
  public interface IObservable
  {
    void addObserver(IObserver o);
    void notify();
  }
}
