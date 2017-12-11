namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.Session.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.Threading;
  using System.Windows.Forms;


  public partial class Plugin_Sessions
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
      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.NotRunning);
      this.t_GuiUpdate.Start();

      // Data initialisation
      try
      {
        this.InitSessionPatterns();
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage("{0} : Error ocurred while initialising pattern file : {1}", this.Config.PluginName, ex.Message);
      }

      this.Refresh();
    }

    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnStartUpdateDelegate();
    public void OnStartUpdate()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnStartUpdateDelegate(this.OnStartUpdate), new object[] { });
        return;
      }

      this.pluginProperties.HostApplication.LogMessage("{0}: Downloading new pattern file(s)", this.Config.PluginName);
      Thread updateProcessThread = new Thread(new ThreadStart(this.SyncPatternFileFromServer));
      updateProcessThread.Start();
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

      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.NotRunning);
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

      this.pluginProperties.HostApplication.ReportPluginSetStatus(this, MinaryLib.Plugin.Status.NotRunning);

      // Clear DataGridView
      ////if (sessions != null)
      ////  sessions.Clear();

      ////dgv_Sessions.DataSource = sessions;
      ////dgv_Sessions.Refresh();

      // Clear TreeView
      try
      {
        if (this.tv_Sessions != null && this.tv_Sessions.Nodes.Count > 0)
        {
          foreach (TreeNode tmpNode in this.tv_Sessions.Nodes)
          {
            foreach (TreeNode tmpSubNode in tmpNode.Nodes)
            {
              if (tmpSubNode != null && tmpSubNode.Nodes.Count > 0)
              {
                tmpSubNode.Nodes.Clear();
              }
            }
          }
        }
      }
      catch (Exception)
      {
      }

      // Select Main TV-Node.
      this.filterNode = this.tv_Sessions.Nodes[0];
      this.tv_Sessions.SelectedNode = this.tv_Sessions.Nodes[0];
      this.tv_Sessions.Select();
      //// myTreeView.SelectedNode = myTreeNode

      this.infrastructureLayer.OnReset();
      this.Refresh();
    }


    /// <summary>
    /// New input newData arrived
    /// TCP||00:11:22:33:44:55||192.168.0.123||51984||74.125.79.136||80||GET...
    /// </summary>
    /// <param name="newData"></param>
    public delegate void OnNewDataDelegate(string newData);
    public void OnNewData(string newData)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new OnNewDataDelegate(this.OnNewData), new object[] { newData });
        return;
      }

      lock (this)
      {
        if (this.dataBatch != null && newData != null && newData.Length > 0)
        {
          this.dataBatch.Add(newData);
          this.Refresh();
        }
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="pTargetList"></param>
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
      newTemplateData.PluginConfigurationItems = this.infrastructureLayer.OnGetTemplateData(this.sessionRecords);

      // Pattern items
      newTemplateData.PluginDataSearchPatternItems = this.manageSessionsPresentationLayer.OnGetTemplateData();

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
      this.sessionRecords.Clear();
      List<TheSessionRecord> loadedSystemRecords = this.infrastructureLayer.OnLoadTemplateData(templateData);
      loadedSystemRecords.ForEach(elem => this.sessionRecords.Add(elem));

      // Pattern items
      this.manageSessionsPresentationLayer.OnLoadTemplateData(templateData);
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
      this.manageSessionsPresentationLayer.LocalPatternsEnabled = true;
      this.manageSessionsPresentationLayer.RemotePatternsEnabled = true;
      this.manageSessionsTaskLayer.ReadSessionPatterns();
      this.Refresh();
    }

    #endregion

  }
}