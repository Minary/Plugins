namespace Minary.Plugin.Main.Systems.ManageSystems.Presentation
{
  using Minary.Plugin.Main.Systems.DataTypes;
  using Minary.Plugin.Main.Systems.ManageSystems.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Drawing;
  using System.IO;
  using System.Linq;
  using System.Windows.Forms;


  public partial class Form_ManageSystems : Form, IObserver
  {

    #region MEMBERS

    private BindingList<SystemPattern> systemPatterns;
    private Task.ManageSystems taskLayer;
    private PluginProperties pluginProperties;

    #endregion


    #region PROPERTIES

    public bool LocalPatternsEnabled { get { return this.cb_LocalPatternsEnabled.Checked; } set { this.cb_LocalPatternsEnabled.Checked = value; } }

    public bool RemotePatternsEnabled { get { return this.cb_RemotePatternsEnabled.Checked; } set { this.cb_RemotePatternsEnabled.Checked = value; } }

    #endregion


    #region PUBLIC METHODS
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Form_ManageSystems"/> class.
    ///
    /// </summary>
    /// <param name="pPluginMain"></param>
    /// <param name="pluginProperties"></param>
    public Form_ManageSystems(PluginProperties pluginProperties)
    {
      this.InitializeComponent();

      DataGridViewTextBoxColumn columnSystemName = new DataGridViewTextBoxColumn();
      columnSystemName.DataPropertyName = "SystemName";
      columnSystemName.Name = "SystemName";
      columnSystemName.HeaderText = "System name";
      columnSystemName.ReadOnly = true;
      columnSystemName.Visible = true;
      columnSystemName.Width = 140;
      this.dgv_Systems.Columns.Add(columnSystemName);

      DataGridViewTextBoxColumn columnSystemPattern = new DataGridViewTextBoxColumn();
      columnSystemPattern.DataPropertyName = "SystemPatternstring";
      columnSystemPattern.Name = "SystemPatternstring";
      columnSystemPattern.HeaderText = "System pattern";
      columnSystemPattern.ReadOnly = true;
      columnSystemPattern.Visible = true;
      columnSystemPattern.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_Systems.Columns.Add(columnSystemPattern);

      DataGridViewTextBoxColumn columnDataSource = new DataGridViewTextBoxColumn();
      columnDataSource.DataPropertyName = "Source";
      columnDataSource.Name = "Source";
      columnDataSource.HeaderText = "Source";
      columnDataSource.ReadOnly = true;
      columnDataSource.Visible = true;
      columnDataSource.DefaultCellStyle.BackColor = Color.WhiteSmoke;
      columnDataSource.DefaultCellStyle.ForeColor = Color.Gray;
      columnDataSource.Width = 80;
      this.dgv_Systems.Columns.Add(columnDataSource);

      DataGridViewCheckBoxColumn columnIsEnabled = new DataGridViewCheckBoxColumn();
      columnIsEnabled.DataPropertyName = "IsEnabled";
      columnIsEnabled.Name = "IsEnabled";
      columnIsEnabled.HeaderText = "Enabled";
      columnIsEnabled.ReadOnly = true;
      columnIsEnabled.Visible = true;
      columnIsEnabled.Width = 80;
      ////      DGV_ApplicationPatterns.CellClick += DGV_AccountPatterns_CellClick;
      this.dgv_Systems.Columns.Add(columnIsEnabled);

      DataGridViewTextBoxColumn columnConfig = new DataGridViewTextBoxColumn();
      columnConfig.DataPropertyName = "PatternFileFullPath";
      columnConfig.Name = "PatternFileFullPath";
      columnConfig.HeaderText = "PatternFileFullPath";
      columnConfig.ReadOnly = true;
      columnConfig.Visible = false;
      columnConfig.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_Systems.Columns.Add(columnConfig);

      this.systemPatterns = new BindingList<SystemPattern>();
      this.dgv_Systems.DataSource = this.systemPatterns;
      this.dgv_Systems.CellClick += this.DGV_SystemPatterns_CellClick;

      this.pluginProperties = pluginProperties;
      this.taskLayer = new Task.ManageSystems(pluginProperties);
      this.taskLayer.AddObserver(this);

      try
      {
        this.taskLayer.ReadSystemPatterns();
      }
      catch (FileNotFoundException fnfex)
      {
        pluginProperties.HostApplication.LogMessage("Form_ManageSystems(): {0}", fnfex.Message);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.StackTrace, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        this.pluginProperties.HostApplication.LogMessage("Form_ManageSystems(): {0}", ex.Message);
      }

      // Configure pattern files file System Watcher
      try
      {
        this.fsw_PatternFiles.Filter = Plugin.Main.Systems.DataTypes.General.PATTERN_FILE_PATTERN;
        this.fsw_PatternFiles.Path = Path.Combine(this.pluginProperties.PluginBaseDir, this.pluginProperties.PatternSubDir);
        this.fsw_PatternFiles.IncludeSubdirectories = true;
        this.fsw_PatternFiles.EnableRaisingEvents = true;
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage("Form_ManageSystems(): {0}", ex.Message);
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public List<SystemPattern> GetActiveSystemPatterns()
    {
      List<SystemPattern> activeSessionPatterns = new List<SystemPattern>();
      List<SystemPattern> localPatterns = this.systemPatterns.Where(elem => elem.Source == "Local" &&
                                                                         this.cb_LocalPatternsEnabled.Checked == true &&
                                                                         elem.IsEnabled == true)
                                                                         .ToList();
      List<SystemPattern> remotePatterns = this.systemPatterns.Where(elem => elem.Source == "Remote" &&
                                                                          this.cb_RemotePatternsEnabled.Checked == true &&
                                                                          elem.IsEnabled == true)
                                                                          .ToList();

      localPatterns.ForEach(elem => activeSessionPatterns.Add(elem));
      remotePatterns.ForEach(elem => activeSessionPatterns.Add(elem));

      return activeSessionPatterns;
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sessionPatterns"></param>
    public void LoadTemplateSystemPatterns(List<SystemPattern> systemPatterns)
    {
      // 1. Add template patterns and display them in the GUI
      systemPatterns.ForEach(elem => this.taskLayer.SaveTemplate(elem));

      // 2. Hide local and remote patterns
      this.cb_LocalPatternsEnabled.Checked = false;
      this.cb_RemotePatternsEnabled.Checked = false;

      this.ChangeRowState();
    }


    public byte[] OnGetTemplateData()
    {
      return this.taskLayer.OnGetTemplateData(this.GetActiveSystemPatterns());
    }


    public void OnLoadTemplateData(TemplatePluginData templateData)
    {
      List<SystemPattern> applicationPatterns = this.taskLayer.OnLoadTemplateData(templateData);
      this.LoadTemplateSystemPatterns(applicationPatterns);
    }

    #endregion


    #region PRIVATE METHODS
    
    /// <summary>
    ///
    /// </summary>
    /// <param name="pIsVisible"></param>
    /// <param name="pColumnName"></param>
    /// <param name="pCellValue"></param>
    private void ChangeRowState()
    {
      BindingList<SystemPattern> allActiveRows = new BindingList<SystemPattern>();

      this.systemPatterns.Where(m => (m.Source == "Local" && this.cb_LocalPatternsEnabled.Checked == true)).ToList().ForEach(elem => allActiveRows.Add(elem));
      this.systemPatterns.Where(m => (m.Source == "Remote" && this.cb_RemotePatternsEnabled.Checked == true)).ToList().ForEach(elem => allActiveRows.Add(elem));
      this.systemPatterns.Where(m => (m.Source == "Template")).ToList().ForEach(elem => allActiveRows.Add(elem));

      this.dgv_Systems.DataSource = allActiveRows;
    }

    #endregion


    #region EVENTS

    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DeleteSystemToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        SystemPattern patternFileObj = (SystemPattern)this.dgv_Systems.SelectedRows[0].DataBoundItem;
        this.taskLayer.RemoveTemplate(patternFileObj);
      }
      catch (Exception ex)
      {
        MessageBox.Show(string.Format("Error occurred while deleting pattern file: {0}", ex.Message), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        this.pluginProperties.HostApplication.LogMessage("Presentation.ManageSystem(): {0}", ex.Message);
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DGV_Systems_MouseUp(object sender, MouseEventArgs e)
    {
      int dgvColumnSource = 3;

      if (e.Button != MouseButtons.Right)
      {
        return;
      }

      try
      {
        DataGridView.HitTestInfo hti = this.dgv_Systems.HitTest(e.X, e.Y);
        if (hti.RowIndex < 0)
        {
          return;
        }

        this.dgv_Systems.ClearSelection();
        this.dgv_Systems.Rows[hti.RowIndex].Selected = true;

        // Only show menu if source is "local"
        if (this.dgv_Systems.Rows[hti.RowIndex].Cells[dgvColumnSource].Value.ToString() == "Local")
        {
          this.deleteSystemToolStripMenuItem.Enabled = true;
          this.cms_ManageSystems.Show(this.dgv_Systems, e.Location);
        }
        else
        {
          this.deleteSystemToolStripMenuItem.Enabled = false;
          this.cms_ManageSystems.Show(this.dgv_Systems, e.Location);
        }
      }
      catch (Exception)
      {
      }
    }



    /// <summary>
    /// Close Systems GUI on Escape.
    /// </summary>
    /// <param name="keyData"></param>
    /// <returns></returns>
    protected override bool ProcessDialogKey(Keys keyData)
    {
      if (keyData == Keys.Escape)
      {
        this.Hide();
        this.pluginProperties.HostApplication.MainWindowForm.Activate();
        return false;
      }
      else
      {
        return base.ProcessDialogKey(keyData);
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FSW_PatternFiles_Changed(object sender, FileSystemEventArgs e)
    {
      // Update pattern list
      lock (this)
      {
        try
        {
          this.taskLayer.ReadSystemPatterns();
          this.dgv_Systems.Refresh();
        }
        catch (Exception ex)
        {
          MessageBox.Show("Error occurred while loading pattern files.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          this.pluginProperties.HostApplication.LogMessage("Form_ManageAuthentications(): {0}", ex.Message);
        }
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ManageSystems_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.Hide();
      e.Cancel = true;
      this.pluginProperties.HostApplication.MainWindowForm.Activate();
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BT_Close_Click(object sender, EventArgs e)
    {
      this.Hide();
      this.pluginProperties.HostApplication.MainWindowForm.Activate();
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BT_New_Click(object sender, EventArgs e)
    {
      Plugin.Main.Systems.ManageSystems.Presentation.CustomPatternAdd customPatternForm = new Plugin.Main.Systems.ManageSystems.Presentation.CustomPatternAdd(this.pluginProperties);
      customPatternForm.ShowDialog();
    }
    

    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DGV_SystemPatterns_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      int dgvColumnCheckboxEnabled = 4;

      if (e.RowIndex >= 0 && e.ColumnIndex == dgvColumnCheckboxEnabled)
      {
        var dataGridView = (DataGridView)sender;
        var cell = dataGridView[dgvColumnCheckboxEnabled, e.RowIndex];

        if (cell.Value == null)
        {
          cell.Value = false;
        }

        cell.Value = !(bool)cell.Value;
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CB_RemotePatternsEnabled_CheckedChanged(object sender, EventArgs e)
    {
      this.ChangeRowState();
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CB_LocalPatternsEnabled_CheckedChanged(object sender, EventArgs e)
    {
      this.ChangeRowState();
    }

    #endregion


    #region OBSERVER

    public void Update(List<SystemPattern> systemList)
    {
      this.systemPatterns.Clear();

      if (systemList != null)
      {
        foreach (SystemPattern tmpSystem in systemList)
        {
          this.systemPatterns.Add(tmpSystem);
        }
      }

      this.ChangeRowState();
    }

    #endregion

  }
}