namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.InjectCode.DataTypes;
  using System;
  using System.IO;
  using System.Windows.Forms;


  public partial class Plugin_HttpInjectCode
  {

    #region MEMBERS

    private string watermarkHttpRegex = "*.google.c*/some/path/file*";

    #endregion


    #region EVENTS

    private void TextBoxGotFocus(object sender, EventArgs e)
    {
      var tb = (TextBox)sender;
      if (tb.Text == this.watermarkHttpRegex)
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
        tb.Text = this.watermarkHttpRegex;
        tb.ForeColor = System.Drawing.Color.LightGray;
      }
      else
      {
        tb.ForeColor = System.Drawing.Color.Black;
      }
    }


    private void BT_Add_Click(object sender, EventArgs e)
    {
      try
      {
        string selectedTag = (this.cb_injectPosition.SelectedItem as ComboboxItem).Value.ToString();
        string position = this.rb_Before.Checked ? "before" : "after";
        string url = this.tb_RequestedUrlRegex.Text;
        string path = this.tb_InjectioinContentFile.Text;

        this.AddRecord(url, path, selectedTag, position);
      }
      catch (Exception ex)
      {
        string msg = $"Error occurred while adding inject code record: \r\n\r\n{ex.Message}";
        this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
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
      catch (Exception)
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
      if (e.Button != MouseButtons.Right)
      {
        return;
      }

      try
      {
        DataGridView.HitTestInfo hti = this.dgv_InjectionTriggerURLs.HitTest(e.X, e.Y);
        if (hti.RowIndex >= 0)
        {
          this.cms_InjectCode.Show(this.dgv_InjectionTriggerURLs, e.Location);
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
        DataGridView.HitTestInfo hti = this.dgv_InjectionTriggerURLs.HitTest(e.X, e.Y);

        if (hti.RowIndex >= 0)
        {
          this.dgv_InjectionTriggerURLs.ClearSelection();
          this.dgv_InjectionTriggerURLs.Rows[hti.RowIndex].Selected = true;
          this.dgv_InjectionTriggerURLs.CurrentCell = this.dgv_InjectionTriggerURLs.Rows[hti.RowIndex].Cells[0];
        }
      }
      catch (Exception)
      {
        this.dgv_InjectionTriggerURLs.ClearSelection();
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
    private void TB_AddRecord_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Enter)
      {
        return;
      }

      this.BT_Add_Click(sender, e);
    }

 
    private void BT_AddFile_Click(object sender, EventArgs e)
    {
      // Set the basic directory of the open file dialog.
      // If it exists jump into the "payload" dicrectory
      this.ofd_FileToInject.InitialDirectory = Path.Combine(this.pluginProperties.HostApplication.HostWorkingDirectory, General.PAYLOADS_DIR);

      if (this.ofd_FileToInject.ShowDialog() != DialogResult.OK)
      {
        return;
      }

      this.tb_InjectioinContentFile.Text = this.ofd_FileToInject.FileName;
      this.tb_InjectioinContentFile.TextAlign = HorizontalAlignment.Right;
      this.tb_InjectioinContentFile.SelectionStart = this.tb_InjectioinContentFile.Text.Length + 1;
    }


    private void RB_Position_CheckedChanged(object sender, EventArgs e)
    {
      this.rb_After.Checked = !this.rb_Before.Checked == true;
    }

    #endregion

  }
}
