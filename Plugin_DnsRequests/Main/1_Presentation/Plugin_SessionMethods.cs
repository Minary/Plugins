using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plugin.Main
{

  public partial class PluginDNSRequestsUC
  {

    #region SESSION METHODS



    /// <summary>
    /// 
    /// </summary>
    /// <param name="pSessionName"></param>
    public delegate void onLoadSessionDataFromFileDelegate(String pSessionName);
    public void onLoadSessionDataFromFile(String pSessionName)
    {
      if (InvokeRequired)
      {
        BeginInvoke(new onLoadSessionDataFromFileDelegate(onLoadSessionDataFromFile), new object[] { pSessionName });
        return;
      } // if (InvokeRequired)

      try
      {
        onResetPlugin();
      }
      catch (Exception lEx)
      {
        PluginParameters.HostApplication.LogMessage(String.Format("{0}: {1}", Config.PluginName, lEx.Message));
      }


      try
      {
        cDomain.loadSessionData(pSessionName);
      }
      catch (Exception lEx)
      {
        PluginParameters.HostApplication.LogMessage(String.Format("{0}: {1}", Config.PluginName, lEx.Message));
      }
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="pSessionData"></param>
    public delegate void onLoadSessionDataFromStringDelegate(String pSessionData);
    public void onLoadSessionDataFromString(String pSessionData)
    {
      if (InvokeRequired)
      {
        BeginInvoke(new onLoadSessionDataFromStringDelegate(onLoadSessionDataFromString), new object[] { pSessionData });
        return;
      } // if (InvokeRequired)

      try
      {
        cDomain.loadSessionDataFromString(pSessionData);
      }
      catch (Exception lEx)
      {
        PluginParameters.HostApplication.LogMessage(String.Format("{0}: {1}", Config.PluginName, lEx.Message));
      }
    }




    /// <summary>
    /// Remove session file with serialized data.
    /// </summary>
    /// <param name="pSessionFileName"></param>
    public delegate void onDeleteSessionDataDelegate(String pSessionName);
    public void onDeleteSessionData(String pSessionName)
    {
      if (InvokeRequired)
      {
        BeginInvoke(new onDeleteSessionDataDelegate(onDeleteSessionData), new object[] { pSessionName });
        return;
      } // if (InvokeRequired)


      try
      {
        cDomain.deleteSession(pSessionName);
      }
      catch (Exception lEx)
      {
        PluginParameters.HostApplication.LogMessage(String.Format("{0}: {1}", Config.PluginName, lEx.Message));
      }
    }



    /// <summary>
    /// Serialize session data
    /// </summary>
    /// <param name="pSessionName"></param>
    public delegate void onSaveSessionDataDelegate(string pSessionName);
    public void onSaveSessionData(string pSessionName)
    {
      if (InvokeRequired)
      {
        BeginInvoke(new onSaveSessionDataDelegate(onSaveSessionData), new object[] { pSessionName });
        return;
      } // if (InvokeRequired)



      if (Config.IsActive)
      {
        try
        {
          cDomain.saveSession(cDNSRequests, pSessionName);
        }
        catch (Exception lEx)
        {
          PluginParameters.HostApplication.LogMessage(String.Format("{0}: {1}", Config.PluginName, lEx.Message));
        }
      } // if (cIsActive)
    }


    #endregion

  }

}
