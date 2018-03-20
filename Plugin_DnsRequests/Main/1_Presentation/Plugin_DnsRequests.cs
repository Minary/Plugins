namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.DnsRequest.DataTypes;
  using Minary.Plugin.Main.DnsRequest.Infrastructure;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Linq;
  using System.Text.RegularExpressions;
  using System.Windows.Forms;


  public partial class Plugin_DnsRequests : UserControl, IPlugin
  {

    #region MEMBERS

    private readonly int maxRowNum = 256;

    private BindingList<DnsRequestRecord> dnsRequests = new BindingList<DnsRequestRecord>();
    private List<string> dataBatch = new List<string>();
    private List<Tuple<string, string, string>> targetList;
    private DnsRequests infrastructureLayer;
    private PluginProperties pluginProperties;

    #endregion


    #region PROPERTIES

    public Control PluginControl { get { return (this); } }

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="Plugin_DnsRequests"/> class.
    ///
    /// </summary>
    public Plugin_DnsRequests(PluginProperties pluginProperties)
    {
      this.InitializeComponent();

      DataGridViewTextBoxColumn columnSrcMac = new DataGridViewTextBoxColumn();
      columnSrcMac.DataPropertyName = "SrcMAC";
      columnSrcMac.Name = "SrcMAC";
      columnSrcMac.HeaderText = "MAC address";
      columnSrcMac.ReadOnly = true;
      columnSrcMac.Width = 180;
      this.dgv_DnsRequests.Columns.Add(columnSrcMac);
      
      DataGridViewTextBoxColumn columnSrcIp = new DataGridViewTextBoxColumn();
      columnSrcIp.DataPropertyName = "SrcIP";
      columnSrcIp.Name = "SrcIP";
      columnSrcIp.HeaderText = "Source IP";
      columnSrcIp.ReadOnly = true;
      columnSrcIp.Width = 150;
      this.dgv_DnsRequests.Columns.Add(columnSrcIp);
      
      DataGridViewTextBoxColumn columnTimestamp = new DataGridViewTextBoxColumn();
      columnTimestamp.DataPropertyName = "Timestamp";
      columnTimestamp.Name = "Timestamp";
      columnTimestamp.HeaderText = "Timestamp";
      columnTimestamp.ReadOnly = true;
      columnTimestamp.Width = 200;
      this.dgv_DnsRequests.Columns.Add(columnTimestamp);

      DataGridViewTextBoxColumn columnRemHost = new DataGridViewTextBoxColumn();
      columnRemHost.DataPropertyName = "DNSHostname";
      columnRemHost.Name = "DNSHostname";
      columnRemHost.HeaderText = "DNS request";
      columnRemHost.ReadOnly = true;
      columnRemHost.Width = 180;
      columnRemHost.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_DnsRequests.Columns.Add(columnRemHost);

      DataGridViewTextBoxColumn columnPacketType = new DataGridViewTextBoxColumn();
      columnPacketType.DataPropertyName = "PacketType";
      columnPacketType.Name = "PacketType";
      columnPacketType.HeaderText = "Packet type";
      columnPacketType.ReadOnly = true;
      //// columnRemHost.Width = 280;
      columnPacketType.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_DnsRequests.Columns.Add(columnPacketType);      
      this.dgv_DnsRequests.DataSource = this.dnsRequests;
      
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
      this.t_GuiUpdate.Interval = 1000;
      this.pluginProperties = pluginProperties;

      this.pluginProperties.PluginName = "DNS requests";
      this.pluginProperties.PluginType = "Passive";
      this.pluginProperties.PluginDescription = "Eavesdrop client systems DNS requests.";
      this.pluginProperties.Ports = new Dictionary<int, IpProtocols>() { { 53, IpProtocols.Udp } };

      // Instantiate infrastructure layer
      this.infrastructureLayer = new DnsRequests(this);

      // Initialize plugin environment
      this.infrastructureLayer.OnInit();
    }

    #endregion


    #region PRIVATE

    /// <summary>
    ///
    /// </summary>
    public void ProcessEntries()
    {
      if (this.dataBatch?.Count > 0 == false)
      {
        return;
      }

      List<DnsRequestRecord> newRecords = new List<DnsRequestRecord>();
      List<string> newData;
      string[] splitter;
      var proto = string.Empty;
      var srcMac = string.Empty;
      var srcIp = string.Empty;
      var srcPort = string.Empty;
      var dstIP = string.Empty;
      var dstPort = string.Empty;
      var hostName = string.Empty;

      lock (this)
      {
        newData = new List<string>(this.dataBatch);
        this.dataBatch.Clear();
      }

      foreach (var tmpRecord in newData)
      {
        if (string.IsNullOrEmpty(tmpRecord))
        {
          continue;
        }

        try
        {
          if ((splitter = Regex.Split(tmpRecord, @"\|\|")).Length == 7)
          {
            proto = splitter[0];
            srcMac = splitter[1];
            srcIp = splitter[2];
            srcPort = splitter[3];
            dstIP = splitter[4];
            dstPort = splitter[5];
            hostName = splitter[6];

            if (proto == "DNSREP")
            {
              hostName = hostName.TrimEnd(new char[] { ' ', '\t', ',' });
              var elements = hostName.Split(new char[] { ',' });
              string requestedHost = elements[0];
              int noElements = elements.Length;
              string resolvedIpsString = "??";

              if (noElements > 1)
              {
                List<string> resolvedHostIps = new List<string>();
                for (int i = 1; i < noElements; i++)
                {
                  resolvedHostIps.Add(elements[i]);
                }

                resolvedIpsString = string.Join(", ", resolvedHostIps);
              }
              
              string data = $"{requestedHost}  \u2192  {resolvedIpsString}";
              newRecords.Add(new DnsRequestRecord(srcMac, srcIp, data, proto));
            }
            else if (proto == "DNSREQ" &&
                     dstPort != null && 
                     dstPort == "53")
            {
              newRecords.Add(new DnsRequestRecord(srcMac, srcIp, hostName, proto));
            }
          }
        }
        catch (Exception ex)
        {
          this.pluginProperties?.HostApplication?.LogMessage($"{this.Config.PluginName}: {ex.Message}");
        }
      }

      if (newRecords.Count > 0)
      {
        try
        {
          this.AddRecordsToDgv(newRecords);
        }
        catch (Exception ex)
        {
          this.pluginProperties?.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message} (Host name: \"{hostName}\")");
        }
      }
    }


    private bool CompareToFilter(string inputData)
    {
      var retVal = false;

      if (Regex.Match(inputData, this.tb_Filter.Text, RegexOptions.IgnoreCase).Success)
      {
        retVal = true;
      }

      return retVal;
    }


    private void UseFilter()
    {
      if (this.dgv_DnsRequests.Rows.Count <= 0)
      {
        return;
      }

      // TODO: Without this line we will get an exception :/ FIX IT!
      this.dgv_DnsRequests.CurrentCell = null;
      for (var i = 0; i < this.dgv_DnsRequests.Rows.Count; i++)
      {
        if (this.tb_Filter.Text.Length <= 0)
        {
          this.dgv_DnsRequests.Rows[i].Visible = true;
        }
        else
        {
          try
          {
            var selectedHostName = this.dgv_DnsRequests.Rows[i].Cells["DNSHostname"].Value.ToString();
            if (!Regex.Match(selectedHostName, Regex.Escape(this.tb_Filter.Text), RegexOptions.IgnoreCase).Success)
            {
              this.dgv_DnsRequests.Rows[i].Visible = false;
            }
            else
            {
              this.dgv_DnsRequests.Rows[i].Visible = true;
            }
          }
          catch (Exception ex)
          {
            this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
          }
        }
      }
    }

    #endregion

  }
}
