namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.HostMapping;
  using MinaryLib;
  using MinaryLib.Exceptions;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Windows.Forms;


  public partial class Plugin_HttpHostMapping : UserControl
  {

    #region EVENTS

    private void BT_AddRecord_Click(object sender, EventArgs e)
    {
      try
      {
        string scheme = this.rb_Http.Checked ? "http" : "https";
        this.AddRecord(this.tb_RequestedHost.Text, scheme, this.tb_MappedHost.Text);
      }
      catch (Exception ex)
      {
        string msg = string.Format("An error occurred while adding a new record:\r\n\r\n{0}", ex.Message);
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
        this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
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


    private void RB_Http_CheckedChanged(object sender, EventArgs e)
    {

    }

    #endregion

  }
}
