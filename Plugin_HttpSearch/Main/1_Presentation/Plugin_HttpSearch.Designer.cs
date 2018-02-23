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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      this.dgv_HttpSearch = new System.Windows.Forms.DataGridView();
      this.L_Method = new System.Windows.Forms.Label();
      this.tb_HostRegex = new System.Windows.Forms.TextBox();
      this.L_Host = new System.Windows.Forms.Label();
      this.L_Path = new System.Windows.Forms.Label();
      this.tb_PathRegex = new System.Windows.Forms.TextBox();
      this.cb_Method = new System.Windows.Forms.ComboBox();
      this.bt_Add = new System.Windows.Forms.Button();
      this.L_DataRegex = new System.Windows.Forms.Label();
      this.tb_DataRegex = new System.Windows.Forms.TextBox();
      this.cms_HttpSearchPatterns = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.deleteEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.clearListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.t_GuiUpdate = new System.Windows.Forms.Timer(this.components);
      this.dgv_Findings = new System.Windows.Forms.DataGridView();
      this.gb_Patterns = new System.Windows.Forms.GroupBox();
      this.cms_HttpSearchFindings = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.deleteEntryToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.clearListToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.gb_findings = new System.Windows.Forms.GroupBox();
      ((System.ComponentModel.ISupportInitialize)(this.dgv_HttpSearch)).BeginInit();
      this.cms_HttpSearchPatterns.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgv_Findings)).BeginInit();
      this.gb_Patterns.SuspendLayout();
      this.cms_HttpSearchFindings.SuspendLayout();
      this.gb_findings.SuspendLayout();
      this.SuspendLayout();
      // 
      // dgv_HttpSearch
      // 
      this.dgv_HttpSearch.AllowUserToAddRows = false;
      this.dgv_HttpSearch.AllowUserToDeleteRows = false;
      this.dgv_HttpSearch.AllowUserToResizeColumns = false;
      this.dgv_HttpSearch.AllowUserToResizeRows = false;
      this.dgv_HttpSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
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
      this.dgv_HttpSearch.Location = new System.Drawing.Point(11, 110);
      this.dgv_HttpSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.dgv_HttpSearch.MultiSelect = false;
      this.dgv_HttpSearch.Name = "dgv_HttpSearch";
      this.dgv_HttpSearch.ReadOnly = true;
      this.dgv_HttpSearch.RowHeadersVisible = false;
      this.dgv_HttpSearch.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dgv_HttpSearch.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.dgv_HttpSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv_HttpSearch.Size = new System.Drawing.Size(1374, 181);
      this.dgv_HttpSearch.TabIndex = 7;
      this.dgv_HttpSearch.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DGV_SearchPatterns_MouseDown);
      this.dgv_HttpSearch.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DGV_SearchPatterns_MouseUp);
      // 
      // L_Method
      // 
      this.L_Method.AutoSize = true;
      this.L_Method.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.L_Method.Location = new System.Drawing.Point(18, 40);
      this.L_Method.Name = "L_Method";
      this.L_Method.Size = new System.Drawing.Size(69, 20);
      this.L_Method.TabIndex = 0;
      this.L_Method.Text = "Method";
      // 
      // tb_HostRegex
      // 
      this.tb_HostRegex.Location = new System.Drawing.Point(303, 29);
      this.tb_HostRegex.Name = "tb_HostRegex";
      this.tb_HostRegex.Size = new System.Drawing.Size(272, 26);
      this.tb_HostRegex.TabIndex = 2;
      this.tb_HostRegex.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnEnterAddSearchPatternRecord);
      // 
      // L_Host
      // 
      this.L_Host.AutoSize = true;
      this.L_Host.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.L_Host.Location = new System.Drawing.Point(207, 36);
      this.L_Host.Name = "L_Host";
      this.L_Host.Size = new System.Drawing.Size(96, 20);
      this.L_Host.TabIndex = 0;
      this.L_Host.Text = "Host regex";
      // 
      // L_Path
      // 
      this.L_Path.AutoSize = true;
      this.L_Path.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.L_Path.Location = new System.Drawing.Point(589, 36);
      this.L_Path.Name = "L_Path";
      this.L_Path.Size = new System.Drawing.Size(95, 20);
      this.L_Path.TabIndex = 0;
      this.L_Path.Text = "Path regex";
      // 
      // tb_PathRegex
      // 
      this.tb_PathRegex.Location = new System.Drawing.Point(690, 29);
      this.tb_PathRegex.Name = "tb_PathRegex";
      this.tb_PathRegex.Size = new System.Drawing.Size(332, 26);
      this.tb_PathRegex.TabIndex = 3;
      this.tb_PathRegex.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnEnterAddSearchPatternRecord);
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
      this.cb_Method.Location = new System.Drawing.Point(87, 33);
      this.cb_Method.Name = "cb_Method";
      this.cb_Method.Size = new System.Drawing.Size(98, 28);
      this.cb_Method.TabIndex = 1;
      // 
      // bt_Add
      // 
      this.bt_Add.Location = new System.Drawing.Point(1041, 69);
      this.bt_Add.Margin = new System.Windows.Forms.Padding(0);
      this.bt_Add.Name = "bt_Add";
      this.bt_Add.Size = new System.Drawing.Size(30, 32);
      this.bt_Add.TabIndex = 6;
      this.bt_Add.Text = "+";
      this.bt_Add.UseVisualStyleBackColor = true;
      this.bt_Add.Click += new System.EventHandler(this.BT_AddSearchPattern_Click);
      // 
      // L_DataRegex
      // 
      this.L_DataRegex.AutoSize = true;
      this.L_DataRegex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.L_DataRegex.Location = new System.Drawing.Point(207, 80);
      this.L_DataRegex.Name = "L_DataRegex";
      this.L_DataRegex.Size = new System.Drawing.Size(97, 20);
      this.L_DataRegex.TabIndex = 0;
      this.L_DataRegex.Text = "Data regex";
      // 
      // tb_DataRegex
      // 
      this.tb_DataRegex.Location = new System.Drawing.Point(303, 74);
      this.tb_DataRegex.Name = "tb_DataRegex";
      this.tb_DataRegex.Size = new System.Drawing.Size(719, 26);
      this.tb_DataRegex.TabIndex = 5;
      this.tb_DataRegex.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnEnterAddSearchPatternRecord);
      // 
      // cms_HttpSearchPatterns
      // 
      this.cms_HttpSearchPatterns.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.cms_HttpSearchPatterns.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteEntryToolStripMenuItem,
            this.clearListToolStripMenuItem});
      this.cms_HttpSearchPatterns.Name = "cms_HttpSearch";
      this.cms_HttpSearchPatterns.Size = new System.Drawing.Size(180, 64);
      // 
      // deleteEntryToolStripMenuItem
      // 
      this.deleteEntryToolStripMenuItem.Name = "deleteEntryToolStripMenuItem";
      this.deleteEntryToolStripMenuItem.Size = new System.Drawing.Size(179, 30);
      this.deleteEntryToolStripMenuItem.Text = "Delete entry";
      this.deleteEntryToolStripMenuItem.Click += new System.EventHandler(this.TSMI_SearchPatternsDelete_Click);
      // 
      // clearListToolStripMenuItem
      // 
      this.clearListToolStripMenuItem.Name = "clearListToolStripMenuItem";
      this.clearListToolStripMenuItem.Size = new System.Drawing.Size(179, 30);
      this.clearListToolStripMenuItem.Text = "Clear list";
      this.clearListToolStripMenuItem.Click += new System.EventHandler(this.TSMI_SearchPatternsClear_Click);
      // 
      // t_GuiUpdate
      // 
      this.t_GuiUpdate.Interval = 500;
      this.t_GuiUpdate.Tick += new System.EventHandler(this.T_GuiUpdate_Tick);
      // 
      // dgv_Findings
      // 
      this.dgv_Findings.AllowUserToAddRows = false;
      this.dgv_Findings.AllowUserToDeleteRows = false;
      this.dgv_Findings.AllowUserToResizeColumns = false;
      this.dgv_Findings.AllowUserToResizeRows = false;
      this.dgv_Findings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dgv_Findings.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.dgv_Findings.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
      this.dgv_Findings.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgv_Findings.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
      this.dgv_Findings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv_Findings.EnableHeadersVisualStyles = false;
      this.dgv_Findings.Location = new System.Drawing.Point(11, 25);
      this.dgv_Findings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.dgv_Findings.MultiSelect = false;
      this.dgv_Findings.Name = "dgv_Findings";
      this.dgv_Findings.ReadOnly = true;
      this.dgv_Findings.RowHeadersVisible = false;
      this.dgv_Findings.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.dgv_Findings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv_Findings.Size = new System.Drawing.Size(1374, 215);
      this.dgv_Findings.TabIndex = 8;
      this.dgv_Findings.DoubleClick += new System.EventHandler(this.DGV_HttpFindings_DoubleClick);
      this.dgv_Findings.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DGV_Findings_MouseDown);
      this.dgv_Findings.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DGV_Findings_MouseUp);
      // 
      // gb_Patterns
      // 
      this.gb_Patterns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.gb_Patterns.Controls.Add(this.L_Method);
      this.gb_Patterns.Controls.Add(this.dgv_HttpSearch);
      this.gb_Patterns.Controls.Add(this.tb_HostRegex);
      this.gb_Patterns.Controls.Add(this.L_Host);
      this.gb_Patterns.Controls.Add(this.L_Path);
      this.gb_Patterns.Controls.Add(this.tb_DataRegex);
      this.gb_Patterns.Controls.Add(this.tb_PathRegex);
      this.gb_Patterns.Controls.Add(this.L_DataRegex);
      this.gb_Patterns.Controls.Add(this.cb_Method);
      this.gb_Patterns.Controls.Add(this.bt_Add);
      this.gb_Patterns.Location = new System.Drawing.Point(26, 15);
      this.gb_Patterns.Name = "gb_Patterns";
      this.gb_Patterns.Size = new System.Drawing.Size(1400, 304);
      this.gb_Patterns.TabIndex = 9;
      this.gb_Patterns.TabStop = false;
      this.gb_Patterns.Text = "Patterns";
      // 
      // cms_HttpSearchFindings
      // 
      this.cms_HttpSearchFindings.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.cms_HttpSearchFindings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteEntryToolStripMenuItem1,
            this.clearListToolStripMenuItem1});
      this.cms_HttpSearchFindings.Name = "cms_HttpSearchFindings";
      this.cms_HttpSearchFindings.Size = new System.Drawing.Size(180, 64);
      // 
      // deleteEntryToolStripMenuItem1
      // 
      this.deleteEntryToolStripMenuItem1.Name = "deleteEntryToolStripMenuItem1";
      this.deleteEntryToolStripMenuItem1.Size = new System.Drawing.Size(179, 30);
      this.deleteEntryToolStripMenuItem1.Text = "Delete entry";
      this.deleteEntryToolStripMenuItem1.Click += new System.EventHandler(this.TSMI_FindingsDelete_Click);
      // 
      // clearListToolStripMenuItem1
      // 
      this.clearListToolStripMenuItem1.Name = "clearListToolStripMenuItem1";
      this.clearListToolStripMenuItem1.Size = new System.Drawing.Size(179, 30);
      this.clearListToolStripMenuItem1.Text = "Clear list";
      this.clearListToolStripMenuItem1.Click += new System.EventHandler(this.TSMI_FindingsClear_Click);
      // 
      // gb_findings
      // 
      this.gb_findings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.gb_findings.Controls.Add(this.dgv_Findings);
      this.gb_findings.Location = new System.Drawing.Point(26, 332);
      this.gb_findings.Name = "gb_findings";
      this.gb_findings.Size = new System.Drawing.Size(1400, 248);
      this.gb_findings.TabIndex = 0;
      this.gb_findings.TabStop = false;
      this.gb_findings.Text = "Findings";
      // 
      // Plugin_HttpSearch
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.Controls.Add(this.gb_Patterns);
      this.Controls.Add(this.gb_findings);
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "Plugin_HttpSearch";
      this.Size = new System.Drawing.Size(1494, 583);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_HttpSearch)).EndInit();
      this.cms_HttpSearchPatterns.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_Findings)).EndInit();
      this.gb_Patterns.ResumeLayout(false);
      this.gb_Patterns.PerformLayout();
      this.cms_HttpSearchFindings.ResumeLayout(false);
      this.gb_findings.ResumeLayout(false);
      this.ResumeLayout(false);

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
    private System.Windows.Forms.Label L_DataRegex;
    private System.Windows.Forms.TextBox tb_DataRegex;
    private System.Windows.Forms.ContextMenuStrip cms_HttpSearchPatterns;
    private System.Windows.Forms.ToolStripMenuItem deleteEntryToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem clearListToolStripMenuItem;
    private System.Windows.Forms.Timer t_GuiUpdate;
    private System.Windows.Forms.DataGridView dgv_Findings;
    private System.Windows.Forms.GroupBox gb_Patterns;
    private System.Windows.Forms.ContextMenuStrip cms_HttpSearchFindings;
    private System.Windows.Forms.ToolStripMenuItem deleteEntryToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem clearListToolStripMenuItem1;
    private System.Windows.Forms.GroupBox gb_findings;
  }
}
