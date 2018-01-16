namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.HttpSearch.DataTypes;
  using System;
  using System.Windows.Forms;


  public partial class Plugin_HttpSearch
  {
    #region EVENTS

    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BT_Add_Click(object sender, EventArgs e)
    {
      var method = this.CB_Method.SelectedValue.ToString();
      var type = this.CB_Type.SelectedValue.ToString();
      var domain = (this.RB_Body.Checked == true) ? "Body" : "Header";
      var hostRegex = this.TB_HostRegex.Text.Trim();
      var pathRegex = this.TB_PathRegex.Text.Trim();
      var dataRegex = this.TB_DataRegex.Text.Trim();

      try
      {
        this.AddRecord(new RecordHttpSearch(method,type, domain, hostRegex, pathRegex, dataRegex));
      }
      catch (Exception ex)
      {
        this.Config.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
        MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }

    #endregion


  }
}
