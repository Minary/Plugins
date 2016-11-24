using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Plugin.Main.Session;
using Plugin.Main.Session.Config;


namespace Plugin.Main
{

  public partial class PluginSessionsUC
  {

    #region SESSION METHODS

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pSessionID"></param>
    /// <returns></returns>
    public delegate String onGetSessionDataDelegate(String pSessionID);
    public String onGetSessionData(String pSessionID)
    {
      if (InvokeRequired)
      {
        BeginInvoke(new onGetSessionDataDelegate(onGetSessionData), new object[] { pSessionID });
        return (String.Empty);
      } // if (InvokeRequired)

      String lRetVal = String.Empty;

      try
      {
        lRetVal = cDomain.getSessionData(pSessionID);
      }
      catch (Exception lEx)
      {
        cPluginParams.HostApplication.LogMessage(String.Format("{0}: {1}", Config.PluginName, lEx.Message));
      }

      return (lRetVal);
    }


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
        cPluginParams.HostApplication.LogMessage(String.Format("{0}: {1}", Config.PluginName, lEx.Message));
      }

      try
      {
        List<Session.Config.Session> lSessionData = cDomain.loadSessionData(pSessionName);

        lock (this)
        {
          DGV_Sessions.SuspendLayout();

          if (lSessionData != null && lSessionData.Count > 0)
            foreach (Session.Config.Session lTmp in lSessionData)
              cSessions.Add(lTmp);

          DGV_Sessions.ResumeLayout();
        } // lock(thi..
      }
      catch (Exception lEx)
      {
        this.cPluginParams.HostApplication.LogMessage(String.Format("{0}: {1}", Config.PluginName, lEx.Message));
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
        BindingList<Session.Config.Session> lSessionData = cDomain.loadSessionDataFromString(pSessionData);
        // Load session data

        lock (this)
        {
          DGV_Sessions.SuspendLayout();

          if (lSessionData != null && lSessionData.Count > 0)
            foreach (Session.Config.Session lTmp in lSessionData)
              cSessions.Add(lTmp);

          DGV_Sessions.ResumeLayout();
        } // lock(thi..
      }
      catch (Exception lEx)
      {
        this.cPluginParams.HostApplication.LogMessage(String.Format("{0}: {1}", Config.PluginName, lEx.Message));
      }
    }


    /// <summary>
    /// Serialize session data
    /// </summary>
    /// <param name="pSessionName"></param>
    public delegate void onSaveSessionDataDelegate(String pSessionName);
    public void onSaveSessionData(String pSessionName)
    {
      if (Config.IsActive)
      {
        if (InvokeRequired)
        {
          BeginInvoke(new onSaveSessionDataDelegate(onSaveSessionData), new object[] { pSessionName });
          return;
        } // if (InvokeRequired)

        cDomain.saveSession(cSessions.ToList(), pSessionName);
      } // if (cIsActiv...
    }


    #endregion


  }

}