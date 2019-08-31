namespace Minary.Plugin.Main
{
    partial class Plugin_HttpsRequests
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
      this.bt_Set = new System.Windows.Forms.Button();
      this.tb_Filter = new System.Windows.Forms.TextBox();
      this.l_UrlFilter = new System.Windows.Forms.Label();
      this.dgv_HttpsRequests = new System.Windows.Forms.DataGridView();
      this.t_GuiUpdate = new System.Windows.Forms.Timer(this.components);
      this.cms_HttpsRequests = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.deleteEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.clearListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openInBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      ((System.ComponentModel.ISupportInitialize)(this.dgv_HttpsRequests)).BeginInit();
      this.cms_HttpsRequests.SuspendLayout();
      this.SuspendLayout();
      // 
      // bt_Set
      // 
      this.bt_Set.Location = new System.Drawing.Point(350, 16);
      this.bt_Set.Name = "bt_Set";
      this.bt_Set.Size = new System.Drawing.Size(33, 20);
      this.bt_Set.TabIndex = 2;
      this.bt_Set.Text = "Set";
      this.bt_Set.UseVisualStyleBackColor = true;
      // 
      // tb_Filter
      // 
      this.tb_Filter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tb_Filter.Location = new System.Drawing.Point(104, 16);
      this.tb_Filter.Name = "tb_Filter";
      this.tb_Filter.Size = new System.Drawing.Size(231, 26);
      this.tb_Filter.TabIndex = 1;
      // 
      // l_UrlFilter
      // 
      this.l_UrlFilter.AutoSize = true;
      this.l_UrlFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_UrlFilter.Location = new System.Drawing.Point(23, 19);
      this.l_UrlFilter.Name = "l_UrlFilter";
      this.l_UrlFilter.Size = new System.Drawing.Size(91, 20);
      this.l_UrlFilter.TabIndex = 0;
      this.l_UrlFilter.Text = "URL filter";
      // 
      // dgv_HttpsRequests
      // 
      this.dgv_HttpsRequests.AllowUserToAddRows = false;
      this.dgv_HttpsRequests.AllowUserToDeleteRows = false;
      this.dgv_HttpsRequests.AllowUserToResizeColumns = false;
      this.dgv_HttpsRequests.AllowUserToResizeRows = false;
      this.dgv_HttpsRequests.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dgv_HttpsRequests.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.dgv_HttpsRequests.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
      this.dgv_HttpsRequests.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgv_HttpsRequests.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dgv_HttpsRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv_HttpsRequests.ContextMenuStrip = this.cms_HttpsRequests;
      this.dgv_HttpsRequests.EnableHeadersVisualStyles = false;
      this.dgv_HttpsRequests.Location = new System.Drawing.Point(17, 44);
      this.dgv_HttpsRequests.MultiSelect = false;
      this.dgv_HttpsRequests.Name = "dgv_HttpsRequests";
      this.dgv_HttpsRequests.ReadOnly = true;
      this.dgv_HttpsRequests.RowHeadersVisible = false;
      this.dgv_HttpsRequests.RowHeadersWidth = 62;
      this.dgv_HttpsRequests.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dgv_HttpsRequests.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.dgv_HttpsRequests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv_HttpsRequests.Size = new System.Drawing.Size(933, 313);
      this.dgv_HttpsRequests.TabIndex = 3;
      this.dgv_HttpsRequests.DoubleClick += new System.EventHandler(this.Dgv_HttpsRequests_DoubleClick);
      this.dgv_HttpsRequests.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Dgv_HttpsRequest_MouseDown);
      // 
      // t_GuiUpdate
      // 
      this.t_GuiUpdate.Interval = 500;
      this.t_GuiUpdate.Tick += new System.EventHandler(this.T_GuiUpdate_Tick);
      // 
      // cms_HttpsRequests
      // 
      this.cms_HttpsRequests.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.cms_HttpsRequests.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteEntryToolStripMenuItem,
            this.clearListToolStripMenuItem,
            this.openInBrowserToolStripMenuItem});
      this.cms_HttpsRequests.Name = "cms_HttpsRequests";
      this.cms_HttpsRequests.Size = new System.Drawing.Size(217, 100);
      // 
      // deleteEntryToolStripMenuItem
      // 
      this.deleteEntryToolStripMenuItem.Name = "deleteEntryToolStripMenuItem";
      this.deleteEntryToolStripMenuItem.Size = new System.Drawing.Size(216, 32);
      this.deleteEntryToolStripMenuItem.Text = "Delete entry";
      this.deleteEntryToolStripMenuItem.Click += new System.EventHandler(this.DeleteEntryToolStripMenuItem_Click);
      // 
      // clearListToolStripMenuItem
      // 
      this.clearListToolStripMenuItem.Name = "clearListToolStripMenuItem";
      this.clearListToolStripMenuItem.Size = new System.Drawing.Size(216, 32);
      this.clearListToolStripMenuItem.Text = "Clear list";
      this.clearListToolStripMenuItem.Click += new System.EventHandler(this.TSMI_Clear_Click);
      // 
      // openInBrowserToolStripMenuItem
      // 
      this.openInBrowserToolStripMenuItem.Name = "openInBrowserToolStripMenuItem";
      this.openInBrowserToolStripMenuItem.Size = new System.Drawing.Size(216, 32);
      this.openInBrowserToolStripMenuItem.Text = "Open in browser";
      this.openInBrowserToolStripMenuItem.Click += new System.EventHandler(this.RequestDetailsToolStripMenuItem_Click);
      // 
      // Plugin_HttpsRequests
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.bt_Set);
      this.Controls.Add(this.tb_Filter);
      this.Controls.Add(this.l_UrlFilter);
      this.Controls.Add(this.dgv_HttpsRequests);
      this.Name = "Plugin_HttpsRequests";
      this.Size = new System.Drawing.Size(996, 379);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_HttpsRequests)).EndInit();
      this.cms_HttpsRequests.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

        }

    #endregion

    private System.Windows.Forms.Button bt_Set;
    private System.Windows.Forms.TextBox tb_Filter;
    private System.Windows.Forms.Label l_UrlFilter;
    private System.Windows.Forms.DataGridView dgv_HttpsRequests;
    private System.Windows.Forms.Timer t_GuiUpdate;
    private System.Windows.Forms.ContextMenuStrip cms_HttpsRequests;
    private System.Windows.Forms.ToolStripMenuItem deleteEntryToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem clearListToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem openInBrowserToolStripMenuItem;
  }
}
