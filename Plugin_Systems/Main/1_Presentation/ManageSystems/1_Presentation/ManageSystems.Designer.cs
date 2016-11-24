namespace Minary.Plugin.Main.Systems.ManageSystems.Presentation
{
  public partial class Form_ManageSystems
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }

      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ManageSystems));
      this.dgv_Systems = new System.Windows.Forms.DataGridView();
      this.cms_ManageSystems = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.deleteSystemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.cb_LocalPatternsEnabled = new System.Windows.Forms.CheckBox();
      this.cb_RemotePatternsEnabled = new System.Windows.Forms.CheckBox();
      this.bt_New = new System.Windows.Forms.Button();
      this.bt_Close = new System.Windows.Forms.Button();
      this.fsw_PatternFiles = new System.IO.FileSystemWatcher();
      ((System.ComponentModel.ISupportInitialize)(this.dgv_Systems)).BeginInit();
      this.cms_ManageSystems.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.fsw_PatternFiles)).BeginInit();
      this.SuspendLayout();
      // 
      // dgv_Systems
      // 
      this.dgv_Systems.AllowUserToAddRows = false;
      this.dgv_Systems.AllowUserToDeleteRows = false;
      this.dgv_Systems.AllowUserToResizeColumns = false;
      this.dgv_Systems.AllowUserToResizeRows = false;
      this.dgv_Systems.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.dgv_Systems.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
      this.dgv_Systems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgv_Systems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dgv_Systems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv_Systems.Location = new System.Drawing.Point(12, 12);
      this.dgv_Systems.Name = "dgv_Systems";
      this.dgv_Systems.RowHeadersVisible = false;
      this.dgv_Systems.RowTemplate.Height = 20;
      this.dgv_Systems.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.dgv_Systems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv_Systems.Size = new System.Drawing.Size(703, 349);
      this.dgv_Systems.TabIndex = 4;
      this.dgv_Systems.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DGV_Systems_MouseUp);
      // 
      // cms_ManageSystems
      // 
      this.cms_ManageSystems.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteSystemToolStripMenuItem});
      this.cms_ManageSystems.Name = "cms_ManageSystems";
      this.cms_ManageSystems.Size = new System.Drawing.Size(153, 48);
      // 
      // deleteSystemToolStripMenuItem
      // 
      this.deleteSystemToolStripMenuItem.Name = "deleteSystemToolStripMenuItem";
      this.deleteSystemToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.deleteSystemToolStripMenuItem.Text = "Delete pattern";
      this.deleteSystemToolStripMenuItem.Click += new System.EventHandler(this.DeleteSystemToolStripMenuItem_Click);
      // 
      // cb_LocalPatternsEnabled
      // 
      this.cb_LocalPatternsEnabled.AutoSize = true;
      this.cb_LocalPatternsEnabled.Checked = true;
      this.cb_LocalPatternsEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cb_LocalPatternsEnabled.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.cb_LocalPatternsEnabled.Location = new System.Drawing.Point(184, 380);
      this.cb_LocalPatternsEnabled.Name = "cb_LocalPatternsEnabled";
      this.cb_LocalPatternsEnabled.Size = new System.Drawing.Size(132, 17);
      this.cb_LocalPatternsEnabled.TabIndex = 18;
      this.cb_LocalPatternsEnabled.Text = "Local patterns enabled";
      this.cb_LocalPatternsEnabled.UseVisualStyleBackColor = true;
      this.cb_LocalPatternsEnabled.CheckedChanged += new System.EventHandler(this.CB_LocalPatternsEnabled_CheckedChanged);
      // 
      // cb_RemotePatternsEnabled
      // 
      this.cb_RemotePatternsEnabled.AutoSize = true;
      this.cb_RemotePatternsEnabled.Checked = true;
      this.cb_RemotePatternsEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cb_RemotePatternsEnabled.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.cb_RemotePatternsEnabled.Location = new System.Drawing.Point(23, 380);
      this.cb_RemotePatternsEnabled.Name = "cb_RemotePatternsEnabled";
      this.cb_RemotePatternsEnabled.Size = new System.Drawing.Size(143, 17);
      this.cb_RemotePatternsEnabled.TabIndex = 17;
      this.cb_RemotePatternsEnabled.Text = "Remote patterns enabled";
      this.cb_RemotePatternsEnabled.UseVisualStyleBackColor = true;
      this.cb_RemotePatternsEnabled.CheckedChanged += new System.EventHandler(this.CB_RemotePatternsEnabled_CheckedChanged);
      // 
      // bt_New
      // 
      this.bt_New.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.bt_New.Location = new System.Drawing.Point(523, 374);
      this.bt_New.Name = "bt_New";
      this.bt_New.Size = new System.Drawing.Size(75, 23);
      this.bt_New.TabIndex = 20;
      this.bt_New.Text = "New ...";
      this.bt_New.UseVisualStyleBackColor = true;
      this.bt_New.Click += new System.EventHandler(this.BT_New_Click);
      // 
      // bt_Close
      // 
      this.bt_Close.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.bt_Close.Location = new System.Drawing.Point(631, 374);
      this.bt_Close.Name = "bt_Close";
      this.bt_Close.Size = new System.Drawing.Size(75, 23);
      this.bt_Close.TabIndex = 19;
      this.bt_Close.Text = "Close";
      this.bt_Close.UseVisualStyleBackColor = true;
      this.bt_Close.Click += new System.EventHandler(this.BT_Close_Click);
      // 
      // fsw_PatternFiles
      // 
      this.fsw_PatternFiles.EnableRaisingEvents = true;
      this.fsw_PatternFiles.SynchronizingObject = this;
      this.fsw_PatternFiles.Created += new System.IO.FileSystemEventHandler(this.FSW_PatternFiles_Changed);
      this.fsw_PatternFiles.Deleted += new System.IO.FileSystemEventHandler(this.FSW_PatternFiles_Changed);
      // 
      // Form_ManageSystems
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(727, 409);
      this.Controls.Add(this.bt_New);
      this.Controls.Add(this.bt_Close);
      this.Controls.Add(this.cb_LocalPatternsEnabled);
      this.Controls.Add(this.cb_RemotePatternsEnabled);
      this.Controls.Add(this.dgv_Systems);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "Form_ManageSystems";
      this.Text = "  Manage systems";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManageSystems_FormClosing);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_Systems)).EndInit();
      this.cms_ManageSystems.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.fsw_PatternFiles)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView dgv_Systems;
    private System.Windows.Forms.ContextMenuStrip cms_ManageSystems;
    private System.Windows.Forms.ToolStripMenuItem deleteSystemToolStripMenuItem;
    private System.Windows.Forms.CheckBox cb_LocalPatternsEnabled;
    private System.Windows.Forms.CheckBox cb_RemotePatternsEnabled;
    private System.Windows.Forms.Button bt_New;
    private System.Windows.Forms.Button bt_Close;
    private System.IO.FileSystemWatcher fsw_PatternFiles;
  }
}