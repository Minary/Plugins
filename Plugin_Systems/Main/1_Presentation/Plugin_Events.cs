namespace Minary.Plugin.Main
{
  using System;
  using System.Windows.Forms;


  public partial class Plugin_Systems
  {

    #region EVENTS

    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void T_GuiUpdate_Tick(object sender, EventArgs e)
    {
      this.ProcessEntries();
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
      int currentIndex = this.dgv_Systems.CurrentCell.RowIndex;
      this.RemoveRecordAt(currentIndex);
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DGV_Systems_DoubleClick(object sender, EventArgs e)
    {
      this.manageSystemsPresentationLayer.ShowDialog();

      lock (this)
      {
        try
        {
          this.systemPatterns = this.manageSystemsPresentationLayer.GetActiveSystemPatterns();
        }
        catch (Exception ex)
        {
          this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
        }
      }
    }





    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BT_Patterns_Click(object sender, EventArgs e)
    {
      this.manageSystemsPresentationLayer.ShowDialog();

      lock (this)
      {
        try
        {
          this.systemPatterns = this.manageSystemsPresentationLayer.GetActiveSystemPatterns();
        }
        catch (Exception ex)
        {
          this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
        }
      }
    }



    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DGV_Systems_MouseDown(object sender, MouseEventArgs e)
    {
      try
      {
        DataGridView.HitTestInfo hti = this.dgv_Systems.HitTest(e.X, e.Y);

        if (hti.RowIndex >= 0)
        {
          this.dgv_Systems.ClearSelection();
          this.dgv_Systems.Rows[hti.RowIndex].Selected = true;
          this.dgv_Systems.CurrentCell = this.dgv_Systems.Rows[hti.RowIndex].Cells[0];
        }
      }
      catch (Exception)
      {
        this.dgv_Systems.ClearSelection();
      }
    }

    #endregion

  }
}
