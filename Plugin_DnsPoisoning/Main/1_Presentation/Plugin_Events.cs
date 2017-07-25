namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.DnsPoison.DataTypes;
  using System;
  using System.Windows.Forms;


  public partial class Plugin_DnsPoisoning
  {

    #region EVENTS

    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BT_Add_Click(object sender, EventArgs e)
    {
      string hostName = this.tb_Host.Text.Trim();
      string ipAddress = this.tb_Address.Text.Trim();
      DnsResponseType responseType = this.cb_Cname.Checked ? DnsResponseType.CNAME : DnsResponseType.A;
      string cname = this.tb_Cname.Text.Trim();

      try
      {
        this.AddRecord(new RecordDnsPoison(hostName, ipAddress, responseType, cname));
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
        MessageBox.Show(string.Format("{0}", ex.Message), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TSMI_Delete_Click(object sender, EventArgs e)
    {
      try
      {
        this.DeleteSelectedRecord();
      }
      catch (Exception)
      {
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DGV_Spoofing_MouseUp(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right)
      {
        return;
      }

      try
      {
        DataGridView.HitTestInfo hti = this.dgv_Spoofing.HitTest(e.X, e.Y);
        if (hti.RowIndex >= 0)
        {
          this.cms_DnsPoison.Show(this.dgv_Spoofing, e.Location);
        }
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DGV_Spoofing_MouseDown(object sender, MouseEventArgs e)
    {
      try
      {
        DataGridView.HitTestInfo hti = this.dgv_Spoofing.HitTest(e.X, e.Y);

        if (hti.RowIndex >= 0)
        {
          this.dgv_Spoofing.ClearSelection();
          this.dgv_Spoofing.Rows[hti.RowIndex].Selected = true;
          this.dgv_Spoofing.CurrentCell = this.dgv_Spoofing.Rows[hti.RowIndex].Cells[0];
        }
      }
      catch (Exception)
      {
        this.dgv_Spoofing.ClearSelection();
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TSMI_Clear_Click(object sender, EventArgs e)
    {
      try
      {
        this.ClearRecordList();
      }
      catch (Exception)
      {
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PluginDNSPoisonUC_Load(object sender, EventArgs e)
    {
      if (this.tb_Address.Text.Length == 0)
      {
        this.tb_Address.Text = this.pluginProperties.HostApplication.CurrentIP;
      }
    }
    

    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnEnterAddRecord(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Enter)
      {
        return;
      }

      e.SuppressKeyPress = true;

      string hostName = this.tb_Host.Text.Trim();
      string ipAddress = this.tb_Address.Text.Trim();
      DnsResponseType responseType = this.cb_Cname.Checked ? DnsResponseType.CNAME : DnsResponseType.A;
      string cname = this.tb_Cname.Text.Trim();

      try
      {
        this.AddRecord(new RecordDnsPoison(hostName, ipAddress, responseType, cname));
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
        MessageBox.Show(string.Format("{0}", ex.Message), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }


    private void CB_Cname_CheckedChanged(object sender, EventArgs e)
    {
      if (this.cb_Cname.Checked)
      {
        this.tb_Cname.Enabled = true;
      }
      else
      {
        this.tb_Cname.Enabled = false;
      }
    }

    #endregion

  }
}
