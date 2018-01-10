namespace Minary.Plugin.Main
{
  using Minary.MiniBrowser;
  using System;
  using System.Windows.Forms;


  public partial class Plugin_Sessions
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
    private void TV_Sessions_AfterCollapse(object sender, TreeViewEventArgs e)
    {
      this.tv_Sessions.ExpandAll();
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TV_Sessions_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      // This wont work :  filterNode = tv_Sessions.SelectedNode;
      this.filterNode = this.tv_Sessions.GetNodeAt(e.X, e.Y);
      this.DgvFilter();
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BT_Patterns_Click(object sender, EventArgs e)
    {
      try
      {
        this.manageSessionsPresentationLayer.ShowDialog();
        this.InitSessionPatterns();
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
      }

      lock (this)
      {
        try
        {
          this.sessionPatterns = this.manageSessionsPresentationLayer.GetActiveSessionPatterns();
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
    private void DGV_Sessions_DoubleClick(object sender, EventArgs e)
    {
      string url = string.Empty;
      string cookies = string.Empty;
      string srcIp = string.Empty;
      string userAgent = string.Empty;

      if (this.dgv_Sessions.SelectedRows.Count > 0)
      {
        try
        {
          url = this.dgv_Sessions.SelectedRows[0].Cells["URL"].Value.ToString();
          cookies = this.dgv_Sessions.SelectedRows[0].Cells["SessionCookies"].Value.ToString();
          srcIp = this.dgv_Sessions.SelectedRows[0].Cells[1].Value.ToString();
          userAgent = this.dgv_Sessions.SelectedRows[0].Cells["Browser"].Value.ToString();

          Browser miniBrowser = new Browser(url, cookies, srcIp, userAgent);
          miniBrowser.Show();
        }
        catch (Exception ex)
        {
          this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
        }
      }
      else
      {
        try
        {
          if (this.tv_Sessions.SelectedNode.Text.ToLower().Contains("sessions"))
          {
            this.manageSessionsPresentationLayer.ShowDialog();
            this.InitSessionPatterns();
          }
        }
        catch (Exception ex)
        {
          this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
        }

        lock (this)
        {
          try
          {
            this.sessionPatterns = this.manageSessionsPresentationLayer.GetActiveSessionPatterns();
          }
          catch (Exception ex)
          {
            this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
          }
        }
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TSMI_Clear_Click(object sender, EventArgs e)
    {
      // Clear DataGridView
      if (this.sessionRecords != null)
      {
        this.sessionRecords.Clear();
      }

      this.dgv_Sessions.DataSource = this.sessionRecords;
      this.dgv_Sessions.Refresh();

      // Clear TreeView
      try
      {
        if (this.tv_Sessions?.Nodes.Count > 0)
        {
          foreach (TreeNode tmpNode in this.tv_Sessions.Nodes)
          {
            foreach (TreeNode subNode in tmpNode.Nodes)
            {
              if (subNode != null && subNode.Nodes.Count > 0)
              {
                subNode.Nodes.Clear();
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
      }

      // Select Main TV-Node.
      this.filterNode = this.tv_Sessions.Nodes[0];
      this.tv_Sessions.SelectedNode = this.tv_Sessions.Nodes[0];
      this.tv_Sessions.Select();

      //// myTreeView.SelectedNode = myTreeNode
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DGV_Sessions_MouseUp(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right)
      {
        return;
      }

      try
      {
        DataGridView.HitTestInfo hti = this.dgv_Sessions.HitTest(e.X, e.Y);
        if (hti.RowIndex >= 0)
        {
          this.cms_Sessions.Show(this.dgv_Sessions, e.Location);
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
    private void TSMI_ShowData_Click(object sender, EventArgs e)
    {
      var sessionNotes = new Main_Notes();
      var dataLine = string.Empty;

      foreach (Session.DataTypes.TheSessionRecord tmpSession in this.sessionRecords)
      {
        dataLine = $"\nSystem\t{tmpSession.SrcMAC} - {tmpSession.SrcIP}\nWebsite\t{tmpSession.URL}\nCookies\t{tmpSession.SessionCookies}\n";
        sessionNotes.AppendText(dataLine);
      }

      sessionNotes.Show();
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DeleteEntryToolStripMensuItem_Click(object sender, EventArgs e)
    {
      try
      {
        int currentIndex = this.dgv_Sessions.CurrentCell.RowIndex;
        this.RemoveRecordAt(currentIndex);
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
    private void DGV_Sessions_MouseDown(object sender, MouseEventArgs e)
    {
      try
      {
        DataGridView.HitTestInfo hti = this.dgv_Sessions.HitTest(e.X, e.Y);

        if (hti.RowIndex >= 0)
        {
          this.dgv_Sessions.ClearSelection();
          this.dgv_Sessions.Rows[hti.RowIndex].Selected = true;
          this.dgv_Sessions.CurrentCell = this.dgv_Sessions.Rows[hti.RowIndex].Cells[0];
        }
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage($"{this.Config.PluginName}: {ex.Message}");
        this.dgv_Sessions.ClearSelection();
      }
    }

    #endregion

  }
}
