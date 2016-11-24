namespace Minary.Plugin
{
  public partial class Main_Notes
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_Notes));
      this.tb_Data = new System.Windows.Forms.RichTextBox();
      this.SuspendLayout();
      // 
      // tb_Data
      // 
      this.tb_Data.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tb_Data.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tb_Data.Location = new System.Drawing.Point(0, 0);
      this.tb_Data.Name = "tb_Data";
      this.tb_Data.Size = new System.Drawing.Size(856, 262);
      this.tb_Data.TabIndex = 0;
      this.tb_Data.Text = string.Empty;
      // 
      // Main_Notes
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(856, 262);
      this.Controls.Add(this.tb_Data);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "Main_Notes";
      this.Text = "Notes";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.RichTextBox tb_Data;

  }
}