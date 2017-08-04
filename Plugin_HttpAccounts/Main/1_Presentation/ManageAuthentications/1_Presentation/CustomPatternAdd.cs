namespace Minary.Plugin.Main.HttpAccounts.ManageAuthentications.Presentation
{
  using Minary.Plugin.Main.HttpAccounts.ManageAuthentications.DataTypes;
  using MinaryLib;
  using System;
  using System.IO;
  using System.Text.RegularExpressions;
  using System.Windows.Forms;

  public partial class CustomPatternAdd : Form
  {

    #region MEMBERS

    private Task.CustomPatternAdd taskLayer;
    private PluginProperties pluginProperties;

    #endregion


    /// <summary>
    /// Initializes a new instance of the <see cref="CustomPatternAdd"/> class.
    ///
    /// </summary>
    /// <param name="pluginProperties"></param>
    public CustomPatternAdd(PluginProperties pluginProperties)
    {
      this.InitializeComponent();

      this.pluginProperties = pluginProperties;

      // Initialize task layer
      this.taskLayer = new Task.CustomPatternAdd(this.pluginProperties);

      // Set default values
      this.cb_Method.SelectedIndex = 0;
    }


    #region EVENTS

    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BT_Add_Click(object sender, EventArgs e)
    {
      string httpMethod = this.cb_Method.SelectedItem.ToString();
      string hostPattern = this.tb_HostPattern.Text;
      string pathPattern = this.tb_PathPattern.Text;
      string dataPattern = this.tb_DataPattern.Text;
      string company = this.tb_Company.Text;
      string webPage = this.tb_WebPage.Text;
      string patternName = this.tb_PatternName.Text;
      string patternDescription = this.tb_Description.Text;
      string repositoryLocalFullpath = Path.Combine(this.pluginProperties.ApplicationBaseDir,
                                                    this.pluginProperties.PluginBaseDir,
                                                    this.pluginProperties.PatternSubDir,
                                                    Plugin.Main.HttpAccounts.DataTypes.General.PATTERN_DIR_LOCAL);
      string fileName = Regex.Replace(company, @"[^\d\w\-]", "_", RegexOptions.IgnoreCase);
      string patternFileFullPath = Path.Combine(repositoryLocalFullpath, fileName + HttpAccounts.DataTypes.General.PATTERN_FILE_EXTENSION);

      try
      {
        HttpAccountPattern newPattern = new HttpAccountPattern(httpMethod, hostPattern, pathPattern, dataPattern, company, webPage, "Local", patternFileFullPath);
        //newPattern.Config.Description = patternDescription;
        //newPattern.Config.Name = patternName;
        //newPattern.Config.Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        this.taskLayer.AddCustomPatternRecord(newPattern);

        this.Dispose();
      }
      catch (Exception ex)
      {
        MessageBox.Show(string.Format("Error occurred while adding new account pattern.\r\nMessage: {0}", ex.Message), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        this.pluginProperties.HostApplication.LogMessage("Error occurred while adding new account pattern : {0}", ex.Message);
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


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BT_Close_Click(object sender, EventArgs e)
    {
      this.Dispose();
    }

    #endregion

  }

}
