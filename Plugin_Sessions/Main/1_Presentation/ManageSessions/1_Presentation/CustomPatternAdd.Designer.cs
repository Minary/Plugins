namespace Minary.Plugin.Main.Session.ManageSessions.Presentation
{
  public partial class CustomPatternAdd
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
      this.bt_Close = new System.Windows.Forms.Button();
      this.bt_Add = new System.Windows.Forms.Button();
      this.gb_CustomPattern = new System.Windows.Forms.GroupBox();
      this.tb_HostRegex = new System.Windows.Forms.TextBox();
      this.tb_PatternDescription = new System.Windows.Forms.TextBox();
      this.l_PatternDescription = new System.Windows.Forms.Label();
      this.tb_CompanyWebPage = new System.Windows.Forms.TextBox();
      this.tb_PatternName = new System.Windows.Forms.TextBox();
      this.l_PatternName = new System.Windows.Forms.Label();
      this.tb_SessionCookieRegex = new System.Windows.Forms.TextBox();
      this.tb_CompanyName = new System.Windows.Forms.TextBox();
      this.l_SessionCookieRegex = new System.Windows.Forms.Label();
      this.l_HostPattern = new System.Windows.Forms.Label();
      this.l_WebPage = new System.Windows.Forms.Label();
      this.l_CompanyName = new System.Windows.Forms.Label();
      this.gb_CustomPattern.SuspendLayout();
      this.SuspendLayout();
      // 
      // bt_Close
      // 
      this.bt_Close.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.bt_Close.Location = new System.Drawing.Point(678, 217);
      this.bt_Close.Name = "bt_Close";
      this.bt_Close.Size = new System.Drawing.Size(75, 23);
      this.bt_Close.TabIndex = 8;
      this.bt_Close.Text = "Close";
      this.bt_Close.UseVisualStyleBackColor = true;
      this.bt_Close.Click += new System.EventHandler(this.BT_Close_Click);
      // 
      // bt_Add
      // 
      this.bt_Add.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.bt_Add.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.bt_Add.Location = new System.Drawing.Point(576, 217);
      this.bt_Add.Name = "bt_Add";
      this.bt_Add.Size = new System.Drawing.Size(75, 23);
      this.bt_Add.TabIndex = 7;
      this.bt_Add.Text = "Add";
      this.bt_Add.UseVisualStyleBackColor = true;
      this.bt_Add.Click += new System.EventHandler(this.BT_Add_Click);
      // 
      // gb_CustomPattern
      // 
      this.gb_CustomPattern.Controls.Add(this.tb_HostRegex);
      this.gb_CustomPattern.Controls.Add(this.tb_PatternDescription);
      this.gb_CustomPattern.Controls.Add(this.l_PatternDescription);
      this.gb_CustomPattern.Controls.Add(this.tb_CompanyWebPage);
      this.gb_CustomPattern.Controls.Add(this.tb_PatternName);
      this.gb_CustomPattern.Controls.Add(this.l_PatternName);
      this.gb_CustomPattern.Controls.Add(this.tb_SessionCookieRegex);
      this.gb_CustomPattern.Controls.Add(this.tb_CompanyName);
      this.gb_CustomPattern.Controls.Add(this.l_SessionCookieRegex);
      this.gb_CustomPattern.Controls.Add(this.l_HostPattern);
      this.gb_CustomPattern.Controls.Add(this.l_WebPage);
      this.gb_CustomPattern.Controls.Add(this.l_CompanyName);
      this.gb_CustomPattern.Location = new System.Drawing.Point(16, 22);
      this.gb_CustomPattern.Name = "gb_CustomPattern";
      this.gb_CustomPattern.Size = new System.Drawing.Size(751, 186);
      this.gb_CustomPattern.TabIndex = 9;
      this.gb_CustomPattern.TabStop = false;
      this.gb_CustomPattern.Text = "Pattern newData";
      // 
      // tb_HostRegex
      // 
      this.tb_HostRegex.Location = new System.Drawing.Point(429, 25);
      this.tb_HostRegex.Name = "tb_HostRegex";
      this.tb_HostRegex.Size = new System.Drawing.Size(308, 20);
      this.tb_HostRegex.TabIndex = 2;
      // 
      // tb_PatternDescription
      // 
      this.tb_PatternDescription.Location = new System.Drawing.Point(133, 105);
      this.tb_PatternDescription.Multiline = true;
      this.tb_PatternDescription.Name = "tb_PatternDescription";
      this.tb_PatternDescription.Size = new System.Drawing.Size(604, 66);
      this.tb_PatternDescription.TabIndex = 6;
      // 
      // l_PatternDescription
      // 
      this.l_PatternDescription.AutoSize = true;
      this.l_PatternDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_PatternDescription.Location = new System.Drawing.Point(11, 108);
      this.l_PatternDescription.Name = "l_PatternDescription";
      this.l_PatternDescription.Size = new System.Drawing.Size(114, 13);
      this.l_PatternDescription.TabIndex = 0;
      this.l_PatternDescription.Text = "Pattern description";
      // 
      // tb_CompanyWebPage
      // 
      this.tb_CompanyWebPage.Location = new System.Drawing.Point(133, 51);
      this.tb_CompanyWebPage.Name = "tb_CompanyWebPage";
      this.tb_CompanyWebPage.Size = new System.Drawing.Size(176, 20);
      this.tb_CompanyWebPage.TabIndex = 3;
      // 
      // tb_PatternName
      // 
      this.tb_PatternName.Location = new System.Drawing.Point(133, 78);
      this.tb_PatternName.Name = "tb_PatternName";
      this.tb_PatternName.Size = new System.Drawing.Size(604, 20);
      this.tb_PatternName.TabIndex = 5;
      // 
      // l_PatternName
      // 
      this.l_PatternName.AutoSize = true;
      this.l_PatternName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_PatternName.Location = new System.Drawing.Point(11, 81);
      this.l_PatternName.Name = "l_PatternName";
      this.l_PatternName.Size = new System.Drawing.Size(82, 13);
      this.l_PatternName.TabIndex = 0;
      this.l_PatternName.Text = "Pattern name";
      // 
      // tb_SessionCookieRegex
      // 
      this.tb_SessionCookieRegex.Location = new System.Drawing.Point(429, 51);
      this.tb_SessionCookieRegex.Name = "tb_SessionCookieRegex";
      this.tb_SessionCookieRegex.Size = new System.Drawing.Size(308, 20);
      this.tb_SessionCookieRegex.TabIndex = 4;
      // 
      // tb_CompanyName
      // 
      this.tb_CompanyName.Location = new System.Drawing.Point(133, 25);
      this.tb_CompanyName.Name = "tb_CompanyName";
      this.tb_CompanyName.Size = new System.Drawing.Size(176, 20);
      this.tb_CompanyName.TabIndex = 1;
      // 
      // l_SessionCookieRegex
      // 
      this.l_SessionCookieRegex.AutoSize = true;
      this.l_SessionCookieRegex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_SessionCookieRegex.Location = new System.Drawing.Point(337, 58);
      this.l_SessionCookieRegex.Name = "l_SessionCookieRegex";
      this.l_SessionCookieRegex.Size = new System.Drawing.Size(86, 13);
      this.l_SessionCookieRegex.TabIndex = 0;
      this.l_SessionCookieRegex.Text = "Session regex";
      // 
      // l_HostPattern
      // 
      this.l_HostPattern.AutoSize = true;
      this.l_HostPattern.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_HostPattern.Location = new System.Drawing.Point(337, 28);
      this.l_HostPattern.Name = "l_HostPattern";
      this.l_HostPattern.Size = new System.Drawing.Size(68, 13);
      this.l_HostPattern.TabIndex = 0;
      this.l_HostPattern.Text = "Host regex";
      // 
      // l_WebPage
      // 
      this.l_WebPage.AutoSize = true;
      this.l_WebPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_WebPage.Location = new System.Drawing.Point(11, 54);
      this.l_WebPage.Name = "l_WebPage";
      this.l_WebPage.Size = new System.Drawing.Size(117, 13);
      this.l_WebPage.TabIndex = 0;
      this.l_WebPage.Text = "Company web page";
      // 
      // l_CompanyName
      // 
      this.l_CompanyName.AutoSize = true;
      this.l_CompanyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_CompanyName.Location = new System.Drawing.Point(11, 28);
      this.l_CompanyName.Name = "l_CompanyName";
      this.l_CompanyName.Size = new System.Drawing.Size(92, 13);
      this.l_CompanyName.TabIndex = 0;
      this.l_CompanyName.Text = "Company name";
      // 
      // CustomPatternAdd
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(784, 255);
      this.Controls.Add(this.bt_Close);
      this.Controls.Add(this.bt_Add);
      this.Controls.Add(this.gb_CustomPattern);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "CustomPatternAdd";
      this.Text = "Custom pattern";
      this.gb_CustomPattern.ResumeLayout(false);
      this.gb_CustomPattern.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button bt_Close;
    private System.Windows.Forms.Button bt_Add;
    private System.Windows.Forms.GroupBox gb_CustomPattern;
    private System.Windows.Forms.TextBox tb_PatternDescription;
    private System.Windows.Forms.Label l_PatternDescription;
    private System.Windows.Forms.TextBox tb_CompanyWebPage;
    private System.Windows.Forms.TextBox tb_PatternName;
    private System.Windows.Forms.Label l_PatternName;
    private System.Windows.Forms.TextBox tb_HostRegex;
    private System.Windows.Forms.TextBox tb_SessionCookieRegex;
    private System.Windows.Forms.TextBox tb_CompanyName;
    private System.Windows.Forms.Label l_SessionCookieRegex;
    private System.Windows.Forms.Label l_HostPattern;
    private System.Windows.Forms.Label l_WebPage;
    private System.Windows.Forms.Label l_CompanyName;
  }
}