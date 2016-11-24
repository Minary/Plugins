namespace Minary.Plugin.Main.Session.ManageSessions.Presentation
{
  using Minary.Plugin.Main.Session.ManageSessions.DataTypes;
  using MinaryLib;
  using System;
  using System.IO;
  using System.Text.RegularExpressions;
  using System.Windows.Forms;


  public partial class CustomPatternAdd : Form
  {

    #region MEMBERS

    private bool addedNewPatternFile = false;
    private Plugin.Main.Session.ManageSessions.Task.CustomPatternAdd taskLayer;
    private PluginProperties pluginProperties;

    #endregion


    #region PROPERTIES

    public bool AddedNewPatternFile { get { return this.addedNewPatternFile; } set { } }

    #endregion


    #region PUBLIC

    public CustomPatternAdd(PluginProperties pluginProperties)
    {
      this.InitializeComponent();

      this.pluginProperties = pluginProperties;
      this.taskLayer = Plugin.Main.Session.ManageSessions.Task.CustomPatternAdd.GetInstance(this.pluginProperties);
    }


    #endregion


    #region EVENTOS

    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BT_Add_Click(object sender, EventArgs e)
    {
      string companyName = this.tb_CompanyName.Text;
      string companyWebPage = this.tb_CompanyWebPage.Text;
      string hostRegex = this.tb_HostRegex.Text;
      string sessionCookiesPattern = this.tb_SessionCookieRegex.Text;
      string patternName = this.tb_PatternName.Text;
      string patternDescription = this.tb_PatternDescription.Text;
      string repositoryLocalFullpath = Path.Combine(
                                                    this.pluginProperties.ApplicationBaseDir,
                                                    this.pluginProperties.PluginBaseDir,
                                                    this.pluginProperties.PatternSubDir,
                                                    Plugin.Main.Session.Config.General.PATTERN_DIR_LOCAL);
      string fileName = Regex.Replace(companyName, @"[^\d\w\-]", "_", RegexOptions.IgnoreCase);
      string patternFileFullPath = Path.Combine(repositoryLocalFullpath, fileName + Plugin.Main.Session.Config.General.PATTERN_FILE_EXTENSION);

      try
      {
        SessionPattern newPattern = new SessionPattern(sessionCookiesPattern, companyName, hostRegex, companyWebPage, patternFileFullPath);
        //newPattern.Config.Description = patternDescription;
        //newPattern.Config.Name = patternName;
        //newPattern.Config.Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        this.taskLayer.AddCustomPatternRecord(newPattern);
        this.addedNewPatternFile = true;

        this.Dispose();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        this.pluginProperties.HostApplication.LogMessage("ManageSessions() : {0}", ex.Message);

        return;
      }
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
      {
        return base.ProcessDialogKey(keyData);
      }
    }

    #endregion

  }
}
