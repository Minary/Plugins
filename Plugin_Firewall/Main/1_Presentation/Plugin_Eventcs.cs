namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.Firewall.DataTypes;
  using System;
  using System.Drawing;
  using System.Linq;
  using System.Windows.Forms;

  public partial class Plugin_Firewall
  {

    #region EVENTS

    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BT_Add_Click(object sender, EventArgs e)
    {
      string protocol = this.cb_Protocol.Text;
      string srcIp = this.cb_SrcIP.Text;
      string dstIp = this.cb_DstIP.Text;
      string srcPortLowerStr = this.tb_SrcPortLower.Text;
      string srcPortUpperStr = this.tb_SrcPortUpper.Text;
      string dstPortLowerStr = this.tb_DstPortLower.Text;
      string dstPortUpperStr = this.tb_DstPortUpper.Text;

      try
      {
        this.AddRecord(protocol, srcIp, dstIp, srcPortLowerStr, srcPortUpperStr, dstPortLowerStr, dstPortUpperStr);
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
        MessageBox.Show(string.Format(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning));
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BT_Clear_Click(object sender, EventArgs e)
    {
      this.ClearRecordList();
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DGV_Patterns_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      string firewallRuleId = string.Empty;
      FirewallRuleRecord firewallRule = null;

      if (e.RowIndex >= 0 && this.dgv_FWRules.SelectedRows.Count > 0)
      {
        try
        {
          firewallRuleId = this.dgv_FWRules["ID", e.RowIndex].Value.ToString();
          firewallRule = this.firewallRules.Where(k => k.ID == firewallRuleId).First();
        }
        catch (Exception ex)
        {
          this.pluginProperties.HostApplication.LogMessage("{0}: {1}", Config.PluginName, ex.Message);
          MessageBox.Show(string.Format("Error occurred : {0}", ex.Message), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return;
        }

        MessageBox.Show(string.Format("FW rule ID : {0}", firewallRuleId));
      }
    }


    /// <summary>
    /// DGV right mouse button click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DGV_FirewallRules_MouseUp(object sender, MouseEventArgs e)
    {
      DataGridView.HitTestInfo hitTestInfo;

      if (e.Button == MouseButtons.Right)
      {
        hitTestInfo = this.dgv_FWRules.HitTest(e.X, e.Y);

        // If cell selection is valid
        if (hitTestInfo.ColumnIndex >= 0 && hitTestInfo.RowIndex >= 0)
        {
          this.dgv_FWRules.Rows[hitTestInfo.RowIndex].Selected = true;
          this.cms_DataGrid_RightMouseButton.Show(this.dgv_FWRules, new Point(e.X, e.Y));
        }
      }
    }


    /// <summary>
    /// Delete firewall rule
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DeleteRuleToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.DeleteSelectedRecord();
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DeleteAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.ClearRecordList();
    }

    #endregion

  }
}
