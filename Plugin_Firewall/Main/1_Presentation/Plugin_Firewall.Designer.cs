namespace Minary.Plugin.Main
{
  public partial class Plugin_Firewall
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
      this.dgv_FWRules = new System.Windows.Forms.DataGridView();
      this.l_SrcIp = new System.Windows.Forms.Label();
      this.bt_Add = new System.Windows.Forms.Button();
      this.l_DstIp = new System.Windows.Forms.Label();
      this.tb_SrcPortLower = new System.Windows.Forms.TextBox();
      this.l_SrcPort = new System.Windows.Forms.Label();
      this.l_Dash = new System.Windows.Forms.Label();
      this.tb_SrcPortUpper = new System.Windows.Forms.TextBox();
      this.tb_DstPortUpper = new System.Windows.Forms.TextBox();
      this.l_DstPort = new System.Windows.Forms.Label();
      this.tb_DstPortLower = new System.Windows.Forms.TextBox();
      this.cb_Protocol = new System.Windows.Forms.ComboBox();
      this.cms_DataGrid_RightMouseButton = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.deleteRuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.deleteAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.cb_SrcIP = new System.Windows.Forms.ComboBox();
      this.cb_DstIP = new System.Windows.Forms.ComboBox();
      ((System.ComponentModel.ISupportInitialize)(this.dgv_FWRules)).BeginInit();
      this.cms_DataGrid_RightMouseButton.SuspendLayout();
      this.SuspendLayout();
      // 
      // dgv_FWRules
      // 
      this.dgv_FWRules.AllowUserToAddRows = false;
      this.dgv_FWRules.AllowUserToDeleteRows = false;
      this.dgv_FWRules.AllowUserToResizeColumns = false;
      this.dgv_FWRules.AllowUserToResizeRows = false;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dgv_FWRules.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
      this.dgv_FWRules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dgv_FWRules.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.dgv_FWRules.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
      this.dgv_FWRules.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
      this.dgv_FWRules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv_FWRules.Location = new System.Drawing.Point(26, 68);
      this.dgv_FWRules.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.dgv_FWRules.MultiSelect = false;
      this.dgv_FWRules.Name = "dgv_FWRules";
      this.dgv_FWRules.ReadOnly = true;
      this.dgv_FWRules.RowHeadersVisible = false;
      this.dgv_FWRules.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dgv_FWRules.RowTemplate.Height = 20;
      this.dgv_FWRules.RowTemplate.ReadOnly = true;
      this.dgv_FWRules.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.dgv_FWRules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv_FWRules.Size = new System.Drawing.Size(1400, 482);
      this.dgv_FWRules.TabIndex = 9;
      this.dgv_FWRules.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DGV_FirewallRules_MouseUp);
      // 
      // l_SrcIp
      // 
      this.l_SrcIp.AutoSize = true;
      this.l_SrcIp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_SrcIp.Location = new System.Drawing.Point(146, 28);
      this.l_SrcIp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.l_SrcIp.Name = "l_SrcIp";
      this.l_SrcIp.Size = new System.Drawing.Size(61, 20);
      this.l_SrcIp.TabIndex = 0;
      this.l_SrcIp.Text = "Src IP";
      // 
      // bt_Add
      // 
      this.bt_Add.Location = new System.Drawing.Point(951, 23);
      this.bt_Add.Margin = new System.Windows.Forms.Padding(0);
      this.bt_Add.Name = "bt_Add";
      this.bt_Add.Size = new System.Drawing.Size(30, 32);
      this.bt_Add.TabIndex = 8;
      this.bt_Add.Text = "+";
      this.bt_Add.UseVisualStyleBackColor = true;
      this.bt_Add.Click += new System.EventHandler(this.BT_Add_Click);
      // 
      // l_DstIp
      // 
      this.l_DstIp.AutoSize = true;
      this.l_DstIp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_DstIp.Location = new System.Drawing.Point(567, 28);
      this.l_DstIp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.l_DstIp.Name = "l_DstIp";
      this.l_DstIp.Size = new System.Drawing.Size(62, 20);
      this.l_DstIp.TabIndex = 0;
      this.l_DstIp.Text = "Dst IP";
      // 
      // tb_SrcPortLower
      // 
      this.tb_SrcPortLower.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tb_SrcPortLower.Location = new System.Drawing.Point(386, 22);
      this.tb_SrcPortLower.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tb_SrcPortLower.Name = "tb_SrcPortLower";
      this.tb_SrcPortLower.Size = new System.Drawing.Size(55, 26);
      this.tb_SrcPortLower.TabIndex = 3;
      // 
      // l_SrcPort
      // 
      this.l_SrcPort.AutoSize = true;
      this.l_SrcPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_SrcPort.Location = new System.Drawing.Point(368, 26);
      this.l_SrcPort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.l_SrcPort.Name = "l_SrcPort";
      this.l_SrcPort.Size = new System.Drawing.Size(15, 20);
      this.l_SrcPort.TabIndex = 0;
      this.l_SrcPort.Text = ":";
      // 
      // l_Dash
      // 
      this.l_Dash.AutoSize = true;
      this.l_Dash.Location = new System.Drawing.Point(868, 28);
      this.l_Dash.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.l_Dash.Name = "l_Dash";
      this.l_Dash.Size = new System.Drawing.Size(14, 20);
      this.l_Dash.TabIndex = 0;
      this.l_Dash.Text = "-";
      // 
      // tb_SrcPortUpper
      // 
      this.tb_SrcPortUpper.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tb_SrcPortUpper.Location = new System.Drawing.Point(459, 22);
      this.tb_SrcPortUpper.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tb_SrcPortUpper.Name = "tb_SrcPortUpper";
      this.tb_SrcPortUpper.Size = new System.Drawing.Size(55, 26);
      this.tb_SrcPortUpper.TabIndex = 4;
      // 
      // tb_DstPortUpper
      // 
      this.tb_DstPortUpper.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tb_DstPortUpper.Location = new System.Drawing.Point(885, 23);
      this.tb_DstPortUpper.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tb_DstPortUpper.Name = "tb_DstPortUpper";
      this.tb_DstPortUpper.Size = new System.Drawing.Size(55, 26);
      this.tb_DstPortUpper.TabIndex = 7;
      // 
      // l_DstPort
      // 
      this.l_DstPort.AutoSize = true;
      this.l_DstPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_DstPort.Location = new System.Drawing.Point(792, 28);
      this.l_DstPort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.l_DstPort.Name = "l_DstPort";
      this.l_DstPort.Size = new System.Drawing.Size(15, 20);
      this.l_DstPort.TabIndex = 0;
      this.l_DstPort.Text = ":";
      // 
      // tb_DstPortLower
      // 
      this.tb_DstPortLower.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tb_DstPortLower.Location = new System.Drawing.Point(810, 23);
      this.tb_DstPortLower.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tb_DstPortLower.Name = "tb_DstPortLower";
      this.tb_DstPortLower.Size = new System.Drawing.Size(55, 26);
      this.tb_DstPortLower.TabIndex = 6;
      // 
      // cb_Protocol
      // 
      this.cb_Protocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cb_Protocol.FormattingEnabled = true;
      this.cb_Protocol.Location = new System.Drawing.Point(26, 20);
      this.cb_Protocol.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.cb_Protocol.Name = "cb_Protocol";
      this.cb_Protocol.Size = new System.Drawing.Size(85, 28);
      this.cb_Protocol.TabIndex = 1;
      // 
      // cms_DataGrid_RightMouseButton
      // 
      this.cms_DataGrid_RightMouseButton.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.cms_DataGrid_RightMouseButton.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteRuleToolStripMenuItem,
            this.deleteAllToolStripMenuItem});
      this.cms_DataGrid_RightMouseButton.Name = "cms_DataGrid_RightMouseButton";
      this.cms_DataGrid_RightMouseButton.Size = new System.Drawing.Size(169, 64);
      // 
      // deleteRuleToolStripMenuItem
      // 
      this.deleteRuleToolStripMenuItem.Name = "deleteRuleToolStripMenuItem";
      this.deleteRuleToolStripMenuItem.Size = new System.Drawing.Size(168, 30);
      this.deleteRuleToolStripMenuItem.Text = "Delete rule";
      this.deleteRuleToolStripMenuItem.Click += new System.EventHandler(this.DeleteRuleToolStripMenuItem_Click);
      // 
      // deleteAllToolStripMenuItem
      // 
      this.deleteAllToolStripMenuItem.Name = "deleteAllToolStripMenuItem";
      this.deleteAllToolStripMenuItem.Size = new System.Drawing.Size(168, 30);
      this.deleteAllToolStripMenuItem.Text = "Delete all";
      this.deleteAllToolStripMenuItem.Click += new System.EventHandler(this.DeleteAllToolStripMenuItem_Click);
      // 
      // cb_SrcIP
      // 
      this.cb_SrcIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cb_SrcIP.FormattingEnabled = true;
      this.cb_SrcIP.Location = new System.Drawing.Point(212, 22);
      this.cb_SrcIP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.cb_SrcIP.Name = "cb_SrcIP";
      this.cb_SrcIP.Size = new System.Drawing.Size(152, 28);
      this.cb_SrcIP.TabIndex = 2;
      // 
      // cb_DstIP
      // 
      this.cb_DstIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cb_DstIP.FormattingEnabled = true;
      this.cb_DstIP.Location = new System.Drawing.Point(636, 22);
      this.cb_DstIP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.cb_DstIP.Name = "cb_DstIP";
      this.cb_DstIP.Size = new System.Drawing.Size(152, 28);
      this.cb_DstIP.TabIndex = 5;
      // 
      // Plugin_Firewall
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.Controls.Add(this.cb_DstIP);
      this.Controls.Add(this.cb_SrcIP);
      this.Controls.Add(this.cb_Protocol);
      this.Controls.Add(this.tb_DstPortUpper);
      this.Controls.Add(this.l_DstPort);
      this.Controls.Add(this.tb_DstPortLower);
      this.Controls.Add(this.tb_SrcPortUpper);
      this.Controls.Add(this.l_Dash);
      this.Controls.Add(this.l_SrcPort);
      this.Controls.Add(this.tb_SrcPortLower);
      this.Controls.Add(this.l_DstIp);
      this.Controls.Add(this.bt_Add);
      this.Controls.Add(this.l_SrcIp);
      this.Controls.Add(this.dgv_FWRules);
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "Plugin_Firewall";
      this.Size = new System.Drawing.Size(1494, 566);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_FWRules)).EndInit();
      this.cms_DataGrid_RightMouseButton.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView dgv_FWRules;
    private System.Windows.Forms.Label l_SrcIp;
    private System.Windows.Forms.Button bt_Add;
    private System.Windows.Forms.Label l_DstIp;
    private System.Windows.Forms.TextBox tb_SrcPortLower;
    private System.Windows.Forms.Label l_SrcPort;
    private System.Windows.Forms.Label l_Dash;
    private System.Windows.Forms.TextBox tb_SrcPortUpper;
    private System.Windows.Forms.TextBox tb_DstPortUpper;
   // private System.Windows.Forms.Label l_Dash2;
    private System.Windows.Forms.Label l_DstPort;
    private System.Windows.Forms.TextBox tb_DstPortLower;
    private System.Windows.Forms.ComboBox cb_Protocol;
    private System.Windows.Forms.ContextMenuStrip cms_DataGrid_RightMouseButton;
    private System.Windows.Forms.ToolStripMenuItem deleteRuleToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem deleteAllToolStripMenuItem;
    private System.Windows.Forms.ComboBox cb_SrcIP;
    private System.Windows.Forms.ComboBox cb_DstIP;
  }
}
