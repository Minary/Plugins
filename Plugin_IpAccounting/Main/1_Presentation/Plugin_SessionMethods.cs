using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;




namespace Plugin.Main
{

  public partial class PluginIPAccountingUC
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
                cPluginParams.HostApplication.LogMessage(String.Format("{0}: {1}", Config.PluginName, lEx.Message));
            }

            try
            {
                cDomain.loadSessionData(pSessionName);
            }
            catch (Exception lEx)
            {
                cPluginParams.HostApplication.LogMessage(String.Format("{0}: {1}", Config.PluginName, lEx.Message));
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

            cDomain.loadSessionDataFromString(pSessionData);
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

                cDomain.saveSession(pSessionName);
            } // if (cIsActiv...
        }



        /// <summary>
        /// Remove session file with serialized data.
        /// </summary>
        /// <param name="pSessionName"></param>
        public delegate void onDeleteSessionDataDelegate(String pSessionName);
        public void onDeleteSessionData(String pSessionName)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new onDeleteSessionDataDelegate(onDeleteSessionData), new object[] { pSessionName });
                return;
            } // if (InvokeRequired)

            cDomain.deleteSession(pSessionName);
        }
    

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

          String lRetVal = cDomain.getSessionData(pSessionID);

          return (lRetVal);
        }


        #endregion

    
  }

}

