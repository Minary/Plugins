namespace Minary.Plugin.Main.Session.ManageSessions.Presentation
{
  using Minary.Plugin.Main.Session.ManageSessions.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Drawing;
  using System.IO;
  using System.Linq;
  using System.Windows.Forms;


  public partial class Form_ManageSessions : Form, IObserver
  {

    #region MEMBERS
    
    private BindingList<SessionPattern> sessionPatternRecords;
    private Task.ManageSessions taskLayer;
    private PluginProperties pluginProperties;

    #endregion


    #region PROPERTIES

    public bool LocalPatternsEnabled { get { return this.cb_LocalPatternsEnabled.Checked; } set { this.cb_LocalPatternsEnabled.Checked = value; } }

    public bool RemotePatternsEnabled { get { return this.cb_RemotePatternsEnabled.Checked; } set { this.cb_RemotePatternsEnabled.Checked = value; } }

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="Form_ManageSessions"/> class.
    ///
    /// </summary>
    /// <param name="pPluginMain"></param>
    /// <param name="pluginProperties"></param>
    public Form_ManageSessions(PluginProperties pluginProperties)
    {
      this.InitializeComponent();

      DataGridViewTextBoxColumn columnSessionName = new DataGridViewTextBoxColumn();
      columnSessionName.DataPropertyName = "CompanyName";
      columnSessionName.Name = "CompanyName";
      columnSessionName.HeaderText = "Company name";
      columnSessionName.ReadOnly = true;
      columnSessionName.Visible = true;
      columnSessionName.Width = 120;
      columnSessionName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
      this.dgv_SessionPatterns.Columns.Add(columnSessionName);

      DataGridViewTextBoxColumn columnWebPage = new DataGridViewTextBoxColumn();
      columnWebPage.DataPropertyName = "CompanyWebpage";
      columnWebPage.Name = "CompanyWebpage";
      columnWebPage.HeaderText = "Company web page";
      columnWebPage.ReadOnly = true;
      columnWebPage.Visible = true;
      columnWebPage.Width = 180;
      columnWebPage.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
      this.dgv_SessionPatterns.Columns.Add(columnWebPage);

      DataGridViewTextBoxColumn columnHttpHost = new DataGridViewTextBoxColumn();
      columnHttpHost.DataPropertyName = "HTTPHostRegex";
      columnHttpHost.Name = "HTTPHostRegex";
      columnHttpHost.HeaderText = "HTTP Host regex";
      columnHttpHost.ReadOnly = true;
      columnHttpHost.Visible = true;
      columnHttpHost.Width = 180;
      columnHttpHost.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
      this.dgv_SessionPatterns.Columns.Add(columnHttpHost);

      DataGridViewTextBoxColumn columnSessionPatternRegex = new DataGridViewTextBoxColumn();
      columnSessionPatternRegex.DataPropertyName = "SessionRegex";
      columnSessionPatternRegex.Name = "SessionRegex";
      columnSessionPatternRegex.HeaderText = "Session regex";
      columnSessionPatternRegex.ReadOnly = true;
      columnSessionPatternRegex.Visible = true;
      //// columnSessionPatternRegex.Width = 90;
      columnSessionPatternRegex.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      columnSessionPatternRegex.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
      this.dgv_SessionPatterns.Columns.Add(columnSessionPatternRegex);

      DataGridViewTextBoxColumn columnDataSource = new DataGridViewTextBoxColumn();
      columnDataSource.DataPropertyName = "Source";
      columnDataSource.Name = "Source";
      columnDataSource.HeaderText = "Source";
      columnDataSource.ReadOnly = true;
      columnDataSource.Visible = true;
      columnDataSource.DefaultCellStyle.BackColor = Color.WhiteSmoke;
      columnDataSource.DefaultCellStyle.ForeColor = Color.Gray;
      columnDataSource.Width = 80;
      this.dgv_SessionPatterns.Columns.Add(columnDataSource);

      DataGridViewCheckBoxColumn columnIsEnabled = new DataGridViewCheckBoxColumn();
      columnIsEnabled.DataPropertyName = "IsEnabled";
      columnIsEnabled.Name = "IsEnabled";
      columnIsEnabled.HeaderText = "Enabled";
      columnIsEnabled.ReadOnly = true;
      columnIsEnabled.Visible = true;
      columnIsEnabled.Width = 80;
      this.dgv_SessionPatterns.Columns.Add(columnIsEnabled);

      DataGridViewTextBoxColumn columnConfig = new DataGridViewTextBoxColumn();
      columnConfig.DataPropertyName = "TemplateConfig";
      columnConfig.Name = "TemplateConfig";
      columnConfig.HeaderText = "Template config";
      columnConfig.ReadOnly = true;
      columnConfig.Visible = false;
      columnConfig.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_SessionPatterns.Columns.Add(columnConfig);

      this.sessionPatternRecords = new BindingList<SessionPattern>();
      this.dgv_SessionPatterns.DataSource = this.sessionPatternRecords;
      this.dgv_SessionPatterns.CellClick += this.DGV_SessionPatterns_CellClick;
      
      this.pluginProperties = pluginProperties;
      this.taskLayer = new Task.ManageSessions(pluginProperties);
      this.taskLayer.AddObserver(this);

      try
      {
        this.taskLayer.ReadSessionPatterns();
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error ocurred while loading pattern files.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        this.pluginProperties.HostApplication.LogMessage("Form_ManageSessions(): {0}", ex.Message);
      }

      // Configure pattern files file System Watcher
      try
      {
        this.fsw_PatternFiles.Filter = Plugin.Main.Session.Config.General.PATTERN_FILE_PATTERN;
        this.fsw_PatternFiles.Path = Path.Combine(this.pluginProperties.PluginBaseDir, this.pluginProperties.PatternSubDir);
        this.fsw_PatternFiles.IncludeSubdirectories = true;
        this.fsw_PatternFiles.EnableRaisingEvents = true;
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage("Form_ManageSessions(): {0}", ex.Message);
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public List<SessionPattern> GetActiveSessionPatterns()
    {
      List<SessionPattern> activeSessionPatterns = new List<SessionPattern>();
      List<SessionPattern> localPatterns = this.sessionPatternRecords.Where(elem => elem.Source == "Local" &&
                                                                                  this.cb_LocalPatternsEnabled.Checked == true &&
                                                                                  elem.IsEnabled == true)
                                                                                  .ToList();
      List<SessionPattern> remotePatterns = this.sessionPatternRecords.Where(elem => elem.Source == "Remote" &&
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
    public void LoadTemplateSessionPatterns(List<SessionPattern> sessionPatterns)
    {
      // 1. Add template patterns and display them in the GUI
      sessionPatterns.ForEach(elem => this.taskLayer.SaveTemplate(elem));

      // 2. Hide local and remote patterns
      this.cb_LocalPatternsEnabled.Checked = false;
      this.cb_RemotePatternsEnabled.Checked = false;

      this.ChangeRowState();
    }


    public byte[] OnGetTemplateData()
    {
      return this.taskLayer.OnGetTemplateData(this.GetActiveSessionPatterns());
    }


    public void OnLoadTemplateData(TemplatePluginData templateData)
    {
      List<SessionPattern> applicationPatterns = this.taskLayer.OnLoadTemplateData(templateData);
      this.LoadTemplateSessionPatterns(applicationPatterns);
    }

    #endregion


    #region EVENTS

    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ManageSessions_MouseUp(object sender, MouseEventArgs e)
    {
      int dgvColumnSource = 5;

      if (e.Button != MouseButtons.Right)
      {
        return;
      }

      try
      {
        DataGridView.HitTestInfo hti = this.dgv_SessionPatterns.HitTest(e.X, e.Y);
        if (hti.RowIndex >= 0)
        {
          // Select selected row
          this.dgv_SessionPatterns.ClearSelection();
          this.dgv_SessionPatterns.Rows[hti.RowIndex].Selected = true;

          // Only show menu if source is "local"
          if (this.dgv_SessionPatterns.Rows[hti.RowIndex].Cells[dgvColumnSource].Value.ToString() == "Local")
          {
            this.deleteSessionPatternToolStripMenuItem.Enabled = true;
            this.cms_ManageSessions.Show(this.dgv_SessionPatterns, e.Location);
          }
          else
          {
            this.deleteSessionPatternToolStripMenuItem.Enabled = false;
            this.cms_ManageSessions.Show(this.dgv_SessionPatterns, e.Location);
          }
        }
      }
      catch (Exception)
      {
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BT_New_Click(object sender, EventArgs e)
    {
      Plugin.Main.Session.ManageSessions.Presentation.CustomPatternAdd customPatternForm = new CustomPatternAdd(this.pluginProperties);
      customPatternForm.ShowDialog();
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


    /// <summary>
    /// 
    /// </summary>
    private void ChangeRowState()
    {
      BindingList<SessionPattern> allActiveRows = new BindingList<SessionPattern>();

      this.sessionPatternRecords.Where(m => (m.Source == "Local" && this.cb_LocalPatternsEnabled.Checked == true)).ToList().ForEach(elem => allActiveRows.Add(elem));
      this.sessionPatternRecords.Where(m => (m.Source == "Remote" && this.cb_RemotePatternsEnabled.Checked == true)).ToList().ForEach(elem => allActiveRows.Add(elem));
      this.sessionPatternRecords.Where(m => (m.Source == "Template")).ToList().ForEach(elem => allActiveRows.Add(elem));

      this.dgv_SessionPatterns.DataSource = allActiveRows;
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DGV_SessionPatterns_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      int dgvColumnCheckboxEnabled = 6;

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
    private void DeleteSessionPatternToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        SessionPattern patternFileObj = (SessionPattern)this.dgv_SessionPatterns.SelectedRows[0].DataBoundItem;
        this.taskLayer.RemoveTemplate(patternFileObj);
      }
      catch (Exception ex)
      {
        MessageBox.Show(string.Format("Error occurred while deleting pattern file: {0}", ex.Message), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        this.pluginProperties.HostApplication.LogMessage("Presentation.ManageSessions(): {0}", ex.Message);
      }
    }




    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ManageSessions_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.Hide();
      e.Cancel = true;
      this.pluginProperties.HostApplication.MainWindowForm.Activate();
    }

    /// <summary>
    /// Close Sessions GUI on Escape.
    /// </summary>
    /// <param name="keyData"></param>
    /// <returns></returns>
    protected override bool ProcessDialogKey(Keys keyData)
    {
      if (keyData == Keys.Escape)
      {
        this.Close();
        this.pluginProperties.HostApplication.MainWindowForm.Activate();
        return true;
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
          this.taskLayer.ReadSessionPatterns();
          this.dgv_SessionPatterns.Refresh();
        }
        catch (Exception ex)
        {
          MessageBox.Show("Error occurred while loading pattern files.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          this.pluginProperties.HostApplication.LogMessage("Form_ManageAuthentications(): {0}", ex.Message);
        }
      }
    }

    #endregion


    #region OBSERVER

    public void Update(List<SessionPattern> patterns)
    {
      if (this.sessionPatternRecords != null)
      {
        this.sessionPatternRecords.Clear();
        foreach (SessionPattern tmpPattern in patterns)
        {
          this.sessionPatternRecords.Add(tmpPattern);
        }

        this.ChangeRowState();
      }
    }

    #endregion

  }
}