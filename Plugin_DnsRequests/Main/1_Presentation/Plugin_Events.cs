﻿namespace Minary.Plugin.Main
{
  using Minary.MiniBrowser;
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
    private void TSMI_OpenInBrowser_Click(object sender, EventArgs e)
    {
      var hostName = string.Empty;
      var url = string.Empty;
      var cookie = string.Empty;
      var srcIp = string.Empty;
      var userAgent = string.Empty;

      try
      {
        var currentIndex = this.dgv_DnsRequests.CurrentCell.RowIndex;
        hostName = this.dgv_DnsRequests.Rows[currentIndex].Cells["DnsRequest"].Value.ToString();
        url = $"http://{hostName}/";
        cookie = this.dgv_DnsRequests.SelectedRows[0].Cells["SessionCookies"].Value.ToString();
        srcIp = this.dgv_DnsRequests.SelectedRows[0].Cells["SrcIP"].Value.ToString();
        userAgent = this.dgv_DnsRequests.SelectedRows[0].Cells["UserAgent"].Value.ToString();
      }
      catch (ArgumentOutOfRangeException aoorex)
      {
        this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {aoorex.Message}");
        return;
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
      }

      try
      {
        var miniBrowser = new Browser(url, cookie, srcIp, userAgent);
        miniBrowser.Show();
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
        MessageBox.Show($"MiniBrowser unexpectedly crashed : {ex.ToString()}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DGV_DnsRequests_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      try
      {
        var currentIndex = this.dgv_DnsRequests.CurrentCell.RowIndex;
        var hostName = this.dgv_DnsRequests.Rows[currentIndex].Cells["DnsRequest"].Value.ToString();
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
    private void TSMI_DeleteEntry_Click(object sender, EventArgs e)
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
    private void TSMI_CopyHostName_Click(object sender, EventArgs e)
    {
      try
      {
        var currentIndex = this.dgv_DnsRequests.CurrentCell.RowIndex;
        var hostName = this.dgv_DnsRequests.Rows[currentIndex].Cells["DnsRequest"].Value.ToString();
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
    private void DGV_DnsRequests_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      try
      {
        var currentIndex = this.dgv_DnsRequests.CurrentCell.RowIndex;
        var hostName = this.dgv_DnsRequests.Rows[currentIndex].Cells["DnsRequest"].Value.ToString();
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
    private void DGV_DnsRequests_MouseDown(object sender, MouseEventArgs e)
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
    private void T_GuiUpdate_Tick(object sender, EventArgs e)
    {
      this.ProcessEntries();
    }

    #endregion

  }
}
