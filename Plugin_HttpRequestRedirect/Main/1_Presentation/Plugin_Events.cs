namespace Minary.Plugin.Main
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Drawing;
  using System.Data;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using System.Windows.Forms;


  public partial class Plugin_HttpRequestRedirect : UserControl
  {

    #region EVENTS

    private void BT_Add_Click(object sender, EventArgs e)
    {
      try
      {
        this.AddRecord(this.tb_RequestedURLRegex.Text, this.tb_RedirectURL.Text);
      }
      catch (Exception ex)
      {
        string msg = string.Format("Error occurred while adding request redirect record: \r\n\r\n{0}", ex.Message);
        this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
        MessageBox.Show(msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }

    #endregion

  }
}
