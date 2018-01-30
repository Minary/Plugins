namespace PluginTest
{
  using MinaryLib.DataTypes;
  using System;
  using System.IO;
  using System.Windows.Forms;


  public partial class PluginTest_MainForm
  {

    #region EVENTS

    private void cb_PluginSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
      ComboBox comboBox = (ComboBox)sender;
      string selectedPlugin = (string)comboBox.SelectedItem;
      this.tb_PluginPath.Text = this.comboboxPluginMap[selectedPlugin];
      this.LoadModule(this.comboboxPluginMap[selectedPlugin]);
    }


    private void BT_PluginPath_Click(object sender, EventArgs e)
    {
      this.ofd_PluginPath.Filter = "Minary files (*.dll)|*.dll";

      // Determine initial directory
      if (Directory.Exists(@"C:\Users\run\code\tmp1\Build\Plugins\") == true)
      {
        this.ofd_PluginPath.InitialDirectory = @"C:\Users\run\code\tmp1\Build\Plugins\";
      }
      else
      {
        this.ofd_PluginPath.InitialDirectory = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\");
      }

      // Open the "open file dialog"
      if (this.ofd_PluginPath.ShowDialog() != DialogResult.OK)
      {
        return;
      }

      var templateFileName = this.ofd_PluginPath.FileName;
      this.LoadModule(templateFileName);
    }


    private void BT_OnInit_Click(object sender, EventArgs e)
    {
      foreach (var key in this.pluginDict.Keys)
      {
        this.pluginDict[key].IPlugin.OnInit();
      }
    }


    private void bt_Reset_Click(object sender, EventArgs e)
    {
      foreach (var key in this.pluginDict.Keys)
      {
        this.pluginDict[key].IPlugin.OnResetPlugin();
      }
    }


    private void bt_StartAttack_Click(object sender, EventArgs e)
    {
      foreach (var key in this.pluginDict.Keys)
      {
        this.pluginDict[key].IPlugin.OnStartAttack();
      }
    }


    private void bt_StopAttack_Click(object sender, EventArgs e)
    {
      foreach (var key in this.pluginDict.Keys)
      {
        this.pluginDict[key].IPlugin.OnStopAttack();
      }
    }


    private void BT_OnShutdown_Click(object sender, EventArgs e)
    {
      foreach (var key in this.pluginDict.Keys)
      {
        this.pluginDict[key].IPlugin.OnShutDown();
      }
    }


    private void BT_OnNewData_Click(object sender, EventArgs e)
    {
      foreach (var key in this.pluginDict.Keys)
      {
        this.pluginDict[key].IPlugin.OnNewData(this.tb_NewData.Text);
      }
    }


    private void BT_OnLoadTemplateData_Click(object sender, EventArgs e)
    {
      foreach (var key in this.pluginDict.Keys)
      {
        TemplatePluginData templData = new TemplatePluginData()
        {
        };

        this.pluginDict[key].IPlugin.OnLoadTemplateData(templData);
      }
    }


    private void BT_OnGetTemplateData_Click(object sender, EventArgs e)
    {
      foreach (var key in this.pluginDict.Keys)
      {
        TemplatePluginData templData = this.pluginDict[key].IPlugin.OnGetTemplateData();
      }
    }


    private void BT_OnUnloadTemplateData_Click(object sender, EventArgs e)
    {
      foreach (var key in this.pluginDict.Keys)
      {
        this.pluginDict[key].IPlugin.OnUnloadTemplateData();
      }
    }


    private void rb_Example_Click(object sender, EventArgs e)
    {
      if (this.rbLastChoice != this.rb_DnsExample.Name &&
          this.rb_DnsExample.Checked)
      {
        this.rbLastChoice = this.rb_DnsExample.Name;
        this.tb_NewData.Text = "TCP||11-22-33-44-55-66||192.168.0.101||12345||8.8.8.8||53||auth.facebook.com";
      }
      else if (this.rbLastChoice != this.rb_HttpExample.Name &&
               this.rb_HttpExample.Checked)
      {
        this.rbLastChoice = this.rb_HttpExample.Name;
        this.tb_NewData.Text = "TCP||11-22-33-44-55-66||192.168.0.101||12345||8.8.8.8||80||....GET /index.htm HTTP/1.1....Host:www.facebook.com....";
      }
    }

    #endregion
  }
}
