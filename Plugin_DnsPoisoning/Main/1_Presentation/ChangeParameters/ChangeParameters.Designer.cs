namespace Minary.Plugin.Main
{
  partial class ChangeParameters
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.GB_Parameters = new System.Windows.Forms.GroupBox();
      this.cb_Type = new System.Windows.Forms.ComboBox();
      this.tb_CName = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.l_ResponseType = new System.Windows.Forms.Label();
      this.tb_IpAddress = new System.Windows.Forms.TextBox();
      this.tb_ttl = new System.Windows.Forms.TextBox();
      this.l_SpoofedIpAddress = new System.Windows.Forms.Label();
      this.L_DnsCacheTtl = new System.Windows.Forms.Label();
      this.BT_Save = new System.Windows.Forms.Button();
      this.BT_Cancel = new System.Windows.Forms.Button();
      this.cms_Cname = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.tsmi_useHostIP = new System.Windows.Forms.ToolStripMenuItem();
      this.GB_Parameters.SuspendLayout();
      this.cms_Cname.SuspendLayout();
      this.SuspendLayout();
      // 
      // GB_Parameters
      // 
      this.GB_Parameters.Controls.Add(this.cb_Type);
      this.GB_Parameters.Controls.Add(this.tb_CName);
      this.GB_Parameters.Controls.Add(this.label1);
      this.GB_Parameters.Controls.Add(this.l_ResponseType);
      this.GB_Parameters.Controls.Add(this.tb_IpAddress);
      this.GB_Parameters.Controls.Add(this.tb_ttl);
      this.GB_Parameters.Controls.Add(this.l_SpoofedIpAddress);
      this.GB_Parameters.Controls.Add(this.L_DnsCacheTtl);
      this.GB_Parameters.Location = new System.Drawing.Point(13, 13);
      this.GB_Parameters.Name = "GB_Parameters";
      this.GB_Parameters.Size = new System.Drawing.Size(290, 153);
      this.GB_Parameters.TabIndex = 0;
      this.GB_Parameters.TabStop = false;
      // 
      // cb_Type
      // 
      this.cb_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cb_Type.FormattingEnabled = true;
      this.cb_Type.Items.AddRange(new object[] {
            "A",
            "CNAME"});
      this.cb_Type.Location = new System.Drawing.Point(123, 83);
      this.cb_Type.Name = "cb_Type";
      this.cb_Type.Size = new System.Drawing.Size(151, 21);
      this.cb_Type.TabIndex = 4;
      // 
      // tb_CName
      // 
      this.tb_CName.ContextMenuStrip = this.cms_Cname;
      this.tb_CName.Location = new System.Drawing.Point(123, 117);
      this.tb_CName.Name = "tb_CName";
      this.tb_CName.Size = new System.Drawing.Size(151, 20);
      this.tb_CName.TabIndex = 5;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 120);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(42, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "CName";
      // 
      // l_ResponseType
      // 
      this.l_ResponseType.AutoSize = true;
      this.l_ResponseType.Location = new System.Drawing.Point(6, 84);
      this.l_ResponseType.Name = "l_ResponseType";
      this.l_ResponseType.Size = new System.Drawing.Size(78, 13);
      this.l_ResponseType.TabIndex = 0;
      this.l_ResponseType.Text = "Response type";
      // 
      // tb_IpAddress
      // 
      this.tb_IpAddress.Location = new System.Drawing.Point(123, 17);
      this.tb_IpAddress.Name = "tb_IpAddress";
      this.tb_IpAddress.Size = new System.Drawing.Size(151, 20);
      this.tb_IpAddress.TabIndex = 2;
      // 
      // tb_ttl
      // 
      this.tb_ttl.Location = new System.Drawing.Point(123, 48);
      this.tb_ttl.Name = "tb_ttl";
      this.tb_ttl.Size = new System.Drawing.Size(151, 20);
      this.tb_ttl.TabIndex = 3;
      // 
      // l_SpoofedIpAddress
      // 
      this.l_SpoofedIpAddress.AutoSize = true;
      this.l_SpoofedIpAddress.Location = new System.Drawing.Point(6, 20);
      this.l_SpoofedIpAddress.Name = "l_SpoofedIpAddress";
      this.l_SpoofedIpAddress.Size = new System.Drawing.Size(100, 13);
      this.l_SpoofedIpAddress.TabIndex = 0;
      this.l_SpoofedIpAddress.Text = "Spoofed IP address";
      // 
      // L_DnsCacheTtl
      // 
      this.L_DnsCacheTtl.AutoSize = true;
      this.L_DnsCacheTtl.Location = new System.Drawing.Point(6, 51);
      this.L_DnsCacheTtl.Name = "L_DnsCacheTtl";
      this.L_DnsCacheTtl.Size = new System.Drawing.Size(86, 13);
      this.L_DnsCacheTtl.TabIndex = 0;
      this.L_DnsCacheTtl.Text = "DNS cache TTL";
      // 
      // BT_Save
      // 
      this.BT_Save.Location = new System.Drawing.Point(212, 176);
      this.BT_Save.Name = "BT_Save";
      this.BT_Save.Size = new System.Drawing.Size(75, 23);
      this.BT_Save.TabIndex = 6;
      this.BT_Save.Text = "Save";
      this.BT_Save.UseVisualStyleBackColor = true;
      this.BT_Save.Click += new System.EventHandler(this.BT_Save_Click);
      // 
      // BT_Cancel
      // 
      this.BT_Cancel.Location = new System.Drawing.Point(104, 176);
      this.BT_Cancel.Name = "BT_Cancel";
      this.BT_Cancel.Size = new System.Drawing.Size(75, 23);
      this.BT_Cancel.TabIndex = 7;
      this.BT_Cancel.Text = "Cancel";
      this.BT_Cancel.UseVisualStyleBackColor = true;
      this.BT_Cancel.Click += new System.EventHandler(this.BT_Cancel_Click);
      // 
      // cms_Cname
      // 
      this.cms_Cname.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.cms_Cname.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_useHostIP});
      this.cms_Cname.Name = "cms_Cname";
      this.cms_Cname.Size = new System.Drawing.Size(260, 36);
      // 
      // tsmi_useHostIP
      // 
      this.tsmi_useHostIP.Name = "tsmi_useHostIP";
      this.tsmi_useHostIP.Size = new System.Drawing.Size(259, 32);
      this.tsmi_useHostIP.Text = "CNAME to spoofed IP";
      this.tsmi_useHostIP.Click += new System.EventHandler(this.TSMI_UseHostIP_Click);
      // 
      // ChangeParameters
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(315, 211);
      this.Controls.Add(this.BT_Cancel);
      this.Controls.Add(this.BT_Save);
      this.Controls.Add(this.GB_Parameters);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "ChangeParameters";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.Text = "Change parameters";
      this.GB_Parameters.ResumeLayout(false);
      this.GB_Parameters.PerformLayout();
      this.cms_Cname.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox GB_Parameters;
    private System.Windows.Forms.Button BT_Save;
    private System.Windows.Forms.Button BT_Cancel;
    private System.Windows.Forms.TextBox tb_IpAddress;
    private System.Windows.Forms.TextBox tb_ttl;
    private System.Windows.Forms.Label l_SpoofedIpAddress;
    private System.Windows.Forms.Label L_DnsCacheTtl;
    private System.Windows.Forms.Label l_ResponseType;
    private System.Windows.Forms.ComboBox cb_Type;
    private System.Windows.Forms.TextBox tb_CName;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ContextMenuStrip cms_Cname;
    private System.Windows.Forms.ToolStripMenuItem tsmi_useHostIP;
  }
}