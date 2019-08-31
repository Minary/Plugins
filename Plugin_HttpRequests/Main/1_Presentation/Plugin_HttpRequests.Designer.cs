namespace Minary.Plugin.Main
{
  public partial class Plugin_HttpRequests
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      this.dgv_HttpRequests = new System.Windows.Forms.DataGridView();
      this.cms_HttpRequests = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.deleteEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.tsmi_Clear = new System.Windows.Forms.ToolStripMenuItem();
      this.requestDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.showDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.l_UrlFilter = new System.Windows.Forms.Label();
      this.tb_Filter = new System.Windows.Forms.TextBox();
      this.bt_Set = new System.Windows.Forms.Button();
      this.t_GuiUpdate = new System.Windows.Forms.Timer(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_HttpRequests)).BeginInit();
      this.cms_HttpRequests.SuspendLayout();
      this.SuspendLayout();
      // 
      // dgv_HttpRequests
      // 
      this.dgv_HttpRequests.AllowUserToAddRows = false;
      this.dgv_HttpRequests.AllowUserToDeleteRows = false;
      this.dgv_HttpRequests.AllowUserToResizeColumns = false;
      this.dgv_HttpRequests.AllowUserToResizeRows = false;
      this.dgv_HttpRequests.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dgv_HttpRequests.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.dgv_HttpRequests.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
      this.dgv_HttpRequests.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgv_HttpRequests.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dgv_HttpRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv_HttpRequests.ContextMenuStrip = this.cms_HttpRequests;
      this.dgv_HttpRequests.EnableHeadersVisualStyles = false;
      this.dgv_HttpRequests.Location = new System.Drawing.Point(17, 44);
      this.dgv_HttpRequests.MultiSelect = false;
      this.dgv_HttpRequests.Name = "dgv_HttpRequests";
      this.dgv_HttpRequests.ReadOnly = true;
      this.dgv_HttpRequests.RowHeadersVisible = false;
      this.dgv_HttpRequests.RowHeadersWidth = 62;
      this.dgv_HttpRequests.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dgv_HttpRequests.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.dgv_HttpRequests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv_HttpRequests.Size = new System.Drawing.Size(933, 313);
      this.dgv_HttpRequests.TabIndex = 3;
      this.dgv_HttpRequests.DoubleClick += new System.EventHandler(this.DGV_HttpRequests_DoubleClick);
      this.dgv_HttpRequests.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DGV_HttpRequests_MouseDown);
      // 
      // cms_HttpRequests
      // 
      this.cms_HttpRequests.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.cms_HttpRequests.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteEntryToolStripMenuItem,
            this.tsmi_Clear,
            this.requestDetailsToolStripMenuItem,
            this.showDataToolStripMenuItem});
      this.cms_HttpRequests.Name = "CMS_Downloads";
      this.cms_HttpRequests.Size = new System.Drawing.Size(234, 132);
      // 
      // deleteEntryToolStripMenuItem
      // 
      this.deleteEntryToolStripMenuItem.Name = "deleteEntryToolStripMenuItem";
      this.deleteEntryToolStripMenuItem.Size = new System.Drawing.Size(233, 32);
      this.deleteEntryToolStripMenuItem.Text = "Delete entry";
      this.deleteEntryToolStripMenuItem.Click += new System.EventHandler(this.DeleteEntryToolStripMenuItem_Click);
      // 
      // tsmi_Clear
      // 
      this.tsmi_Clear.Name = "tsmi_Clear";
      this.tsmi_Clear.Size = new System.Drawing.Size(233, 32);
      this.tsmi_Clear.Text = "Clear list";
      this.tsmi_Clear.Click += new System.EventHandler(this.TSMI_Clear_Click);
      // 
      // requestDetailsToolStripMenuItem
      // 
      this.requestDetailsToolStripMenuItem.Name = "requestDetailsToolStripMenuItem";
      this.requestDetailsToolStripMenuItem.Size = new System.Drawing.Size(233, 32);
      this.requestDetailsToolStripMenuItem.Text = "Open in browser ...";
      this.requestDetailsToolStripMenuItem.Click += new System.EventHandler(this.TSMI_RequestDetails_Click);
      // 
      // showDataToolStripMenuItem
      // 
      this.showDataToolStripMenuItem.Name = "showDataToolStripMenuItem";
      this.showDataToolStripMenuItem.Size = new System.Drawing.Size(233, 32);
      this.showDataToolStripMenuItem.Text = "Show data ...";
      this.showDataToolStripMenuItem.Click += new System.EventHandler(this.TSMI_ShowData_Click);
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
      // tb_Filter
      // 
      this.tb_Filter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tb_Filter.Location = new System.Drawing.Point(104, 16);
      this.tb_Filter.Name = "tb_Filter";
      this.tb_Filter.Size = new System.Drawing.Size(231, 26);
      this.tb_Filter.TabIndex = 1;
      this.tb_Filter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TB_Filter_KeyUp);
      // 
      // bt_Set
      // 
      this.bt_Set.Location = new System.Drawing.Point(350, 16);
      this.bt_Set.Name = "bt_Set";
      this.bt_Set.Size = new System.Drawing.Size(33, 20);
      this.bt_Set.TabIndex = 2;
      this.bt_Set.Text = "Set";
      this.bt_Set.UseVisualStyleBackColor = true;
      this.bt_Set.Click += new System.EventHandler(this.BT_Set_Click);
      // 
      // t_GuiUpdate
      // 
      this.t_GuiUpdate.Interval = 500;
      this.t_GuiUpdate.Tick += new System.EventHandler(this.T_GuiUpdate_Tick);
      // 
      // Plugin_HttpRequests
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.Controls.Add(this.bt_Set);
      this.Controls.Add(this.tb_Filter);
      this.Controls.Add(this.l_UrlFilter);
      this.Controls.Add(this.dgv_HttpRequests);
      this.DoubleBuffered = true;
      this.Name = "Plugin_HttpRequests";
      this.Size = new System.Drawing.Size(996, 368);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_HttpRequests)).EndInit();
      this.cms_HttpRequests.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_HttpRequests;
        private System.Windows.Forms.ContextMenuStrip cms_HttpRequests;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Clear;
        private System.Windows.Forms.Label l_UrlFilter;
        private System.Windows.Forms.TextBox tb_Filter;
        private System.Windows.Forms.Button bt_Set;
        private System.Windows.Forms.Timer t_GuiUpdate;
        private System.Windows.Forms.ToolStripMenuItem deleteEntryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem requestDetailsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem showDataToolStripMenuItem;
  }
}
