namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.SslStrip.DataTypes;
  using System;
  using System.Windows.Forms;

  public partial class Plugin_SslStrip
  {

    #region EVENTS

    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BT_Add_Click(object sender, EventArgs e)
    {
      IContentTypeState contentTypeObj = (IContentTypeState)(this.cb_ContentType.SelectedItem as ComboboxItem).Value;
      string contentType = contentTypeObj.UsedContentType;
      SslStripRecord tmpRecord = new SslStripRecord(this.tb_HostName.Text.Trim(), contentType);

      try
      {
        this.AddRecord(tmpRecord);
      }
      catch (Exception ex)
      {
        string msg = string.Format("Error occurred while adding SslStrip record for host name \"{0}\": {1}", this.tb_HostName.Text, ex.Message);
        this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
        MessageBox.Show(msg, "Can't add SslStrip record", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TB_Host_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        e.SuppressKeyPress = true;

        IContentTypeState contentTypeObj = (IContentTypeState)(this.cb_ContentType.SelectedItem as ComboboxItem).Value;
        string contentType = contentTypeObj.UsedContentType;
        SslStripRecord tmpRecord = new SslStripRecord(this.tb_HostName.Text.Trim(), contentType);

        try
        {
          this.AddRecord(tmpRecord);
        }
        catch (Exception ex)
        {
          this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
          MessageBox.Show(string.Format("{0}", ex.Message), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TSMI_Clear_Click(object sender, EventArgs e)
    {
      ClearRecordList();
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DGV_SslStripRecords_MouseUp(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        try
        {
          DataGridView.HitTestInfo hti = this.dgv_SslStrippingTargets.HitTest(e.X, e.Y);
          if (hti.RowIndex >= 0)
          {
            this.cms_SslStripRecords.Show(this.dgv_SslStrippingTargets, e.Location);
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
    private void DeleteEntryToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        int currentIndex = this.dgv_SslStrippingTargets.CurrentCell.RowIndex;
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
    private void DGV_SslStripRecords_MouseDown(object sender, MouseEventArgs e)
    {
      try
      {
        DataGridView.HitTestInfo hti = this.dgv_SslStrippingTargets.HitTest(e.X, e.Y);

        if (hti.RowIndex >= 0)
        {
          this.dgv_SslStrippingTargets.ClearSelection();
          this.dgv_SslStrippingTargets.Rows[hti.RowIndex].Selected = true;
          this.dgv_SslStrippingTargets.CurrentCell = this.dgv_SslStrippingTargets.Rows[hti.RowIndex].Cells[0];
        }
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
        this.dgv_SslStrippingTargets.ClearSelection();
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CB_ContentType_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        IContentTypeState newContentTypeState = (IContentTypeState)((ComboboxItem)this.cb_ContentType.SelectedItem).Value;
      }
      catch (Exception ex)
      {
        MessageBox.Show("Exception: " + ex.Message);
      }
    }

    #endregion

  }
}
