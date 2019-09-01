namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.DnsPoisoning.DataTypes;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data;
  using System.Drawing;
  using System.Linq;
  using System.Text.RegularExpressions;
  using System.Threading.Tasks;
  using System.Windows.Forms;


  public partial class ChangeParameters : Form
  {

    #region MEMBERS

    private Plugin_DnsPoisoning parentPlugin;
    private DataGridView dgv_Spoofing;

    #endregion


    #region PUBLIC

    public ChangeParameters(Plugin_DnsPoisoning parentPlugin, DataGridView dgv_Spoofing)
    {
      InitializeComponent();

      this.parentPlugin = parentPlugin;
      this.dgv_Spoofing = dgv_Spoofing;
    }

    #endregion


    #region EVENTS

    private void BT_Cancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }


    private void BT_Save_Click(object sender, EventArgs e)
    {      
      var hostname = string.IsNullOrEmpty(this.tb_Hostname?.Text) ? "" : this.tb_Hostname?.Text.Trim();
      var ipAddress = string.IsNullOrEmpty(this.tb_IpAddress?.Text) ? "" : this.tb_IpAddress?.Text.Trim();
      var ttl = string.IsNullOrEmpty(this.tb_ttl?.Text) ? 0 : long.Parse(this.tb_ttl.Text.Trim());

      try
      {
        this.VerifyInputData(hostname, ipAddress, ttl);
        this.ReplaceValuesInList(hostname, ipAddress, ttl);
        this.Close();
      }
      catch (Exception ex)
      {
        this.parentPlugin.Config.HostApplication.LogMessage($"{this.parentPlugin.Config.PluginName}: {ex.Message}");
        MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }


    protected override bool ProcessDialogKey(Keys keyData)
    {
      if (keyData == Keys.Escape)
      {
        this.Close();
        return false;
      }
      else
      {
        return base.ProcessDialogKey(keyData);
      }
    }

    #endregion


    #region PRIVATE

    private void ReplaceValuesInList(string hostname, string ipAddress, long ttl)
    {
      var indexList = new List<int>();
      foreach (DataGridViewRow row in this.dgv_Spoofing.SelectedRows)
      {
        indexList.Add(row.Index);
      }

      foreach (int index in indexList)
      {
        this.dgv_Spoofing.Rows[index].Cells["HostName"].Value = hostname;
        this.dgv_Spoofing.Rows[index].Cells["IPAddress"].Value = ipAddress;
        this.dgv_Spoofing.Rows[index].Cells["TTL"].Value = ttl.ToString();
      }
    }


    private void VerifyInputData(string hostname, string ipAddress, long ttl)
    {
      // Verify whether Hostname and IPaddress for correctness.
      if (this.parentPlugin.VerifyHostNameStructure(hostname) == false)
      {
        throw new Exception("Something is wrong with the host name");
      }

      if (this.parentPlugin.VerifyIpAddressStructure(ipAddress) == false)
      {
        throw new Exception("Something is wrong with the IP address");
      }

      // Verify whether TTL has a valid value
      if (Regex.Match(this.tb_ttl.Text, @"^\d{1,10}$").Success == false ||
          long.TryParse(this.tb_ttl.Text, out ttl) == false ||
          ttl < 1 ||
          ttl > 4294967296)
      {
        throw new Exception("Something is wront with the TTL.\r\nValue must be 1-4'294'967'296");
      }
    }

    #endregion

  }
}
