using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Plugin.Main.SSLStrip
{
    public class SSLStripRecord : INotifyPropertyChanged
    {

        #region MEMBERS

        private String cHostName;
        private String cContentType;
        private String cTag;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion


        #region PUBLIC

        public SSLStripRecord()
        {
            cHostName = String.Empty;
            cContentType = String.Empty;
            cTag = String.Empty;
        }


        public SSLStripRecord(String pHostName, String pContentType, String pTag)
        {
            cHostName = pHostName;
            cContentType = pContentType;
            cTag = pTag;
        }

        #endregion


        #region PROPERTIES


        public String HostName
        {
            get { return cHostName; }
            set
            {
                cHostName = value;
                this.NotifyPropertyChanged("HostName");
            }
        }

        public String ContentType
        {
            get { return cContentType; }
            set
            {
                cContentType = value;
                this.NotifyPropertyChanged("ContentType");
            }
        }

        public String Tag
        {
            get { return cTag; }
            set
            {
                cTag = value;
                this.NotifyPropertyChanged("Tag");
            }
        }

        #endregion


        #region PRIVATE

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pName"></param>
        private void NotifyPropertyChanged(string pName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(pName));
        }

        #endregion

    }
}
