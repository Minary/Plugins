﻿namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.InjectFile.DataTypes;
  using System;
  using System.IO;
  using System.Windows.Forms;


  public partial class Plugin_HttpInjectFile
  {

    #region MEMBERS

    private string watermarkHttpRegex = "*.google.com/some/path/file*";

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
        string url = this.tb_RequestedUrlRegex.Text;
        string replacementResource = this.tb_ReplacementResource.Text;

        this.AddRecord(url, replacementResource);
      }
      catch (Exception ex)
      {
        string msg = $"Error occurred while adding inject file record: \r\n\r\n{ex.Message}";
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

      this.tb_ReplacementResource.Text = this.ofd_FileToInject.FileName;
      this.tb_ReplacementResource.TextAlign = HorizontalAlignment.Right;
      this.tb_ReplacementResource.SelectionStart = this.tb_ReplacementResource.Text.Length + 1;
    }

    #endregion

  }
}
