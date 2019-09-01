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
      this.GB_Parameters = new System.Windows.Forms.GroupBox();
      this.tb_IpAddress = new System.Windows.Forms.TextBox();
      this.tb_ttl = new System.Windows.Forms.TextBox();
      this.tb_Hostname = new System.Windows.Forms.TextBox();
      this.l_SpoofedIpAddress = new System.Windows.Forms.Label();
      this.L_DnsCacheTtl = new System.Windows.Forms.Label();
      this.Hostname = new System.Windows.Forms.Label();
      this.BT_Save = new System.Windows.Forms.Button();
      this.BT_Cancel = new System.Windows.Forms.Button();
      this.GB_Parameters.SuspendLayout();
      this.SuspendLayout();
      // 
      // GB_Parameters
      // 
      this.GB_Parameters.Controls.Add(this.tb_IpAddress);
      this.GB_Parameters.Controls.Add(this.tb_ttl);
      this.GB_Parameters.Controls.Add(this.tb_Hostname);
      this.GB_Parameters.Controls.Add(this.l_SpoofedIpAddress);
      this.GB_Parameters.Controls.Add(this.L_DnsCacheTtl);
      this.GB_Parameters.Controls.Add(this.Hostname);
      this.GB_Parameters.Location = new System.Drawing.Point(13, 13);
      this.GB_Parameters.Name = "GB_Parameters";
      this.GB_Parameters.Size = new System.Drawing.Size(290, 113);
      this.GB_Parameters.TabIndex = 0;
      this.GB_Parameters.TabStop = false;
      // 
      // tb_IpAddress
      // 
      this.tb_IpAddress.Location = new System.Drawing.Point(123, 46);
      this.tb_IpAddress.Name = "tb_IpAddress";
      this.tb_IpAddress.Size = new System.Drawing.Size(137, 20);
      this.tb_IpAddress.TabIndex = 2;
      // 
      // tb_ttl
      // 
      this.tb_ttl.Location = new System.Drawing.Point(123, 77);
      this.tb_ttl.Name = "tb_ttl";
      this.tb_ttl.Size = new System.Drawing.Size(137, 20);
      this.tb_ttl.TabIndex = 3;
      // 
      // tb_Hostname
      // 
      this.tb_Hostname.Location = new System.Drawing.Point(123, 17);
      this.tb_Hostname.Name = "tb_Hostname";
      this.tb_Hostname.Size = new System.Drawing.Size(137, 20);
      this.tb_Hostname.TabIndex = 1;
      // 
      // l_SpoofedIpAddress
      // 
      this.l_SpoofedIpAddress.AutoSize = true;
      this.l_SpoofedIpAddress.Location = new System.Drawing.Point(6, 49);
      this.l_SpoofedIpAddress.Name = "l_SpoofedIpAddress";
      this.l_SpoofedIpAddress.Size = new System.Drawing.Size(100, 13);
      this.l_SpoofedIpAddress.TabIndex = 0;
      this.l_SpoofedIpAddress.Text = "Spoofed IP address";
      // 
      // L_DnsCacheTtl
      // 
      this.L_DnsCacheTtl.AutoSize = true;
      this.L_DnsCacheTtl.Location = new System.Drawing.Point(6, 80);
      this.L_DnsCacheTtl.Name = "L_DnsCacheTtl";
      this.L_DnsCacheTtl.Size = new System.Drawing.Size(86, 13);
      this.L_DnsCacheTtl.TabIndex = 0;
      this.L_DnsCacheTtl.Text = "DNS cache TTL";
      // 
      // Hostname
      // 
      this.Hostname.AutoSize = true;
      this.Hostname.Location = new System.Drawing.Point(6, 24);
      this.Hostname.Name = "Hostname";
      this.Hostname.Size = new System.Drawing.Size(55, 13);
      this.Hostname.TabIndex = 0;
      this.Hostname.Text = "Hostname";
      // 
      // BT_Save
      // 
      this.BT_Save.Location = new System.Drawing.Point(187, 137);
      this.BT_Save.Name = "BT_Save";
      this.BT_Save.Size = new System.Drawing.Size(75, 23);
      this.BT_Save.TabIndex = 4;
      this.BT_Save.Text = "Save";
      this.BT_Save.UseVisualStyleBackColor = true;
      this.BT_Save.Click += new System.EventHandler(this.BT_Save_Click);
      // 
      // BT_Cancel
      // 
      this.BT_Cancel.Location = new System.Drawing.Point(79, 137);
      this.BT_Cancel.Name = "BT_Cancel";
      this.BT_Cancel.Size = new System.Drawing.Size(75, 23);
      this.BT_Cancel.TabIndex = 5;
      this.BT_Cancel.Text = "Cancel";
      this.BT_Cancel.UseVisualStyleBackColor = true;
      this.BT_Cancel.Click += new System.EventHandler(this.BT_Cancel_Click);
      // 
      // ChangeParameters
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(338, 168);
      this.Controls.Add(this.BT_Cancel);
      this.Controls.Add(this.BT_Save);
      this.Controls.Add(this.GB_Parameters);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "ChangeParameters";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.Text = "Change parameters";
      this.GB_Parameters.ResumeLayout(false);
      this.GB_Parameters.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox GB_Parameters;
    private System.Windows.Forms.Button BT_Save;
    private System.Windows.Forms.Button BT_Cancel;
    private System.Windows.Forms.TextBox tb_IpAddress;
    private System.Windows.Forms.TextBox tb_ttl;
    private System.Windows.Forms.TextBox tb_Hostname;
    private System.Windows.Forms.Label l_SpoofedIpAddress;
    private System.Windows.Forms.Label L_DnsCacheTtl;
    private System.Windows.Forms.Label Hostname;
  }
}