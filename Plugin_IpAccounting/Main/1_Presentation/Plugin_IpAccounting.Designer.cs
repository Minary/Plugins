namespace Minary.Plugin.Main
{
  public partial class Plugin_IpAccounting
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
      this.dgv_TrafficData = new System.Windows.Forms.DataGridView();
      this.cms_DataGrid_RightMouseButton = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.tsmi_clear = new System.Windows.Forms.ToolStripMenuItem();
      this.rb_Service = new System.Windows.Forms.RadioButton();
      this.rb_LocalIP = new System.Windows.Forms.RadioButton();
      this.rb_RemoteIP = new System.Windows.Forms.RadioButton();
      this.l_RadioButtonTitle = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.dgv_TrafficData)).BeginInit();
      this.cms_DataGrid_RightMouseButton.SuspendLayout();
      this.SuspendLayout();
      // 
      // dgv_TrafficData
      // 
      this.dgv_TrafficData.AllowUserToAddRows = false;
      this.dgv_TrafficData.AllowUserToDeleteRows = false;
      this.dgv_TrafficData.AllowUserToResizeColumns = false;
      this.dgv_TrafficData.AllowUserToResizeRows = false;
      this.dgv_TrafficData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dgv_TrafficData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.dgv_TrafficData.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
      this.dgv_TrafficData.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgv_TrafficData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dgv_TrafficData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv_TrafficData.Location = new System.Drawing.Point(17, 44);
      this.dgv_TrafficData.MultiSelect = false;
      this.dgv_TrafficData.Name = "dgv_TrafficData";
      this.dgv_TrafficData.ReadOnly = true;
      this.dgv_TrafficData.RowHeadersVisible = false;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dgv_TrafficData.RowsDefaultCellStyle = dataGridViewCellStyle2;
      this.dgv_TrafficData.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dgv_TrafficData.RowTemplate.Height = 20;
      this.dgv_TrafficData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.dgv_TrafficData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv_TrafficData.Size = new System.Drawing.Size(933, 313);
      this.dgv_TrafficData.TabIndex = 0;
      this.dgv_TrafficData.TabStop = false;
      this.dgv_TrafficData.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DGV_TrafficData_MouseUp);
      // 
      // cms_DataGrid_RightMouseButton
      // 
      this.cms_DataGrid_RightMouseButton.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_clear});
      this.cms_DataGrid_RightMouseButton.Name = "cms_DataGrid_RightMouseButton";
      this.cms_DataGrid_RightMouseButton.Size = new System.Drawing.Size(120, 26);
      // 
      // tsmi_clear
      // 
      this.tsmi_clear.Name = "tsmi_clear";
      this.tsmi_clear.Size = new System.Drawing.Size(119, 22);
      this.tsmi_clear.Text = "Clear list";
      this.tsmi_clear.Click += new System.EventHandler(this.ClearToolStripMenuItem_Click);
      // 
      // rb_Service
      // 
      this.rb_Service.AutoSize = true;
      this.rb_Service.Checked = true;
      this.rb_Service.Location = new System.Drawing.Point(178, 16);
      this.rb_Service.Name = "rb_Service";
      this.rb_Service.Size = new System.Drawing.Size(61, 17);
      this.rb_Service.TabIndex = 1;
      this.rb_Service.TabStop = true;
      this.rb_Service.Text = "Service";
      this.rb_Service.UseVisualStyleBackColor = true;
      this.rb_Service.Click += new System.EventHandler(this.RB_Service_Click);
      // 
      // rb_LocalIP
      // 
      this.rb_LocalIP.AutoSize = true;
      this.rb_LocalIP.Location = new System.Drawing.Point(264, 16);
      this.rb_LocalIP.Name = "rb_LocalIP";
      this.rb_LocalIP.Size = new System.Drawing.Size(64, 17);
      this.rb_LocalIP.TabIndex = 2;
      this.rb_LocalIP.Text = "Local IP";
      this.rb_LocalIP.UseVisualStyleBackColor = true;
      this.rb_LocalIP.Click += new System.EventHandler(this.RB_LocalIP_Click);
      // 
      // rb_RemoteIP
      // 
      this.rb_RemoteIP.AutoSize = true;
      this.rb_RemoteIP.Location = new System.Drawing.Point(349, 16);
      this.rb_RemoteIP.Name = "rb_RemoteIP";
      this.rb_RemoteIP.Size = new System.Drawing.Size(75, 17);
      this.rb_RemoteIP.TabIndex = 3;
      this.rb_RemoteIP.Text = "Remote IP";
      this.rb_RemoteIP.UseVisualStyleBackColor = true;
      this.rb_RemoteIP.Click += new System.EventHandler(this.RB_RemoteIP_Click);
      // 
      // l_RadioButtonTitle
      // 
      this.l_RadioButtonTitle.AutoSize = true;
      this.l_RadioButtonTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_RadioButtonTitle.Location = new System.Drawing.Point(29, 19);
      this.l_RadioButtonTitle.Name = "l_RadioButtonTitle";
      this.l_RadioButtonTitle.Size = new System.Drawing.Size(127, 13);
      this.l_RadioButtonTitle.TabIndex = 4;
      this.l_RadioButtonTitle.Text = "Accounting based on";
      // 
      // PluginIpAccountingUC
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Transparent;
      this.Controls.Add(this.l_RadioButtonTitle);
      this.Controls.Add(this.rb_RemoteIP);
      this.Controls.Add(this.rb_LocalIP);
      this.Controls.Add(this.rb_Service);
      this.Controls.Add(this.dgv_TrafficData);
      this.Name = "PluginIpAccountingUC";
      this.Size = new System.Drawing.Size(996, 368);
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PluginIpAccountingUC_MouseDown);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_TrafficData)).EndInit();
      this.cms_DataGrid_RightMouseButton.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_TrafficData;
        private System.Windows.Forms.ContextMenuStrip cms_DataGrid_RightMouseButton;
        private System.Windows.Forms.ToolStripMenuItem tsmi_clear;
        private System.Windows.Forms.RadioButton rb_Service;
        private System.Windows.Forms.RadioButton rb_LocalIP;
        private System.Windows.Forms.RadioButton rb_RemoteIP;
        private System.Windows.Forms.Label l_RadioButtonTitle;
    }
}
