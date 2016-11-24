namespace Minary.Plugin.Main.HttpAccounts.ManageAuthentications.Presentation
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
      this.gb_CustomPattern = new System.Windows.Forms.GroupBox();
      this.cb_Method = new System.Windows.Forms.ComboBox();
      this.tb_WebPage = new System.Windows.Forms.TextBox();
      this.tb_HostPattern = new System.Windows.Forms.TextBox();
      this.tb_DataPattern = new System.Windows.Forms.TextBox();
      this.tb_PathPattern = new System.Windows.Forms.TextBox();
      this.tb_Company = new System.Windows.Forms.TextBox();
      this.l_PathPattern = new System.Windows.Forms.Label();
      this.l_DataPattern = new System.Windows.Forms.Label();
      this.l_HostPattern = new System.Windows.Forms.Label();
      this.l_Method = new System.Windows.Forms.Label();
      this.l_WebPage = new System.Windows.Forms.Label();
      this.l_Company = new System.Windows.Forms.Label();
      this.bt_Add = new System.Windows.Forms.Button();
      this.bt_Close = new System.Windows.Forms.Button();
      this.tb_Description = new System.Windows.Forms.TextBox();
      this.l_Description = new System.Windows.Forms.Label();
      this.tb_PatternName = new System.Windows.Forms.TextBox();
      this.l_PatternName = new System.Windows.Forms.Label();
      this.gb_CustomPattern.SuspendLayout();
      this.SuspendLayout();
      // 
      // gb_CustomPattern
      // 
      this.gb_CustomPattern.Controls.Add(this.tb_Description);
      this.gb_CustomPattern.Controls.Add(this.l_Description);
      this.gb_CustomPattern.Controls.Add(this.cb_Method);
      this.gb_CustomPattern.Controls.Add(this.tb_WebPage);
      this.gb_CustomPattern.Controls.Add(this.tb_PatternName);
      this.gb_CustomPattern.Controls.Add(this.l_PatternName);
      this.gb_CustomPattern.Controls.Add(this.tb_HostPattern);
      this.gb_CustomPattern.Controls.Add(this.tb_DataPattern);
      this.gb_CustomPattern.Controls.Add(this.tb_PathPattern);
      this.gb_CustomPattern.Controls.Add(this.tb_Company);
      this.gb_CustomPattern.Controls.Add(this.l_PathPattern);
      this.gb_CustomPattern.Controls.Add(this.l_DataPattern);
      this.gb_CustomPattern.Controls.Add(this.l_HostPattern);
      this.gb_CustomPattern.Controls.Add(this.l_Method);
      this.gb_CustomPattern.Controls.Add(this.l_WebPage);
      this.gb_CustomPattern.Controls.Add(this.l_Company);
      this.gb_CustomPattern.Location = new System.Drawing.Point(13, 13);
      this.gb_CustomPattern.Name = "gb_CustomPattern";
      this.gb_CustomPattern.Size = new System.Drawing.Size(736, 251);
      this.gb_CustomPattern.TabIndex = 0;
      this.gb_CustomPattern.TabStop = false;
      this.gb_CustomPattern.Text = "Pattern data";
      // 
      // cb_Method
      // 
      this.cb_Method.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cb_Method.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.cb_Method.FormattingEnabled = true;
      this.cb_Method.Items.AddRange(new object[] {
            "POST",
            "GET",
            "CONNECT",
            "DELETE",
            "HEAD",
            "OPTIONS",
            "PUT",
            "TRACE"});
      this.cb_Method.Location = new System.Drawing.Point(112, 90);
      this.cb_Method.Name = "cb_Method";
      this.cb_Method.Size = new System.Drawing.Size(176, 21);
      this.cb_Method.TabIndex = 5;
      // 
      // tb_WebPage
      // 
      this.tb_WebPage.Location = new System.Drawing.Point(112, 57);
      this.tb_WebPage.Name = "tb_WebPage";
      this.tb_WebPage.Size = new System.Drawing.Size(176, 20);
      this.tb_WebPage.TabIndex = 3;
      // 
      // tb_HostPattern
      // 
      this.tb_HostPattern.Location = new System.Drawing.Point(419, 25);
      this.tb_HostPattern.Name = "tb_HostPattern";
      this.tb_HostPattern.Size = new System.Drawing.Size(297, 20);
      this.tb_HostPattern.TabIndex = 2;
      // 
      // tb_DataPattern
      // 
      this.tb_DataPattern.Location = new System.Drawing.Point(419, 90);
      this.tb_DataPattern.Name = "tb_DataPattern";
      this.tb_DataPattern.Size = new System.Drawing.Size(297, 20);
      this.tb_DataPattern.TabIndex = 6;
      // 
      // tb_PathPattern
      // 
      this.tb_PathPattern.Location = new System.Drawing.Point(419, 57);
      this.tb_PathPattern.Name = "tb_PathPattern";
      this.tb_PathPattern.Size = new System.Drawing.Size(297, 20);
      this.tb_PathPattern.TabIndex = 4;
      // 
      // tb_Company
      // 
      this.tb_Company.Location = new System.Drawing.Point(112, 25);
      this.tb_Company.Name = "tb_Company";
      this.tb_Company.Size = new System.Drawing.Size(176, 20);
      this.tb_Company.TabIndex = 1;
      // 
      // l_PathPattern
      // 
      this.l_PathPattern.AutoSize = true;
      this.l_PathPattern.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_PathPattern.Location = new System.Drawing.Point(316, 60);
      this.l_PathPattern.Name = "l_PathPattern";
      this.l_PathPattern.Size = new System.Drawing.Size(77, 13);
      this.l_PathPattern.TabIndex = 0;
      this.l_PathPattern.Text = "Path pattern";
      // 
      // l_DataPattern
      // 
      this.l_DataPattern.AutoSize = true;
      this.l_DataPattern.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_DataPattern.Location = new System.Drawing.Point(316, 97);
      this.l_DataPattern.Name = "l_DataPattern";
      this.l_DataPattern.Size = new System.Drawing.Size(78, 13);
      this.l_DataPattern.TabIndex = 0;
      this.l_DataPattern.Text = "Data pattern";
      // 
      // l_HostPattern
      // 
      this.l_HostPattern.AutoSize = true;
      this.l_HostPattern.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_HostPattern.Location = new System.Drawing.Point(316, 28);
      this.l_HostPattern.Name = "l_HostPattern";
      this.l_HostPattern.Size = new System.Drawing.Size(77, 13);
      this.l_HostPattern.TabIndex = 0;
      this.l_HostPattern.Text = "Host pattern";
      // 
      // l_Method
      // 
      this.l_Method.AutoSize = true;
      this.l_Method.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_Method.Location = new System.Drawing.Point(16, 98);
      this.l_Method.Name = "l_Method";
      this.l_Method.Size = new System.Drawing.Size(49, 13);
      this.l_Method.TabIndex = 0;
      this.l_Method.Text = "Method";
      // 
      // l_WebPage
      // 
      this.l_WebPage.AutoSize = true;
      this.l_WebPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_WebPage.Location = new System.Drawing.Point(16, 60);
      this.l_WebPage.Name = "l_WebPage";
      this.l_WebPage.Size = new System.Drawing.Size(65, 13);
      this.l_WebPage.TabIndex = 0;
      this.l_WebPage.Text = "Web page";
      // 
      // l_Company
      // 
      this.l_Company.AutoSize = true;
      this.l_Company.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_Company.Location = new System.Drawing.Point(16, 28);
      this.l_Company.Name = "l_Company";
      this.l_Company.Size = new System.Drawing.Size(58, 13);
      this.l_Company.TabIndex = 0;
      this.l_Company.Text = "Company";
      // 
      // bt_Add
      // 
      this.bt_Add.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.bt_Add.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.bt_Add.Location = new System.Drawing.Point(559, 277);
      this.bt_Add.Name = "bt_Add";
      this.bt_Add.Size = new System.Drawing.Size(75, 23);
      this.bt_Add.TabIndex = 7;
      this.bt_Add.Text = "Add";
      this.bt_Add.UseVisualStyleBackColor = true;
      this.bt_Add.Click += new System.EventHandler(this.BT_Add_Click);
      // 
      // bt_Close
      // 
      this.bt_Close.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.bt_Close.Location = new System.Drawing.Point(661, 277);
      this.bt_Close.Name = "bt_Close";
      this.bt_Close.Size = new System.Drawing.Size(75, 23);
      this.bt_Close.TabIndex = 8;
      this.bt_Close.Text = "Close";
      this.bt_Close.UseVisualStyleBackColor = true;
      this.bt_Close.Click += new System.EventHandler(this.BT_Close_Click);
      // 
      // tb_Description
      // 
      this.tb_Description.Location = new System.Drawing.Point(112, 161);
      this.tb_Description.Multiline = true;
      this.tb_Description.Name = "tb_Description";
      this.tb_Description.Size = new System.Drawing.Size(602, 66);
      this.tb_Description.TabIndex = 30;
      // 
      // l_Description
      // 
      this.l_Description.AutoSize = true;
      this.l_Description.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_Description.Location = new System.Drawing.Point(16, 168);
      this.l_Description.Name = "l_Description";
      this.l_Description.Size = new System.Drawing.Size(71, 13);
      this.l_Description.TabIndex = 29;
      this.l_Description.Text = "Description";
      // 
      // tb_PatternName
      // 
      this.tb_PatternName.Location = new System.Drawing.Point(112, 127);
      this.tb_PatternName.Name = "tb_PatternName";
      this.tb_PatternName.Size = new System.Drawing.Size(604, 20);
      this.tb_PatternName.TabIndex = 28;
      // 
      // l_PatternName
      // 
      this.l_PatternName.AutoSize = true;
      this.l_PatternName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_PatternName.Location = new System.Drawing.Point(16, 130);
      this.l_PatternName.Name = "l_PatternName";
      this.l_PatternName.Size = new System.Drawing.Size(82, 13);
      this.l_PatternName.TabIndex = 27;
      this.l_PatternName.Text = "Pattern name";
      // 
      // CustomPatternAdd
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(761, 315);
      this.Controls.Add(this.bt_Close);
      this.Controls.Add(this.bt_Add);
      this.Controls.Add(this.gb_CustomPattern);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "CustomPatternAdd";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.Text = "Custom pattern";
      this.gb_CustomPattern.ResumeLayout(false);
      this.gb_CustomPattern.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox gb_CustomPattern;
    private System.Windows.Forms.Button bt_Add;
    private System.Windows.Forms.Button bt_Close;
    private System.Windows.Forms.Label l_DataPattern;
    private System.Windows.Forms.Label l_PathPattern;
    private System.Windows.Forms.Label l_HostPattern;
    private System.Windows.Forms.Label l_Method;
    private System.Windows.Forms.Label l_WebPage;
    private System.Windows.Forms.Label l_Company;
    private System.Windows.Forms.TextBox tb_Company;
    private System.Windows.Forms.TextBox tb_PathPattern;
    private System.Windows.Forms.TextBox tb_DataPattern;
    private System.Windows.Forms.ComboBox cb_Method;
    private System.Windows.Forms.TextBox tb_WebPage;
    private System.Windows.Forms.TextBox tb_HostPattern;
    private System.Windows.Forms.TextBox tb_Description;
    private System.Windows.Forms.Label l_Description;
    private System.Windows.Forms.TextBox tb_PatternName;
    private System.Windows.Forms.Label l_PatternName;
  }
}