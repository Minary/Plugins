namespace Minary.Plugin.Main
{
  using Minary.MiniBrowser;
  using Minary.Plugin.Main.HttpsRequest.DataTypes;
  using System;
  using System.ComponentModel;
  using System.Windows.Forms;


  public partial class Plugin_HttpsRequests
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


    private void DGV_HttpsRequests_MouseUp(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        try
        {
          DataGridView.HitTestInfo hti = this.dgv_HttpsRequests.HitTest(e.X, e.Y);
          if (hti.RowIndex >= 0)
          {
            this.cms_HttpsRequests.Show(this.dgv_HttpsRequests, e.Location);
          }
        }
        catch (Exception ex)
        {
          this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
        }
      }
    }


    private void BT_Set_Click(object sender, EventArgs e)
    {
      this.UseFilter();
      this.dgv_HttpsRequests.Refresh();
    }


    private void TB_Filter_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        this.UseFilter();
        this.dgv_HttpsRequests.Refresh();
      }
    }


    private void Dgv_HttpsRequest_DoubleClick(object sender, EventArgs e)
    {
      try
      {
        var tmpHosts = new BindingList<RecordHttpsRequest>();
        int currentIndex = this.dgv_HttpsRequests.CurrentCell.RowIndex;

       // this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: currentIndex:{currentIndex}");
        //string request = this.dgv_HttpsRequests.Rows[currentIndex].Cells["Request"].Value.ToString();

        //request = Regex.Replace(request, @"\.\.", "\r\n");
        //var requestDetails = new ShowRequest(request);
        //requestDetails.ShowDialog();
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
      }
    }

    
    private void T_GuiUpdate_Tick(object sender, EventArgs e)
    {
      this.ProcessEntries();
    }


    private void DeleteEntryToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        int currentIndex = this.dgv_HttpsRequests.CurrentCell.RowIndex;
        this.RemoveRecordAt(currentIndex);
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
      }
    }


    private void Dgv_HttpsRequest_MouseDown(object sender, MouseEventArgs e)
    {
      try
      {
        DataGridView.HitTestInfo hti = this.dgv_HttpsRequests.HitTest(e.X, e.Y);

        if (hti.RowIndex >= 0)
        {
          this.dgv_HttpsRequests.ClearSelection();
          this.dgv_HttpsRequests.Rows[hti.RowIndex].Selected = true;
        }
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
        this.dgv_HttpsRequests.ClearSelection();
      }
    }


    private void RequestDetailsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      var url = string.Empty;
      var cookie = string.Empty;
      var srcIp = string.Empty;
      var userAgent = string.Empty;

      try
      {
        url = this.dgv_HttpsRequests.SelectedRows[0].Cells["RemoteHost"].Value.ToString();
        srcIp = this.dgv_HttpsRequests.SelectedRows[0].Cells["SrcIP"].Value.ToString();
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
        Browser miniBrowser = new Browser(url, cookie, srcIp, userAgent);
        miniBrowser.Show();
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
        MessageBox.Show($"MiniBrowser unexpectedly crashed : {ex.ToString()}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
    
    #endregion

  }
}
