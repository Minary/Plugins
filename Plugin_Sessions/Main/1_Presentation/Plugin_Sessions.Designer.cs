namespace Minary.Plugin.Main
{
  public partial class Plugin_Sessions
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Sessions");
      this.dgv_Sessions = new System.Windows.Forms.DataGridView();
      this.tv_Sessions = new System.Windows.Forms.TreeView();
      this.il_Sessions = new System.Windows.Forms.ImageList(this.components);
      this.cms_Sessions = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.deleteEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.tsmi_Clear = new System.Windows.Forms.ToolStripMenuItem();
      this.tsmi_ShowData = new System.Windows.Forms.ToolStripMenuItem();
      this.t_GuiUpdate = new System.Windows.Forms.Timer(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_Sessions)).BeginInit();
      this.cms_Sessions.SuspendLayout();
      this.SuspendLayout();
      // 
      // dgv_Sessions
      // 
      this.dgv_Sessions.AllowUserToAddRows = false;
      this.dgv_Sessions.AllowUserToDeleteRows = false;
      this.dgv_Sessions.AllowUserToResizeColumns = false;
      this.dgv_Sessions.AllowUserToResizeRows = false;
      this.dgv_Sessions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dgv_Sessions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.dgv_Sessions.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
      this.dgv_Sessions.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgv_Sessions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dgv_Sessions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv_Sessions.EnableHeadersVisualStyles = false;
      this.dgv_Sessions.Location = new System.Drawing.Point(290, 29);
      this.dgv_Sessions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.dgv_Sessions.MultiSelect = false;
      this.dgv_Sessions.Name = "dgv_Sessions";
      this.dgv_Sessions.RowHeadersVisible = false;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dgv_Sessions.RowsDefaultCellStyle = dataGridViewCellStyle2;
      this.dgv_Sessions.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dgv_Sessions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.dgv_Sessions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv_Sessions.Size = new System.Drawing.Size(1136, 537);
      this.dgv_Sessions.TabIndex = 1;
      this.dgv_Sessions.DoubleClick += new System.EventHandler(this.DGV_Sessions_DoubleClick);
      this.dgv_Sessions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DGV_Sessions_MouseDown);
      this.dgv_Sessions.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DGV_Sessions_MouseUp);
      // 
      // tv_Sessions
      // 
      this.tv_Sessions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.tv_Sessions.ImageIndex = 0;
      this.tv_Sessions.ImageList = this.il_Sessions;
      this.tv_Sessions.ItemHeight = 22;
      this.tv_Sessions.Location = new System.Drawing.Point(26, 29);
      this.tv_Sessions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tv_Sessions.Name = "tv_Sessions";
      treeNode1.Name = "SessionRoot";
      treeNode1.Text = "Sessions";
      this.tv_Sessions.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
      this.tv_Sessions.SelectedImageIndex = 0;
      this.tv_Sessions.Size = new System.Drawing.Size(240, 535);
      this.tv_Sessions.TabIndex = 1;
      this.tv_Sessions.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.TV_Sessions_AfterCollapse);
      this.tv_Sessions.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TV_Sessions_NodeMouseClick);
      // 
      // il_Sessions
      // 
      this.il_Sessions.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
      this.il_Sessions.ImageSize = new System.Drawing.Size(16, 16);
      this.il_Sessions.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // cms_Sessions
      // 
      this.cms_Sessions.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.cms_Sessions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteEntryToolStripMenuItem,
            this.tsmi_Clear,
            this.tsmi_ShowData});
      this.cms_Sessions.Name = "cms_Sessions";
      this.cms_Sessions.Size = new System.Drawing.Size(203, 94);
      // 
      // deleteEntryToolStripMenuItem
      // 
      this.deleteEntryToolStripMenuItem.Name = "deleteEntryToolStripMenuItem";
      this.deleteEntryToolStripMenuItem.Size = new System.Drawing.Size(202, 30);
      this.deleteEntryToolStripMenuItem.Text = "Delete entry";
      this.deleteEntryToolStripMenuItem.Click += new System.EventHandler(this.DeleteEntryToolStripMensuItem_Click);
      // 
      // tsmi_Clear
      // 
      this.tsmi_Clear.Name = "tsmi_Clear";
      this.tsmi_Clear.Size = new System.Drawing.Size(202, 30);
      this.tsmi_Clear.Text = "Clear list";
      this.tsmi_Clear.Click += new System.EventHandler(this.TSMI_Clear_Click);
      // 
      // tsmi_ShowData
      // 
      this.tsmi_ShowData.Name = "tsmi_ShowData";
      this.tsmi_ShowData.Size = new System.Drawing.Size(202, 30);
      this.tsmi_ShowData.Text = "Show newData";
      this.tsmi_ShowData.Click += new System.EventHandler(this.TSMI_ShowData_Click);
      // 
      // t_GuiUpdate
      // 
      this.t_GuiUpdate.Interval = 500;
      this.t_GuiUpdate.Tick += new System.EventHandler(this.T_GuiUpdate_Tick);
      // 
      // Plugin_Sessions
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.Controls.Add(this.tv_Sessions);
      this.Controls.Add(this.dgv_Sessions);
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "Plugin_Sessions";
      this.Size = new System.Drawing.Size(1494, 583);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_Sessions)).EndInit();
      this.cms_Sessions.ResumeLayout(false);
      this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_Sessions;
        private System.Windows.Forms.TreeView tv_Sessions;
        private System.Windows.Forms.ImageList il_Sessions;
        private System.Windows.Forms.ContextMenuStrip cms_Sessions;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Clear;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ShowData;
        private System.Windows.Forms.ToolStripMenuItem deleteEntryToolStripMenuItem;
        private System.Windows.Forms.Timer t_GuiUpdate;


    }
}
