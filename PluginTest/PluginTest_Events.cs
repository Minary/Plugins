namespace PluginTest
{
  using MinaryLib.DataTypes;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Windows.Forms;


  public partial class PluginTest_MainForm
  {

    #region EVENTS

    private void CB_PluginSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
      ComboBox comboBox = (ComboBox)sender;
      string selectedPlugin = (string)comboBox.SelectedItem;
      this.tb_PluginPath.Text = this.comboboxPluginMap[selectedPlugin];
      this.LoadModule(this.comboboxPluginMap[selectedPlugin]);
    }

    
    private void BT_OnInit_Click(object sender, EventArgs e)
    {
      foreach (var key in this.pluginDict.Keys)
      {
        this.pluginDict[key].IPlugin.OnInit();
      }
    }


    private void BT_Reset_Click(object sender, EventArgs e)
    {
      foreach (var key in this.pluginDict.Keys)
      {
        this.pluginDict[key].IPlugin.OnResetPlugin();
      }
    }


    private void BT_StartAttack_Click(object sender, EventArgs e)
    {
      this.AttackStarted = true;

      foreach (var key in this.pluginDict.Keys)
      {
        this.pluginDict[key].IPlugin.OnStartAttack();
      }
    }


    private void BT_StopAttack_Click(object sender, EventArgs e)
    {
      this.AttackStarted = false;

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
      this.ofd_LoadTemplate.Filter = "Minary template files (*.mry)|*.mry";
      
      // Open the "open file dialog"
      if (this.ofd_PluginPath.ShowDialog() != DialogResult.OK)
      {
        return;
      }

      var templateFileName = this.ofd_PluginPath.FileName;


      //foreach (var key in this.pluginDict.Keys)
      //{
      //  TemplatePluginData templData = new TemplatePluginData()
      //  {
      //  };

      //  this.pluginDict[key].IPlugin.OnLoadTemplateData(templData);
      //}
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


    private void RB_Example_Click(object sender, EventArgs e)
    {
      if (this.rbLastChoice != this.rb_DnsExample.Name &&
          this.rb_DnsExample.Checked)
      {
        this.rbLastChoice = this.rb_DnsExample.Name;
        this.tb_NewData.Text = "DNSREQ||11-22-33-44-55-66||192.168.0.101||12345||8.8.8.8||53||auth.facebook.com";
      }
      else if (this.rbLastChoice != this.rb_HttpExample.Name &&
               this.rb_HttpExample.Checked)
      {
        this.rbLastChoice = this.rb_HttpExample.Name;
        this.tb_NewData.Text = "DNSREQ||11-22-33-44-55-66||192.168.0.101||12345||8.8.8.8||80||....GET /index.htm HTTP/1.1....Host:www.facebook.com....";
      }
    }


    protected override bool ProcessDialogKey(Keys keyData)
    {
      if (keyData == Keys.Escape)
      {
        this.Hide();
        return false;
      }
      else if (keyData == (Keys.Control | Keys.C))
      {
        lock (this)
        {
          this.tb_Logs.Text = string.Empty;
        }

        return false;
      }
      else
      {
        return base.ProcessDialogKey(keyData);
      }
    }


    private void BT_OnSetTargets_Click(object sender, EventArgs e)
    {
      var targetList = new List<Tuple<string, string, string>>();

      // Parse target records from TextBox
      foreach (var line in this.tb_TargetList.Lines)
      {
        // Ignore line if it does not contain 2 commas
        if (line.Count(f => f == ',') != 2)
        {
          continue;
        }

        // Ignore line if splitting it does not yield 3 elements
        var splitter = line.Split(new char[] { ',' });
        if (splitter.Count() != 3)
        {
          continue;
        }

        targetList.Add(new Tuple<string, string, string>(splitter[0].Trim(), splitter[1].Trim(), splitter[2].Trim()));
      }

      // Do nothing and return if target list is empty
      if (targetList.Count() <= 0)
      {
        return;
      }

      // Pass the target record list to the plugins
      foreach (var key in this.pluginDict.Keys)
      {
        this.pluginDict[key].IPlugin.SetTargets(targetList);
      }
    }

    #endregion
  }
}
