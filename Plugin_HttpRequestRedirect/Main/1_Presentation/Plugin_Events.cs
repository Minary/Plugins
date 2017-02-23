namespace Minary.Plugin.Main
{
  using System;
  using System.Windows.Forms;


  public partial class Plugin_HttpRequestRedirect : UserControl
  {

    #region EVENTS

    private void BT_Add_Click(object sender, EventArgs e)
    {
      try
      {
        string[] splitter = this.cb_RedirectType.Text.Split('/');
        string redirectType = splitter[0];
        string redirectDescription = splitter[1];
        this.AddRecord(this.tb_RequestedURLRegex.Text, this.tb_RedirectURL.Text, redirectType, redirectDescription);
      }
      catch (Exception ex)
      {
        string msg = string.Format("Error occurred while adding request redirect record: \r\n\r\n{0}", ex.Message);
        this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
        MessageBox.Show(msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TB_Host_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Enter)
      {
        return;
      }

      e.SuppressKeyPress = true;
      this.BT_Add_Click(null, null);
    }

    #endregion

  }
}
