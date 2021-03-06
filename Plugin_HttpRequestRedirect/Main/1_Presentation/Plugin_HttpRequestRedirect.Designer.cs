﻿namespace Minary.Plugin.Main
{
  partial class Plugin_HttpRequestRedirect
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
      this.tb_RedirectURL = new System.Windows.Forms.TextBox();
      this.dgv_RequestRedirectURLs = new System.Windows.Forms.DataGridView();
      this.tb_RequestedUrlRegex = new System.Windows.Forms.TextBox();
      this.l_RequestedURL = new System.Windows.Forms.Label();
      this.l_RedirectURL = new System.Windows.Forms.Label();
      this.bt_AddRecord = new System.Windows.Forms.Button();
      this.cb_RedirectType = new System.Windows.Forms.ComboBox();
      this.cms_RequestRedirect = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.deleteEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.clearListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.l_Scheme = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.dgv_RequestRedirectURLs)).BeginInit();
      this.cms_RequestRedirect.SuspendLayout();
      this.SuspendLayout();
      // 
      // tb_RedirectURL
      // 
      this.tb_RedirectURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tb_RedirectURL.Location = new System.Drawing.Point(505, 14);
      this.tb_RedirectURL.Name = "tb_RedirectURL";
      this.tb_RedirectURL.Size = new System.Drawing.Size(206, 26);
      this.tb_RedirectURL.TabIndex = 2;
      this.tb_RedirectURL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_Host_KeyDown);
      // 
      // dgv_RequestRedirectURLs
      // 
      this.dgv_RequestRedirectURLs.AllowUserToAddRows = false;
      this.dgv_RequestRedirectURLs.AllowUserToDeleteRows = false;
      this.dgv_RequestRedirectURLs.AllowUserToResizeColumns = false;
      this.dgv_RequestRedirectURLs.AllowUserToResizeRows = false;
      this.dgv_RequestRedirectURLs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dgv_RequestRedirectURLs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.dgv_RequestRedirectURLs.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
      this.dgv_RequestRedirectURLs.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgv_RequestRedirectURLs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dgv_RequestRedirectURLs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv_RequestRedirectURLs.ContextMenuStrip = this.cms_RequestRedirect;
      this.dgv_RequestRedirectURLs.EnableHeadersVisualStyles = false;
      this.dgv_RequestRedirectURLs.Location = new System.Drawing.Point(17, 44);
      this.dgv_RequestRedirectURLs.MultiSelect = false;
      this.dgv_RequestRedirectURLs.Name = "dgv_RequestRedirectURLs";
      this.dgv_RequestRedirectURLs.RowHeadersVisible = false;
      this.dgv_RequestRedirectURLs.RowHeadersWidth = 62;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
      this.dgv_RequestRedirectURLs.RowsDefaultCellStyle = dataGridViewCellStyle2;
      this.dgv_RequestRedirectURLs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.dgv_RequestRedirectURLs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv_RequestRedirectURLs.Size = new System.Drawing.Size(933, 313);
      this.dgv_RequestRedirectURLs.TabIndex = 5;
      this.dgv_RequestRedirectURLs.TabStop = false;
      this.dgv_RequestRedirectURLs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DGV_RequestRedirect_MouseDown);
      // 
      // tb_RequestedUrlRegex
      // 
      this.tb_RequestedUrlRegex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tb_RequestedUrlRegex.Location = new System.Drawing.Point(203, 16);
      this.tb_RequestedUrlRegex.Name = "tb_RequestedUrlRegex";
      this.tb_RequestedUrlRegex.Size = new System.Drawing.Size(184, 26);
      this.tb_RequestedUrlRegex.TabIndex = 1;
      this.tb_RequestedUrlRegex.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_Host_KeyDown);
      // 
      // l_RequestedURL
      // 
      this.l_RequestedURL.AutoSize = true;
      this.l_RequestedURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_RequestedURL.Location = new System.Drawing.Point(23, 19);
      this.l_RequestedURL.Name = "l_RequestedURL";
      this.l_RequestedURL.Size = new System.Drawing.Size(193, 20);
      this.l_RequestedURL.TabIndex = 0;
      this.l_RequestedURL.Text = "Requested URL regex";
      // 
      // l_RedirectURL
      // 
      this.l_RedirectURL.AutoSize = true;
      this.l_RedirectURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_RedirectURL.Location = new System.Drawing.Point(404, 18);
      this.l_RedirectURL.Name = "l_RedirectURL";
      this.l_RedirectURL.Size = new System.Drawing.Size(145, 20);
      this.l_RedirectURL.TabIndex = 0;
      this.l_RedirectURL.Text = "Redirect to URL";
      // 
      // bt_AddRecord
      // 
      this.bt_AddRecord.Location = new System.Drawing.Point(853, 14);
      this.bt_AddRecord.Name = "bt_AddRecord";
      this.bt_AddRecord.Size = new System.Drawing.Size(23, 21);
      this.bt_AddRecord.TabIndex = 4;
      this.bt_AddRecord.Text = "+";
      this.bt_AddRecord.UseVisualStyleBackColor = true;
      this.bt_AddRecord.Click += new System.EventHandler(this.BT_Add_Click);
      // 
      // cb_RedirectType
      // 
      this.cb_RedirectType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cb_RedirectType.FormattingEnabled = true;
      this.cb_RedirectType.Location = new System.Drawing.Point(723, 14);
      this.cb_RedirectType.Name = "cb_RedirectType";
      this.cb_RedirectType.Size = new System.Drawing.Size(121, 21);
      this.cb_RedirectType.TabIndex = 3;
      // 
      // cms_RequestRedirect
      // 
      this.cms_RequestRedirect.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.cms_RequestRedirect.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteEntryToolStripMenuItem,
            this.clearListToolStripMenuItem});
      this.cms_RequestRedirect.Name = "cms_RequestRedirect";
      this.cms_RequestRedirect.Size = new System.Drawing.Size(180, 68);
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
      this.l_Scheme.Location = new System.Drawing.Point(159, 19);
      this.l_Scheme.Name = "l_Scheme";
      this.l_Scheme.Size = new System.Drawing.Size(73, 20);
      this.l_Scheme.TabIndex = 6;
      this.l_Scheme.Text = "http(s)://";
      // 
      // Plugin_HttpRequestRedirect
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.Controls.Add(this.l_Scheme);
      this.Controls.Add(this.cb_RedirectType);
      this.Controls.Add(this.bt_AddRecord);
      this.Controls.Add(this.l_RedirectURL);
      this.Controls.Add(this.tb_RedirectURL);
      this.Controls.Add(this.dgv_RequestRedirectURLs);
      this.Controls.Add(this.tb_RequestedUrlRegex);
      this.Controls.Add(this.l_RequestedURL);
      this.Name = "Plugin_HttpRequestRedirect";
      this.Size = new System.Drawing.Size(996, 368);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_RequestRedirectURLs)).EndInit();
      this.cms_RequestRedirect.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.TextBox tb_RedirectURL;
    private System.Windows.Forms.DataGridView dgv_RequestRedirectURLs;
    private System.Windows.Forms.TextBox tb_RequestedUrlRegex;
    private System.Windows.Forms.Label l_RequestedURL;
    private System.Windows.Forms.Label l_RedirectURL;
    private System.Windows.Forms.Button bt_AddRecord;
    private System.Windows.Forms.ComboBox cb_RedirectType;
    private System.Windows.Forms.ContextMenuStrip cms_RequestRedirect;
    private System.Windows.Forms.ToolStripMenuItem deleteEntryToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem clearListToolStripMenuItem;
    private System.Windows.Forms.Label l_Scheme;
  }
}
