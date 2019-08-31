namespace Minary.Plugin.Main
{
  using Minary.MiniBrowser;
  using Minary.Plugin.Main.HttpRequest.DataTypes;
  using System;
  using System.ComponentModel;
  using System.Text.RegularExpressions;
  using System.Windows.Forms;


  public partial class Plugin_HttpRequests
  {

    #region EVENTS

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
    private void BT_Set_Click(object sender, EventArgs e)
    {
      this.UseFilter();
      this.dgv_HttpRequests.Refresh();
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TB_Filter_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Enter)
      {
        return;
      }
      this.UseFilter();
      this.dgv_HttpRequests.Refresh();

    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DGV_HttpRequests_DoubleClick(object sender, EventArgs e)
    {
      try
      {
        var tmpHosts = new BindingList<HttpRequests>();
        int currentIndex = this.dgv_HttpRequests.CurrentCell.RowIndex;

        this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: currentIndex:{currentIndex}");
        string request = this.dgv_HttpRequests.Rows[currentIndex].Cells["Request"].Value.ToString();

        request = Regex.Replace(request, @"\.\.", "\r\n");
        var requestDetails = new ShowRequest(request);
        requestDetails.ShowDialog();
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
    private void T_GuiUpdate_Tick(object sender, EventArgs e)
    {
      this.ProcessEntries();
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
        int currentIndex = this.dgv_HttpRequests.CurrentCell.RowIndex;
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
    private void DGV_HttpRequests_MouseDown(object sender, MouseEventArgs e)
    {
      try
      {
        DataGridView.HitTestInfo hti = this.dgv_HttpRequests.HitTest(e.X, e.Y);

        if (hti.RowIndex >= 0)
        {
          this.dgv_HttpRequests.ClearSelection();
          this.dgv_HttpRequests.Rows[hti.RowIndex].Selected = true;
        }
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
        this.dgv_HttpRequests.ClearSelection();
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TSMI_RequestDetails_Click(object sender, EventArgs e)
    {
      var url = string.Empty;
      var cookie = string.Empty;
      var srcIp = string.Empty;
      var userAgent = string.Empty;

      try
      {
        url = this.dgv_HttpRequests.SelectedRows[0].Cells["URL"].Value.ToString();
        cookie = this.dgv_HttpRequests.SelectedRows[0].Cells["SessionCookies"].Value.ToString();
        srcIp = this.dgv_HttpRequests.SelectedRows[0].Cells["SrcIP"].Value.ToString();
        userAgent = this.dgv_HttpRequests.SelectedRows[0].Cells["UserAgent"].Value.ToString();
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


    private void TSMI_ShowData_Click(object sender, EventArgs e)
    {
      try
      {
        var host = this.dgv_HttpRequests.SelectedRows[0].Cells["RemoteHost"].Value.ToString();
        var path = this.dgv_HttpRequests.SelectedRows[0].Cells["Path"].Value.ToString();

        ShowData dataForm = new ShowData($"http://{host}{path}");
        dataForm.ShowDialog();
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


    }

    #endregion

  }
}
