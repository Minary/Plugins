namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.HttpSearch.DataTypes.Class;
  using System;
  using System.Text.RegularExpressions;
  using System.Windows.Forms;


  public partial class Plugin_HttpSearch
  {

    #region EVENTS

    #region SEARCH PATTERN RECORDS

    private void BT_AddSearchPattern_Click(object sender, EventArgs e)
    {
      var method = this.cb_Method?.SelectedItem?.ToString() ?? string.Empty;
      var hostRegex = this.tb_HostRegex?.Text?.Trim() ?? string.Empty;
      var pathRegex = this.tb_PathRegex?.Text?.Trim() ?? string.Empty;
      var dataRegex = this.tb_DataRegex?.Text?.Trim() ?? string.Empty;

      try
      {
        this.AddRecord(new RecordHttpSearch(method, hostRegex, pathRegex, dataRegex));
      }
      catch (Exception ex)
      {
        this.Config.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
        MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }


    private void TSMI_SearchPatternsDelete_Click(object sender, EventArgs e)
    {
      try
      {
        this.infrastructureLayer.DeleteSearchPatternRecordAt(dgv_HttpSearch.CurrentCell.RowIndex);
      }
      catch (Exception)
      {
      }
    }


    private void DGV_SearchPatterns_MouseUp(object sender, MouseEventArgs e)
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
          this.cms_HttpSearchPatterns.Show(this.dgv_HttpSearch, e.Location);
        }
      }
      catch (Exception ex)
      {
        this.Config.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
      }
    }


    private void DGV_SearchPatterns_MouseDown(object sender, MouseEventArgs e)
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


    private void TSMI_SearchPatternsClear_Click(object sender, EventArgs e)
    {
      try
      {
        this.infrastructureLayer.ClearSearchPatternRecordList();
      }
      catch
      {
      }
    }
    
    
    private void OnEnterAddSearchPatternRecord(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Enter)
      {
        return;
      }

      e.SuppressKeyPress = true;

      var method = this.cb_Method?.SelectedItem?.ToString() ?? string.Empty;
      var hostRegex = this.tb_HostRegex?.Text?.Trim() ?? string.Empty;
      var pathRegex = this.tb_PathRegex?.Text?.Trim() ?? string.Empty;
      var dataRegex = this.tb_DataRegex?.Text?.Trim() ?? string.Empty;
      var newRecord = new RecordHttpSearch(method,  hostRegex, pathRegex, dataRegex);
            
      try
      {
        this.AddRecord(newRecord);
      }
      catch (Exception ex)
      {
        this.Config.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
        MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }

    #endregion


    #region FINDINGS RECORDS

    private void TSMI_FindingsClear_Click(object sender, EventArgs e)
    {
      try
      {
        this.httpFindingRedcords.Clear();
      }
      catch
      {
      }
    }


    private void TSMI_FindingsDelete_Click(object sender, EventArgs e)
    {
      try
      {
        this.httpFindingRedcords.RemoveAt(dgv_Findings.CurrentCell.RowIndex);
      }
      catch
      {
      }
    }


    private void DGV_Findings_MouseUp(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right)
      {
        return;
      }

      try
      {
        DataGridView.HitTestInfo hti = this.dgv_Findings.HitTest(e.X, e.Y);
        if (hti.RowIndex >= 0)
        {
          this.cms_HttpSearchFindings.Show(this.dgv_Findings, e.Location);
        }
      }
      catch (Exception ex)
      {
        this.Config.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
      }
    }


    private void DGV_Findings_MouseDown(object sender, MouseEventArgs e)
    {
      try
      {
        DataGridView.HitTestInfo hti = this.dgv_Findings.HitTest(e.X, e.Y);

        if (hti.RowIndex >= 0)
        {
          this.dgv_Findings.ClearSelection();
          this.dgv_Findings.Rows[hti.RowIndex].Selected = true;
          this.dgv_Findings.CurrentCell = this.dgv_Findings.Rows[hti.RowIndex].Cells[0];
        }
      }
      catch (Exception)
      {
        this.dgv_Findings.ClearSelection();
      }
    }

    #endregion


    private void DGV_HttpFindings_DoubleClick(object sender, EventArgs e)
    {
      try
      {
        int currentIndex = this.dgv_Findings.CurrentCell.RowIndex;

        this.Config.HostApplication.LogMessage($"{this.Config.PluginName}: currentIndex:{currentIndex}");

        var requestData = this.dgv_Findings.Rows[currentIndex].Cells["Data"].Value.ToString();
        requestData = Regex.Replace(requestData, @"\.\.", "\r\n");

        var requestForm = new ShowRequest(requestData, this.tb_HostRegex.Text, this.tb_PathRegex.Text, this.tb_DataRegex.Text);
        requestForm.ShowDialog();
      }
      catch (Exception ex)
      {
        this.Config.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
      }
    }


    private void T_GuiUpdate_Tick(object sender, EventArgs e)
    {
      if (this.dataBatch?.Count > 0 == false)
      {
        return;
      }

      if (this.Config.HostApplication.AttackStarted == false)
      {
        return;
      }

      this.ProcessEntries();
    }

    #endregion

  }
}
