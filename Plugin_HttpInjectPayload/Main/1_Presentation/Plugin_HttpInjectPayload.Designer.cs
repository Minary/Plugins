namespace Minary.Plugin.Main
{
  public partial class Plugin_HttpInjectPayload
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
      this.dgv_InjectionTriggerURLs = new System.Windows.Forms.DataGridView();
      this.tb_RequestedURLRegex = new System.Windows.Forms.TextBox();
      this.bt_AddRecord = new System.Windows.Forms.Button();
      this.l_RequestedURL = new System.Windows.Forms.Label();
      this.tb_ReplacementResource = new System.Windows.Forms.TextBox();
      this.l_ReplacementResource = new System.Windows.Forms.Label();
      this.ofd_FileToInject = new System.Windows.Forms.OpenFileDialog();
      this.bt_AddFile = new System.Windows.Forms.Button();
      this.cms_InjectPayload = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.deleteEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.clearListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      ((System.ComponentModel.ISupportInitialize)(this.dgv_InjectionTriggerURLs)).BeginInit();
      this.cms_InjectPayload.SuspendLayout();
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
      this.dgv_InjectionTriggerURLs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv_InjectionTriggerURLs.Location = new System.Drawing.Point(17, 44);
      this.dgv_InjectionTriggerURLs.MultiSelect = false;
      this.dgv_InjectionTriggerURLs.Name = "dgv_InjectionTriggerURLs";
      this.dgv_InjectionTriggerURLs.RowHeadersVisible = false;
      dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
      this.dgv_InjectionTriggerURLs.RowsDefaultCellStyle = dataGridViewCellStyle7;
      this.dgv_InjectionTriggerURLs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.dgv_InjectionTriggerURLs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv_InjectionTriggerURLs.Size = new System.Drawing.Size(933, 313);
      this.dgv_InjectionTriggerURLs.TabIndex = 0;
      this.dgv_InjectionTriggerURLs.TabStop = false;
      this.dgv_InjectionTriggerURLs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DGV_Spoofing_MouseDown);
      this.dgv_InjectionTriggerURLs.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DGV_Spoofing_MouseUp);
      // 
      // tb_RequestedURLRegex
      // 
      this.tb_RequestedURLRegex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tb_RequestedURLRegex.Location = new System.Drawing.Point(126, 16);
      this.tb_RequestedURLRegex.Name = "tb_RequestedURLRegex";
      this.tb_RequestedURLRegex.Size = new System.Drawing.Size(231, 20);
      this.tb_RequestedURLRegex.TabIndex = 1;
      this.tb_RequestedURLRegex.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_AddRecord_KeyDown);
      // 
      // bt_AddRecord
      // 
      this.bt_AddRecord.Location = new System.Drawing.Point(858, 14);
      this.bt_AddRecord.Name = "bt_AddRecord";
      this.bt_AddRecord.Size = new System.Drawing.Size(23, 21);
      this.bt_AddRecord.TabIndex = 5;
      this.bt_AddRecord.Text = "+";
      this.bt_AddRecord.UseVisualStyleBackColor = true;
      this.bt_AddRecord.Click += new System.EventHandler(this.BT_Add_Click);
      this.bt_AddRecord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_AddRecord_KeyDown);
      // 
      // l_RequestedURL
      // 
      this.l_RequestedURL.AutoSize = true;
      this.l_RequestedURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_RequestedURL.Location = new System.Drawing.Point(23, 19);
      this.l_RequestedURL.Name = "l_RequestedURL";
      this.l_RequestedURL.Size = new System.Drawing.Size(97, 13);
      this.l_RequestedURL.TabIndex = 0;
      this.l_RequestedURL.Text = "Requested URL";
      // 
      // tb_ReplacementResource
      // 
      this.tb_ReplacementResource.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tb_ReplacementResource.Location = new System.Drawing.Point(493, 14);
      this.tb_ReplacementResource.Name = "tb_ReplacementResource";
      this.tb_ReplacementResource.Size = new System.Drawing.Size(232, 20);
      this.tb_ReplacementResource.TabIndex = 2;
      this.tb_ReplacementResource.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_AddRecord_KeyDown);
      // 
      // l_ReplacementResource
      // 
      this.l_ReplacementResource.AutoSize = true;
      this.l_ReplacementResource.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_ReplacementResource.Location = new System.Drawing.Point(424, 18);
      this.l_ReplacementResource.Name = "l_ReplacementResource";
      this.l_ReplacementResource.Size = new System.Drawing.Size(60, 13);
      this.l_ReplacementResource.TabIndex = 0;
      this.l_ReplacementResource.Text = "Inject file";
      // 
      // ofd_FileToInject
      // 
      this.ofd_FileToInject.FileName = "...";
      // 
      // bt_AddFile
      // 
      this.bt_AddFile.Location = new System.Drawing.Point(745, 14);
      this.bt_AddFile.Name = "bt_AddFile";
      this.bt_AddFile.Size = new System.Drawing.Size(23, 21);
      this.bt_AddFile.TabIndex = 6;
      this.bt_AddFile.Text = "...";
      this.bt_AddFile.UseVisualStyleBackColor = true;
      this.bt_AddFile.Click += new System.EventHandler(this.BT_AddFile_Click);
      // 
      // cms_InjectPayload
      // 
      this.cms_InjectPayload.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteEntryToolStripMenuItem,
            this.clearListToolStripMenuItem});
      this.cms_InjectPayload.Name = "cms_InjectPayload";
      this.cms_InjectPayload.Size = new System.Drawing.Size(138, 48);
      // 
      // deleteEntryToolStripMenuItem
      // 
      this.deleteEntryToolStripMenuItem.Name = "deleteEntryToolStripMenuItem";
      this.deleteEntryToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
      this.deleteEntryToolStripMenuItem.Text = "Delete entry";
      this.deleteEntryToolStripMenuItem.Click += new System.EventHandler(this.TSMI_Delete_Click);
      // 
      // clearListToolStripMenuItem
      // 
      this.clearListToolStripMenuItem.Name = "clearListToolStripMenuItem";
      this.clearListToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
      this.clearListToolStripMenuItem.Text = "Clear list";
      this.clearListToolStripMenuItem.Click += new System.EventHandler(this.TSMI_Clear_Click);
      // 
      // Plugin_HttpInjectPayload
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.bt_AddFile);
      this.Controls.Add(this.tb_ReplacementResource);
      this.Controls.Add(this.l_ReplacementResource);
      this.Controls.Add(this.dgv_InjectionTriggerURLs);
      this.Controls.Add(this.tb_RequestedURLRegex);
      this.Controls.Add(this.bt_AddRecord);
      this.Controls.Add(this.l_RequestedURL);
      this.Name = "Plugin_HttpInjectPayload";
      this.Size = new System.Drawing.Size(996, 368);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_AddRecord_KeyDown);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_InjectionTriggerURLs)).EndInit();
      this.cms_InjectPayload.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.DataGridView dgv_InjectionTriggerURLs;
    private System.Windows.Forms.TextBox tb_RequestedURLRegex;
    private System.Windows.Forms.Button bt_AddRecord;
    private System.Windows.Forms.Label l_RequestedURL;
    private System.Windows.Forms.TextBox tb_ReplacementResource;
    private System.Windows.Forms.Label l_ReplacementResource;
    private System.Windows.Forms.OpenFileDialog ofd_FileToInject;
    private System.Windows.Forms.Button bt_AddFile;
    private System.Windows.Forms.ContextMenuStrip cms_InjectPayload;
    private System.Windows.Forms.ToolStripMenuItem deleteEntryToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem clearListToolStripMenuItem;
  }
}
