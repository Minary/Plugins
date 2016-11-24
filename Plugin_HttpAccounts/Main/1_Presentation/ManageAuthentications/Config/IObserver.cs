using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Plugin.Main.HTTPAccounts.ManageAuthentications
{
  public interface IObserver
  {
    void update(List<AccountPattern> oDict);
  }
}
