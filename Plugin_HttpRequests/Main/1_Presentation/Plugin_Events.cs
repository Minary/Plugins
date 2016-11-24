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
    private void DGV_HTTPRequests_MouseUp(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        try
        {
          DataGridView.HitTestInfo hti = this.dgv_HTTPRequests.HitTest(e.X, e.Y);
          if (hti.RowIndex >= 0)
          {
            this.cms_HTTPRequests.Show(this.dgv_HTTPRequests, e.Location);
          }
        }
        catch (Exception ex)
        {
          this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
        }
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
        UseFilter();
      }
    }



    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DGV_HTTPRequests_DoubleClick(object sender, EventArgs e)
    {
      try
      {
        BindingList<HTTPRequests> tmpHosts = new BindingList<HTTPRequests>();
        int currentIndex = this.dgv_HTTPRequests.CurrentCell.RowIndex;

        this.pluginProperties.HostApplication.LogMessage("{0}: currentIndex:{1}", this.Config.PluginName, currentIndex);
        string request = this.dgv_HTTPRequests.Rows[currentIndex].Cells["Request"].Value.ToString();

        request = Regex.Replace(request, @"\.\.", "\r\n");
        ShowRequest requestDetails = new ShowRequest(request);
        requestDetails.ShowDialog();
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
    private void T_GUIUpdate_Tick(object sender, EventArgs e)
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
        int currentIndex = this.dgv_HTTPRequests.CurrentCell.RowIndex;
        this.RemoveRecordAt(currentIndex);
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
    private void DGV_HTTPRequests_MouseDown(object sender, MouseEventArgs e)
    {
      try
      {
        DataGridView.HitTestInfo hti = this.dgv_HTTPRequests.HitTest(e.X, e.Y);

        if (hti.RowIndex >= 0)
        {
          this.dgv_HTTPRequests.ClearSelection();
          this.dgv_HTTPRequests.Rows[hti.RowIndex].Selected = true;
////pluginProperties.HostApplication.LogMessage("{0}: 4", Config.PluginName));
////          dgv_HTTPRequests.currentr.CurrentCell = dgv_HTTPRequests.Rows[hti.RowIndex].Cells[2];
        }
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
        this.dgv_HTTPRequests.ClearSelection();
      }
    }



    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RequestDetailsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      string url = string.Empty;
      string cookie = string.Empty;
      string srcIp = string.Empty;
      string userAgent = string.Empty;

      try
      {
        url = this.dgv_HTTPRequests.SelectedRows[0].Cells["URL"].Value.ToString();
        cookie = this.dgv_HTTPRequests.SelectedRows[0].Cells["SessionCookies"].Value.ToString();
        srcIp = this.dgv_HTTPRequests.SelectedRows[0].Cells["SrcIP"].Value.ToString();
        userAgent = string.Empty; //// dgv_HTTPRequests.SelectedRows[0].Cells[1].Value.ToString();
      }
      catch (ArgumentOutOfRangeException aoorex)
      {
        this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, aoorex.Message);
        return;
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
      }


      try
      {
        Browser miniBrowser = new Browser(url, cookie, srcIp, userAgent);
        miniBrowser.Show();
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
        MessageBox.Show("MiniBrowser unexpectedly crashed : " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    #endregion

  }
}
