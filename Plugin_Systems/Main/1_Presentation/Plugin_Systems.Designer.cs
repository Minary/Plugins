using System.Windows.Forms;
namespace Minary.Plugin.Main
{
  public partial class Plugin_Systems 
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }



        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      this.cms_Systems = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.deleteEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.tsmi_Clear = new System.Windows.Forms.ToolStripMenuItem();
      this.dgv_Systems = new System.Windows.Forms.DataGridView();
      this.t_GUIUpdate = new System.Windows.Forms.Timer(this.components);
      this.cms_Systems.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgv_Systems)).BeginInit();
      this.SuspendLayout();
      // 
      // cms_Systems
      // 
      this.cms_Systems.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.cms_Systems.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteEntryToolStripMenuItem,
            this.tsmi_Clear});
      this.cms_Systems.Name = "cms_Systems";
      this.cms_Systems.Size = new System.Drawing.Size(180, 64);
      // 
      // deleteEntryToolStripMenuItem
      // 
      this.deleteEntryToolStripMenuItem.Name = "deleteEntryToolStripMenuItem";
      this.deleteEntryToolStripMenuItem.Size = new System.Drawing.Size(179, 30);
      this.deleteEntryToolStripMenuItem.Text = "Delete entry";
      this.deleteEntryToolStripMenuItem.Click += new System.EventHandler(this.DeleteEntryToolStripMenuItem_Click);
      // 
      // tsmi_Clear
      // 
      this.tsmi_Clear.Name = "tsmi_Clear";
      this.tsmi_Clear.Size = new System.Drawing.Size(179, 30);
      this.tsmi_Clear.Text = "Clear list";
      this.tsmi_Clear.Click += new System.EventHandler(this.TSMI_Clear_Click);
      // 
      // dgv_Systems
      // 
      this.dgv_Systems.AllowUserToAddRows = false;
      this.dgv_Systems.AllowUserToDeleteRows = false;
      this.dgv_Systems.AllowUserToResizeColumns = false;
      this.dgv_Systems.AllowUserToResizeRows = false;
      this.dgv_Systems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dgv_Systems.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.dgv_Systems.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
      this.dgv_Systems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgv_Systems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dgv_Systems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv_Systems.Location = new System.Drawing.Point(26, 29);
      this.dgv_Systems.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.dgv_Systems.MultiSelect = false;
      this.dgv_Systems.Name = "dgv_Systems";
      this.dgv_Systems.RowHeadersVisible = false;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dgv_Systems.RowsDefaultCellStyle = dataGridViewCellStyle2;
      this.dgv_Systems.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dgv_Systems.RowTemplate.Height = 20;
      this.dgv_Systems.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.dgv_Systems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv_Systems.Size = new System.Drawing.Size(1400, 537);
      this.dgv_Systems.TabIndex = 1;
      this.dgv_Systems.DoubleClick += new System.EventHandler(this.DGV_Systems_DoubleClick);
      this.dgv_Systems.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DGV_Systems_MouseDown);
      this.dgv_Systems.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DGV_Systems_MouseUp_1);
      // 
      // t_GUIUpdate
      // 
      this.t_GUIUpdate.Interval = 500;
      this.t_GUIUpdate.Tick += new System.EventHandler(this.T_GUIUpdate_Tick);
      // 
      // Plugin_Systems
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.Controls.Add(this.dgv_Systems);
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "Plugin_Systems";
      this.Size = new System.Drawing.Size(1494, 583);
      this.cms_Systems.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_Systems)).EndInit();
      this.ResumeLayout(false);

        }

        #endregion

        private ContextMenuStrip cms_Systems;
        private ToolStripMenuItem tsmi_Clear;
        private DataGridView dgv_Systems;
        private ToolStripMenuItem deleteEntryToolStripMenuItem;
        private Timer t_GUIUpdate;



    }
}
