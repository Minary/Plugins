﻿namespace Minary.Plugin.Main
{
  public partial class Plugin_HttpInjectFile
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      this.dgv_InjectionTriggerURLs = new System.Windows.Forms.DataGridView();
      this.tb_RequestedUrlRegex = new System.Windows.Forms.TextBox();
      this.bt_AddRecord = new System.Windows.Forms.Button();
      this.l_RequestedURL = new System.Windows.Forms.Label();
      this.tb_ReplacementResource = new System.Windows.Forms.TextBox();
      this.l_ReplacementResource = new System.Windows.Forms.Label();
      this.ofd_FileToInject = new System.Windows.Forms.OpenFileDialog();
      this.bt_AddFile = new System.Windows.Forms.Button();
      this.cms_InjectFile = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.deleteEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.clearListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.l_Scheme = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.dgv_InjectionTriggerURLs)).BeginInit();
      this.cms_InjectFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // dgv_InjectionTriggerURLs
      // 
      this.dgv_InjectionTriggerURLs.AllowUserToAddRows = false;
      this.dgv_InjectionTriggerURLs.AllowUserToDeleteRows = false;
      this.dgv_InjectionTriggerURLs.AllowUserToResizeColumns = false;
      this.dgv_InjectionTriggerURLs.AllowUserToResizeRows = false;
      this.dgv_InjectionTriggerURLs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dgv_InjectionTriggerURLs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.dgv_InjectionTriggerURLs.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
      this.dgv_InjectionTriggerURLs.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgv_InjectionTriggerURLs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dgv_InjectionTriggerURLs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv_InjectionTriggerURLs.ContextMenuStrip = this.cms_InjectFile;
      this.dgv_InjectionTriggerURLs.EnableHeadersVisualStyles = false;
      this.dgv_InjectionTriggerURLs.Location = new System.Drawing.Point(17, 44);
      this.dgv_InjectionTriggerURLs.MultiSelect = false;
      this.dgv_InjectionTriggerURLs.Name = "dgv_InjectionTriggerURLs";
      this.dgv_InjectionTriggerURLs.RowHeadersVisible = false;
      this.dgv_InjectionTriggerURLs.RowHeadersWidth = 62;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
      this.dgv_InjectionTriggerURLs.RowsDefaultCellStyle = dataGridViewCellStyle2;
      this.dgv_InjectionTriggerURLs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.dgv_InjectionTriggerURLs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv_InjectionTriggerURLs.Size = new System.Drawing.Size(933, 313);
      this.dgv_InjectionTriggerURLs.TabIndex = 0;
      this.dgv_InjectionTriggerURLs.TabStop = false;
      this.dgv_InjectionTriggerURLs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DGV_Spoofing_MouseDown);
      // 
      // tb_RequestedUrlRegex
      // 
      this.tb_RequestedUrlRegex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tb_RequestedUrlRegex.Location = new System.Drawing.Point(195, 16);
      this.tb_RequestedUrlRegex.Name = "tb_RequestedUrlRegex";
      this.tb_RequestedUrlRegex.Size = new System.Drawing.Size(197, 26);
      this.tb_RequestedUrlRegex.TabIndex = 1;
      this.tb_RequestedUrlRegex.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_AddRecord_KeyDown);
      // 
      // bt_AddRecord
      // 
      this.bt_AddRecord.Location = new System.Drawing.Point(713, 12);
      this.bt_AddRecord.Name = "bt_AddRecord";
      this.bt_AddRecord.Size = new System.Drawing.Size(23, 21);
      this.bt_AddRecord.TabIndex = 4;
      this.bt_AddRecord.Text = "+";
      this.bt_AddRecord.UseVisualStyleBackColor = true;
      this.bt_AddRecord.Click += new System.EventHandler(this.BT_Add_Click);
      this.bt_AddRecord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_AddRecord_KeyDown);
      // 
      // l_RequestedURL
      // 
      this.l_RequestedURL.AutoSize = true;
      this.l_RequestedURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_RequestedURL.Location = new System.Drawing.Point(21, 19);
      this.l_RequestedURL.Name = "l_RequestedURL";
      this.l_RequestedURL.Size = new System.Drawing.Size(193, 20);
      this.l_RequestedURL.TabIndex = 0;
      this.l_RequestedURL.Text = "Requested URL regex";
      // 
      // tb_ReplacementResource
      // 
      this.tb_ReplacementResource.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tb_ReplacementResource.Location = new System.Drawing.Point(470, 14);
      this.tb_ReplacementResource.Name = "tb_ReplacementResource";
      this.tb_ReplacementResource.Size = new System.Drawing.Size(194, 26);
      this.tb_ReplacementResource.TabIndex = 2;
      this.tb_ReplacementResource.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_AddRecord_KeyDown);
      // 
      // l_ReplacementResource
      // 
      this.l_ReplacementResource.AutoSize = true;
      this.l_ReplacementResource.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_ReplacementResource.Location = new System.Drawing.Point(414, 18);
      this.l_ReplacementResource.Name = "l_ReplacementResource";
      this.l_ReplacementResource.Size = new System.Drawing.Size(87, 20);
      this.l_ReplacementResource.TabIndex = 0;
      this.l_ReplacementResource.Text = "Inject file";
      // 
      // ofd_FileToInject
      // 
      this.ofd_FileToInject.FileName = "...";
      // 
      // bt_AddFile
      // 
      this.bt_AddFile.Location = new System.Drawing.Point(671, 12);
      this.bt_AddFile.Name = "bt_AddFile";
      this.bt_AddFile.Size = new System.Drawing.Size(23, 21);
      this.bt_AddFile.TabIndex = 3;
      this.bt_AddFile.Text = "...";
      this.bt_AddFile.UseVisualStyleBackColor = true;
      this.bt_AddFile.Click += new System.EventHandler(this.BT_AddFile_Click);
      // 
      // cms_InjectFile
      // 
      this.cms_InjectFile.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.cms_InjectFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteEntryToolStripMenuItem,
            this.clearListToolStripMenuItem});
      this.cms_InjectFile.Name = "cms_InjectFile";
      this.cms_InjectFile.Size = new System.Drawing.Size(180, 68);
      // 
      // deleteEntryToolStripMenuItem
      // 
      this.deleteEntryToolStripMenuItem.Name = "deleteEntryToolStripMenuItem";
      this.deleteEntryToolStripMenuItem.Size = new System.Drawing.Size(179, 32);
      this.deleteEntryToolStripMenuItem.Text = "Delete entry";
      this.deleteEntryToolStripMenuItem.Click += new System.EventHandler(this.TSMI_Delete_Click);
      // 
      // clearListToolStripMenuItem
      // 
      this.clearListToolStripMenuItem.Name = "clearListToolStripMenuItem";
      this.clearListToolStripMenuItem.Size = new System.Drawing.Size(179, 32);
      this.clearListToolStripMenuItem.Text = "Clear list";
      this.clearListToolStripMenuItem.Click += new System.EventHandler(this.TSMI_Clear_Click);
      // 
      // l_Scheme
      // 
      this.l_Scheme.AutoSize = true;
      this.l_Scheme.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_Scheme.Location = new System.Drawing.Point(151, 20);
      this.l_Scheme.Name = "l_Scheme";
      this.l_Scheme.Size = new System.Drawing.Size(73, 20);
      this.l_Scheme.TabIndex = 0;
      this.l_Scheme.Text = "http(s)://";
      // 
      // Plugin_HttpInjectFile
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.Controls.Add(this.tb_RequestedUrlRegex);
      this.Controls.Add(this.l_Scheme);
      this.Controls.Add(this.bt_AddFile);
      this.Controls.Add(this.tb_ReplacementResource);
      this.Controls.Add(this.l_ReplacementResource);
      this.Controls.Add(this.dgv_InjectionTriggerURLs);
      this.Controls.Add(this.bt_AddRecord);
      this.Controls.Add(this.l_RequestedURL);
      this.Name = "Plugin_HttpInjectFile";
      this.Size = new System.Drawing.Size(996, 368);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_AddRecord_KeyDown);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_InjectionTriggerURLs)).EndInit();
      this.cms_InjectFile.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.DataGridView dgv_InjectionTriggerURLs;
    private System.Windows.Forms.TextBox tb_RequestedUrlRegex;
    private System.Windows.Forms.Button bt_AddRecord;
    private System.Windows.Forms.Label l_RequestedURL;
    private System.Windows.Forms.TextBox tb_ReplacementResource;
    private System.Windows.Forms.Label l_ReplacementResource;
    private System.Windows.Forms.OpenFileDialog ofd_FileToInject;
    private System.Windows.Forms.Button bt_AddFile;
    private System.Windows.Forms.ContextMenuStrip cms_InjectFile;
    private System.Windows.Forms.ToolStripMenuItem deleteEntryToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem clearListToolStripMenuItem;
    private System.Windows.Forms.Label l_Scheme;
  }
}
