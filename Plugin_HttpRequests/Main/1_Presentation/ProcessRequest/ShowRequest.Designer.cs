namespace Minary.Plugin.Main
{
  public partial class ShowRequest
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowRequest));
      this.tb_Request = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // tb_Request
      // 
      this.tb_Request.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tb_Request.Location = new System.Drawing.Point(8, 9);
      this.tb_Request.Multiline = true;
      this.tb_Request.Name = "tb_Request";
      this.tb_Request.ReadOnly = true;
      this.tb_Request.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.tb_Request.Size = new System.Drawing.Size(484, 312);
      this.tb_Request.TabIndex = 0;
      this.tb_Request.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PluginHTTPAccountsUC_KeyUp);
      // 
      // ShowRequest
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(502, 329);
      this.Controls.Add(this.tb_Request);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ShowRequest";
      this.Text = "Show HTTP request details";
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PluginHTTPAccountsUC_KeyUp);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox tb_Request;
  }
}