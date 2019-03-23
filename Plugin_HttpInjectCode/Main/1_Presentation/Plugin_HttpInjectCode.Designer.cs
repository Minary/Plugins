namespace Minary.Plugin.Main
{
  public partial class Plugin_HttpInjectCode
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
      this.tb_InjectioinContentFile = new System.Windows.Forms.TextBox();
      this.l_ReplacementResource = new System.Windows.Forms.Label();
      this.ofd_FileToInject = new System.Windows.Forms.OpenFileDialog();
      this.bt_AddFile = new System.Windows.Forms.Button();
      this.cms_InjectCode = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.deleteEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.clearListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.l_Tag = new System.Windows.Forms.Label();
      this.cb_injectPosition = new System.Windows.Forms.ComboBox();
      this.rb_Before = new System.Windows.Forms.RadioButton();
      this.rb_After = new System.Windows.Forms.RadioButton();
      this.l_Scheme = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.dgv_InjectionTriggerURLs)).BeginInit();
      this.cms_InjectCode.SuspendLayout();
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
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dgv_InjectionTriggerURLs.DefaultCellStyle = dataGridViewCellStyle1;
      this.dgv_InjectionTriggerURLs.EnableHeadersVisualStyles = false;
      this.dgv_InjectionTriggerURLs.Location = new System.Drawing.Point(26, 68);
      this.dgv_InjectionTriggerURLs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.dgv_InjectionTriggerURLs.MultiSelect = false;
      this.dgv_InjectionTriggerURLs.Name = "dgv_InjectionTriggerURLs";
      this.dgv_InjectionTriggerURLs.RowHeadersVisible = false;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
      this.dgv_InjectionTriggerURLs.RowsDefaultCellStyle = dataGridViewCellStyle2;
      this.dgv_InjectionTriggerURLs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.dgv_InjectionTriggerURLs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv_InjectionTriggerURLs.Size = new System.Drawing.Size(1400, 482);
      this.dgv_InjectionTriggerURLs.TabIndex = 0;
      this.dgv_InjectionTriggerURLs.TabStop = false;
      this.dgv_InjectionTriggerURLs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DGV_Spoofing_MouseDown);
      this.dgv_InjectionTriggerURLs.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DGV_Spoofing_MouseUp);
      // 
      // tb_RequestedURLRegex
      // 
      this.tb_RequestedUrlRegex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tb_RequestedUrlRegex.Location = new System.Drawing.Point(265, 26);
      this.tb_RequestedUrlRegex.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tb_RequestedUrlRegex.Name = "tb_RequestedURLRegex";
      this.tb_RequestedUrlRegex.Size = new System.Drawing.Size(256, 26);
      this.tb_RequestedUrlRegex.TabIndex = 1;
      this.tb_RequestedUrlRegex.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_AddRecord_KeyDown);
      // 
      // bt_AddRecord
      // 
      this.bt_AddRecord.Location = new System.Drawing.Point(1323, 22);
      this.bt_AddRecord.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.bt_AddRecord.Name = "bt_AddRecord";
      this.bt_AddRecord.Size = new System.Drawing.Size(34, 32);
      this.bt_AddRecord.TabIndex = 7;
      this.bt_AddRecord.Text = "+";
      this.bt_AddRecord.UseVisualStyleBackColor = true;
      this.bt_AddRecord.Click += new System.EventHandler(this.BT_Add_Click);
      this.bt_AddRecord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_AddRecord_KeyDown);
      // 
      // l_RequestedURL
      // 
      this.l_RequestedURL.AutoSize = true;
      this.l_RequestedURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_RequestedURL.Location = new System.Drawing.Point(32, 29);
      this.l_RequestedURL.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.l_RequestedURL.Name = "l_RequestedURL";
      this.l_RequestedURL.Size = new System.Drawing.Size(142, 20);
      this.l_RequestedURL.TabIndex = 0;
      this.l_RequestedURL.Text = "Req. URL regex";
      // 
      // tb_InjectioinContentFile
      // 
      this.tb_InjectioinContentFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tb_InjectioinContentFile.Location = new System.Drawing.Point(675, 23);
      this.tb_InjectioinContentFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tb_InjectioinContentFile.Name = "tb_InjectioinContentFile";
      this.tb_InjectioinContentFile.Size = new System.Drawing.Size(168, 26);
      this.tb_InjectioinContentFile.TabIndex = 2;
      this.tb_InjectioinContentFile.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_AddRecord_KeyDown);
      // 
      // l_ReplacementResource
      // 
      this.l_ReplacementResource.AutoSize = true;
      this.l_ReplacementResource.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_ReplacementResource.Location = new System.Drawing.Point(534, 29);
      this.l_ReplacementResource.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.l_ReplacementResource.Name = "l_ReplacementResource";
      this.l_ReplacementResource.Size = new System.Drawing.Size(128, 20);
      this.l_ReplacementResource.TabIndex = 0;
      this.l_ReplacementResource.Text = "Code from file";
      // 
      // ofd_FileToInject
      // 
      this.ofd_FileToInject.FileName = "...";
      // 
      // bt_AddFile
      // 
      this.bt_AddFile.Location = new System.Drawing.Point(850, 20);
      this.bt_AddFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.bt_AddFile.Name = "bt_AddFile";
      this.bt_AddFile.Size = new System.Drawing.Size(34, 32);
      this.bt_AddFile.TabIndex = 3;
      this.bt_AddFile.Text = "...";
      this.bt_AddFile.UseVisualStyleBackColor = true;
      this.bt_AddFile.Click += new System.EventHandler(this.BT_AddFile_Click);
      // 
      // cms_InjectCode
      // 
      this.cms_InjectCode.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.cms_InjectCode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteEntryToolStripMenuItem,
            this.clearListToolStripMenuItem});
      this.cms_InjectCode.Name = "cms_InjectCode";
      this.cms_InjectCode.Size = new System.Drawing.Size(180, 64);
      // 
      // deleteEntryToolStripMenuItem
      // 
      this.deleteEntryToolStripMenuItem.Name = "deleteEntryToolStripMenuItem";
      this.deleteEntryToolStripMenuItem.Size = new System.Drawing.Size(179, 30);
      this.deleteEntryToolStripMenuItem.Text = "Delete entry";
      this.deleteEntryToolStripMenuItem.Click += new System.EventHandler(this.TSMI_Delete_Click);
      // 
      // clearListToolStripMenuItem
      // 
      this.clearListToolStripMenuItem.Name = "clearListToolStripMenuItem";
      this.clearListToolStripMenuItem.Size = new System.Drawing.Size(179, 30);
      this.clearListToolStripMenuItem.Text = "Clear list";
      this.clearListToolStripMenuItem.Click += new System.EventHandler(this.TSMI_Clear_Click);
      // 
      // l_Tag
      // 
      this.l_Tag.AutoSize = true;
      this.l_Tag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_Tag.Location = new System.Drawing.Point(917, 29);
      this.l_Tag.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.l_Tag.Name = "l_Tag";
      this.l_Tag.Size = new System.Drawing.Size(40, 20);
      this.l_Tag.TabIndex = 0;
      this.l_Tag.Text = "Tag";
      // 
      // cb_injectPosition
      // 
      this.cb_injectPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cb_injectPosition.FormattingEnabled = true;
      this.cb_injectPosition.Location = new System.Drawing.Point(965, 21);
      this.cb_injectPosition.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.cb_injectPosition.Name = "cb_injectPosition";
      this.cb_injectPosition.Size = new System.Drawing.Size(132, 28);
      this.cb_injectPosition.TabIndex = 4;
      // 
      // rb_Before
      // 
      this.rb_Before.AutoSize = true;
      this.rb_Before.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.rb_Before.Location = new System.Drawing.Point(1133, 28);
      this.rb_Before.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.rb_Before.Name = "rb_Before";
      this.rb_Before.Size = new System.Drawing.Size(90, 24);
      this.rb_Before.TabIndex = 5;
      this.rb_Before.Text = "Before";
      this.rb_Before.UseVisualStyleBackColor = true;
      this.rb_Before.CheckedChanged += new System.EventHandler(this.RB_Position_CheckedChanged);
      // 
      // rb_After
      // 
      this.rb_After.AutoSize = true;
      this.rb_After.Checked = true;
      this.rb_After.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.rb_After.Location = new System.Drawing.Point(1215, 28);
      this.rb_After.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.rb_After.Name = "rb_After";
      this.rb_After.Size = new System.Drawing.Size(75, 24);
      this.rb_After.TabIndex = 6;
      this.rb_After.TabStop = true;
      this.rb_After.Text = "After";
      this.rb_After.UseVisualStyleBackColor = true;
      // 
      // l_Scheme
      // 
      this.l_Scheme.AutoSize = true;
      this.l_Scheme.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_Scheme.Location = new System.Drawing.Point(192, 29);
      this.l_Scheme.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.l_Scheme.Name = "l_Scheme";
      this.l_Scheme.Size = new System.Drawing.Size(73, 20);
      this.l_Scheme.TabIndex = 0;
      this.l_Scheme.Text = "http(s)://";
      // 
      // Plugin_HttpInjectCode
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.Controls.Add(this.tb_RequestedUrlRegex);
      this.Controls.Add(this.l_Scheme);
      this.Controls.Add(this.rb_After);
      this.Controls.Add(this.rb_Before);
      this.Controls.Add(this.cb_injectPosition);
      this.Controls.Add(this.l_Tag);
      this.Controls.Add(this.bt_AddFile);
      this.Controls.Add(this.tb_InjectioinContentFile);
      this.Controls.Add(this.l_ReplacementResource);
      this.Controls.Add(this.dgv_InjectionTriggerURLs);
      this.Controls.Add(this.bt_AddRecord);
      this.Controls.Add(this.l_RequestedURL);
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "Plugin_HttpInjectCode";
      this.Size = new System.Drawing.Size(1494, 566);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_AddRecord_KeyDown);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_InjectionTriggerURLs)).EndInit();
      this.cms_InjectCode.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.DataGridView dgv_InjectionTriggerURLs;
    private System.Windows.Forms.TextBox tb_RequestedUrlRegex;
    private System.Windows.Forms.Button bt_AddRecord;
    private System.Windows.Forms.Label l_RequestedURL;
    private System.Windows.Forms.TextBox tb_InjectioinContentFile;
    private System.Windows.Forms.Label l_ReplacementResource;
    private System.Windows.Forms.OpenFileDialog ofd_FileToInject;
    private System.Windows.Forms.Button bt_AddFile;
    private System.Windows.Forms.ContextMenuStrip cms_InjectCode;
    private System.Windows.Forms.ToolStripMenuItem deleteEntryToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem clearListToolStripMenuItem;
    private System.Windows.Forms.Label l_Tag;
    private System.Windows.Forms.ComboBox cb_injectPosition;
    private System.Windows.Forms.RadioButton rb_Before;
    private System.Windows.Forms.RadioButton rb_After;
    private System.Windows.Forms.Label l_Scheme;
  }
}
