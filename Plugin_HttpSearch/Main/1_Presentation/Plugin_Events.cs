namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.HttpSearch.DataTypes;
  using System;
  using System.Windows.Forms;


  public partial class Plugin_HttpSearch
  {
    #region EVENTS

    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BT_Add_Click(object sender, EventArgs e)
    {
      var method = this.cb_Method?.SelectedItem?.ToString() ?? string.Empty;
      var type = this.cb_Type?.SelectedItem?.ToString() ?? string.Empty;
      var domain = (this.rb_Body?.Checked == true) ? "Body" : "Header";
      var hostRegex = this.tb_HostRegex?.Text?.Trim() ?? string.Empty;
      var pathRegex = this.tb_PathRegex?.Text?.Trim() ?? string.Empty;
      var dataRegex = this.tb_DataRegex?.Text?.Trim() ?? string.Empty;

      try
      {
        this.AddRecord(new RecordHttpSearch(method, type, domain, hostRegex, pathRegex, dataRegex));
      }
      catch (Exception ex)
      {
        this.Config.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
        MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
      catch (Exception)
      {
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DGV_MouseUp(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right)
      {
        return;
      }

      try
      {
        DataGridView.HitTestInfo hti = this.dgv_HttpSearch.HitTest(e.X, e.Y);
        if (hti.RowIndex >= 0)
        {
          this.cms_HttpSearch.Show(this.dgv_HttpSearch, e.Location);
        }
      }
      catch (Exception ex)
      {
        this.Config.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DGV_MouseDown(object sender, MouseEventArgs e)
    {
      try
      {
        DataGridView.HitTestInfo hti = this.dgv_HttpSearch.HitTest(e.X, e.Y);

        if (hti.RowIndex >= 0)
        {
          this.dgv_HttpSearch.ClearSelection();
          this.dgv_HttpSearch.Rows[hti.RowIndex].Selected = true;
          this.dgv_HttpSearch.CurrentCell = this.dgv_HttpSearch.Rows[hti.RowIndex].Cells[0];
        }
      }
      catch (Exception)
      {
        this.dgv_HttpSearch.ClearSelection();
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


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //private void PluginDNSPoisonUC_Load(object sender, EventArgs e)
    //{
    //  if (this.tb_Address.Text.Length == 0)
    //  {
    //    this.tb_Address.Text = this.Config.HostApplication.CurrentIP;
    //  }
    //}




    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnEnterAddRecord(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Enter)
      {
        return;
      }

      e.SuppressKeyPress = true;

      var method = this.cb_Method?.SelectedItem?.ToString() ?? string.Empty;
      var type = this.cb_Type?.SelectedItem?.ToString() ?? string.Empty;
      var domain = (this.rb_Body?.Checked == true) ? "Body" : "Header";
      var hostRegex = this.tb_HostRegex?.Text?.Trim() ?? string.Empty;
      var pathRegex = this.tb_PathRegex?.Text?.Trim() ?? string.Empty;
      var dataRegex = this.tb_DataRegex?.Text?.Trim() ?? string.Empty;

      try
      {
        this.AddRecord(new RecordHttpSearch(method, type, domain, hostRegex, pathRegex, dataRegex));
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
