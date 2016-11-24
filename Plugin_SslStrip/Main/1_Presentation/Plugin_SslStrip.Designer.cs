namespace Minary.Plugin.Main
{
  public partial class Plugin_SslStrip
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
      this.l_HostName = new System.Windows.Forms.Label();
      this.bt_Add = new System.Windows.Forms.Button();
      this.tb_HostName = new System.Windows.Forms.TextBox();
      this.dgv_SslStrippingTargets = new System.Windows.Forms.DataGridView();
      this.cms_SslStripRecords = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.deleteEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.clearListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.cb_ContentType = new System.Windows.Forms.ComboBox();
      this.l_ContentType = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.dgv_SslStrippingTargets)).BeginInit();
      this.cms_SslStripRecords.SuspendLayout();
      this.SuspendLayout();
      // 
      // l_HostName
      // 
      this.l_HostName.AutoSize = true;
      this.l_HostName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_HostName.Location = new System.Drawing.Point(23, 19);
      this.l_HostName.Name = "l_HostName";
      this.l_HostName.Size = new System.Drawing.Size(67, 13);
      this.l_HostName.TabIndex = 0;
      this.l_HostName.Text = "Host name";
      // 
      // bt_Add
      // 
      this.bt_Add.Location = new System.Drawing.Point(751, 14);
      this.bt_Add.Name = "bt_Add";
      this.bt_Add.Size = new System.Drawing.Size(23, 21);
      this.bt_Add.TabIndex = 3;
      this.bt_Add.Text = "+";
      this.bt_Add.UseVisualStyleBackColor = true;
      this.bt_Add.Click += new System.EventHandler(this.BT_Add_Click);
      // 
      // tb_HostName
      // 
      this.tb_HostName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tb_HostName.Location = new System.Drawing.Point(98, 16);
      this.tb_HostName.Name = "tb_HostName";
      this.tb_HostName.Size = new System.Drawing.Size(336, 20);
      this.tb_HostName.TabIndex = 1;
      this.tb_HostName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_Host_KeyDown);
      // 
      // dgv_SslStrippingTargets
      // 
      this.dgv_SslStrippingTargets.AllowUserToAddRows = false;
      this.dgv_SslStrippingTargets.AllowUserToDeleteRows = false;
      this.dgv_SslStrippingTargets.AllowUserToResizeColumns = false;
      this.dgv_SslStrippingTargets.AllowUserToResizeRows = false;
      this.dgv_SslStrippingTargets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dgv_SslStrippingTargets.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.dgv_SslStrippingTargets.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
      this.dgv_SslStrippingTargets.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
      this.dgv_SslStrippingTargets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv_SslStrippingTargets.Location = new System.Drawing.Point(17, 44);
      this.dgv_SslStrippingTargets.MultiSelect = false;
      this.dgv_SslStrippingTargets.Name = "dgv_SslStrippingTargets";
      this.dgv_SslStrippingTargets.RowHeadersVisible = false;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
      this.dgv_SslStrippingTargets.RowsDefaultCellStyle = dataGridViewCellStyle1;
      this.dgv_SslStrippingTargets.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.dgv_SslStrippingTargets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv_SslStrippingTargets.Size = new System.Drawing.Size(933, 313);
      this.dgv_SslStrippingTargets.TabIndex = 0;
      this.dgv_SslStrippingTargets.TabStop = false;
      this.dgv_SslStrippingTargets.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DGV_SslStripRecords_MouseDown);
      this.dgv_SslStrippingTargets.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DGV_SslStripRecords_MouseUp);
      // 
      // cms_SslStripRecords
      // 
      this.cms_SslStripRecords.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteEntryToolStripMenuItem,
            this.clearListToolStripMenuItem});
      this.cms_SslStripRecords.Name = "cms_SslStripRecords";
      this.cms_SslStripRecords.Size = new System.Drawing.Size(138, 48);
      // 
      // deleteEntryToolStripMenuItem
      // 
      this.deleteEntryToolStripMenuItem.Name = "deleteEntryToolStripMenuItem";
      this.deleteEntryToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
      this.deleteEntryToolStripMenuItem.Text = "Delete entry";
      this.deleteEntryToolStripMenuItem.Click += new System.EventHandler(this.DeleteEntryToolStripMenuItem_Click);
      // 
      // clearListToolStripMenuItem
      // 
      this.clearListToolStripMenuItem.Name = "clearListToolStripMenuItem";
      this.clearListToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
      this.clearListToolStripMenuItem.Text = "Clear list";
      this.clearListToolStripMenuItem.Click += new System.EventHandler(this.TSMI_Clear_Click);
      // 
      // cb_ContentType
      // 
      this.cb_ContentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cb_ContentType.FormattingEnabled = true;
      this.cb_ContentType.Location = new System.Drawing.Point(548, 15);
      this.cb_ContentType.Name = "cb_ContentType";
      this.cb_ContentType.Size = new System.Drawing.Size(150, 21);
      this.cb_ContentType.TabIndex = 2;
      this.cb_ContentType.SelectedIndexChanged += new System.EventHandler(this.CB_ContentType_SelectedIndexChanged);
      // 
      // l_ContentType
      // 
      this.l_ContentType.AutoSize = true;
      this.l_ContentType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_ContentType.Location = new System.Drawing.Point(461, 19);
      this.l_ContentType.Name = "l_ContentType";
      this.l_ContentType.Size = new System.Drawing.Size(79, 13);
      this.l_ContentType.TabIndex = 0;
      this.l_ContentType.Text = "Content type";
      // 
      // PluginSslStripUC
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Transparent;
      this.Controls.Add(this.l_ContentType);
      this.Controls.Add(this.cb_ContentType);
      this.Controls.Add(this.dgv_SslStrippingTargets);
      this.Controls.Add(this.tb_HostName);
      this.Controls.Add(this.bt_Add);
      this.Controls.Add(this.l_HostName);
      this.Name = "PluginSslStripUC";
      this.Size = new System.Drawing.Size(996, 368);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_SslStrippingTargets)).EndInit();
      this.cms_SslStripRecords.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label l_HostName;
        private System.Windows.Forms.Button bt_Add;
        private System.Windows.Forms.TextBox tb_HostName;
        private System.Windows.Forms.DataGridView dgv_SslStrippingTargets;
        private System.Windows.Forms.ContextMenuStrip cms_SslStripRecords;
        private System.Windows.Forms.ToolStripMenuItem deleteEntryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearListToolStripMenuItem;
        private System.Windows.Forms.ComboBox cb_ContentType;
        private System.Windows.Forms.Label l_ContentType;
    }
}
