namespace Minary.Plugin.Main
{
  public partial class Plugin_DnsRequests
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
      this.dgv_DnsRequests = new System.Windows.Forms.DataGridView();
      this.bt_Set = new System.Windows.Forms.Button();
      this.tb_Filter = new System.Windows.Forms.TextBox();
      this.l_RequestFilter = new System.Windows.Forms.Label();
      this.cms_DnsRequests = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.deleteEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.tsmi_Clear = new System.Windows.Forms.ToolStripMenuItem();
      this.copyHostNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.t_GuiUpdate = new System.Windows.Forms.Timer(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_DnsRequests)).BeginInit();
      this.cms_DnsRequests.SuspendLayout();
      this.SuspendLayout();
      // 
      // dgv_DnsRequests
      // 
      this.dgv_DnsRequests.AllowUserToAddRows = false;
      this.dgv_DnsRequests.AllowUserToDeleteRows = false;
      this.dgv_DnsRequests.AllowUserToResizeColumns = false;
      this.dgv_DnsRequests.AllowUserToResizeRows = false;
      this.dgv_DnsRequests.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dgv_DnsRequests.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.dgv_DnsRequests.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
      this.dgv_DnsRequests.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgv_DnsRequests.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dgv_DnsRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv_DnsRequests.Location = new System.Drawing.Point(26, 68);
      this.dgv_DnsRequests.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.dgv_DnsRequests.MultiSelect = false;
      this.dgv_DnsRequests.Name = "dgv_DnsRequests";
      this.dgv_DnsRequests.ReadOnly = true;
      this.dgv_DnsRequests.RowHeadersVisible = false;
      this.dgv_DnsRequests.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dgv_DnsRequests.RowTemplate.Height = 20;
      this.dgv_DnsRequests.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.dgv_DnsRequests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv_DnsRequests.Size = new System.Drawing.Size(1400, 482);
      this.dgv_DnsRequests.TabIndex = 0;
      this.dgv_DnsRequests.TabStop = false;
      this.dgv_DnsRequests.VirtualMode = true;
      this.dgv_DnsRequests.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_DNSRequests_CellContentClick);
      this.dgv_DnsRequests.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DGV_DNSRequests_MouseDown);
      this.dgv_DnsRequests.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DGV_DNSRequests_MouseUp);
      // 
      // bt_Set
      // 
      this.bt_Set.Location = new System.Drawing.Point(538, 25);
      this.bt_Set.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.bt_Set.Name = "bt_Set";
      this.bt_Set.Size = new System.Drawing.Size(50, 31);
      this.bt_Set.TabIndex = 2;
      this.bt_Set.Text = "Set";
      this.bt_Set.UseVisualStyleBackColor = true;
      this.bt_Set.Click += new System.EventHandler(this.BT_Set_Click);
      // 
      // tb_Filter
      // 
      this.tb_Filter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tb_Filter.Location = new System.Drawing.Point(170, 25);
      this.tb_Filter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tb_Filter.Name = "tb_Filter";
      this.tb_Filter.Size = new System.Drawing.Size(344, 26);
      this.tb_Filter.TabIndex = 1;
      this.tb_Filter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TB_Filter_KeyUp);
      // 
      // l_RequestFilter
      // 
      this.l_RequestFilter.AutoSize = true;
      this.l_RequestFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_RequestFilter.Location = new System.Drawing.Point(34, 29);
      this.l_RequestFilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.l_RequestFilter.Name = "l_RequestFilter";
      this.l_RequestFilter.Size = new System.Drawing.Size(123, 20);
      this.l_RequestFilter.TabIndex = 0;
      this.l_RequestFilter.Text = "Request filter";
      // 
      // cms_DnsRequests
      // 
      this.cms_DnsRequests.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.cms_DnsRequests.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteEntryToolStripMenuItem,
            this.tsmi_Clear,
            this.copyHostNameToolStripMenuItem});
      this.cms_DnsRequests.Name = "cms_DNSRequests";
      this.cms_DnsRequests.Size = new System.Drawing.Size(216, 94);
      // 
      // deleteEntryToolStripMenuItem
      // 
      this.deleteEntryToolStripMenuItem.Name = "deleteEntryToolStripMenuItem";
      this.deleteEntryToolStripMenuItem.Size = new System.Drawing.Size(215, 30);
      this.deleteEntryToolStripMenuItem.Text = "Delete entry";
      this.deleteEntryToolStripMenuItem.Click += new System.EventHandler(this.DeleteEntryToolStripMenuItem_Click);
      // 
      // tsmi_Clear
      // 
      this.tsmi_Clear.Name = "tsmi_Clear";
      this.tsmi_Clear.Size = new System.Drawing.Size(215, 30);
      this.tsmi_Clear.Text = "Clear list";
      this.tsmi_Clear.Click += new System.EventHandler(this.TSMI_Clear_Click);
      // 
      // copyHostNameToolStripMenuItem
      // 
      this.copyHostNameToolStripMenuItem.Name = "copyHostNameToolStripMenuItem";
      this.copyHostNameToolStripMenuItem.Size = new System.Drawing.Size(215, 30);
      this.copyHostNameToolStripMenuItem.Text = "Copy host name";
      this.copyHostNameToolStripMenuItem.Click += new System.EventHandler(this.CopyHostNameToolStripMenuItem_Click);
      // 
      // t_GuiUpdate
      // 
      this.t_GuiUpdate.Interval = 500;
      this.t_GuiUpdate.Tick += new System.EventHandler(this.T_GUIUpdate_Tick);
      // 
      // Plugin_DnsRequests
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.Controls.Add(this.bt_Set);
      this.Controls.Add(this.tb_Filter);
      this.Controls.Add(this.l_RequestFilter);
      this.Controls.Add(this.dgv_DnsRequests);
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "Plugin_DnsRequests";
      this.Size = new System.Drawing.Size(1494, 566);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_DnsRequests)).EndInit();
      this.cms_DnsRequests.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_DnsRequests;
        private System.Windows.Forms.Button bt_Set;
        private System.Windows.Forms.TextBox tb_Filter;
        private System.Windows.Forms.Label l_RequestFilter;
        private System.Windows.Forms.ContextMenuStrip cms_DnsRequests;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Clear;
        private System.Windows.Forms.ToolStripMenuItem deleteEntryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyHostNameToolStripMenuItem;
        private System.Windows.Forms.Timer t_GuiUpdate;
    }
}
