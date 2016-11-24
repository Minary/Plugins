namespace Minary.Plugin.Main
{
  using System;
  using System.Windows.Forms;

  public partial class Plugin_HttpAccounts
  {

    #region EVENTS

    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DGV_Accounts_MouseUp(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right)
      {
        return;
      }

      try
      {
        DataGridView.HitTestInfo hti = this.dgv_Accounts.HitTest(e.X, e.Y);
        if (hti.RowIndex >= 0)
        {
          this.cms_HTTPAccounts.Show(this.dgv_Accounts, e.Location);
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
    private void TSMI_Clear_Click(object sender, EventArgs e)
    {
      ClearRecordList();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DGV_Accounts_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
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
        int currentIndex = this.dgv_Accounts.CurrentCell.RowIndex;
        RemoveRecordAt(currentIndex);
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
    private void BT_Patterns_Click(object sender, EventArgs e)
    {
      this.manageHttpAccountsPresentationLayer.ShowDialog();
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DGV_Accounts_DoubleClick(object sender, EventArgs e)
    {
      this.manageHttpAccountsPresentationLayer.ShowDialog();
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

    #endregion

  }
}
