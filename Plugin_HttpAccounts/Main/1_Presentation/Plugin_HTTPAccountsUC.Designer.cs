namespace Plugin.Main
{
  partial class PluginHTTPAccountsUC
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
            this.DGV_Accounts = new System.Windows.Forms.DataGridView();
            this.CMS_HTTPAccounts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TSMI_DeleteEntry = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_Clear = new System.Windows.Forms.ToolStripMenuItem();
            this.T_GUIUpdate = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Accounts)).BeginInit();
            this.CMS_HTTPAccounts.SuspendLayout();
            this.SuspendLayout();
            // 
            // DGV_Accounts
            // 
            this.DGV_Accounts.AllowUserToAddRows = false;
            this.DGV_Accounts.AllowUserToDeleteRows = false;
            this.DGV_Accounts.AllowUserToResizeColumns = false;
            this.DGV_Accounts.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV_Accounts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGV_Accounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Accounts.Location = new System.Drawing.Point(17, 19);
            this.DGV_Accounts.MultiSelect = false;
            this.DGV_Accounts.Name = "DGV_Accounts";
            this.DGV_Accounts.RowHeadersVisible = false;
            this.DGV_Accounts.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DGV_Accounts.RowTemplate.Height = 20;
            this.DGV_Accounts.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DGV_Accounts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGV_Accounts.Size = new System.Drawing.Size(830, 334);
            this.DGV_Accounts.TabIndex = 3;
            this.DGV_Accounts.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_Accounts_CellContentClick);
            this.DGV_Accounts.DoubleClick += new System.EventHandler(this.DGV_Accounts_DoubleClick);
            this.DGV_Accounts.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DGV_Accounts_MouseUp);
            // 
            // CMS_HTTPAccounts
            // 
            this.CMS_HTTPAccounts.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.CMS_HTTPAccounts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_DeleteEntry,
            this.TSMI_Clear});
            this.CMS_HTTPAccounts.Name = "CMS_HTTPAccounts";
            this.CMS_HTTPAccounts.Size = new System.Drawing.Size(153, 70);
            // 
            // TSMI_DeleteEntry
            // 
            this.TSMI_DeleteEntry.Name = "TSMI_DeleteEntry";
            this.TSMI_DeleteEntry.Size = new System.Drawing.Size(152, 22);
            this.TSMI_DeleteEntry.Text = "Delete entry";
            this.TSMI_DeleteEntry.Click += new System.EventHandler(this.DeleteEntryToolStripMenuItem_Click);
            // 
            // TSMI_Clear
            // 
            this.TSMI_Clear.Name = "TSMI_Clear";
            this.TSMI_Clear.Size = new System.Drawing.Size(152, 22);
            this.TSMI_Clear.Text = "Clear list";
            this.TSMI_Clear.Click += new System.EventHandler(this.TSMI_Clear_Click);
            // 
            // T_GUIUpdate
            // 
            this.T_GUIUpdate.Interval = 500;
            this.T_GUIUpdate.Tick += new System.EventHandler(this.T_GUIUpdate_Tick);
            // 
            // PluginHTTPAccountsUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.DGV_Accounts);
            this.Name = "PluginHTTPAccountsUC";
            this.Size = new System.Drawing.Size(996, 368);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Accounts)).EndInit();
            this.CMS_HTTPAccounts.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGV_Accounts;
        private System.Windows.Forms.ContextMenuStrip CMS_HTTPAccounts;
        private System.Windows.Forms.ToolStripMenuItem TSMI_Clear;
        private System.Windows.Forms.ToolStripMenuItem TSMI_DeleteEntry;
        private System.Windows.Forms.Timer T_GUIUpdate;

    }
}
