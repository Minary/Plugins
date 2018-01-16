namespace Minary.Plugin.Main
{
  partial class Plugin_HttpSearch
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      this.dgv_HttpSearch = new System.Windows.Forms.DataGridView();
      this.L_Method = new System.Windows.Forms.Label();
      this.TB_HostRegex = new System.Windows.Forms.TextBox();
      this.L_Host = new System.Windows.Forms.Label();
      this.L_Path = new System.Windows.Forms.Label();
      this.TB_PathRegex = new System.Windows.Forms.TextBox();
      this.CB_Method = new System.Windows.Forms.ComboBox();
      this.bt_Add = new System.Windows.Forms.Button();
      this.RB_Header = new System.Windows.Forms.RadioButton();
      this.RB_Body = new System.Windows.Forms.RadioButton();
      this.L_DataRegex = new System.Windows.Forms.Label();
      this.TB_DataRegex = new System.Windows.Forms.TextBox();
      this.CB_Type = new System.Windows.Forms.ComboBox();
      this.L_Type = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.dgv_HttpSearch)).BeginInit();
      this.SuspendLayout();
      // 
      // dgv_HttpSearch
      // 
      this.dgv_HttpSearch.AllowUserToAddRows = false;
      this.dgv_HttpSearch.AllowUserToDeleteRows = false;
      this.dgv_HttpSearch.AllowUserToResizeColumns = false;
      this.dgv_HttpSearch.AllowUserToResizeRows = false;
      this.dgv_HttpSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dgv_HttpSearch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.dgv_HttpSearch.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
      this.dgv_HttpSearch.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgv_HttpSearch.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dgv_HttpSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv_HttpSearch.EnableHeadersVisualStyles = false;
      this.dgv_HttpSearch.Location = new System.Drawing.Point(26, 99);
      this.dgv_HttpSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.dgv_HttpSearch.MultiSelect = false;
      this.dgv_HttpSearch.Name = "dgv_HttpSearch";
      this.dgv_HttpSearch.ReadOnly = true;
      this.dgv_HttpSearch.RowHeadersVisible = false;
      this.dgv_HttpSearch.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dgv_HttpSearch.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.dgv_HttpSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv_HttpSearch.Size = new System.Drawing.Size(1400, 451);
      this.dgv_HttpSearch.TabIndex = 8;
      // 
      // L_Method
      // 
      this.L_Method.AutoSize = true;
      this.L_Method.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.L_Method.Location = new System.Drawing.Point(33, 29);
      this.L_Method.Name = "L_Method";
      this.L_Method.Size = new System.Drawing.Size(69, 20);
      this.L_Method.TabIndex = 0;
      this.L_Method.Text = "Method";
      // 
      // TB_HostRegex
      // 
      this.TB_HostRegex.Location = new System.Drawing.Point(318, 18);
      this.TB_HostRegex.Name = "TB_HostRegex";
      this.TB_HostRegex.Size = new System.Drawing.Size(272, 26);
      this.TB_HostRegex.TabIndex = 2;
      // 
      // L_Host
      // 
      this.L_Host.AutoSize = true;
      this.L_Host.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.L_Host.Location = new System.Drawing.Point(222, 25);
      this.L_Host.Name = "L_Host";
      this.L_Host.Size = new System.Drawing.Size(96, 20);
      this.L_Host.TabIndex = 0;
      this.L_Host.Text = "Host regex";
      // 
      // L_Path
      // 
      this.L_Path.AutoSize = true;
      this.L_Path.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.L_Path.Location = new System.Drawing.Point(604, 25);
      this.L_Path.Name = "L_Path";
      this.L_Path.Size = new System.Drawing.Size(95, 20);
      this.L_Path.TabIndex = 0;
      this.L_Path.Text = "Path regex";
      // 
      // TB_PathRegex
      // 
      this.TB_PathRegex.Location = new System.Drawing.Point(705, 18);
      this.TB_PathRegex.Name = "TB_PathRegex";
      this.TB_PathRegex.Size = new System.Drawing.Size(332, 26);
      this.TB_PathRegex.TabIndex = 3;
      // 
      // CB_Method
      // 
      this.CB_Method.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.CB_Method.FormattingEnabled = true;
      this.CB_Method.Items.AddRange(new object[] {
            "GET",
            "POST",
            "PUT",
            "DELETE",
            "HEAD"});
      this.CB_Method.Location = new System.Drawing.Point(98, 22);
      this.CB_Method.Name = "CB_Method";
      this.CB_Method.Size = new System.Drawing.Size(98, 28);
      this.CB_Method.TabIndex = 1;
      // 
      // bt_Add
      // 
      this.bt_Add.Location = new System.Drawing.Point(1056, 17);
      this.bt_Add.Margin = new System.Windows.Forms.Padding(0);
      this.bt_Add.Name = "bt_Add";
      this.bt_Add.Size = new System.Drawing.Size(30, 32);
      this.bt_Add.TabIndex = 7;
      this.bt_Add.Text = "+";
      this.bt_Add.UseVisualStyleBackColor = true;
      // 
      // RB_Header
      // 
      this.RB_Header.AutoSize = true;
      this.RB_Header.Checked = true;
      this.RB_Header.Location = new System.Drawing.Point(38, 67);
      this.RB_Header.Name = "RB_Header";
      this.RB_Header.Size = new System.Drawing.Size(87, 24);
      this.RB_Header.TabIndex = 4;
      this.RB_Header.TabStop = true;
      this.RB_Header.Text = "Header";
      this.RB_Header.UseVisualStyleBackColor = true;
      // 
      // RB_Body
      // 
      this.RB_Body.AutoSize = true;
      this.RB_Body.Location = new System.Drawing.Point(119, 67);
      this.RB_Body.Name = "RB_Body";
      this.RB_Body.Size = new System.Drawing.Size(70, 24);
      this.RB_Body.TabIndex = 5;
      this.RB_Body.TabStop = true;
      this.RB_Body.Text = "Body";
      this.RB_Body.UseVisualStyleBackColor = true;
      // 
      // L_DataRegex
      // 
      this.L_DataRegex.AutoSize = true;
      this.L_DataRegex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.L_DataRegex.Location = new System.Drawing.Point(222, 69);
      this.L_DataRegex.Name = "L_DataRegex";
      this.L_DataRegex.Size = new System.Drawing.Size(97, 20);
      this.L_DataRegex.TabIndex = 0;
      this.L_DataRegex.Text = "Data regex";
      // 
      // TB_DataRegex
      // 
      this.TB_DataRegex.Location = new System.Drawing.Point(318, 63);
      this.TB_DataRegex.Name = "TB_DataRegex";
      this.TB_DataRegex.Size = new System.Drawing.Size(719, 26);
      this.TB_DataRegex.TabIndex = 6;
      // 
      // CB_Type
      // 
      this.CB_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.CB_Type.FormattingEnabled = true;
      this.CB_Type.Items.AddRange(new object[] {
            "Account",
            "Cookie",
            "Session",
            "Other"});
      this.CB_Type.Location = new System.Drawing.Point(1101, 61);
      this.CB_Type.Name = "CB_Type";
      this.CB_Type.Size = new System.Drawing.Size(98, 28);
      this.CB_Type.TabIndex = 9;
      // 
      // L_Type
      // 
      this.L_Type.AutoSize = true;
      this.L_Type.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.L_Type.Location = new System.Drawing.Point(1052, 68);
      this.L_Type.Name = "L_Type";
      this.L_Type.Size = new System.Drawing.Size(47, 20);
      this.L_Type.TabIndex = 8;
      this.L_Type.Text = "Type";
      // 
      // Plugin_HttpSearch
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.Controls.Add(this.CB_Type);
      this.Controls.Add(this.L_Type);
      this.Controls.Add(this.TB_DataRegex);
      this.Controls.Add(this.L_DataRegex);
      this.Controls.Add(this.RB_Body);
      this.Controls.Add(this.RB_Header);
      this.Controls.Add(this.bt_Add);
      this.Controls.Add(this.CB_Method);
      this.Controls.Add(this.TB_PathRegex);
      this.Controls.Add(this.L_Path);
      this.Controls.Add(this.L_Host);
      this.Controls.Add(this.TB_HostRegex);
      this.Controls.Add(this.L_Method);
      this.Controls.Add(this.dgv_HttpSearch);
      this.DoubleBuffered = true;
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "Plugin_HttpSearch";
      this.Size = new System.Drawing.Size(1494, 566);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_HttpSearch)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView dgv_HttpSearch;
    private System.Windows.Forms.Label L_Method;
    private System.Windows.Forms.TextBox TB_HostRegex;
    private System.Windows.Forms.Label L_Host;
    private System.Windows.Forms.Label L_Path;
    private System.Windows.Forms.TextBox TB_PathRegex;
    private System.Windows.Forms.ComboBox CB_Method;
    private System.Windows.Forms.Button bt_Add;
    private System.Windows.Forms.RadioButton RB_Header;
    private System.Windows.Forms.RadioButton RB_Body;
    private System.Windows.Forms.Label L_DataRegex;
    private System.Windows.Forms.TextBox TB_DataRegex;
    private System.Windows.Forms.ComboBox CB_Type;
    private System.Windows.Forms.Label L_Type;
  }
}
