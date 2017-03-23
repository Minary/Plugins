namespace Minary.Plugin.Main
{
  using System;
  using System.Windows.Forms;


  public partial class Plugin_HttpRequestRedirect : UserControl
  {

    #region EVENTS

    private void BT_Add_Click(object sender, EventArgs e)
    {
      try
      {
        string[] splitter = this.cb_RedirectType.Text.Split('/');
        string redirectType = splitter[0];
        string redirectDescription = splitter[1];

        this.AddRecord(redirectType, redirectDescription, this.tb_RequestedURLRegex.Text, this.tb_RedirectURL.Text);
      }
      catch (Exception ex)
      {
        string msg = string.Format("Error occurred while adding request redirect record: \r\n\r\n{0}", ex.Message);
        this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
        MessageBox.Show(msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
      catch(Exception)
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
      catch(Exception)
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
      if(e.Button != MouseButtons.Right)
      {
        return;
      }

      try
      {
        DataGridView.HitTestInfo hti = this.dgv_RequestRedirectURLs.HitTest(e.X, e.Y);
        if(hti.RowIndex >= 0)
        {
          this.cms_RequestRedirect.Show(this.dgv_RequestRedirectURLs, e.Location);
        }
      }
      catch(Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DGV_RequestRedirect_MouseDown(object sender, MouseEventArgs e)
    {
      try
      {
        DataGridView.HitTestInfo hti = this.dgv_RequestRedirectURLs.HitTest(e.X, e.Y);

        if(hti.RowIndex >= 0)
        {
          this.dgv_RequestRedirectURLs.ClearSelection();
          this.dgv_RequestRedirectURLs.Rows[hti.RowIndex].Selected = true;
          this.dgv_RequestRedirectURLs.CurrentCell = this.dgv_RequestRedirectURLs.Rows[hti.RowIndex].Cells[0];
        }
      }
      catch(Exception)
      {
        this.dgv_RequestRedirectURLs.ClearSelection();
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TB_Host_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Enter)
      {
        return;
      }

      e.SuppressKeyPress = true;
      this.BT_Add_Click(null, null);
    }

    #endregion

  }
}
