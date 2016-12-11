namespace Minary.Plugin.Main.Systems.ManageSystems.Presentation
{
  using Minary.Plugin.Main.Systems.DataTypes;
  using MinaryLib;
  using System;
  using System.IO;
  using System.Text.RegularExpressions;
  using System.Windows.Forms;

  public partial class CustomPatternAdd : Form
  {

    #region MEMBERS

    private PluginProperties pluginProperties;
    private Plugin.Main.Systems.ManageSystems.Task.CustomPatternAdd taskLayer;

    #endregion


    #region PUBLIC

    public CustomPatternAdd(PluginProperties pluginProperties)
    {
      this.InitializeComponent();
      this.pluginProperties = pluginProperties;
      this.taskLayer = Plugin.Main.Systems.ManageSystems.Task.CustomPatternAdd.GetInstance(this.pluginProperties);
    }

    #endregion


    #region EVENTS

    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BT_Close_Click(object sender, EventArgs e)
    {
      this.Dispose();
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BT_Add_Click(object sender, EventArgs e)
    {
      string systemName = this.tb_SystemName.Text;
      string systemRegex = this.tb_SystemRegex.Text;
      string patternName = this.tb_PatternName.Text;
      string patternDescription = this.tb_PatternDescription.Text;

      string repositoryLocalFullpath = Path.Combine(
                                                    this.pluginProperties.ApplicationBaseDir,
                                                    this.pluginProperties.PluginBaseDir,
                                                    this.pluginProperties.PatternSubDir,
                                                    General.PATTERN_DIR_LOCAL);
      string fileName = Regex.Replace(systemName, @"[^\d\w\-]", "_", RegexOptions.IgnoreCase);
      string patternFileFullPath = Path.Combine(repositoryLocalFullpath, fileName + General.PATTERN_FILE_EXTENSION);

      try
      {
        SystemPattern newPattern = new SystemPattern(systemRegex, systemName, "Local", patternFileFullPath);

        newPattern.IsEnabled = true;
        //newPattern.Config.Name = patternName;
        //newPattern.Config.Description = patternDescription;
        //newPattern.Config.Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        this.taskLayer.AddCustomPatternRecord(newPattern);
        this.Dispose();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        this.pluginProperties.HostApplication.LogMessage("ManageSystems(): {0}", ex.Message);

        return;
      }
    }


    /// <summary>
    /// Close form on Escape.
    /// </summary>
    /// <param name="keyData"></param>
    /// <returns></returns>
    protected override bool ProcessDialogKey(Keys keyData)
    {
      if (keyData == Keys.Escape)
      {
        this.Dispose();
        return true;
      }
      else
        return base.ProcessDialogKey(keyData);
    }

    #endregion

  }
}
