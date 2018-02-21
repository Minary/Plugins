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
      this.rtb_Request = new System.Windows.Forms.RichTextBox();
      this.SuspendLayout();
      // 
      // rtb_Request
      // 
      this.rtb_Request.BackColor = System.Drawing.Color.WhiteSmoke;
      this.rtb_Request.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.rtb_Request.Location = new System.Drawing.Point(11, 11);
      this.rtb_Request.Margin = new System.Windows.Forms.Padding(5);
      this.rtb_Request.Name = "rtb_Request";
      this.rtb_Request.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
      this.rtb_Request.Size = new System.Drawing.Size(933, 637);
      this.rtb_Request.TabIndex = 0;
      this.rtb_Request.Text = "";
      // 
      // ShowRequest
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(958, 659);
      this.Controls.Add(this.rtb_Request);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ShowRequest";
      this.Text = "Raw HTTP request";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.RichTextBox rtb_Request;
  }
}