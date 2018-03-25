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
      this.cb_PluginSelection = new System.Windows.Forms.ComboBox();
      this.tb_PluginPath = new System.Windows.Forms.TextBox();
      this.l_Plugin = new System.Windows.Forms.Label();
      this.ofd_PluginPath = new System.Windows.Forms.OpenFileDialog();
      this.gb_Events = new System.Windows.Forms.GroupBox();
      this.bt_OnLoadTemplate = new System.Windows.Forms.Button();
      this.rb_HttpExample = new System.Windows.Forms.RadioButton();
      this.rb_DnsExample = new System.Windows.Forms.RadioButton();
      this.tb_NewData = new System.Windows.Forms.TextBox();
      this.bt_OnShutdown = new System.Windows.Forms.Button();
      this.bt_OnNewData = new System.Windows.Forms.Button();
      this.bt_OnStopAttack = new System.Windows.Forms.Button();
      this.bt_OnStartAttack = new System.Windows.Forms.Button();
      this.bt_OnReset = new System.Windows.Forms.Button();
      this.bt_OnInit = new System.Windows.Forms.Button();
      this.gb_Logs = new System.Windows.Forms.GroupBox();
      this.tb_Logs = new System.Windows.Forms.TextBox();
      this.TC_PluginTester = new System.Windows.Forms.TabControl();
      this.tp_Oper = new System.Windows.Forms.TabPage();
      this.ofd_LoadTemplate = new System.Windows.Forms.OpenFileDialog();
      this.gb_PluginTest.SuspendLayout();
      this.gb_Events.SuspendLayout();
      this.gb_Logs.SuspendLayout();
      this.TC_PluginTester.SuspendLayout();
      this.tp_Oper.SuspendLayout();
      this.SuspendLayout();
      // 
      // gb_PluginTest
      // 
      this.gb_PluginTest.Controls.Add(this.cb_PluginSelection);
      this.gb_PluginTest.Controls.Add(this.tb_PluginPath);
      this.gb_PluginTest.Controls.Add(this.l_Plugin);
      this.gb_PluginTest.Location = new System.Drawing.Point(13, 13);
      this.gb_PluginTest.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      this.gb_PluginTest.Name = "gb_PluginTest";
      this.gb_PluginTest.Size = new System.Drawing.Size(1225, 78);
      this.gb_PluginTest.TabIndex = 0;
      this.gb_PluginTest.TabStop = false;
      // 
      // cb_PluginSelection
      // 
      this.cb_PluginSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cb_PluginSelection.FormattingEnabled = true;
      this.cb_PluginSelection.Location = new System.Drawing.Point(962, 26);
      this.cb_PluginSelection.Name = "cb_PluginSelection";
      this.cb_PluginSelection.Size = new System.Drawing.Size(243, 28);
      this.cb_PluginSelection.TabIndex = 3;
      this.cb_PluginSelection.SelectedIndexChanged += new System.EventHandler(this.CB_PluginSelection_SelectedIndexChanged);
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
      this.gb_Events.Controls.Add(this.bt_OnLoadTemplate);
      this.gb_Events.Controls.Add(this.rb_HttpExample);
      this.gb_Events.Controls.Add(this.rb_DnsExample);
      this.gb_Events.Controls.Add(this.tb_NewData);
      this.gb_Events.Controls.Add(this.bt_OnShutdown);
      this.gb_Events.Controls.Add(this.bt_OnNewData);
      this.gb_Events.Controls.Add(this.bt_OnStopAttack);
      this.gb_Events.Controls.Add(this.bt_OnStartAttack);
      this.gb_Events.Controls.Add(this.bt_OnReset);
      this.gb_Events.Controls.Add(this.bt_OnInit);
      this.gb_Events.Location = new System.Drawing.Point(12, 28);
      this.gb_Events.Name = "gb_Events";
      this.gb_Events.Size = new System.Drawing.Size(1209, 187);
      this.gb_Events.TabIndex = 1;
      this.gb_Events.TabStop = false;
      this.gb_Events.Text = "Events";
      // 
      // bt_OnLoadTemplate
      // 
      this.bt_OnLoadTemplate.Location = new System.Drawing.Point(279, 37);
      this.bt_OnLoadTemplate.Name = "bt_OnLoadTemplate";
      this.bt_OnLoadTemplate.Size = new System.Drawing.Size(123, 41);
      this.bt_OnLoadTemplate.TabIndex = 6;
      this.bt_OnLoadTemplate.Text = "OnLoadTempl.";
      this.bt_OnLoadTemplate.UseVisualStyleBackColor = true;
      this.bt_OnLoadTemplate.Click += new System.EventHandler(this.BT_OnLoadTemplateData_Click);
      // 
      // rb_HttpExample
      // 
      this.rb_HttpExample.AutoSize = true;
      this.rb_HttpExample.Checked = true;
      this.rb_HttpExample.Location = new System.Drawing.Point(652, 45);
      this.rb_HttpExample.Name = "rb_HttpExample";
      this.rb_HttpExample.Size = new System.Drawing.Size(74, 24);
      this.rb_HttpExample.TabIndex = 0;
      this.rb_HttpExample.TabStop = true;
      this.rb_HttpExample.Text = "HTTP";
      this.rb_HttpExample.UseVisualStyleBackColor = true;
      this.rb_HttpExample.Click += new System.EventHandler(this.RB_Example_Click);
      // 
      // rb_DnsExample
      // 
      this.rb_DnsExample.AutoSize = true;
      this.rb_DnsExample.Location = new System.Drawing.Point(568, 45);
      this.rb_DnsExample.Name = "rb_DnsExample";
      this.rb_DnsExample.Size = new System.Drawing.Size(68, 24);
      this.rb_DnsExample.TabIndex = 0;
      this.rb_DnsExample.Text = "DNS";
      this.rb_DnsExample.UseVisualStyleBackColor = true;
      this.rb_DnsExample.Click += new System.EventHandler(this.RB_Example_Click);
      // 
      // tb_NewData
      // 
      this.tb_NewData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tb_NewData.Location = new System.Drawing.Point(420, 84);
      this.tb_NewData.Multiline = true;
      this.tb_NewData.Name = "tb_NewData";
      this.tb_NewData.Size = new System.Drawing.Size(769, 88);
      this.tb_NewData.TabIndex = 8;
      this.tb_NewData.Text = "HTTPREQ||11-22-33-44-55-66||192.168.0.101||12345||8.8.8.8||80||GET /index.htm HTTP/1." +
    "1..Host:www.facebook.com..User-Agent: Opera....<html>..<body>..it works ... ..</" +
    "body>..</html>";
      // 
      // bt_OnShutdown
      // 
      this.bt_OnShutdown.Location = new System.Drawing.Point(16, 131);
      this.bt_OnShutdown.Name = "bt_OnShutdown";
      this.bt_OnShutdown.Size = new System.Drawing.Size(118, 41);
      this.bt_OnShutdown.TabIndex = 3;
      this.bt_OnShutdown.Text = "OnShutdown";
      this.bt_OnShutdown.UseVisualStyleBackColor = true;
      this.bt_OnShutdown.Click += new System.EventHandler(this.BT_OnShutdown_Click);
      // 
      // bt_OnNewData
      // 
      this.bt_OnNewData.Location = new System.Drawing.Point(420, 37);
      this.bt_OnNewData.Name = "bt_OnNewData";
      this.bt_OnNewData.Size = new System.Drawing.Size(123, 41);
      this.bt_OnNewData.TabIndex = 7;
      this.bt_OnNewData.Text = "OnNewData";
      this.bt_OnNewData.UseVisualStyleBackColor = true;
      this.bt_OnNewData.Click += new System.EventHandler(this.BT_OnNewData_Click);
      // 
      // bt_OnStopAttack
      // 
      this.bt_OnStopAttack.Location = new System.Drawing.Point(140, 84);
      this.bt_OnStopAttack.Name = "bt_OnStopAttack";
      this.bt_OnStopAttack.Size = new System.Drawing.Size(123, 41);
      this.bt_OnStopAttack.TabIndex = 5;
      this.bt_OnStopAttack.Text = "OnStopAttack";
      this.bt_OnStopAttack.UseVisualStyleBackColor = true;
      this.bt_OnStopAttack.Click += new System.EventHandler(this.BT_StopAttack_Click);
      // 
      // bt_OnStartAttack
      // 
      this.bt_OnStartAttack.Location = new System.Drawing.Point(140, 37);
      this.bt_OnStartAttack.Name = "bt_OnStartAttack";
      this.bt_OnStartAttack.Size = new System.Drawing.Size(123, 41);
      this.bt_OnStartAttack.TabIndex = 4;
      this.bt_OnStartAttack.Text = "OnStartAttack";
      this.bt_OnStartAttack.UseVisualStyleBackColor = true;
      this.bt_OnStartAttack.Click += new System.EventHandler(this.BT_StartAttack_Click);
      // 
      // bt_OnReset
      // 
      this.bt_OnReset.Location = new System.Drawing.Point(16, 84);
      this.bt_OnReset.Name = "bt_OnReset";
      this.bt_OnReset.Size = new System.Drawing.Size(118, 41);
      this.bt_OnReset.TabIndex = 2;
      this.bt_OnReset.Text = "OnReset";
      this.bt_OnReset.UseVisualStyleBackColor = true;
      this.bt_OnReset.Click += new System.EventHandler(this.BT_Reset_Click);
      // 
      // bt_OnInit
      // 
      this.bt_OnInit.Location = new System.Drawing.Point(16, 37);
      this.bt_OnInit.Name = "bt_OnInit";
      this.bt_OnInit.Size = new System.Drawing.Size(118, 41);
      this.bt_OnInit.TabIndex = 1;
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
      this.gb_Logs.Size = new System.Drawing.Size(1209, 216);
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
      this.tb_Logs.Size = new System.Drawing.Size(1182, 167);
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
      this.TC_PluginTester.Size = new System.Drawing.Size(1380, 504);
      this.TC_PluginTester.TabIndex = 3;
      // 
      // tp_Oper
      // 
      this.tp_Oper.Controls.Add(this.gb_Logs);
      this.tp_Oper.Controls.Add(this.gb_Events);
      this.tp_Oper.Location = new System.Drawing.Point(4, 29);
      this.tp_Oper.Name = "tp_Oper";
      this.tp_Oper.Padding = new System.Windows.Forms.Padding(3);
      this.tp_Oper.Size = new System.Drawing.Size(1372, 471);
      this.tp_Oper.TabIndex = 0;
      this.tp_Oper.Text = "Plugin operation";
      this.tp_Oper.UseVisualStyleBackColor = true;
      // 
      // ofd_LoadTemplate
      // 
      this.ofd_LoadTemplate.FileName = "ofdTemplateName";
      // 
      // PluginTest_MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1405, 613);
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
    private System.Windows.Forms.Button bt_OnShutdown;
    private System.Windows.Forms.TextBox tb_NewData;
    private System.Windows.Forms.TabControl TC_PluginTester;
    private System.Windows.Forms.TabPage tp_Oper;
    private System.Windows.Forms.RadioButton rb_HttpExample;
    private System.Windows.Forms.RadioButton rb_DnsExample;
    private System.Windows.Forms.ComboBox cb_PluginSelection;
    private System.Windows.Forms.Button bt_OnLoadTemplate;
    private System.Windows.Forms.OpenFileDialog ofd_LoadTemplate;
  }
}

