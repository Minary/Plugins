namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.Systems.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Linq;
  using System.Reflection;
  using System.Text.RegularExpressions;
  using System.Windows.Forms;
  using ManageSystems = Minary.Plugin.Main.Systems.ManageSystems;


  public partial class Plugin_Systems : UserControl, IPlugin
  {

    #region DATATYPES

    private enum EntryType
    {
      Empty,
      Half,
      Full
    }

    struct DataPacket
    {
      public string Proto;
      public string SrcMacAddress;
      public string SrcIpAddress;
      public string SrcPort;
      public string DstIpAddress;
      public string DstPort;
      public string Data;
      public EntryType EntryType;
    }

    #endregion


    #region MEMBERS

    private const int MAX_TABLE_ROWS = 128;
    private List<Tuple<string, string, string>> targetList;
    private BindingList<SystemRecord> systemRecords;
    private List<SystemPattern> systemPatterns = new List<SystemPattern>();
    private List<string> dataBatch = new List<string>();
    private Systems.Infrastructure.Systems infrastructureLayer;
    private PluginProperties pluginProperties;
    private ManageSystems.Presentation.Form_ManageSystems manageSystemsPresentationLayer;
    private ManageSystems.Task.ManageSystems manageSystemsTaskLayer;

    #endregion


    #region PROPERTIES

    public Control PluginControl { get { return (this); } }

    public BindingList<SystemRecord> SystemRecords { get { return this.systemRecords; } }

    //public BindingList<RecordHttpRequestData> HttpFindingRedcords
    //{
    //  get { return this.httpFindingRedcords; }
    //}

    #endregion


    #region PUBLIC

    public Plugin_Systems(PluginProperties pluginProperties)
    {
      this.InitializeComponent();

      this.dgv_Systems.AutoGenerateColumns = false;

      DataGridViewTextBoxColumn columnMac = new DataGridViewTextBoxColumn();
      columnMac.DataPropertyName = "SrcMac";
      columnMac.Name = "SrcMac";
      columnMac.HeaderText = "MAC address";
      columnMac.ReadOnly = true;
      columnMac.Width = 180;
      columnMac.Resizable = DataGridViewTriState.False;
      this.dgv_Systems.Columns.Add(columnMac);

      DataGridViewTextBoxColumn columnSrcIp = new DataGridViewTextBoxColumn();
      columnSrcIp.DataPropertyName = "SrcIp";
      columnSrcIp.Name = "SrcIp";
      columnSrcIp.HeaderText = "Source IP";
      columnSrcIp.Width = 140;
      columnSrcIp.ReadOnly = true;
      columnSrcIp.Resizable = DataGridViewTriState.False;
      this.dgv_Systems.Columns.Add(columnSrcIp);

      DataGridViewTextBoxColumn columnAppUrl = new DataGridViewTextBoxColumn();
      columnAppUrl.DataPropertyName = "OperatingSystem";
      columnAppUrl.Name = "OperatingSystem";
      columnAppUrl.HeaderText = "Operating System";
      columnAppUrl.ReadOnly = true;
      columnAppUrl.Width = 200;
      columnAppUrl.Resizable = DataGridViewTriState.False;
      this.dgv_Systems.Columns.Add(columnAppUrl);

      DataGridViewTextBoxColumn columnHardwareVendor = new DataGridViewTextBoxColumn();
      columnHardwareVendor.DataPropertyName = "HWVendor";
      columnHardwareVendor.Name = "HWVendor";
      columnHardwareVendor.HeaderText = "Hardware vendor";
      columnHardwareVendor.ReadOnly = true;
      columnHardwareVendor.Width = 200;
      columnHardwareVendor.Resizable = DataGridViewTriState.False;
      this.dgv_Systems.Columns.Add(columnHardwareVendor);

      DataGridViewTextBoxColumn columnLastSeen = new DataGridViewTextBoxColumn();
      columnLastSeen.DataPropertyName = "LastSeen";
      columnLastSeen.Name = "LastSeen";
      columnLastSeen.HeaderText = "Last seen";
      columnLastSeen.ReadOnly = true;
      columnLastSeen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      columnLastSeen.Resizable = DataGridViewTriState.False;
      this.dgv_Systems.Columns.Add(columnLastSeen);

      this.systemRecords = new BindingList<SystemRecord>();
      this.dgv_Systems.DataSource = this.systemRecords;
      
      // To reduce DGV flickering use DoubleBuffering
      Type dgvType = this.dgv_Systems.GetType();
      PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
      pi.SetValue(this.dgv_Systems, true, null);

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
      this.pluginProperties.PluginName = "Systems";
      this.pluginProperties.PluginType = "Passive";
      this.pluginProperties.PluginDescription = "Determine operating system of detected target systems";
      this.pluginProperties.Ports = new Dictionary<int, IpProtocols>() { { 80, IpProtocols.Tcp }, { 443, IpProtocols.Tcp } };

      this.t_GuiUpdate.Start();

      // Instantiate infrastructure layer
      this.infrastructureLayer = new Systems.Infrastructure.Systems(this);

      // Initialize plugin environment
      this.infrastructureLayer.OnInit();

      // Load authentication pattern management GUI
      this.manageSystemsPresentationLayer = new ManageSystems.Presentation.Form_ManageSystems(this.pluginProperties);
      this.manageSystemsTaskLayer = new ManageSystems.Task.ManageSystems(this.pluginProperties);
      this.systemPatterns = this.manageSystemsPresentationLayer.GetActiveSystemPatterns();
    }

    #endregion


    #region PRIVATE
    

    /// <summary>
    ///
    /// </summary>
    public void ProcessEntries()
    {
      if (this.dataBatch == null || 
          this.dataBatch.Count <= 0)
      {
        return;
      }

      var newRecords = new List<SystemRecord>();
      List<string> newData;
      Match matchUserAgent;
      DataPacket dataPacket;

      lock (this)
      {
        newData = new List<string>(this.dataBatch);
        this.dataBatch.Clear();
      }

      foreach (string tmpNewData in newData)
      {
        if (string.IsNullOrEmpty(tmpNewData))
        {
          continue;
        }

        try
        {
          dataPacket = this.GetDataPacket(tmpNewData);
        }
        catch (Exception ex)
        {
          this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
          continue;
        }

        // Determine the operating system due to the HTTP User-Agent string.
        if ((matchUserAgent = Regex.Match(dataPacket.Data, @"\.\.User-Agent\s*:\s*(.+?)\.\.", RegexOptions.IgnoreCase)).Success)
        {
          this.DetermineAndSetOS(dataPacket, matchUserAgent);

          // The operating system cant be determined.
        }
        else if (dataPacket.EntryType == EntryType.Empty && 
                 dataPacket.SrcIpAddress.Length > 0 && 
                 dataPacket.SrcMacAddress.Length > 0)
        {
          this.TryAddRecord(new SystemRecord(dataPacket.SrcMacAddress, dataPacket.SrcIpAddress, string.Empty, string.Empty, string.Empty, string.Empty));
        }

        // Updating LastSeen column.
        using (DataGridViewRow tmpRow = this.GetListEntryByMac(dataPacket.SrcMacAddress))
        {
          if (tmpRow?.Cells["LastSeen"] != null)
          {
            tmpRow.Cells["LastSeen"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
          }
        }
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="macAddress"></param>
    /// <returns></returns>
    private DataGridViewRow GetRowByMac(string macAddress)
    {
      DataGridViewRow retVal = null;

      if (this.dgv_Systems.RowCount <= 0)
      {
        return retVal;
      }

      foreach (DataGridViewRow tmpRow in this.dgv_Systems.Rows)
      {
        if (tmpRow.Cells["SrcMac"].Value.ToString() == macAddress)
        {
          retVal = tmpRow;
          break;
        }
      }

      return retVal;
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="srcMac"></param>
    /// <param name="srcIp"></param>
    /// <returns></returns>
    private EntryType FullEntryExists(string srcMac, string srcIp)
    {
      EntryType retVal = EntryType.Empty;

      if (this.systemRecords == null || 
          this.systemRecords.Count <= 0)
      {
        return retVal;
      }

      foreach (SystemRecord tmpSystem in this.systemRecords)
      {
        string srcMacReal = srcMac.Replace('-', ':');
        string systemMacReal = tmpSystem.SrcMac.Replace('-', ':');

        if (systemMacReal == srcMacReal && 
            tmpSystem.SrcIp == srcIp && 
            tmpSystem.OperatingSystem.Length > 0)
        {
          retVal = EntryType.Full;
          break;
        }
        else if (systemMacReal == srcMacReal && 
                 tmpSystem.SrcIp == srcIp)
        {
          retVal = EntryType.Half;
          break;
        }
      }

      return retVal;
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="srcMac"></param>
    /// <param name="srcIp"></param>
    /// <param name="operatingSystem"></param>
    /// <returns></returns>
    private bool SetOS(string srcMac, string srcIp, string operatingSystem)
    {
      bool retVal = false;

      if (this.systemRecords == null || 
          this.systemRecords.Count <= 0)
      {
        return retVal;
      }

      foreach (SystemRecord tmpSystem in this.systemRecords)
      {
        string srcMacReal = srcMac.Replace('-', ':');
        string systemMacReal = tmpSystem.SrcMac.Replace('-', ':');

        if (srcMacReal == systemMacReal && 
            tmpSystem.SrcIp == srcIp)
        {
          tmpSystem.OperatingSystem = operatingSystem;
          retVal = true;
          break;
        }
      }

      return retVal;
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="userAgent"></param>
    /// <returns></returns>
    private string GetOperatingSystem(string userAgent)
    {
      string retVal = string.Empty;

      foreach (SystemPattern tmpSystemPattern in this.systemPatterns)
      {
        if (userAgent != null && 
            Regex.Match(userAgent, tmpSystemPattern.SystemPatternstring, RegexOptions.IgnoreCase).Success)
        {
          retVal = tmpSystemPattern.SystemName;
          break;
        }
      }

      return retVal;
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="macAddress"></param>
    /// <returns></returns>
    private DataGridViewRow GetListEntryByMac(string macAddress)
    {
      DataGridViewRow retVal = null;

      foreach (DataGridViewRow tmpRow in this.dgv_Systems.Rows)
      {
        if (tmpRow.Cells["SrcMac"].Value.ToString() == macAddress)
        {
          retVal = tmpRow;
          break;
        }
      }

      return retVal;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private DataPacket GetDataPacket(string data)
    {
      DataPacket dataPacket = new DataPacket();
      string[] splitter;
      
      if ((splitter = Regex.Split(data, @"\|\|")).Length != 7)
      {
        throw new Exception("The record structure is invalid");
      }

      dataPacket.Proto = splitter[0].Trim().ToLower();
      dataPacket.SrcMacAddress = splitter[1].Trim().ToLower();
      dataPacket.SrcIpAddress = splitter[2].Trim().ToLower();
      dataPacket.SrcPort = splitter[3].Trim().ToLower();
      dataPacket.DstIpAddress = splitter[4].Trim().ToLower();
      dataPacket.DstPort = splitter[5].Trim().ToLower();
      dataPacket.Data = splitter[6].Trim().ToLower();
      dataPacket.SrcMacAddress = Regex.Replace(dataPacket.SrcMacAddress, @"-", ":");
      dataPacket.EntryType = this.FullEntryExists(dataPacket.SrcMacAddress, dataPacket.SrcIpAddress);

      return dataPacket;
    }


    /// <summary>
    /// 
    /// </summary>
    private void TryAddRecord(SystemRecord newRecord)
    {
      try
      {
        this.AddRecord(newRecord);
      }
      catch (RecordException rex)
      {
        this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {rex.Message}");
      }
      catch (RecordExistsException)
      {
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName} 2: {ex.Message}");
      }
    }
    

    private void DetermineAndSetOS(DataPacket dataPacket, Match matchUserAgent)
    {
      string operatingSystem = string.Empty;
      string userAgent = string.Empty; ;
      DataGridViewRow tabelRow;

      try
      {
        userAgent = matchUserAgent.Groups[1].Value.ToString();
        operatingSystem = this.GetOperatingSystem(userAgent);
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
      }

      if (dataPacket.EntryType != EntryType.Full && 
          operatingSystem.Length > 0)
      {
        if (dataPacket.EntryType == EntryType.Empty)
        {
          this.TryAddRecord(new SystemRecord(dataPacket.SrcMacAddress, dataPacket.SrcIpAddress, userAgent, string.Empty, operatingSystem, string.Empty));
        }
        else if (dataPacket.EntryType == EntryType.Half)
        {
          this.SetOS(dataPacket.SrcMacAddress, dataPacket.SrcIpAddress, operatingSystem);
        }

        if ((tabelRow = this.GetRowByMac(dataPacket.SrcMacAddress)) != null)
        {
          tabelRow.Cells["OperatingSystem"].ToolTipText = userAgent;
        }
      }
      else if (dataPacket.SrcIpAddress.Length > 0 && 
               dataPacket.SrcMacAddress.Length > 0)
      {
        this.TryAddRecord(new SystemRecord(dataPacket.SrcMacAddress, dataPacket.SrcIpAddress, userAgent, string.Empty, string.Empty, string.Empty));
      }
    }

    #endregion

  }
}
