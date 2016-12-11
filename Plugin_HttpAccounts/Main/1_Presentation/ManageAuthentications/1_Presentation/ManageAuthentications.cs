namespace Minary.Plugin.Main.HttpAccounts.ManageAuthentications.Presentation
{
  using Minary.Plugin.Main.HttpAccounts.ManageAuthentications.DataTypes;
  using MinaryLib;
  using MinaryLib.DataTypes;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Drawing;
  using System.IO;
  using System.Linq;
  using System.Windows.Forms;

  public partial class Form_ManageAuthentications : Form, IObserver
  {

    #region MEMBERS

    private static Form_ManageAuthentications instance;
    private BindingList<HttpAccountPattern> httpAccountPatterns;
    private PluginProperties pluginProperties;
    private Task.ManageAuthentications taskLayer;

    #endregion


    #region PROPERTIES

    public bool LocalPatternsEnabled { get { return this.cb_LocalPatternsEnabled.Checked; } set { this.cb_LocalPatternsEnabled.Checked = value; } }

    public bool RemotePatternsEnabled { get { return this.cb_RemotePatternsEnabled.Checked; } set { this.cb_RemotePatternsEnabled.Checked = value; } }

    #endregion


    #region PUBLIC

    /// <summary>
    ///
    /// </summary>
    /// <param name="pPluginParameters"></param>
    /// <returns></returns>
    public static Form_ManageAuthentications GetInstance(PluginProperties pluginProperties)
    {
      return instance ?? (instance = new Form_ManageAuthentications(pluginProperties));
    }


    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public List<HttpAccountPattern> GetActiveAuthenticationPatterns()
    {
      List<HttpAccountPattern> activeAuthenticationPatterns = new List<HttpAccountPattern>();
      List<HttpAccountPattern> localPatterns = this.httpAccountPatterns.Where(elem => elem.Source == "Local" &&
                                                                               this.cb_LocalPatternsEnabled.Checked == true &&
                                                                               elem.IsEnabled == true)
                                                                               .ToList();
      List<HttpAccountPattern> remotePatterns = this.httpAccountPatterns.Where(elem => elem.Source == "Remote" &&
                                                                               this.cb_RemotePatternsEnabled.Checked == true &&
                                                                               elem.IsEnabled == true)
                                                                               .ToList();

      localPatterns.ForEach(elem => activeAuthenticationPatterns.Add(elem));
      remotePatterns.ForEach(elem => activeAuthenticationPatterns.Add(elem));

      return activeAuthenticationPatterns;
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="authenticationPatterns"></param>
    public void LoadTemplateAuthenticationPatterns(BindingList<HttpAccountPattern> authenticationPatterns)
    {
      // 1. Add template patterns and display them in the GUI
      authenticationPatterns.ToList().ForEach(elem => this.taskLayer.SaveTemplate(elem));

      // 2. Hide local and remote patterns
      this.cb_LocalPatternsEnabled.Checked = false;
      this.cb_RemotePatternsEnabled.Checked = false;

      this.ChangeRowState();
    }


    public byte[] OnGetTemplateData()
    {
      return this.taskLayer.OnGetTemplateData(this.GetActiveAuthenticationPatterns());
    }


    public void OnLoadTemplateData(TemplatePluginData templateData)
    {
      List<HttpAccountPattern> authenticationPatterns = this.taskLayer.OnLoadTemplateData(templateData);
      this.LoadTemplateAuthenticationPatterns(this.httpAccountPatterns);
    }

    #endregion


    #region PRIVATE

    /// <summary>
    /// Initializes a new instance of the <see cref="Form_ManageAuthentications"/> class.
    ///
    /// </summary>
    /// <param name="pPluginParameters"></param>
    private Form_ManageAuthentications(PluginProperties pluginProperties)
    {
      this.InitializeComponent();

      DataGridViewTextBoxColumn columnCompany = new DataGridViewTextBoxColumn();
      columnCompany.DataPropertyName = "Company";
      columnCompany.Name = "Company";
      columnCompany.HeaderText = "Company";
      columnCompany.ReadOnly = true;
      columnCompany.Visible = true;
      columnCompany.Width = 130;
      this.dgv_AccountPatterns.Columns.Add(columnCompany);

      DataGridViewTextBoxColumn columnWebPage = new DataGridViewTextBoxColumn();
      columnWebPage.DataPropertyName = "WebPage";
      columnWebPage.Name = "WebPage";
      columnWebPage.HeaderText = "Web page";
      columnWebPage.ReadOnly = true;
      columnWebPage.Visible = true;
      columnWebPage.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_AccountPatterns.Columns.Add(columnWebPage);

      DataGridViewTextBoxColumn columnMethod = new DataGridViewTextBoxColumn();
      columnMethod.DataPropertyName = "Method";
      columnMethod.Name = "Method";
      columnMethod.HeaderText = "Method";
      columnMethod.ReadOnly = true;
      columnMethod.Visible = true;
      columnMethod.Width = 60;
      this.dgv_AccountPatterns.Columns.Add(columnMethod);

      DataGridViewTextBoxColumn columnHostPattern = new DataGridViewTextBoxColumn();
      columnHostPattern.DataPropertyName = "HostPattern";
      columnHostPattern.Name = "HostPattern";
      columnHostPattern.HeaderText = "Host pattern";
      columnHostPattern.ReadOnly = true;
      columnHostPattern.Visible = true;
      columnHostPattern.Width = 130;
      this.dgv_AccountPatterns.Columns.Add(columnHostPattern);

      DataGridViewTextBoxColumn columnPathPattern = new DataGridViewTextBoxColumn();
      columnPathPattern.DataPropertyName = "PathPattern";
      columnPathPattern.Name = "PathPattern";
      columnPathPattern.HeaderText = "Path pattern";
      columnPathPattern.ReadOnly = true;
      columnPathPattern.Visible = true;
      columnPathPattern.Width = 260;
      this.dgv_AccountPatterns.Columns.Add(columnPathPattern);

      DataGridViewTextBoxColumn columnDataPattern = new DataGridViewTextBoxColumn();
      columnDataPattern.DataPropertyName = "DataPattern";
      columnDataPattern.Name = "DataPattern";
      columnDataPattern.HeaderText = "Data pattern";
      columnDataPattern.ReadOnly = true;
      columnDataPattern.Visible = true;
      columnDataPattern.Width = 390;
      this.dgv_AccountPatterns.Columns.Add(columnDataPattern);

      DataGridViewTextBoxColumn columnDataSource = new DataGridViewTextBoxColumn();
      columnDataSource.DataPropertyName = "Source";
      columnDataSource.Name = "Source";
      columnDataSource.HeaderText = "Source";
      columnDataSource.ReadOnly = true;
      columnDataSource.Visible = true;
      columnDataSource.DefaultCellStyle.BackColor = Color.WhiteSmoke;
      columnDataSource.DefaultCellStyle.ForeColor = Color.Gray;
      columnDataSource.Width = 80;
      this.dgv_AccountPatterns.Columns.Add(columnDataSource);

      DataGridViewCheckBoxColumn columnIsEnabled = new DataGridViewCheckBoxColumn();
      columnIsEnabled.DataPropertyName = "IsEnabled";
      columnIsEnabled.Name = "IsEnabled";
      columnIsEnabled.HeaderText = "Enabled";
      columnIsEnabled.ReadOnly = true;
      columnIsEnabled.Visible = true;
      columnIsEnabled.Width = 80;
      this.dgv_AccountPatterns.Columns.Add(columnIsEnabled);

      DataGridViewTextBoxColumn columnConfig = new DataGridViewTextBoxColumn();
      columnConfig.DataPropertyName = "TemplateConfig";
      columnConfig.DisplayIndex = 5;
      columnConfig.Name = "TemplateConfig";
      columnConfig.HeaderText = "TemplateConfig";
      columnConfig.ReadOnly = true;
      columnConfig.Visible = false;
      columnConfig.Width = 0;
//      columnConfig.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_AccountPatterns.Columns.Add(columnConfig);

      this.httpAccountPatterns = new BindingList<HttpAccountPattern>();
      this.dgv_AccountPatterns.DataSource = this.httpAccountPatterns;
      this.dgv_AccountPatterns.CellClick += this.DGV_AccountPatterns_CellClick;

      this.pluginProperties = pluginProperties;
      this.taskLayer = Task.ManageAuthentications.GetInstance(this.pluginProperties);
      this.taskLayer.AddObserver(this);

      // Read pattern files
      try
      {
        this.taskLayer.ReadAccountsPatterns();
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error occurred while loading pattern files.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        this.pluginProperties.HostApplication.LogMessage("Form_ManageAuthentications(): {0}", ex.Message);
      }

      // Configure pattern files file System Watcher
      try
      {
        this.fsw_PatternFiles.Filter = Plugin.Main.HttpAccounts.DataTypes.General.PATTERN_FILE_PATTERN;
        this.fsw_PatternFiles.Path = Path.Combine(this.pluginProperties.PluginBaseDir, this.pluginProperties.PatternSubDir);
        this.fsw_PatternFiles.IncludeSubdirectories = true;
        this.fsw_PatternFiles.EnableRaisingEvents = true;
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage("Form_ManageAuthentications(): {0}", ex.Message);
      }
    }



    /// <summary>
    ///
    /// </summary>
    /// <param name="pIsVisible"></param>
    /// <param name="pColumnName"></param>
    /// <param name="pCellValue"></param>
    private void ChangeRowState()
    {
      BindingList<HttpAccountPattern> allActiveRows = new BindingList<HttpAccountPattern>();

      this.httpAccountPatterns.Where(m => (m.Source == "Local" && this.cb_LocalPatternsEnabled.Checked == true)).ToList().ForEach(elem => allActiveRows.Add(elem));
      this.httpAccountPatterns.Where(m => (m.Source == "Remote" && this.cb_RemotePatternsEnabled.Checked == true)).ToList().ForEach(elem => allActiveRows.Add(elem));
      this.httpAccountPatterns.Where(m => (m.Source == "Template")).ToList().ForEach(elem => allActiveRows.Add(elem));

      this.dgv_AccountPatterns.DataSource = allActiveRows;
    }

    #endregion


    #region EVENTS

    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DGV_AccountPatterns_MouseUp(object sender, MouseEventArgs e)
    {
      int dgv_column_source = this.dgv_AccountPatterns.Columns["Source"].Index;

      if (e.Button != MouseButtons.Right)
      {
        return;
      }

      try
      {
        DataGridView.HitTestInfo hti = this.dgv_AccountPatterns.HitTest(e.X, e.Y);
        if (hti.RowIndex < 0)
        {
          return;
        }

        // Select selected Row
        this.dgv_AccountPatterns.ClearSelection();
        this.dgv_AccountPatterns.Rows[hti.RowIndex].Selected = true;

        // Only show menu if source is "local"
        if (this.dgv_AccountPatterns.Rows[hti.RowIndex].Cells[dgv_column_source].Value.ToString() == "Local")
        {
          this.deletePatternToolStripMenuItem.Enabled = true;
          this.cms_ManageAccounts.Show(this.dgv_AccountPatterns, e.Location);
        }
        else
        {
          this.deletePatternToolStripMenuItem.Enabled = false;
          this.cms_ManageAccounts.Show(this.dgv_AccountPatterns, e.Location);
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
    private void DeletePatternToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        HttpAccountPattern patternFileObj = (HttpAccountPattern)this.dgv_AccountPatterns.SelectedRows[0].DataBoundItem;
        this.taskLayer.RemoveTemplate(patternFileObj);
      }
      catch (Exception ex)
      {
        MessageBox.Show(string.Format("Error occurred while deleting pattern file: {0}", ex.Message), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        this.pluginProperties.HostApplication.LogMessage("Presentation.ManageAuthentications(): {0}", ex.Message);
      }
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
    private void ManageAuthentications_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.Hide();
      e.Cancel = true;
      this.pluginProperties.HostApplication.MainWindowForm.Activate();
    }


    /// <summary>
    /// Hide Sessions GUI on Escape.
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
        return base.ProcessDialogKey(keyData);
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BT_New_Click(object sender, EventArgs e)
    {
      Plugin.Main.HttpAccounts.ManageAuthentications.Presentation.CustomPatternAdd customPatternForm = new Plugin.Main.HttpAccounts.ManageAuthentications.Presentation.CustomPatternAdd(this.pluginProperties);
      customPatternForm.ShowDialog();
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DGV_AccountPatterns_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      int dgv_column_checkbox_enabled = 8;

      if (e.RowIndex >= 0 && e.ColumnIndex == dgv_column_checkbox_enabled)
      {
        var dataGridView = (DataGridView)sender;
        var cell = dataGridView[dgv_column_checkbox_enabled, e.RowIndex];

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
          this.taskLayer.ReadAccountsPatterns();
          this.dgv_AccountPatterns.Refresh();
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

    public void Update(List<HttpAccountPattern> patterns)
    {
      if (this.httpAccountPatterns == null)
      {
        return;
      }

      this.httpAccountPatterns.Clear();
      foreach (HttpAccountPattern tmp in patterns)
      {
        this.httpAccountPatterns.Add(tmp);
      }

      this.ChangeRowState();
    }

    #endregion

  }
}
