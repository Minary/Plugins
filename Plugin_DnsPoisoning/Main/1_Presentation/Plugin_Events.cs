namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.DnsPoisoning.DataTypes;
  using System;
  using System.Text.RegularExpressions;
  using System.Windows.Forms;


  public partial class Plugin_DnsPoisoning
  {

    #region MEMBERS

    private string watermarkHttpHost = "*.google.c*";

    #endregion


    #region EVENTS

    private void TextBoxGotFocus(object sender, EventArgs e)
    {
      var tb = (TextBox)sender;
      if (tb.Text == this.watermarkHttpHost)
      {
        tb.Text = string.Empty;
        tb.ForeColor = System.Drawing.Color.Black;
      }
      else
      {
      }
    }


    private void TextBoxLostFocus(object sender, EventArgs e)
    {
      var tb = (TextBox)sender;
      if (string.IsNullOrEmpty(tb.Text))
      {
        tb.Text = this.watermarkHttpHost;
        tb.ForeColor = System.Drawing.Color.LightGray;
      }
      else
      {
        tb.ForeColor = System.Drawing.Color.Black;
      }
    }


    private void TSMI_UseHostIP_Click(object sender, EventArgs e)
    {
      try
      {
        this.ValidateHostName(this.tb_CName.Text);
        var hostEntry = System.Net.Dns.GetHostEntry(this.tb_CName.Text);
        this.tb_Address.Text = hostEntry.AddressList[0].ToString();
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Exception occurred: {ex.Message}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BT_Add_Click(object sender, EventArgs e)
    {
      var hostName = this.tb_Host.Text.Trim();
      var ipAddress = this.tb_Address.Text.Trim();
      var responseType = this.cb_Cname.Checked ? DnsResponseType.CNAME : DnsResponseType.A;
      var cname = this.cb_Cname.Checked ? this.tb_CName.Text.Trim() : string.Empty;
      var ttl = long.Parse(this.tb_ttl.Text.Trim());

      try
      {
        this.AddRecord(new RecordDnsPoison(hostName, ipAddress, responseType, cname, ttl));
      }
      catch (Exception ex)
      {
        this.Config.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
        MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        this.DeleteSelectedRecords();
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
        this.tb_Address.Text = this.Config.HostApplication.CurrentIP;
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

      var hostName = this.tb_Host.Text.Trim();
      var ipAddress = this.tb_Address.Text.Trim();
      var responseType = this.cb_Cname.Checked ? DnsResponseType.CNAME : DnsResponseType.A;
      var cname = this.cb_Cname.Checked ? this.tb_CName.Text.Trim() : string.Empty;
      var ttl = long.Parse(this.tb_ttl.Text.Trim());

      try
      {
        this.AddRecord(new RecordDnsPoison(hostName, ipAddress, responseType, cname, ttl));
      }
      catch (Exception ex)
      {
        this.Config.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
        MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CB_Cname_CheckedChanged(object sender, EventArgs e)
    {
      if (this.cb_Cname.Checked)
      {
        this.tb_CName.Enabled = true;
      }
      else
      {
        this.tb_CName.Enabled = false;
      }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TSMI_ChangeParameters_Click(object sender, EventArgs e)
    {
      var changeParams = new ChangeParameters(this, this.dgv_Spoofing);
      changeParams.ShowDialog(this);
    }
   
    
    private void DGV_Spoofing_CellDoubleClick(object sender, EventArgs e)
    {
      try
      {
        var currentIndex = this.dgv_Spoofing.CurrentCell.RowIndex;
        var hostName = this.dgv_Spoofing.Rows[currentIndex].Cells["HostName"].Value.ToString();
        Clipboard.SetText(hostName);
      }
      catch (Exception ex)
      {
        this.Config.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
      }
    }


    private void DGV_Spoofing_MouseDown(object sender, MouseEventArgs e)
    {
      try
      {
        DataGridView.HitTestInfo hti = this.dgv_Spoofing.HitTest(e.X, e.Y);

        if (hti.RowIndex >= 0 && this.dgv_Spoofing.SelectedRows.Count == 1)
        {
          this.dgv_Spoofing.ClearSelection();
          this.dgv_Spoofing.Rows[hti.RowIndex].Selected = true;
          this.dgv_Spoofing.CurrentCell = this.dgv_Spoofing.Rows[hti.RowIndex].Cells[0];
        }
      }
      catch (Exception ex)
      {
        this.Config.HostApplication.LogMessage($"{Config.PluginName}: {ex.Message}");
        this.dgv_Spoofing.ClearSelection();
      }
    }

    #endregion


    #region PUBLIC

    public void ValidateHostName(string hostName)
    {
      if (string.IsNullOrEmpty(hostName) == true ||
          string.IsNullOrWhiteSpace(hostName) == true)
      {
        throw new Exception("Hostname is empty");
      }

      if (Regex.Match(hostName, @"^[\d\w\-_\.]+$").Success == false)
      {
        throw new Exception($"Hostname is invalid: {hostName}");
      }
    }

    #endregion

  }
}
