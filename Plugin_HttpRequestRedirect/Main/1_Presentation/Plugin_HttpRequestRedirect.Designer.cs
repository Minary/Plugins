namespace Minary.Plugin.Main
{
  partial class Plugin_HttpRequestRedirect
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      this.tb_RedirectURL = new System.Windows.Forms.TextBox();
      this.dgv_RequestRedirectURLs = new System.Windows.Forms.DataGridView();
      this.tb_RequestedURLRegex = new System.Windows.Forms.TextBox();
      this.l_RequestedURL = new System.Windows.Forms.Label();
      this.l_RedirectURL = new System.Windows.Forms.Label();
      this.bt_AddRecord = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.dgv_RequestRedirectURLs)).BeginInit();
      this.SuspendLayout();
      // 
      // tb_RedirectURL
      // 
      this.tb_RedirectURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tb_RedirectURL.Location = new System.Drawing.Point(493, 14);
      this.tb_RedirectURL.Name = "tb_RedirectURL";
      this.tb_RedirectURL.Size = new System.Drawing.Size(232, 20);
      this.tb_RedirectURL.TabIndex = 2;
      // 
      // dgv_RequestRedirectURLs
      // 
      this.dgv_RequestRedirectURLs.AllowUserToAddRows = false;
      this.dgv_RequestRedirectURLs.AllowUserToDeleteRows = false;
      this.dgv_RequestRedirectURLs.AllowUserToResizeColumns = false;
      this.dgv_RequestRedirectURLs.AllowUserToResizeRows = false;
      this.dgv_RequestRedirectURLs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dgv_RequestRedirectURLs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.dgv_RequestRedirectURLs.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
      this.dgv_RequestRedirectURLs.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
      this.dgv_RequestRedirectURLs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv_RequestRedirectURLs.Location = new System.Drawing.Point(17, 44);
      this.dgv_RequestRedirectURLs.MultiSelect = false;
      this.dgv_RequestRedirectURLs.Name = "dgv_RequestRedirectURLs";
      this.dgv_RequestRedirectURLs.RowHeadersVisible = false;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
      this.dgv_RequestRedirectURLs.RowsDefaultCellStyle = dataGridViewCellStyle2;
      this.dgv_RequestRedirectURLs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.dgv_RequestRedirectURLs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv_RequestRedirectURLs.Size = new System.Drawing.Size(933, 313);
      this.dgv_RequestRedirectURLs.TabIndex = 7;
      this.dgv_RequestRedirectURLs.TabStop = false;
      // 
      // tb_RequestedURLRegex
      // 
      this.tb_RequestedURLRegex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tb_RequestedURLRegex.Location = new System.Drawing.Point(126, 16);
      this.tb_RequestedURLRegex.Name = "tb_RequestedURLRegex";
      this.tb_RequestedURLRegex.Size = new System.Drawing.Size(231, 20);
      this.tb_RequestedURLRegex.TabIndex = 1;
      // 
      // l_RequestedURL
      // 
      this.l_RequestedURL.AutoSize = true;
      this.l_RequestedURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_RequestedURL.Location = new System.Drawing.Point(23, 19);
      this.l_RequestedURL.Name = "l_RequestedURL";
      this.l_RequestedURL.Size = new System.Drawing.Size(97, 13);
      this.l_RequestedURL.TabIndex = 0;
      this.l_RequestedURL.Text = "Requested URL";
      // 
      // l_RedirectURL
      // 
      this.l_RedirectURL.AutoSize = true;
      this.l_RedirectURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_RedirectURL.Location = new System.Drawing.Point(387, 18);
      this.l_RedirectURL.Name = "l_RedirectURL";
      this.l_RedirectURL.Size = new System.Drawing.Size(99, 13);
      this.l_RedirectURL.TabIndex = 0;
      this.l_RedirectURL.Text = "Redirect to URL";
      // 
      // bt_AddRecord
      // 
      this.bt_AddRecord.Location = new System.Drawing.Point(745, 14);
      this.bt_AddRecord.Name = "bt_AddRecord";
      this.bt_AddRecord.Size = new System.Drawing.Size(23, 21);
      this.bt_AddRecord.TabIndex = 3;
      this.bt_AddRecord.Text = "+";
      this.bt_AddRecord.UseVisualStyleBackColor = true;
      this.bt_AddRecord.Click += new System.EventHandler(this.BT_Add_Click);
      // 
      // Plugin_HttpRequestRedirect
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.bt_AddRecord);
      this.Controls.Add(this.l_RedirectURL);
      this.Controls.Add(this.tb_RedirectURL);
      this.Controls.Add(this.dgv_RequestRedirectURLs);
      this.Controls.Add(this.tb_RequestedURLRegex);
      this.Controls.Add(this.l_RequestedURL);
      this.Name = "Plugin_HttpRequestRedirect";
      this.Size = new System.Drawing.Size(996, 368);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_RequestRedirectURLs)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.TextBox tb_RedirectURL;
    private System.Windows.Forms.DataGridView dgv_RequestRedirectURLs;
    private System.Windows.Forms.TextBox tb_RequestedURLRegex;
    private System.Windows.Forms.Label l_RequestedURL;
    private System.Windows.Forms.Label l_RedirectURL;
    private System.Windows.Forms.Button bt_AddRecord;
  }
}
