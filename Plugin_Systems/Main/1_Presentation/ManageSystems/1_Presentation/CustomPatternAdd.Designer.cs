namespace Minary.Plugin.Main.Systems.ManageSystems.Presentation
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
      this.tb_PatternDescription = new System.Windows.Forms.TextBox();
      this.l_PatternDescription = new System.Windows.Forms.Label();
      this.tb_PatternName = new System.Windows.Forms.TextBox();
      this.l_PatternName = new System.Windows.Forms.Label();
      this.tb_SystemRegex = new System.Windows.Forms.TextBox();
      this.tb_SystemName = new System.Windows.Forms.TextBox();
      this.l_SystemRegex = new System.Windows.Forms.Label();
      this.l_SystemName = new System.Windows.Forms.Label();
      this.bt_Close = new System.Windows.Forms.Button();
      this.bt_Add = new System.Windows.Forms.Button();
      this.gb_CustomPattern.SuspendLayout();
      this.SuspendLayout();
      // 
      // gb_CustomPattern
      // 
      this.gb_CustomPattern.Controls.Add(this.tb_PatternDescription);
      this.gb_CustomPattern.Controls.Add(this.l_PatternDescription);
      this.gb_CustomPattern.Controls.Add(this.tb_PatternName);
      this.gb_CustomPattern.Controls.Add(this.l_PatternName);
      this.gb_CustomPattern.Controls.Add(this.tb_SystemRegex);
      this.gb_CustomPattern.Controls.Add(this.tb_SystemName);
      this.gb_CustomPattern.Controls.Add(this.l_SystemRegex);
      this.gb_CustomPattern.Controls.Add(this.l_SystemName);
      this.gb_CustomPattern.Location = new System.Drawing.Point(12, 12);
      this.gb_CustomPattern.Name = "gb_CustomPattern";
      this.gb_CustomPattern.Size = new System.Drawing.Size(751, 161);
      this.gb_CustomPattern.TabIndex = 12;
      this.gb_CustomPattern.TabStop = false;
      this.gb_CustomPattern.Text = "Pattern data";
      // 
      // tb_PatternDescription
      // 
      this.tb_PatternDescription.Location = new System.Drawing.Point(133, 81);
      this.tb_PatternDescription.Multiline = true;
      this.tb_PatternDescription.Name = "tb_PatternDescription";
      this.tb_PatternDescription.Size = new System.Drawing.Size(602, 66);
      this.tb_PatternDescription.TabIndex = 6;
      // 
      // l_PatternDescription
      // 
      this.l_PatternDescription.AutoSize = true;
      this.l_PatternDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_PatternDescription.Location = new System.Drawing.Point(11, 84);
      this.l_PatternDescription.Name = "l_PatternDescription";
      this.l_PatternDescription.Size = new System.Drawing.Size(114, 13);
      this.l_PatternDescription.TabIndex = 0;
      this.l_PatternDescription.Text = "Pattern description";
      // 
      // tb_PatternName
      // 
      this.tb_PatternName.Location = new System.Drawing.Point(133, 53);
      this.tb_PatternName.Name = "tb_PatternName";
      this.tb_PatternName.Size = new System.Drawing.Size(604, 20);
      this.tb_PatternName.TabIndex = 5;
      // 
      // l_PatternName
      // 
      this.l_PatternName.AutoSize = true;
      this.l_PatternName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_PatternName.Location = new System.Drawing.Point(11, 56);
      this.l_PatternName.Name = "l_PatternName";
      this.l_PatternName.Size = new System.Drawing.Size(82, 13);
      this.l_PatternName.TabIndex = 0;
      this.l_PatternName.Text = "Pattern name";
      // 
      // tb_SystemRegex
      // 
      this.tb_SystemRegex.Location = new System.Drawing.Point(440, 25);
      this.tb_SystemRegex.Name = "tb_SystemRegex";
      this.tb_SystemRegex.Size = new System.Drawing.Size(297, 20);
      this.tb_SystemRegex.TabIndex = 2;
      // 
      // tb_SystemName
      // 
      this.tb_SystemName.Location = new System.Drawing.Point(133, 25);
      this.tb_SystemName.Name = "tb_SystemName";
      this.tb_SystemName.Size = new System.Drawing.Size(176, 20);
      this.tb_SystemName.TabIndex = 1;
      // 
      // l_SystemRegex
      // 
      this.l_SystemRegex.AutoSize = true;
      this.l_SystemRegex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_SystemRegex.Location = new System.Drawing.Point(337, 28);
      this.l_SystemRegex.Name = "l_SystemRegex";
      this.l_SystemRegex.Size = new System.Drawing.Size(82, 13);
      this.l_SystemRegex.TabIndex = 0;
      this.l_SystemRegex.Text = "System regex";
      // 
      // l_SystemName
      // 
      this.l_SystemName.AutoSize = true;
      this.l_SystemName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_SystemName.Location = new System.Drawing.Point(11, 28);
      this.l_SystemName.Name = "l_SystemName";
      this.l_SystemName.Size = new System.Drawing.Size(81, 13);
      this.l_SystemName.TabIndex = 0;
      this.l_SystemName.Text = "System name";
      // 
      // bt_Close
      // 
      this.bt_Close.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.bt_Close.Location = new System.Drawing.Point(670, 184);
      this.bt_Close.Name = "bt_Close";
      this.bt_Close.Size = new System.Drawing.Size(75, 23);
      this.bt_Close.TabIndex = 14;
      this.bt_Close.Text = "Close";
      this.bt_Close.UseVisualStyleBackColor = true;
      this.bt_Close.Click += new System.EventHandler(this.BT_Close_Click);
      // 
      // bt_Add
      // 
      this.bt_Add.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.bt_Add.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.bt_Add.Location = new System.Drawing.Point(568, 184);
      this.bt_Add.Name = "bt_Add";
      this.bt_Add.Size = new System.Drawing.Size(75, 23);
      this.bt_Add.TabIndex = 13;
      this.bt_Add.Text = "Add";
      this.bt_Add.UseVisualStyleBackColor = true;
      this.bt_Add.Click += new System.EventHandler(this.BT_Add_Click);
      // 
      // CustomPatternAdd
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(777, 222);
      this.Controls.Add(this.gb_CustomPattern);
      this.Controls.Add(this.bt_Close);
      this.Controls.Add(this.bt_Add);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "CustomPatternAdd";
      this.Text = "Custom pattern";
      this.gb_CustomPattern.ResumeLayout(false);
      this.gb_CustomPattern.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox gb_CustomPattern;
    private System.Windows.Forms.TextBox tb_PatternDescription;
    private System.Windows.Forms.Label l_PatternDescription;
    private System.Windows.Forms.TextBox tb_PatternName;
    private System.Windows.Forms.Label l_PatternName;
    private System.Windows.Forms.TextBox tb_SystemRegex;
    private System.Windows.Forms.TextBox tb_SystemName;
    private System.Windows.Forms.Label l_SystemRegex;
    private System.Windows.Forms.Label l_SystemName;
    private System.Windows.Forms.Button bt_Close;
    private System.Windows.Forms.Button bt_Add;
  }
}