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
      this.AddRecord();
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
      var firewallRuleId = string.Empty;
      FirewallRuleRecord firewallRule = null;

      if (e.RowIndex >= 0 && 
          this.dgv_FWRules.SelectedRows.Count > 0)
      {
        try
        {
          firewallRuleId = this.dgv_FWRules["ID", e.RowIndex].Value.ToString();
          firewallRule = this.firewallRules.Where(k => k.ID == firewallRuleId).First();
        }
        catch (Exception ex)
        {
          this.Config.HostApplication.LogMessage($"{Config.PluginName}: {ex.Message}");
          MessageBox.Show($"Error occurred : {ex.Message}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return;
        }

        MessageBox.Show($"FW rule ID : {firewallRuleId}");
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


    private void OnEnterAddRecord(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Enter)
      {
        return;
      }

      e.SuppressKeyPress = true;
      this.AddRecord();
    }

    #endregion


    #region PRIVATE

    private void AddRecord()
    {
      var protocol = this.cb_Protocol.Text;
      var srcIp = this.cb_SrcIP.Text;
      var dstIp = this.cb_DstIP.Text;
      var srcPortLowerStr = this.tb_SrcPortLower.Text;
      var srcPortUpperStr = this.tb_SrcPortUpper.Text;
      var dstPortLowerStr = this.tb_DstPortLower.Text;
      var dstPortUpperStr = this.tb_DstPortUpper.Text;

      try
      {
        this.EvaluateAndAddRecord(protocol, srcIp, dstIp, srcPortLowerStr, srcPortUpperStr, dstPortLowerStr, dstPortUpperStr);
      }
      catch (Exception ex)
      {
        this.Config.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
        MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }

    #endregion

  }
}
