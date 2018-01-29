namespace PluginTest
{
  using MinaryLib.DataTypes;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Windows.Forms;


  public partial class PluginTest_MainForm
  {

    #region EVENTS

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

    #endregion
  }
}
