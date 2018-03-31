namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.Systems.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;


  public partial class Plugin_Systems
  {

    #region IPlugin Member

    /// <summary>
    ///
    /// </summary>
    public PluginProperties Config { get { return this.pluginProperties; } set { this.pluginProperties = value; } }


    /// <summary>
    ///
    /// </summary>
    public delegate void OnInitDelegate();
    public void OnInit()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnInitDelegate(this.OnInit), new object[] { });
        return;
      }

      // Plugin initialisation
      this.infrastructureLayer.OnInit();
      this.pluginProperties.HostApplication.Register(this);
      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, Status.NotRunning);
      this.Refresh();
    }


    /// <summary>
    ///
    /// </summary>
    public delegate void OnStartAttackDelegate();
    public void OnStartAttack()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnStartAttackDelegate(this.OnStartAttack), new object[] { });
        return;
      }

      // Add all system from ARP scan to the list
      this.ClearRecordList();
      foreach (Tuple<string, string, string> tmpSystem in this.targetList)
      {
        try
        {
          this.AddRecord(new SystemRecord(tmpSystem.Item2.Trim(), tmpSystem.Item1.Trim(), string.Empty, tmpSystem.Item3.Trim(), string.Empty, string.Empty));
        }
        catch (RecordExistsException ex)
        {
          this.pluginProperties.HostApplication.LogMessage($"Plugin_System.OnStartAttack(RecordExistsException): {tmpSystem.Item1}/{tmpSystem.Item2} - {ex.Message}\r\n{ex.StackTrace}");
        }
        catch (RecordException ex)
        {
          this.pluginProperties.HostApplication.LogMessage($"Plugin_System.OnStartAttack(RecordException): {ex.Message}\r\n{ex.StackTrace}");
        }
      }

      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, Status.Running);
      this.Refresh();
    }


    /// <summary>
    ///
    /// </summary>
    public delegate void OnStopAttackDelegate();
    public void OnStopAttack()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnStopAttackDelegate(this.OnStopAttack), new object[] { });
        return;
      }

      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, Status.NotRunning);
      this.Refresh();
    }


    /// <summary>
    ///
    /// </summary>
    public delegate void OnShutDownDelegate();
    public void OnShutDown()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnShutDownDelegate(this.OnShutDown), new object[] { });
        return;
      }
    }


    /// <summary>
    ///
    /// </summary>
    public delegate void OnResetPluginDelegate();
    public void OnResetPlugin()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnResetPluginDelegate(this.OnResetPlugin), new object[] { });
        return;
      }

      if (this.systemRecords != null)
      {
        this.ClearRecordList();
      }

      this.infrastructureLayer.OnReset();
      this.Refresh();
    }


    /// <summary>
    /// New input data arrived
    /// </summary>
    /// <param name="pData"></param>
    public delegate void OnNewDataDelegate(string data);
    public void OnNewData(string data)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnNewDataDelegate(this.OnNewData), new object[] { data });
        return;
      }

      lock (this)
      {
        if (this.dataBatch != null &&
            data != null && 
            data.Length > 0)
        {
          this.dataBatch.Add(data);
          this.Refresh();
        }
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="targetList"></param>
    public void SetTargets(List<Tuple<string, string, string>> targetList)
    {
      this.targetList = targetList;
    }


    public delegate TemplatePluginData OnGetTemplateDataDelegate();
    public TemplatePluginData OnGetTemplateData()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnGetTemplateDataDelegate(this.OnGetTemplateData), new object[] { });
        return null;
      }

      TemplatePluginData newTemplateData = new TemplatePluginData();

      // Configuration items
      newTemplateData.PluginConfigurationItems = this.infrastructureLayer.OnGetTemplateData(this.systemRecords);

      // Pattern items
      newTemplateData.PluginDataSearchPatternItems = this.manageSystemsPresentationLayer.OnGetTemplateData();

      return newTemplateData;
    }


    public delegate void OnLoadTemplateDataDelegate(TemplatePluginData templateData);
    public void OnLoadTemplateData(TemplatePluginData templateData)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnLoadTemplateDataDelegate(this.OnLoadTemplateData), new object[] { templateData });
        return;
      }

      // Configuration items
      this.systemRecords.Clear();
      List<SystemRecord> loadedSystemRecords = this.infrastructureLayer.OnLoadTemplateData(templateData);
      loadedSystemRecords.ForEach(elem => this.systemRecords.Add(elem));

      // Pattern items
      this.manageSystemsPresentationLayer.OnLoadTemplateData(templateData);
      this.Refresh();
    }


    public delegate void OnUnloadTemplateDataDelegate();
    public void OnUnloadTemplateData()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnUnloadTemplateDataDelegate(this.OnUnloadTemplateData), new object[] { });
        return;
      }

      // Remove all template data
      this.infrastructureLayer.OnUnoadTemplateData();

      // Reload local and remote pattern files
      this.manageSystemsPresentationLayer.LocalPatternsEnabled = true;
      this.manageSystemsPresentationLayer.RemotePatternsEnabled = true;
      this.manageSystemsTaskLayer.ReadSystemPatterns();
      this.Refresh();
    }

    #endregion


    #region OBSERVER INTERFACE METHODS

    /// <summary>
    ///
    /// </summary>
    /// <param name="recordList"></param>
    public void UpdateRecordList(List<SystemRecord> recordList)
    {
      bool isLastLine = false;
      int lastPosition = -1;
      int lastRowIndex = -1;
      int selectedIndex = -1;

      lock (this)
      {
        // Remember DGV positions
        if (this.dgv_Systems.CurrentRow != null && this.dgv_Systems.CurrentRow == this.dgv_Systems.Rows[this.dgv_Systems.Rows.Count - 1])
        {
          isLastLine = true;
        }

        lastPosition = this.dgv_Systems.FirstDisplayedScrollingRowIndex;
        lastRowIndex = this.dgv_Systems.Rows.Count - 1;

        if (this.dgv_Systems.CurrentCell != null)
        {
          selectedIndex = this.dgv_Systems.CurrentCell.RowIndex;
        }

        this.systemRecords.Clear();
        if (recordList != null)
        {
          foreach (SystemRecord tmpRecord in recordList)
          {
            this.systemRecords.Add(new SystemRecord(tmpRecord.SrcMac, tmpRecord.SrcIp, tmpRecord.UserAgent, tmpRecord.HWVendor, tmpRecord.OperatingSystem, tmpRecord.LastSeen));
          }
        }

        // Selected cell/row
        try
        {
          if (selectedIndex >= 0)
          {
            this.dgv_Systems.CurrentCell = this.dgv_Systems.Rows[selectedIndex].Cells[0];
          }
        }
        catch (Exception)
        {
        }

        // Reset position
        try
        {
          if (lastPosition >= 0)
          {
            this.dgv_Systems.FirstDisplayedScrollingRowIndex = lastPosition;
          }
        }
        catch (Exception)
        {
        }

        this.dgv_Systems.Refresh();
      }
    }

    #endregion

  }
}