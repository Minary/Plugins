namespace Minary.Plugin.Main
{
  partial class ShowData
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
      this.rtb_Response = new System.Windows.Forms.RichTextBox();
      this.SuspendLayout();
      // 
      // rtb_Response
      // 
      this.rtb_Response.BackColor = System.Drawing.Color.WhiteSmoke;
      this.rtb_Response.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rtb_Response.Location = new System.Drawing.Point(0, 0);
      this.rtb_Response.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.rtb_Response.Name = "rtb_Response";
      this.rtb_Response.Size = new System.Drawing.Size(501, 328);
      this.rtb_Response.TabIndex = 0;
      this.rtb_Response.Text = "";
      this.rtb_Response.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ShowData_KeyUp);
      // 
      // ShowData
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(501, 328);
      this.Controls.Add(this.rtb_Response);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.MinimizeBox = false;
      this.Name = "ShowData";
      this.Text = "Fetch and show data ...";
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ShowData_KeyUp);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.RichTextBox rtb_Response;
  }
}