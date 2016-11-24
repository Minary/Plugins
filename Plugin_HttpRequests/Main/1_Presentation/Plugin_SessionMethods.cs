using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Plugin.Main.HTTPRequest;


namespace Plugin.Main
{

  public partial class PluginHTTPRequestsUC
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

      lRetVal = cDomain.getSessionData(pSessionID);

      return (lRetVal);
    }



    /// <summary>
    /// Remove session file with serialized data. 
    /// </summary>
    /// <param name="pSessionFileName"></param>
    public delegate void onDeleteSessionDataDelegate(String pSessionID);
    public void onDeleteSessionData(String pSessionID)
    {
      if (InvokeRequired)
      {
        BeginInvoke(new onDeleteSessionDataDelegate(onDeleteSessionData), new object[] { pSessionID });
        return;
      } // if (InvokeRequired)

      try
      {
        cDomain.deleteSession(pSessionID);
      }
      catch (Exception lEx)
      {
        PluginParameters.HostApplication.LogMessage(String.Format("{0}: {1}", Config.PluginName, lEx.Message));
      }
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
        PluginParameters.HostApplication.LogMessage(String.Format("{0}: {1}", Config.PluginName, lEx.Message));
      }


      try
      {
        // Update DataGridView
        BindingList<HTTPRequests> lSessionData = cDomain.loadSessionData(pSessionName);
        DGV_HTTPRequests.SuspendLayout();

        lock (this)
        {
          cHTTPRequests.Clear();
          if (lSessionData != null && lSessionData.Count > 0)
            foreach (HTTPRequests lTmp in lSessionData)
              cHTTPRequests.Insert(0, lTmp);
        } // lock(thi...

        DGV_HTTPRequests.ResumeLayout();
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
        BindingList<HTTPRequests> lRecords = cDomain.loadSessionDataFromString(pSessionData);

        DGV_HTTPRequests.SuspendLayout();

        lock (this)
        {
          cHTTPRequests.Clear();
          if (lRecords != null && lRecords.Count > 0)
            foreach (HTTPRequests lTmp in lRecords)
              cHTTPRequests.Insert(0, lTmp);
        } // lock(thi...

        DGV_HTTPRequests.ResumeLayout();
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


        try
        {
          cDomain.saveSession(cHTTPRequests.ToList(), pSessionName);
        }
        catch (Exception lEx)
        {
          PluginParameters.HostApplication.LogMessage(String.Format("{0}: {1}", Config.PluginName, lEx.Message));
        }
      } // if (cIsActiv...
    }





    #endregion

  }

}
