namespace Minary.Plugin.Main
{
  using System;
  using System.Windows.Forms;


  public partial class Plugin_HttpHostMapping : UserControl
  {

    #region MEMBERS

    private string watermarkHttpHost = "*.google.com";

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


    private void BT_AddRecord_Click(object sender, EventArgs e)
    {
      try
      {
        this.AddRecord(this.tb_RequestedHost.Text, this.tb_MappedHost.Text);
      }
      catch (Exception ex)
      {
        var msg = $"An error occurred while adding a new record:\r\n\r\n{ex.Message}";
        this.pluginProperties.HostApplication.LogMessage(msg);
        MessageBox.Show(msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }


    private void DGV_Spoofing_MouseUp(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right)
      {
        return;
      }

      try
      {
        DataGridView.HitTestInfo hti = this.dgv_HostMapping.HitTest(e.X, e.Y);
        if (hti.RowIndex >= 0)
        {
          this.cms_HostMapping.Show(this.dgv_HostMapping, e.Location);
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
    private void DGV_Spoofing_MouseDown(object sender, MouseEventArgs e)
    {
      try
      {
        DataGridView.HitTestInfo hti = this.dgv_HostMapping.HitTest(e.X, e.Y);

        if (hti.RowIndex >= 0)
        {
          this.dgv_HostMapping.ClearSelection();
          this.dgv_HostMapping.Rows[hti.RowIndex].Selected = true;
          this.dgv_HostMapping.CurrentCell = this.dgv_HostMapping.Rows[hti.RowIndex].Cells[0];
        }
      }
      catch (Exception)
      {
        this.dgv_HostMapping.ClearSelection();
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


    private void DeleteRuleToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.DeleteSelectedRecord();
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TB_AddRecord_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Enter)
      {
        return;
      }

      this.BT_AddRecord_Click(sender, e);
    }

    #endregion

  }
}
