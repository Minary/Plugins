namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.HttpRequest.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Text.RegularExpressions;
  using System.Windows.Forms;


  public partial class Plugin_HttpRequests : UserControl, IPlugin
  {

    #region MEMBERS

    private const int MaxTableRows = 128;
    private List<Tuple<string, string, string>> targetList;
    private BindingList<HttpRequests> httpRequests = new BindingList<HttpRequests>();
    private List<string> dataBatch = new List<string>();
    private bool isUpToDate = false;
    private HttpRequest.Infrastructure.HttpRequest infrastructureLayer;
    private PluginProperties pluginProperties;

    #endregion


    #region PROPERTIES

    public Control PluginControl { get { return (this); } }

    #endregion


    #region PUBLIC

    public Plugin_HttpRequests(PluginProperties pluginProperties)
    {
      this.InitializeComponent();

      DataGridViewTextBoxColumn columnMacAddr = new DataGridViewTextBoxColumn();
      columnMacAddr.DataPropertyName = "SrcMAC";
      columnMacAddr.Name = "SrcMAC";
      columnMacAddr.HeaderText = "MAC address";
      columnMacAddr.ReadOnly = true;
      columnMacAddr.Width = 180;
      this.dgv_HttpRequests.Columns.Add(columnMacAddr);

      DataGridViewTextBoxColumn columnSrcIp = new DataGridViewTextBoxColumn();
      columnSrcIp.DataPropertyName = "SrcIP";
      columnSrcIp.Name = "SrcIP";
      columnSrcIp.HeaderText = "Source IP";
      columnSrcIp.ReadOnly = true;
      columnSrcIp.Width = 120;
      this.dgv_HttpRequests.Columns.Add(columnSrcIp);

      DataGridViewTextBoxColumn columnTimestamp = new DataGridViewTextBoxColumn();
      columnTimestamp.DataPropertyName = "Timestamp";
      columnTimestamp.Name = "Timestamp";
      columnTimestamp.HeaderText = "Timestamp";
      columnTimestamp.ReadOnly = true;
      columnTimestamp.Visible = false;
      columnTimestamp.Width = 120;
      this.dgv_HttpRequests.Columns.Add(columnTimestamp);

      DataGridViewTextBoxColumn columnRequestMethod = new DataGridViewTextBoxColumn();
      columnRequestMethod.DataPropertyName = "Method";
      columnRequestMethod.Name = "Method";
      columnRequestMethod.HeaderText = "Method";
      columnRequestMethod.ReadOnly = true;
      columnRequestMethod.Visible = true;
      columnRequestMethod.Width = 80;
      this.dgv_HttpRequests.Columns.Add(columnRequestMethod);

      DataGridViewTextBoxColumn columnRemHost = new DataGridViewTextBoxColumn();
      columnRemHost.DataPropertyName = "RemoteHost";
      columnRemHost.Name = "RemoteHost";
      columnRemHost.HeaderText = "Server";
      columnRemHost.ReadOnly = true;
      columnRemHost.Width = 150;
      this.dgv_HttpRequests.Columns.Add(columnRemHost);

      DataGridViewTextBoxColumn columnRemFileName = new DataGridViewTextBoxColumn();
      columnRemFileName.DataPropertyName = "RemoteFile";
      columnRemFileName.Name = "RemoteFile";
      columnRemFileName.HeaderText = "File name";
      columnRemFileName.ReadOnly = true;
      columnRemFileName.Width = 216;
      columnRemFileName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_HttpRequests.Columns.Add(columnRemFileName);

      DataGridViewTextBoxColumn columnUrl = new DataGridViewTextBoxColumn();
      columnUrl.DataPropertyName = "URL";
      columnUrl.Name = "URL";
      columnUrl.HeaderText = "URL";
      columnUrl.Visible = false;
      this.dgv_HttpRequests.Columns.Add(columnUrl);

      DataGridViewTextBoxColumn columnCookies = new DataGridViewTextBoxColumn();
      columnCookies.DataPropertyName = "SessionCookies";
      columnCookies.Name = "SessionCookies";
      columnCookies.HeaderText = "Cookies";
      columnCookies.Visible = false;
      this.dgv_HttpRequests.Columns.Add(columnCookies);

      DataGridViewTextBoxColumn columnRequest = new DataGridViewTextBoxColumn();
      columnRequest.DataPropertyName = "Request";
      columnRequest.Name = "Request";
      columnRequest.HeaderText = "Request";
      columnRequest.Visible = false;
      this.dgv_HttpRequests.Columns.Add(columnRequest);
      
      this.dgv_HttpRequests.DataSource = this.httpRequests;

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

      this.pluginProperties.PluginName = "HTTP requests";
      this.pluginProperties.PluginType = "Passive";
      this.pluginProperties.PluginDescription = "Eavesdrop client systems HTTP requests.";
      this.pluginProperties.Ports = new Dictionary<int, IpProtocols>() { { 80, IpProtocols.Tcp }, { 443, IpProtocols.Tcp } };

      // Instantiate infrastructure layer
      this.infrastructureLayer = new HttpRequest.Infrastructure.HttpRequest(this);

      // Initialize plugin environment
      this.infrastructureLayer.OnInit();

      this.t_GuiUpdate.Interval = 1000;
      this.t_GuiUpdate.Start();
    }

    #endregion


    #region PRIVATE

    /// <summary>
    ///
    /// </summary>
    public void ProcessEntries()
    {
      var newRecords = new List<HttpRequests>();
      List<string> newDataRecords;
      Match matchUri;
      Match matchHost;
      Match matchCookies;
      var requestMethod = string.Empty;
      var remoteHost = string.Empty;
      var requestString = string.Empty;
      var cookies = string.Empty;
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

          if (((matchUri = Regex.Match(data, @"(\s+|^)(GET|POST)\s+([^\s]+)\s+HTTP\/"))).Success)
          {
            requestMethod = matchUri.Groups[2].Value.ToString();
            requestString = matchUri.Groups[3].Value.ToString();

            if (((matchHost = Regex.Match(data, @"\.\.Host\s*:\s*([\w\d\-_\.]+?)\.\.", RegexOptions.IgnoreCase))).Success)
            {
              remoteHost = matchHost.Groups[1].Value.ToString();
            }
            else
            {
              remoteHost = dstIp;
            }

            if (((matchCookies = Regex.Match(data, @"\.\.Cookie\s*:\s*(.*?)(\.\.|$)", RegexOptions.IgnoreCase))).Success)
            {
              cookies = matchCookies.Groups[1].Value.ToString();
            }
            else
            {
              cookies = string.Empty;
            }

            newRecords.Add(new HttpRequests(macAddr, srcIp, requestMethod, remoteHost, requestString, cookies, data));
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


    /// <summary>
    ///
    /// </summary>
    /// <param name="inputData"></param>
    /// <returns></returns>
    private bool CompareToFilter(string inputData)
    {
      bool retVal = false;

      if (inputData != null && inputData.Length > 0)
      {
        if (Regex.Match(inputData, Regex.Escape(this.tb_Filter.Text), RegexOptions.IgnoreCase).Success)
        {
          retVal = true;
        }
      }

      return retVal;
    }



    /// <summary>
    ///
    /// </summary>
    private void UseFilter()
    {
      if (this.dgv_HttpRequests.Rows.Count <= 0)
      {
        return;
      }

      // TODO: Without this line we will get an exception. FIX IT!
      this.dgv_HttpRequests.CurrentCell = null;
      for (var i = 0; i < this.dgv_HttpRequests.RowCount; i++)
      {
        if (this.tb_Filter.Text.Length <= 0)
        {
          this.dgv_HttpRequests.Rows[i].Visible = true;
        }
        else
        {
          try
          {
            var cellData = this.dgv_HttpRequests.Rows[i].Cells["URL"].Value.ToString();
            if (!Regex.Match(cellData, Regex.Escape(this.tb_Filter.Text), RegexOptions.IgnoreCase).Success)
            {
              this.dgv_HttpRequests.Rows[i].Visible = false;
            }
            else
            {
              this.dgv_HttpRequests.Rows[i].Visible = true;
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
        this.dgv_HttpRequests.SuspendLayout();

        try
        {
          this.httpRequests.RemoveAt(index);
        }
        catch (Exception)
        {
        }

        this.dgv_HttpRequests.ResumeLayout();
      }
    }

    #endregion

  }
}
