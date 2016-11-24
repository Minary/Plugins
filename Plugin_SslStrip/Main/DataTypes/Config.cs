using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plugin.Main.SSLStrip.Config
{

    #region TYPE DEFINITION

    public delegate void onSSLStripExitDelegate();

    #endregion


    /// <summary>
    /// Global injection Configuration
    /// </summary>
    public class SSLStripConfig
    {
        public bool isDebuggingOn;
        public onSSLStripExitDelegate onSSLStripExit;
        public String BasisDirectory;
//        public String ConfigurationFile;
//        public String StructureParameter;
    }

    /// <summary>
    /// 
    /// </summary>
    public class ExceptionRecords : Exception
    {
        public ExceptionRecords(String pMsg) : base(pMsg)
        {
        }
    }

}
