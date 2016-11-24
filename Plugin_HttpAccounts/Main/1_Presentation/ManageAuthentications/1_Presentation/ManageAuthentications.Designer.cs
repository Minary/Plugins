namespace Minary.Plugin.Main.HttpAccounts.ManageAuthentications.Presentation
{
  public partial class Form_ManageAuthentications
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ManageAuthentications));
      this.dgv_AccountPatterns = new System.Windows.Forms.DataGridView();
      this.cms_ManageAccounts = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.deletePatternToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.bt_Close = new System.Windows.Forms.Button();
      this.bt_New = new System.Windows.Forms.Button();
      this.cb_RemotePatternsEnabled = new System.Windows.Forms.CheckBox();
      this.cb_LocalPatternsEnabled = new System.Windows.Forms.CheckBox();
      this.fsw_PatternFiles = new System.IO.FileSystemWatcher();
      ((System.ComponentModel.ISupportInitialize)(this.dgv_AccountPatterns)).BeginInit();
      this.cms_ManageAccounts.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.fsw_PatternFiles)).BeginInit();
      this.SuspendLayout();
      // 
      // dgv_AccountPatterns
      // 
      this.dgv_AccountPatterns.AllowUserToAddRows = false;
      this.dgv_AccountPatterns.AllowUserToDeleteRows = false;
      this.dgv_AccountPatterns.AllowUserToResizeColumns = false;
      this.dgv_AccountPatterns.AllowUserToResizeRows = false;
      this.dgv_AccountPatterns.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.dgv_AccountPatterns.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
      this.dgv_AccountPatterns.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgv_AccountPatterns.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
      this.dgv_AccountPatterns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv_AccountPatterns.Location = new System.Drawing.Point(12, 12);
      this.dgv_AccountPatterns.Name = "dgv_AccountPatterns";
      this.dgv_AccountPatterns.RowHeadersVisible = false;
      this.dgv_AccountPatterns.RowTemplate.Height = 18;
      this.dgv_AccountPatterns.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.dgv_AccountPatterns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv_AccountPatterns.Size = new System.Drawing.Size(1264, 367);
      this.dgv_AccountPatterns.TabIndex = 8;
      this.dgv_AccountPatterns.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DGV_AccountPatterns_MouseUp);
      // 
      // cms_ManageAccounts
      // 
      this.cms_ManageAccounts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deletePatternToolStripMenuItem});
      this.cms_ManageAccounts.Name = "cms_ManageAccounts";
      this.cms_ManageAccounts.Size = new System.Drawing.Size(149, 26);
      // 
      // deletePatternToolStripMenuItem
      // 
      this.deletePatternToolStripMenuItem.Name = "deletePatternToolStripMenuItem";
      this.deletePatternToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
      this.deletePatternToolStripMenuItem.Text = "Delete pattern";
      this.deletePatternToolStripMenuItem.Click += new System.EventHandler(this.DeletePatternToolStripMenuItem_Click);
      // 
      // bt_Close
      // 
      this.bt_Close.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.bt_Close.Location = new System.Drawing.Point(1185, 390);
      this.bt_Close.Name = "bt_Close";
      this.bt_Close.Size = new System.Drawing.Size(75, 23);
      this.bt_Close.TabIndex = 13;
      this.bt_Close.Text = "Close";
      this.bt_Close.UseVisualStyleBackColor = true;
      this.bt_Close.Click += new System.EventHandler(this.BT_Close_Click);
      // 
      // bt_New
      // 
      this.bt_New.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.bt_New.Location = new System.Drawing.Point(1077, 390);
      this.bt_New.Name = "bt_New";
      this.bt_New.Size = new System.Drawing.Size(75, 23);
      this.bt_New.TabIndex = 14;
      this.bt_New.Text = "New ...";
      this.bt_New.UseVisualStyleBackColor = true;
      this.bt_New.Click += new System.EventHandler(this.BT_New_Click);
      // 
      // cb_RemotePatternsEnabled
      // 
      this.cb_RemotePatternsEnabled.AutoSize = true;
      this.cb_RemotePatternsEnabled.Checked = true;
      this.cb_RemotePatternsEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cb_RemotePatternsEnabled.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.cb_RemotePatternsEnabled.Location = new System.Drawing.Point(22, 393);
      this.cb_RemotePatternsEnabled.Name = "cb_RemotePatternsEnabled";
      this.cb_RemotePatternsEnabled.Size = new System.Drawing.Size(143, 17);
      this.cb_RemotePatternsEnabled.TabIndex = 15;
      this.cb_RemotePatternsEnabled.Text = "Remote patterns enabled";
      this.cb_RemotePatternsEnabled.UseVisualStyleBackColor = true;
      this.cb_RemotePatternsEnabled.CheckedChanged += new System.EventHandler(this.CB_RemotePatternsEnabled_CheckedChanged);
      // 
      // cb_LocalPatternsEnabled
      // 
      this.cb_LocalPatternsEnabled.AutoSize = true;
      this.cb_LocalPatternsEnabled.Checked = true;
      this.cb_LocalPatternsEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cb_LocalPatternsEnabled.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.cb_LocalPatternsEnabled.Location = new System.Drawing.Point(183, 393);
      this.cb_LocalPatternsEnabled.Name = "cb_LocalPatternsEnabled";
      this.cb_LocalPatternsEnabled.Size = new System.Drawing.Size(132, 17);
      this.cb_LocalPatternsEnabled.TabIndex = 16;
      this.cb_LocalPatternsEnabled.Text = "Local patterns enabled";
      this.cb_LocalPatternsEnabled.UseVisualStyleBackColor = true;
      this.cb_LocalPatternsEnabled.CheckedChanged += new System.EventHandler(this.CB_LocalPatternsEnabled_CheckedChanged);
      // 
      // fsw_PatternFiles
      // 
      this.fsw_PatternFiles.EnableRaisingEvents = true;
      this.fsw_PatternFiles.SynchronizingObject = this;
      this.fsw_PatternFiles.Created += new System.IO.FileSystemEventHandler(this.FSW_PatternFiles_Changed);
      this.fsw_PatternFiles.Deleted += new System.IO.FileSystemEventHandler(this.FSW_PatternFiles_Changed);
      // 
      // Form_ManageAuthentications
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1288, 422);
      this.Controls.Add(this.cb_LocalPatternsEnabled);
      this.Controls.Add(this.cb_RemotePatternsEnabled);
      this.Controls.Add(this.bt_New);
      this.Controls.Add(this.bt_Close);
      this.Controls.Add(this.dgv_AccountPatterns);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "Form_ManageAuthentications";
      this.Text = "  Manage authentications patterns";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManageAuthentications_FormClosing);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_AccountPatterns)).EndInit();
      this.cms_ManageAccounts.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.fsw_PatternFiles)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView dgv_AccountPatterns;
    private System.Windows.Forms.ContextMenuStrip cms_ManageAccounts;
    private System.Windows.Forms.ToolStripMenuItem deletePatternToolStripMenuItem;
    private System.Windows.Forms.Button bt_Close;
    private System.Windows.Forms.Button bt_New;
    private System.Windows.Forms.CheckBox cb_RemotePatternsEnabled;
    private System.Windows.Forms.CheckBox cb_LocalPatternsEnabled;
    private System.IO.FileSystemWatcher fsw_PatternFiles;
  }
}