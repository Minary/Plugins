namespace Minary.Plugin.Main
{
  using System;
  using System.Collections.Generic;
  using System.Text.RegularExpressions;
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

      if (this.dgv_Spoofing.SelectedRows.Count > 0)
      {
        this.tb_IpAddress.Text = this.dgv_Spoofing.SelectedRows[0].Cells["IPAddress"].Value.ToString();
        this.tb_ttl.Text = this.dgv_Spoofing.SelectedRows[0].Cells["TTL"].Value.ToString();
        this.tb_CName.Text = this.dgv_Spoofing.SelectedRows[0].Cells["CName"].Value.ToString();
      }
      else
      {
        this.tb_IpAddress.Text = this.parentPlugin.TbSpoofedIpAddress;
        this.tb_ttl.Text = this.parentPlugin.TbTtl;
        this.tb_IpAddress.Text = this.parentPlugin.TbCName;
      }


      this.cb_Type.SelectedIndex = 0;
    }

    #endregion


    #region EVENTS

    private void TSMI_UseHostIP_Click(object sender, EventArgs e)
    {
      try
      {
        this.parentPlugin.ValidateHostName(this.tb_CName.Text);
        var hostEntry = System.Net.Dns.GetHostEntry(this.tb_CName.Text);
        this.tb_IpAddress.Text = hostEntry.AddressList[0].ToString();
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Exception occurred: {ex.Message}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }


    private void BT_Cancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }


    private void BT_Save_Click(object sender, EventArgs e)
    {
      var ipAddress = string.IsNullOrEmpty(this.tb_IpAddress?.Text) ? "" : this.tb_IpAddress?.Text.Trim();
      var ttl = string.IsNullOrEmpty(this.tb_ttl?.Text) ? 0 : long.Parse(this.tb_ttl.Text.Trim());
      var cName = string.IsNullOrEmpty(this.tb_CName?.Text) ? "" : this.tb_CName?.Text.Trim();

      try
      {
        this.VerifyInputData(ipAddress, ttl);
        this.ReplaceValuesInList(ipAddress, ttl, cName);
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

    private void ReplaceValuesInList(string ipAddress, long ttl, string cName)
    {
      var indexList = new List<int>();
      foreach (DataGridViewRow row in this.dgv_Spoofing.SelectedRows)
      {
        indexList.Add(row.Index);
      }

      foreach (int index in indexList)
      {
        this.dgv_Spoofing.Rows[index].Cells["IPAddress"].Value = ipAddress;
        this.dgv_Spoofing.Rows[index].Cells["TTL"].Value = ttl.ToString();
        this.dgv_Spoofing.Rows[index].Cells["CName"].Value = cName;
      }
    }


    private void VerifyInputData(string ipAddress, long ttl)
    {
      // Verify whether IPaddress and TTL for correctness.
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
