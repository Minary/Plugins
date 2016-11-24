namespace Plugin.Main
{
    partial class PluginSSLStripUC
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
      this.L_HostName = new System.Windows.Forms.Label();
      this.BT_Add = new System.Windows.Forms.Button();
      this.TB_HostName = new System.Windows.Forms.TextBox();
      this.DGV_SSLStrippingTargets = new System.Windows.Forms.DataGridView();
      this.CMS_SSLStripRecords = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.deleteEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.clearListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.CB_ContentType = new System.Windows.Forms.ComboBox();
      this.L_ContentType = new System.Windows.Forms.Label();
      this.L_Tags = new System.Windows.Forms.Label();
      this.CB_HTMLTag = new System.Windows.Forms.ComboBox();
      ((System.ComponentModel.ISupportInitialize)(this.DGV_SSLStrippingTargets)).BeginInit();
      this.CMS_SSLStripRecords.SuspendLayout();
      this.SuspendLayout();
      // 
      // L_HostName
      // 
      this.L_HostName.AutoSize = true;
      this.L_HostName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.L_HostName.Location = new System.Drawing.Point(23, 19);
      this.L_HostName.Name = "L_HostName";
      this.L_HostName.Size = new System.Drawing.Size(67, 13);
      this.L_HostName.TabIndex = 0;
      this.L_HostName.Text = "Host name";
      // 
      // BT_Add
      // 
      this.BT_Add.Location = new System.Drawing.Point(811, 16);
      this.BT_Add.Name = "BT_Add";
      this.BT_Add.Size = new System.Drawing.Size(21, 21);
      this.BT_Add.TabIndex = 4;
      this.BT_Add.Text = "+";
      this.BT_Add.UseVisualStyleBackColor = true;
      this.BT_Add.Click += new System.EventHandler(this.BT_Add_Click);
      // 
      // TB_HostName
      // 
      this.TB_HostName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.TB_HostName.Location = new System.Drawing.Point(98, 16);
      this.TB_HostName.Name = "TB_HostName";
      this.TB_HostName.Size = new System.Drawing.Size(235, 20);
      this.TB_HostName.TabIndex = 1;
      this.TB_HostName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_Host_KeyDown);
      // 
      // DGV_SSLStrippingTargets
      // 
      this.DGV_SSLStrippingTargets.AllowUserToAddRows = false;
      this.DGV_SSLStrippingTargets.AllowUserToDeleteRows = false;
      this.DGV_SSLStrippingTargets.AllowUserToResizeColumns = false;
      this.DGV_SSLStrippingTargets.AllowUserToResizeRows = false;
      this.DGV_SSLStrippingTargets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.DGV_SSLStrippingTargets.Location = new System.Drawing.Point(17, 44);
      this.DGV_SSLStrippingTargets.MultiSelect = false;
      this.DGV_SSLStrippingTargets.Name = "DGV_SSLStrippingTargets";
      this.DGV_SSLStrippingTargets.RowHeadersVisible = false;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
      this.DGV_SSLStrippingTargets.RowsDefaultCellStyle = dataGridViewCellStyle1;
      this.DGV_SSLStrippingTargets.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.DGV_SSLStrippingTargets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.DGV_SSLStrippingTargets.Size = new System.Drawing.Size(830, 309);
      this.DGV_SSLStrippingTargets.TabIndex = 0;
      this.DGV_SSLStrippingTargets.TabStop = false;
      this.DGV_SSLStrippingTargets.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DGV_SSLStripRecords_MouseDown);
      this.DGV_SSLStrippingTargets.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DGV_SSLStripRecords_MouseUp);
      // 
      // CMS_SSLStripRecords
      // 
      this.CMS_SSLStripRecords.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteEntryToolStripMenuItem,
            this.clearListToolStripMenuItem});
      this.CMS_SSLStripRecords.Name = "CMS_SSLStripRecords";
      this.CMS_SSLStripRecords.Size = new System.Drawing.Size(138, 48);
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
      // CB_ContentType
      // 
      this.CB_ContentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.CB_ContentType.FormattingEnabled = true;
      this.CB_ContentType.Location = new System.Drawing.Point(441, 15);
      this.CB_ContentType.Name = "CB_ContentType";
      this.CB_ContentType.Size = new System.Drawing.Size(121, 21);
      this.CB_ContentType.TabIndex = 2;
      this.CB_ContentType.SelectedIndexChanged += new System.EventHandler(this.CB_ContentType_SelectedIndexChanged);
      // 
      // L_ContentType
      // 
      this.L_ContentType.AutoSize = true;
      this.L_ContentType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.L_ContentType.Location = new System.Drawing.Point(354, 19);
      this.L_ContentType.Name = "L_ContentType";
      this.L_ContentType.Size = new System.Drawing.Size(79, 13);
      this.L_ContentType.TabIndex = 0;
      this.L_ContentType.Text = "Content type";
      // 
      // L_Tags
      // 
      this.L_Tags.AutoSize = true;
      this.L_Tags.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.L_Tags.Location = new System.Drawing.Point(594, 19);
      this.L_Tags.Name = "L_Tags";
      this.L_Tags.Size = new System.Drawing.Size(63, 13);
      this.L_Tags.TabIndex = 0;
      this.L_Tags.Text = "HTML tag";
      // 
      // CB_HTMLTag
      // 
      this.CB_HTMLTag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.CB_HTMLTag.FormattingEnabled = true;
      this.CB_HTMLTag.Location = new System.Drawing.Point(664, 15);
      this.CB_HTMLTag.Name = "CB_HTMLTag";
      this.CB_HTMLTag.Size = new System.Drawing.Size(121, 21);
      this.CB_HTMLTag.TabIndex = 3;
      // 
      // PluginSSLStripUC
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.CB_HTMLTag);
      this.Controls.Add(this.L_Tags);
      this.Controls.Add(this.L_ContentType);
      this.Controls.Add(this.CB_ContentType);
      this.Controls.Add(this.DGV_SSLStrippingTargets);
      this.Controls.Add(this.TB_HostName);
      this.Controls.Add(this.BT_Add);
      this.Controls.Add(this.L_HostName);
      this.Name = "PluginSSLStripUC";
      this.Size = new System.Drawing.Size(996, 368);
      ((System.ComponentModel.ISupportInitialize)(this.DGV_SSLStrippingTargets)).EndInit();
      this.CMS_SSLStripRecords.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label L_HostName;
        private System.Windows.Forms.Button BT_Add;
        private System.Windows.Forms.TextBox TB_HostName;
        private System.Windows.Forms.DataGridView DGV_SSLStrippingTargets;
        private System.Windows.Forms.ContextMenuStrip CMS_SSLStripRecords;
        private System.Windows.Forms.ToolStripMenuItem deleteEntryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearListToolStripMenuItem;
        private System.Windows.Forms.ComboBox CB_ContentType;
        private System.Windows.Forms.Label L_ContentType;
        private System.Windows.Forms.Label L_Tags;
        private System.Windows.Forms.ComboBox CB_HTMLTag;
    }
}
