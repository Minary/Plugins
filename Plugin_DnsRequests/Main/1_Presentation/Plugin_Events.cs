namespace Minary.Plugin.Main
{
  using System;
  using System.Windows.Forms;


  public partial class Plugin_DnsRequests
  {

    #region EVENTS

    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BT_Set_Click(object sender, EventArgs e)
    {
      this.UseFilter();
      this.dgv_DnsRequests.Refresh();
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TSMI_Clear_Click(object sender, EventArgs e)
    {
      this.ClearRecordList();
    }



    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DGV_DNSRequests_MouseUp(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right)
      {
        return;
      }

      try
      {
        DataGridView.HitTestInfo hti = this.dgv_DnsRequests.HitTest(e.X, e.Y);
        if (hti.RowIndex >= 0)
        {
          this.cms_DnsRequests.Show(this.dgv_DnsRequests, e.Location);
        }
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TB_Filter_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        this.UseFilter();
        this.dgv_DnsRequests.Refresh();
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DeleteEntryToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        var currentIndex = this.dgv_DnsRequests.CurrentCell.RowIndex;
        this.RemoveRecordAt(currentIndex);
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CopyHostNameToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        var currentIndex = this.dgv_DnsRequests.CurrentCell.RowIndex;
        var hostName = this.dgv_DnsRequests.Rows[currentIndex].Cells["DNSHostname"].Value.ToString();
        Clipboard.SetText(hostName);
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DGV_DNSRequests_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      try
      {
        var currentIndex = this.dgv_DnsRequests.CurrentCell.RowIndex;
        var hostName = this.dgv_DnsRequests.Rows[currentIndex].Cells["DNSHostname"].Value.ToString();
        Clipboard.SetText(hostName);
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage($"{Config.PluginName}: {ex.Message}");
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DGV_DNSRequests_MouseDown(object sender, MouseEventArgs e)
    {
      try
      {
        DataGridView.HitTestInfo hti = this.dgv_DnsRequests.HitTest(e.X, e.Y);

        if (hti.RowIndex >= 0)
        {
          this.dgv_DnsRequests.ClearSelection();
          this.dgv_DnsRequests.Rows[hti.RowIndex].Selected = true;
          this.dgv_DnsRequests.CurrentCell = this.dgv_DnsRequests.Rows[hti.RowIndex].Cells[0];
        }
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage($"{Config.PluginName}: {ex.Message}");
        this.dgv_DnsRequests.ClearSelection();
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void T_GUIUpdate_Tick(object sender, EventArgs e)
    {
      this.ProcessEntries();
    }

    #endregion

  }
}
