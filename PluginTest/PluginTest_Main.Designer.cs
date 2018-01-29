namespace PluginTest
{
  public partial class PluginTest_MainForm
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
      this.gb_PluginTest = new System.Windows.Forms.GroupBox();
      this.bt_PluginPath = new System.Windows.Forms.Button();
      this.tb_PluginPath = new System.Windows.Forms.TextBox();
      this.l_Plugin = new System.Windows.Forms.Label();
      this.ofd_PluginPath = new System.Windows.Forms.OpenFileDialog();
      this.gb_Events = new System.Windows.Forms.GroupBox();
      this.tb_NewData = new System.Windows.Forms.TextBox();
      this.button2 = new System.Windows.Forms.Button();
      this.bt_OnNewData = new System.Windows.Forms.Button();
      this.bt_OnStopAttack = new System.Windows.Forms.Button();
      this.bt_OnStartAttack = new System.Windows.Forms.Button();
      this.bt_OnReset = new System.Windows.Forms.Button();
      this.bt_OnInit = new System.Windows.Forms.Button();
      this.gb_Logs = new System.Windows.Forms.GroupBox();
      this.tb_Logs = new System.Windows.Forms.TextBox();
      this.TC_PluginTester = new System.Windows.Forms.TabControl();
      this.tp_Oper = new System.Windows.Forms.TabPage();
      this.gb_PluginTest.SuspendLayout();
      this.gb_Events.SuspendLayout();
      this.gb_Logs.SuspendLayout();
      this.TC_PluginTester.SuspendLayout();
      this.tp_Oper.SuspendLayout();
      this.SuspendLayout();
      // 
      // gb_PluginTest
      // 
      this.gb_PluginTest.Controls.Add(this.bt_PluginPath);
      this.gb_PluginTest.Controls.Add(this.tb_PluginPath);
      this.gb_PluginTest.Controls.Add(this.l_Plugin);
      this.gb_PluginTest.Location = new System.Drawing.Point(13, 13);
      this.gb_PluginTest.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      this.gb_PluginTest.Name = "gb_PluginTest";
      this.gb_PluginTest.Size = new System.Drawing.Size(1087, 78);
      this.gb_PluginTest.TabIndex = 0;
      this.gb_PluginTest.TabStop = false;
      // 
      // bt_PluginPath
      // 
      this.bt_PluginPath.Location = new System.Drawing.Point(874, 29);
      this.bt_PluginPath.Name = "bt_PluginPath";
      this.bt_PluginPath.Size = new System.Drawing.Size(21, 23);
      this.bt_PluginPath.TabIndex = 2;
      this.bt_PluginPath.Text = "+";
      this.bt_PluginPath.UseVisualStyleBackColor = true;
      this.bt_PluginPath.Click += new System.EventHandler(this.BT_PluginPath_Click);
      // 
      // tb_PluginPath
      // 
      this.tb_PluginPath.Location = new System.Drawing.Point(78, 26);
      this.tb_PluginPath.Name = "tb_PluginPath";
      this.tb_PluginPath.Size = new System.Drawing.Size(790, 26);
      this.tb_PluginPath.TabIndex = 1;
      // 
      // l_Plugin
      // 
      this.l_Plugin.AutoSize = true;
      this.l_Plugin.Location = new System.Drawing.Point(19, 26);
      this.l_Plugin.Name = "l_Plugin";
      this.l_Plugin.Size = new System.Drawing.Size(52, 20);
      this.l_Plugin.TabIndex = 0;
      this.l_Plugin.Text = "Plugin";
      // 
      // gb_Events
      // 
      this.gb_Events.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.gb_Events.Controls.Add(this.tb_NewData);
      this.gb_Events.Controls.Add(this.button2);
      this.gb_Events.Controls.Add(this.bt_OnNewData);
      this.gb_Events.Controls.Add(this.bt_OnStopAttack);
      this.gb_Events.Controls.Add(this.bt_OnStartAttack);
      this.gb_Events.Controls.Add(this.bt_OnReset);
      this.gb_Events.Controls.Add(this.bt_OnInit);
      this.gb_Events.Location = new System.Drawing.Point(12, 28);
      this.gb_Events.Name = "gb_Events";
      this.gb_Events.Size = new System.Drawing.Size(1051, 187);
      this.gb_Events.TabIndex = 1;
      this.gb_Events.TabStop = false;
      this.gb_Events.Text = "Events";
      // 
      // tb_NewData
      // 
      this.tb_NewData.Location = new System.Drawing.Point(398, 44);
      this.tb_NewData.Name = "tb_NewData";
      this.tb_NewData.Size = new System.Drawing.Size(600, 26);
      this.tb_NewData.TabIndex = 6;
      this.tb_NewData.Text = "TCP||11-22-33-44-55-66||192.168.0.101||12345||8.8.8.8||53||auth.facebook.com";
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(16, 131);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(118, 41);
      this.button2.TabIndex = 5;
      this.button2.Text = "OnShutdown";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.BT_OnShutdown_Click);
      // 
      // bt_OnNewData
      // 
      this.bt_OnNewData.Location = new System.Drawing.Point(269, 37);
      this.bt_OnNewData.Name = "bt_OnNewData";
      this.bt_OnNewData.Size = new System.Drawing.Size(123, 41);
      this.bt_OnNewData.TabIndex = 4;
      this.bt_OnNewData.Text = "OnNewData";
      this.bt_OnNewData.UseVisualStyleBackColor = true;
      this.bt_OnNewData.Click += new System.EventHandler(this.BT_OnNewData_Click);
      // 
      // bt_OnStopAttack
      // 
      this.bt_OnStopAttack.Location = new System.Drawing.Point(140, 84);
      this.bt_OnStopAttack.Name = "bt_OnStopAttack";
      this.bt_OnStopAttack.Size = new System.Drawing.Size(123, 41);
      this.bt_OnStopAttack.TabIndex = 3;
      this.bt_OnStopAttack.Text = "OnStopAttack";
      this.bt_OnStopAttack.UseVisualStyleBackColor = true;
      this.bt_OnStopAttack.Click += new System.EventHandler(this.bt_StopAttack_Click);
      // 
      // bt_OnStartAttack
      // 
      this.bt_OnStartAttack.Location = new System.Drawing.Point(140, 37);
      this.bt_OnStartAttack.Name = "bt_OnStartAttack";
      this.bt_OnStartAttack.Size = new System.Drawing.Size(123, 41);
      this.bt_OnStartAttack.TabIndex = 2;
      this.bt_OnStartAttack.Text = "OnStartAttack";
      this.bt_OnStartAttack.UseVisualStyleBackColor = true;
      this.bt_OnStartAttack.Click += new System.EventHandler(this.bt_StartAttack_Click);
      // 
      // bt_OnReset
      // 
      this.bt_OnReset.Location = new System.Drawing.Point(16, 84);
      this.bt_OnReset.Name = "bt_OnReset";
      this.bt_OnReset.Size = new System.Drawing.Size(118, 41);
      this.bt_OnReset.TabIndex = 1;
      this.bt_OnReset.Text = "OnReset";
      this.bt_OnReset.UseVisualStyleBackColor = true;
      this.bt_OnReset.Click += new System.EventHandler(this.bt_Reset_Click);
      // 
      // bt_OnInit
      // 
      this.bt_OnInit.Location = new System.Drawing.Point(16, 37);
      this.bt_OnInit.Name = "bt_OnInit";
      this.bt_OnInit.Size = new System.Drawing.Size(118, 41);
      this.bt_OnInit.TabIndex = 0;
      this.bt_OnInit.Text = "OnInit";
      this.bt_OnInit.UseVisualStyleBackColor = true;
      this.bt_OnInit.Click += new System.EventHandler(this.BT_OnInit_Click);
      // 
      // gb_Logs
      // 
      this.gb_Logs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.gb_Logs.Controls.Add(this.tb_Logs);
      this.gb_Logs.Location = new System.Drawing.Point(12, 235);
      this.gb_Logs.Name = "gb_Logs";
      this.gb_Logs.Size = new System.Drawing.Size(1051, 216);
      this.gb_Logs.TabIndex = 2;
      this.gb_Logs.TabStop = false;
      this.gb_Logs.Text = "Logs";
      // 
      // tb_Logs
      // 
      this.tb_Logs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tb_Logs.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.tb_Logs.Location = new System.Drawing.Point(7, 26);
      this.tb_Logs.Multiline = true;
      this.tb_Logs.Name = "tb_Logs";
      this.tb_Logs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.tb_Logs.Size = new System.Drawing.Size(1023, 167);
      this.tb_Logs.TabIndex = 0;
      // 
      // TC_PluginTester
      // 
      this.TC_PluginTester.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TC_PluginTester.Controls.Add(this.tp_Oper);
      this.TC_PluginTester.Location = new System.Drawing.Point(13, 97);
      this.TC_PluginTester.Name = "TC_PluginTester";
      this.TC_PluginTester.SelectedIndex = 0;
      this.TC_PluginTester.Size = new System.Drawing.Size(1091, 504);
      this.TC_PluginTester.TabIndex = 3;
      // 
      // tp_Oper
      // 
      this.tp_Oper.Controls.Add(this.gb_Logs);
      this.tp_Oper.Controls.Add(this.gb_Events);
      this.tp_Oper.Location = new System.Drawing.Point(4, 29);
      this.tp_Oper.Name = "tp_Oper";
      this.tp_Oper.Padding = new System.Windows.Forms.Padding(3);
      this.tp_Oper.Size = new System.Drawing.Size(1083, 471);
      this.tp_Oper.TabIndex = 0;
      this.tp_Oper.Text = "Plugin oper";
      this.tp_Oper.UseVisualStyleBackColor = true;
      // 
      // PluginTest_MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1116, 613);
      this.Controls.Add(this.TC_PluginTester);
      this.Controls.Add(this.gb_PluginTest);
      this.Name = "PluginTest_MainForm";
      this.Text = "Plugin Tester";
      this.gb_PluginTest.ResumeLayout(false);
      this.gb_PluginTest.PerformLayout();
      this.gb_Events.ResumeLayout(false);
      this.gb_Events.PerformLayout();
      this.gb_Logs.ResumeLayout(false);
      this.gb_Logs.PerformLayout();
      this.TC_PluginTester.ResumeLayout(false);
      this.tp_Oper.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox gb_PluginTest;
    private System.Windows.Forms.Button bt_PluginPath;
    private System.Windows.Forms.TextBox tb_PluginPath;
    private System.Windows.Forms.Label l_Plugin;
    private System.Windows.Forms.OpenFileDialog ofd_PluginPath;
    private System.Windows.Forms.GroupBox gb_Events;
    private System.Windows.Forms.Button bt_OnInit;
    private System.Windows.Forms.GroupBox gb_Logs;
    private System.Windows.Forms.TextBox tb_Logs;
    private System.Windows.Forms.Button bt_OnReset;
    private System.Windows.Forms.Button bt_OnStartAttack;
    private System.Windows.Forms.Button bt_OnStopAttack;
    private System.Windows.Forms.Button bt_OnNewData;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.TextBox tb_NewData;
    private System.Windows.Forms.TabControl TC_PluginTester;
    private System.Windows.Forms.TabPage tp_Oper;
  }
}

