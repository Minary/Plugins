namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.HttpsRequest.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Concurrent;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Linq;
  using System.Net;
  using System.Text.RegularExpressions;
  using System.Windows.Forms;


  public partial class Plugin_HttpsRequests : UserControl, IPlugin
  {

    #region MEMBERS

    private const int MaxTableRows = 128;
    private List<Tuple<string, string, string>> targetList;
    private BindingList<RecordHttpsRequest> foundHttpsRequests = new BindingList<RecordHttpsRequest>();
    private List<string> dataBatch = new List<string>();
    private HttpsRequest.Infrastructure.HttpsRequest infrastructureLayer;
    private PluginProperties pluginProperties;
    private ConcurrentDictionary<string, string> dnsReverseCache = new ConcurrentDictionary<string, string>();
    private ConcurrentDictionary<string, string> ipCache = new ConcurrentDictionary<string, string>();

    #endregion


    #region PROPERTIES

    public Control PluginControl { get { return (this); } }

    public BindingList<RecordHttpsRequest> FoundHttpsRequests { get { return this.foundHttpsRequests; } }

    public ConcurrentDictionary<string, string> DnsCache { get { return this.dnsReverseCache; } }

    public ConcurrentDictionary<string, string> IpCache { get { return this.ipCache; } }

    #endregion


    #region PLBLIC

    public Plugin_HttpsRequests(PluginProperties pluginProperties)
    {
      InitializeComponent();
      DataGridViewTextBoxColumn columnMacAddr = new DataGridViewTextBoxColumn();
      columnMacAddr.DataPropertyName = "SrcMAC";
      columnMacAddr.Name = "SrcMAC";
      columnMacAddr.HeaderText = "MAC address";
      columnMacAddr.ReadOnly = true;
      columnMacAddr.Width = 180;
      this.dgv_HttpsRequests.Columns.Add(columnMacAddr);

      DataGridViewTextBoxColumn columnSrcIp = new DataGridViewTextBoxColumn();
      columnSrcIp.DataPropertyName = "SrcIP";
      columnSrcIp.Name = "SrcIP";
      columnSrcIp.HeaderText = "Source IP";
      columnSrcIp.ReadOnly = true;
      columnSrcIp.Width = 150;
      this.dgv_HttpsRequests.Columns.Add(columnSrcIp);

      DataGridViewTextBoxColumn columnTimestamp = new DataGridViewTextBoxColumn();
      columnTimestamp.DataPropertyName = "Timestamp";
      columnTimestamp.Name = "Timestamp";
      columnTimestamp.HeaderText = "Timestamp";
      columnTimestamp.ReadOnly = true;
      columnTimestamp.Visible = false;
      columnTimestamp.Width = 120;
      this.dgv_HttpsRequests.Columns.Add(columnTimestamp);

      DataGridViewTextBoxColumn columnRemHost = new DataGridViewTextBoxColumn();
      columnRemHost.DataPropertyName = "RemoteHost";
      columnRemHost.Name = "RemoteHost";
      columnRemHost.HeaderText = "Server";
      columnRemHost.ReadOnly = true;
      columnRemHost.Width = 250;
      columnRemHost.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_HttpsRequests.Columns.Add(columnRemHost);

      this.dgv_HttpsRequests.DataSource = this.foundHttpsRequests;

      // Verify passed parameter(s)
      if (pluginProperties == null)
      {
        throw new Exception("Parameter PluginParameters is null");
      }

      if (pluginProperties.HostApplication == null)
      {
        throw new Exception("Parameter HostApplication is null");
      }

      if (pluginProperties.ApplicationBaseDir == null)
      {
        throw new Exception("Parameter ApplicationBaseDir is null");
      }

      if (pluginProperties.PluginBaseDir == null)
      {
        throw new Exception("Parameter PluginBaseDir is null");
      }

      // Plugin configuration
      this.pluginProperties = pluginProperties;

      this.pluginProperties.PluginName = "HTTPS requests";
      this.pluginProperties.PluginType = "Passive";
      this.pluginProperties.PluginDescription = "Detect HTTPS  requests.";
      this.pluginProperties.Ports = new Dictionary<int, IpProtocols>() { { 53, IpProtocols.Udp }, { 443, IpProtocols.Tcp } };

      // Instantiate infrastructure layer
      this.infrastructureLayer = new HttpsRequest.Infrastructure.HttpsRequest(this);

      // Initialize plugin environment
      this.infrastructureLayer.OnInit();

      this.t_GuiUpdate.Interval = 1000;
      this.t_GuiUpdate.Start();
    }


    public List<string> DataBatch { get { return this.dataBatch; } private set { } }

    #endregion


    #region PRIVATE

    public void ProcessEntries()
    {
      var newRecords = new List<RecordHttpsRequest>();
      List<string> newDataRecords;
      string[] splitter;
      var proto = string.Empty;
      var macAddr = string.Empty;
      var srcIp = string.Empty;
      var srcPort = string.Empty;
      var dstIp = string.Empty;
      var dstPort = string.Empty;
      var data = string.Empty;
      
      if (this.dataBatch == null ||
          this.dataBatch.Count <= 0)
      {
        return;
      }

      lock (this)
      {
        newDataRecords = new List<string>(this.dataBatch);
        this.dataBatch.Clear();
      }

      foreach (string tmpRecord in newDataRecords)
      {
        try
        {
          if (string.IsNullOrEmpty(tmpRecord))
          {
            continue;
          }

          if ((splitter = Regex.Split(tmpRecord, @"\|\|")).Length < 7)
          {
            continue;
          }

          proto = splitter[0];
          macAddr = splitter[1];
          srcIp = splitter[2];
          srcPort = splitter[3];
          dstIp = splitter[4];
          dstPort = splitter[5];
          data = splitter[6];
          
          // Process DNS Request and populate local
          // DNS cache
          if (proto.ToLower() == "dnsrep" &&
              Regex.Match(data, @"[\w\d-_]{1,}\.[\w\d-_]{2,}").Success == true)
          {
            data = data.TrimEnd(new char[] { ' ', '\t', ',' });
            var elements = data.Split(new char[] { ',' });
            string requestedHost = elements[0];
            int noElements = elements.Length;

            if (noElements > 1)
            {
              for (int i = 1; i < noElements; i++)
              {
                this.dnsReverseCache.TryAdd(elements[i], requestedHost);
              }              
            }

          // Process HTTPS requests
          }
          else if (proto.ToLower() == "https" &&
                   data.ToLower().StartsWith("connect:") == true)
          {
            string[] dataSplit = data.Split(new char[] { ':' });
            var targetIp = dataSplit[1];
            
            // DNS record for this IP address already exists
            if (this.dnsReverseCache.ContainsKey(targetIp) == true)
            {
              var resolvedHost = string.Empty;
              this.dnsReverseCache.TryGetValue(targetIp, out resolvedHost);
              this.ipCache.TryAdd(targetIp, resolvedHost);
              var tmpHostName = string.IsNullOrEmpty(resolvedHost) ? targetIp : resolvedHost;
              newRecords.Add(new RecordHttpsRequest(macAddr, srcIp, $"https://{tmpHostName}/..."));
              continue;

            // IPCache record for this IP address already exists
            }
            else if (this.ipCache.ContainsKey(targetIp) == true)
            {
              var resolvedHost = string.Empty;
              this.ipCache.TryGetValue(dstIp, out resolvedHost);
              var tmpHostName = string.IsNullOrEmpty(resolvedHost) ? targetIp : resolvedHost;
              newRecords.Add(new RecordHttpsRequest(macAddr, srcIp, $"https://{tmpHostName}/..."));
              continue;
            }


            // Resolve IP
            try
            {
              IPHostEntry hostEntry = Dns.GetHostEntry(targetIp);
              hostEntry.AddressList.ToList().ForEach(elem => { this.dnsReverseCache.TryAdd(targetIp, elem.ToString()); });
            }
            catch (Exception ex)
            {
              this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
            }

            if (this.dnsReverseCache.ContainsKey(targetIp) == false)
            {
              var resolvedHost = string.Empty;
              this.dnsReverseCache.TryGetValue(dstIp, out resolvedHost);
              var tmpHostName = string.IsNullOrEmpty(resolvedHost) ? targetIp : resolvedHost;
              newRecords.Add(new RecordHttpsRequest(macAddr, srcIp, $"https://{tmpHostName}/..."));
              this.ipCache.TryAdd(targetIp, resolvedHost);
            }
            else
            {
              var resolvedHost = string.Empty;
              var tmpHostName = string.IsNullOrEmpty(resolvedHost) ? targetIp : resolvedHost;
              newRecords.Add(new RecordHttpsRequest(macAddr, srcIp, $"https://{tmpHostName}/..."));
              this.ipCache.TryAdd(targetIp, resolvedHost);
            }
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show($"{this.Config.PluginName} : {ex.ToString()}");
        }
      }
      
      if (newRecords.Count > 0)
      {
        try
        {
          this.AddRecords(newRecords);
        }
        catch (Exception ex)
        {
          MessageBox.Show($"{this.Config.PluginName} : {ex.ToString()}");
        }
      }
    }

    
    private bool CompareToFilter(string inputData)
    {
      bool retVal = false;

      if (inputData != null && 
          inputData.Length > 0)
      {
        if (Regex.Match(inputData, Regex.Escape(this.tb_Filter.Text), RegexOptions.IgnoreCase).Success)
        {
          retVal = true;
        }
      }

      return retVal;
    }

    
    private void UseFilter()
    {
      if (this.dgv_HttpsRequests.Rows.Count <= 0)
      {
        return;
      }

      // TODO: Without this line we will get an exception. FIX IT!
      this.dgv_HttpsRequests.CurrentCell = null;
      for (var i = 0; i < this.dgv_HttpsRequests.RowCount; i++)
      {
        if (this.tb_Filter.Text.Length <= 0)
        {
          this.dgv_HttpsRequests.Rows[i].Visible = true;
        }
        else
        {
          try
          {
            var cellData = this.dgv_HttpsRequests.Rows[i].Cells["URL"].Value.ToString();
            if (!Regex.Match(cellData, Regex.Escape(this.tb_Filter.Text), RegexOptions.IgnoreCase).Success)
            {
              this.dgv_HttpsRequests.Rows[i].Visible = false;
            }
            else
            {
              this.dgv_HttpsRequests.Rows[i].Visible = true;
            }
          }
          catch (Exception ex)
          {
            this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
          }
        }
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="index"></param>
    public void RemoveRecordAt(int index)
    {
      lock (this)
      {
        this.dgv_HttpsRequests.SuspendLayout();

        try
        {
          this.foundHttpsRequests.RemoveAt(index);
        }
        catch (Exception)
        {
        }

        this.dgv_HttpsRequests.ResumeLayout();
      }
    }

    #endregion

  }
}
