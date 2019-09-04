namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.SslStrip.DataTypes;
  using System;
  using System.Windows.Forms;


  public partial class Plugin_SslStrip
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


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BT_Add_Click(object sender, EventArgs e)
    {
      var contentTypeObj = (IContentTypeState)(this.cb_ContentType.SelectedItem as ComboboxItem).Value;
      string contentType = contentTypeObj.UsedContentType;
      var tmpRecord = new SslStripRecord(this.tb_HostName.Text.Trim(), contentType);

      try
      {
        this.AddRecord(tmpRecord);
      }
      catch (Exception ex)
      {
        var msg = $"Error occurred while adding SslStrip record for host name \"{this.tb_HostName.Text}\": {ex.Message}";
        this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
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
          this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
          MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
      this.ClearRecordList();
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
        this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
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
        this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
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
