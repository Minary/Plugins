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
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      this.dgv_HttpSearch = new System.Windows.Forms.DataGridView();
      this.L_Method = new System.Windows.Forms.Label();
      this.tb_HostRegex = new System.Windows.Forms.TextBox();
      this.L_Host = new System.Windows.Forms.Label();
      this.L_Path = new System.Windows.Forms.Label();
      this.tb_PathRegex = new System.Windows.Forms.TextBox();
      this.cb_Method = new System.Windows.Forms.ComboBox();
      this.bt_Add = new System.Windows.Forms.Button();
      this.rb_Header = new System.Windows.Forms.RadioButton();
      this.rb_Body = new System.Windows.Forms.RadioButton();
      this.L_DataRegex = new System.Windows.Forms.Label();
      this.tb_DataRegex = new System.Windows.Forms.TextBox();
      this.cb_Type = new System.Windows.Forms.ComboBox();
      this.L_Type = new System.Windows.Forms.Label();
      this.cms_HttpSearch = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.deleteEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.clearListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.t_GuiUpdate = new System.Windows.Forms.Timer(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_HttpSearch)).BeginInit();
      this.cms_HttpSearch.SuspendLayout();
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
      this.dgv_HttpSearch.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DGV_MouseDown);
      this.dgv_HttpSearch.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DGV_MouseUp);
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
      // tb_HostRegex
      // 
      this.tb_HostRegex.Location = new System.Drawing.Point(318, 18);
      this.tb_HostRegex.Name = "tb_HostRegex";
      this.tb_HostRegex.Size = new System.Drawing.Size(272, 26);
      this.tb_HostRegex.TabIndex = 2;
      this.tb_HostRegex.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnEnterAddRecord);
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
      // tb_PathRegex
      // 
      this.tb_PathRegex.Location = new System.Drawing.Point(705, 18);
      this.tb_PathRegex.Name = "tb_PathRegex";
      this.tb_PathRegex.Size = new System.Drawing.Size(332, 26);
      this.tb_PathRegex.TabIndex = 3;
      this.tb_PathRegex.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnEnterAddRecord);
      // 
      // cb_Method
      // 
      this.cb_Method.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cb_Method.FormattingEnabled = true;
      this.cb_Method.Items.AddRange(new object[] {
            "GET",
            "POST",
            "PUT",
            "DELETE",
            "HEAD"});
      this.cb_Method.Location = new System.Drawing.Point(98, 22);
      this.cb_Method.Name = "cb_Method";
      this.cb_Method.Size = new System.Drawing.Size(98, 28);
      this.cb_Method.TabIndex = 1;
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
      this.bt_Add.Click += new System.EventHandler(this.BT_Add_Click);
      // 
      // rb_Header
      // 
      this.rb_Header.AutoSize = true;
      this.rb_Header.Checked = true;
      this.rb_Header.Location = new System.Drawing.Point(38, 67);
      this.rb_Header.Name = "rb_Header";
      this.rb_Header.Size = new System.Drawing.Size(87, 24);
      this.rb_Header.TabIndex = 4;
      this.rb_Header.TabStop = true;
      this.rb_Header.Text = "Header";
      this.rb_Header.UseVisualStyleBackColor = true;
      // 
      // rb_Body
      // 
      this.rb_Body.AutoSize = true;
      this.rb_Body.Location = new System.Drawing.Point(119, 67);
      this.rb_Body.Name = "rb_Body";
      this.rb_Body.Size = new System.Drawing.Size(70, 24);
      this.rb_Body.TabIndex = 5;
      this.rb_Body.TabStop = true;
      this.rb_Body.Text = "Body";
      this.rb_Body.UseVisualStyleBackColor = true;
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
      // tb_DataRegex
      // 
      this.tb_DataRegex.Location = new System.Drawing.Point(318, 63);
      this.tb_DataRegex.Name = "tb_DataRegex";
      this.tb_DataRegex.Size = new System.Drawing.Size(719, 26);
      this.tb_DataRegex.TabIndex = 6;
      this.tb_DataRegex.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnEnterAddRecord);
      // 
      // cb_Type
      // 
      this.cb_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cb_Type.FormattingEnabled = true;
      this.cb_Type.Items.AddRange(new object[] {
            "Account",
            "Cookie",
            "Session",
            "Other"});
      this.cb_Type.Location = new System.Drawing.Point(1101, 61);
      this.cb_Type.Name = "cb_Type";
      this.cb_Type.Size = new System.Drawing.Size(98, 28);
      this.cb_Type.TabIndex = 9;
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
      // cms_HttpSearch
      // 
      this.cms_HttpSearch.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.cms_HttpSearch.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteEntryToolStripMenuItem,
            this.clearListToolStripMenuItem});
      this.cms_HttpSearch.Name = "cms_HttpSearch";
      this.cms_HttpSearch.Size = new System.Drawing.Size(180, 64);
      // 
      // deleteEntryToolStripMenuItem
      // 
      this.deleteEntryToolStripMenuItem.Name = "deleteEntryToolStripMenuItem";
      this.deleteEntryToolStripMenuItem.Size = new System.Drawing.Size(179, 30);
      this.deleteEntryToolStripMenuItem.Text = "Delete entry";
      this.deleteEntryToolStripMenuItem.Click += new System.EventHandler(this.TSMI_Delete_Click);
      // 
      // clearListToolStripMenuItem
      // 
      this.clearListToolStripMenuItem.Name = "clearListToolStripMenuItem";
      this.clearListToolStripMenuItem.Size = new System.Drawing.Size(179, 30);
      this.clearListToolStripMenuItem.Text = "Clear list";
      this.clearListToolStripMenuItem.Click += new System.EventHandler(this.TSMI_Clear_Click);
      // 
      // Plugin_HttpSearch
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.Controls.Add(this.cb_Type);
      this.Controls.Add(this.L_Type);
      this.Controls.Add(this.tb_DataRegex);
      this.Controls.Add(this.L_DataRegex);
      this.Controls.Add(this.rb_Body);
      this.Controls.Add(this.rb_Header);
      this.Controls.Add(this.bt_Add);
      this.Controls.Add(this.cb_Method);
      this.Controls.Add(this.tb_PathRegex);
      this.Controls.Add(this.L_Path);
      this.Controls.Add(this.L_Host);
      this.Controls.Add(this.tb_HostRegex);
      this.Controls.Add(this.L_Method);
      this.Controls.Add(this.dgv_HttpSearch);
      this.DoubleBuffered = true;
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "Plugin_HttpSearch";
      this.Size = new System.Drawing.Size(1494, 566);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_HttpSearch)).EndInit();
      this.cms_HttpSearch.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView dgv_HttpSearch;
    private System.Windows.Forms.Label L_Method;
    private System.Windows.Forms.TextBox tb_HostRegex;
    private System.Windows.Forms.Label L_Host;
    private System.Windows.Forms.Label L_Path;
    private System.Windows.Forms.TextBox tb_PathRegex;
    private System.Windows.Forms.ComboBox cb_Method;
    private System.Windows.Forms.Button bt_Add;
    private System.Windows.Forms.RadioButton rb_Header;
    private System.Windows.Forms.RadioButton rb_Body;
    private System.Windows.Forms.Label L_DataRegex;
    private System.Windows.Forms.TextBox tb_DataRegex;
    private System.Windows.Forms.ComboBox cb_Type;
    private System.Windows.Forms.Label L_Type;
    private System.Windows.Forms.ContextMenuStrip cms_HttpSearch;
    private System.Windows.Forms.ToolStripMenuItem deleteEntryToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem clearListToolStripMenuItem;
    private System.Windows.Forms.Timer t_GuiUpdate;
  }
}
