namespace Minary.Plugin.Main
{
  public partial class Plugin_HttpAccounts
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
      this.dgv_Accounts = new System.Windows.Forms.DataGridView();
      this.cms_HTTPAccounts = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.tsmi_DeleteEntry = new System.Windows.Forms.ToolStripMenuItem();
      this.tsmi_Clear = new System.Windows.Forms.ToolStripMenuItem();
      this.t_GuiUpdate = new System.Windows.Forms.Timer(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_Accounts)).BeginInit();
      this.cms_HTTPAccounts.SuspendLayout();
      this.SuspendLayout();
      // 
      // dgv_Accounts
      // 
      this.dgv_Accounts.AllowUserToAddRows = false;
      this.dgv_Accounts.AllowUserToDeleteRows = false;
      this.dgv_Accounts.AllowUserToResizeColumns = false;
      this.dgv_Accounts.AllowUserToResizeRows = false;
      this.dgv_Accounts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dgv_Accounts.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.dgv_Accounts.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
      this.dgv_Accounts.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgv_Accounts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dgv_Accounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv_Accounts.EnableHeadersVisualStyles = false;
      this.dgv_Accounts.Location = new System.Drawing.Point(26, 29);
      this.dgv_Accounts.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.dgv_Accounts.MultiSelect = false;
      this.dgv_Accounts.Name = "dgv_Accounts";
      this.dgv_Accounts.RowHeadersVisible = false;
      this.dgv_Accounts.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dgv_Accounts.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.dgv_Accounts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv_Accounts.Size = new System.Drawing.Size(1400, 537);
      this.dgv_Accounts.TabIndex = 3;
      this.dgv_Accounts.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_Accounts_CellContentClick);
      this.dgv_Accounts.DoubleClick += new System.EventHandler(this.DGV_Accounts_DoubleClick);
      this.dgv_Accounts.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DGV_Accounts_MouseUp);
      // 
      // cms_HTTPAccounts
      // 
      this.cms_HTTPAccounts.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.cms_HTTPAccounts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_DeleteEntry,
            this.tsmi_Clear});
      this.cms_HTTPAccounts.Name = "cms_HTTPAccounts";
      this.cms_HTTPAccounts.Size = new System.Drawing.Size(180, 64);
      // 
      // tsmi_DeleteEntry
      // 
      this.tsmi_DeleteEntry.Name = "tsmi_DeleteEntry";
      this.tsmi_DeleteEntry.Size = new System.Drawing.Size(179, 30);
      this.tsmi_DeleteEntry.Text = "Delete entry";
      this.tsmi_DeleteEntry.Click += new System.EventHandler(this.DeleteEntryToolStripMenuItem_Click);
      // 
      // tsmi_Clear
      // 
      this.tsmi_Clear.Name = "tsmi_Clear";
      this.tsmi_Clear.Size = new System.Drawing.Size(179, 30);
      this.tsmi_Clear.Text = "Clear list";
      this.tsmi_Clear.Click += new System.EventHandler(this.TSMI_Clear_Click);
      // 
      // t_GuiUpdate
      // 
      this.t_GuiUpdate.Interval = 500;
      this.t_GuiUpdate.Tick += new System.EventHandler(this.T_GUIUpdate_Tick);
      // 
      // Plugin_HttpAccounts
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.Controls.Add(this.dgv_Accounts);
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "Plugin_HttpAccounts";
      this.Size = new System.Drawing.Size(1494, 583);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_Accounts)).EndInit();
      this.cms_HTTPAccounts.ResumeLayout(false);
      this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_Accounts;
        private System.Windows.Forms.ContextMenuStrip cms_HTTPAccounts;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Clear;
        private System.Windows.Forms.ToolStripMenuItem tsmi_DeleteEntry;
        private System.Windows.Forms.Timer t_GuiUpdate;

    }
}
