namespace PluginTest
{
  using PluginTest.DataTypes;
  using MinaryLib.DataTypes;
  using System;
  using System.IO;
  using System.Runtime.Serialization.Formatters.Binary;
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
      foreach (var key in this.pluginRecordDict.Keys)
      {
        this.pluginRecordDict[key].IPlugin.OnInit();
      }
    }


    private void BT_Reset_Click(object sender, EventArgs e)
    {
      foreach (var key in this.pluginRecordDict.Keys)
      {
        this.pluginRecordDict[key].IPlugin.OnResetPlugin();
      }
    }


    private void BT_StartAttack_Click(object sender, EventArgs e)
    {
      this.AttackStarted = true;

      foreach (var key in this.pluginRecordDict.Keys)
      {
        this.pluginRecordDict[key].IPlugin.OnStartAttack();
      }
    }


    private void BT_StopAttack_Click(object sender, EventArgs e)
    {
      this.AttackStarted = false;

      foreach (var key in this.pluginRecordDict.Keys)
      {
        this.pluginRecordDict[key].IPlugin.OnStopAttack();
      }
    }


    private void BT_OnShutdown_Click(object sender, EventArgs e)
    {
      foreach (var key in this.pluginRecordDict.Keys)
      {
        this.pluginRecordDict[key].IPlugin.OnShutDown();
      }
    }


    private void BT_OnNewData_Click(object sender, EventArgs e)
    {
      foreach (var key in this.pluginRecordDict.Keys)
      {
        this.pluginRecordDict[key].IPlugin.OnNewData(this.tb_NewData.Text);
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
      MinaryTemplateData templData = this.LoadAttackTemplate(this.ofd_PluginPath.FileName);

      //TemplatePluginData
      
      foreach (var key in this.pluginsDict.Keys)
      {
TemplatePluginData tmpPluginData = this.pluginRecordDict[key].IPlugin.OnGetTemplateData();        
        this.pluginRecordDict[key].IPlugin.OnLoadTemplateData(tmpPluginData);
      }
    }


    private MinaryTemplateData LoadAttackTemplate(string templateFile)
    {
      MinaryTemplateData deserializedObject;
      var myBinaryFormat = new BinaryFormatter();

      Stream myStream = File.OpenRead(templateFile);
      deserializedObject = (MinaryTemplateData)myBinaryFormat.Deserialize(myStream);
      myStream.Close();

      return deserializedObject;
    }

    private void BT_OnGetTemplateData_Click(object sender, EventArgs e)
    {
      foreach (var key in this.pluginRecordDict.Keys)
      {
        TemplatePluginData templData = this.pluginRecordDict[key].IPlugin.OnGetTemplateData();
      }
    }


    private void BT_OnUnloadTemplateData_Click(object sender, EventArgs e)
    {
      foreach (var key in this.pluginRecordDict.Keys)
      {
        this.pluginRecordDict[key].IPlugin.OnUnloadTemplateData();
      }
    }


    private void RB_Example_Click(object sender, EventArgs e)
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
          //this.
          this.tb_Logs.Text = string.Empty;
        }

        return false;
      }
      else
      {
        return base.ProcessDialogKey(keyData);
      }
    }
    #endregion
  }
}
