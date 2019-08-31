namespace Minary.Plugin.Main
{
  using System;
  using System.Windows.Forms;


  public partial class Plugin_HttpRequestRedirect : UserControl
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
        var splitter = this.cb_RedirectType.Text.Split('/');
        var redirectType = splitter[0];
        var redirectDescription = splitter[1];

        this.AddRecord(redirectType, redirectDescription, this.tb_RequestedUrlRegex.Text, this.tb_RedirectURL.Text);
      }
      catch (Exception ex)
      {
        var msg = $"Error occurred while adding request redirect record: \r\n\r\n{ex.Message}";
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
      catch(Exception)
      {
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
      catch(Exception)
      {
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DGV_RequestRedirect_MouseDown(object sender, MouseEventArgs e)
    {
      try
      {
        DataGridView.HitTestInfo hti = this.dgv_RequestRedirectURLs.HitTest(e.X, e.Y);

        if(hti.RowIndex >= 0)
        {
          this.dgv_RequestRedirectURLs.ClearSelection();
          this.dgv_RequestRedirectURLs.Rows[hti.RowIndex].Selected = true;
          this.dgv_RequestRedirectURLs.CurrentCell = this.dgv_RequestRedirectURLs.Rows[hti.RowIndex].Cells[0];
        }
      }
      catch(Exception)
      {
        this.dgv_RequestRedirectURLs.ClearSelection();
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TB_Host_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Enter)
      {
        return;
      }

      e.SuppressKeyPress = true;
      this.BT_Add_Click(null, null);
    }

    #endregion

  }
}
