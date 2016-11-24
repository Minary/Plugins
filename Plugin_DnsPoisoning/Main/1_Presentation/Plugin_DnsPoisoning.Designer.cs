namespace Minary.Plugin.Main
{
  public partial class Plugin_DnsPoisoning
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      this.bt_Add = new System.Windows.Forms.Button();
      this.tb_Host = new System.Windows.Forms.TextBox();
      this.tb_Address = new System.Windows.Forms.TextBox();
      this.l_Host = new System.Windows.Forms.Label();
      this.l_IpAddress = new System.Windows.Forms.Label();
      this.cms_DnsPoison = new System.Windows.Forms.ContextMenuStrip();
      this.tsmi_Delete = new System.Windows.Forms.ToolStripMenuItem();
      this.tsmi_ClearList = new System.Windows.Forms.ToolStripMenuItem();
      this.dgv_Spoofing = new System.Windows.Forms.DataGridView();
      this.cms_DnsPoison.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgv_Spoofing)).BeginInit();
      this.SuspendLayout();
      // 
      // bt_Add
      // 
      this.bt_Add.Location = new System.Drawing.Point(452, 15);
      this.bt_Add.Margin = new System.Windows.Forms.Padding(0);
      this.bt_Add.Name = "bt_Add";
      this.bt_Add.Size = new System.Drawing.Size(20, 21);
      this.bt_Add.TabIndex = 3;
      this.bt_Add.Text = "+";
      this.bt_Add.UseVisualStyleBackColor = true;
      this.bt_Add.Click += new System.EventHandler(this.BT_Add_Click);
      // 
      // tb_Host
      // 
      this.tb_Host.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tb_Host.Location = new System.Drawing.Point(96, 16);
      this.tb_Host.Name = "tb_Host";
      this.tb_Host.Size = new System.Drawing.Size(154, 20);
      this.tb_Host.TabIndex = 1;
      this.tb_Host.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_Host_KeyDown);
      // 
      // tb_Address
      // 
      this.tb_Address.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tb_Address.Location = new System.Drawing.Point(341, 15);
      this.tb_Address.Name = "tb_Address";
      this.tb_Address.Size = new System.Drawing.Size(100, 20);
      this.tb_Address.TabIndex = 2;
      this.tb_Address.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_Address_KeyDown);
      // 
      // l_Host
      // 
      this.l_Host.AutoSize = true;
      this.l_Host.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_Host.Location = new System.Drawing.Point(23, 19);
      this.l_Host.Name = "l_Host";
      this.l_Host.Size = new System.Drawing.Size(67, 13);
      this.l_Host.TabIndex = 0;
      this.l_Host.Text = "Host name";
      // 
      // l_IpAddress
      // 
      this.l_IpAddress.AutoSize = true;
      this.l_IpAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_IpAddress.Location = new System.Drawing.Point(266, 19);
      this.l_IpAddress.Name = "l_IpAddress";
      this.l_IpAddress.Size = new System.Drawing.Size(67, 13);
      this.l_IpAddress.TabIndex = 0;
      this.l_IpAddress.Text = "IP address";
      // 
      // cms_DnsPoison
      // 
      this.cms_DnsPoison.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Delete,
            this.tsmi_ClearList});
      this.cms_DnsPoison.Name = "cms_DNSPoison";
      this.cms_DnsPoison.Size = new System.Drawing.Size(138, 48);
      // 
      // tsmi_Delete
      // 
      this.tsmi_Delete.Name = "tsmi_Delete";
      this.tsmi_Delete.Size = new System.Drawing.Size(137, 22);
      this.tsmi_Delete.Text = "Delete entry";
      this.tsmi_Delete.Click += new System.EventHandler(this.TSMI_Delete_Click);
      // 
      // tsmi_ClearList
      // 
      this.tsmi_ClearList.Name = "tsmi_ClearList";
      this.tsmi_ClearList.Size = new System.Drawing.Size(137, 22);
      this.tsmi_ClearList.Text = "Clear list";
      this.tsmi_ClearList.Click += new System.EventHandler(this.TSMI_Clear_Click);
      // 
      // dgv_Spoofing
      // 
      this.dgv_Spoofing.AllowUserToAddRows = false;
      this.dgv_Spoofing.AllowUserToDeleteRows = false;
      this.dgv_Spoofing.AllowUserToResizeColumns = false;
      this.dgv_Spoofing.AllowUserToResizeRows = false;
      this.dgv_Spoofing.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dgv_Spoofing.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.dgv_Spoofing.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
      this.dgv_Spoofing.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgv_Spoofing.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dgv_Spoofing.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv_Spoofing.Location = new System.Drawing.Point(17, 44);
      this.dgv_Spoofing.MultiSelect = false;
      this.dgv_Spoofing.Name = "dgv_Spoofing";
      this.dgv_Spoofing.RowHeadersVisible = false;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dgv_Spoofing.RowsDefaultCellStyle = dataGridViewCellStyle2;
      this.dgv_Spoofing.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dgv_Spoofing.RowTemplate.Height = 20;
      this.dgv_Spoofing.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.dgv_Spoofing.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv_Spoofing.Size = new System.Drawing.Size(933, 324);
      this.dgv_Spoofing.TabIndex = 4;
      this.dgv_Spoofing.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DGV_Spoofing_MouseDown);
      this.dgv_Spoofing.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DGV_Spoofing_MouseUp);
      // 
      // Plugin_DnsPoisoning
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Transparent;
      this.Controls.Add(this.dgv_Spoofing);
      this.Controls.Add(this.l_IpAddress);
      this.Controls.Add(this.l_Host);
      this.Controls.Add(this.tb_Address);
      this.Controls.Add(this.tb_Host);
      this.Controls.Add(this.bt_Add);
      this.Name = "Plugin_DnsPoisoning";
      this.Size = new System.Drawing.Size(996, 379);
      this.Load += new System.EventHandler(this.PluginDNSPoisonUC_Load);
      this.cms_DnsPoison.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_Spoofing)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_Add;
        private System.Windows.Forms.TextBox tb_Host;
        private System.Windows.Forms.TextBox tb_Address;
        private System.Windows.Forms.Label l_Host;
        private System.Windows.Forms.Label l_IpAddress;
        private System.Windows.Forms.ContextMenuStrip cms_DnsPoison;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Delete;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ClearList;
        private System.Windows.Forms.DataGridView dgv_Spoofing;
    }
}
