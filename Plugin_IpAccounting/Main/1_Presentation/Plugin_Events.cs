namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.IpAccounting.DataTypes;
  using System;
  using System.ComponentModel;
  using System.Drawing;
  using System.Windows.Forms;

  public partial class Plugin_IpAccounting
  {

    #region EVENTS


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BT_Clear_Click(object sender, EventArgs e)
    {
      this.domainLayer.EmptyRecordList();
    }



    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DGV_TrafficData_MouseUp(object sender, MouseEventArgs e)
    {
      DataGridView.HitTestInfo hitTestInfo;
      if (e.Button == MouseButtons.Right)
      {
        hitTestInfo = this.dgv_TrafficData.HitTest(e.X, e.Y);

        // If cell selection is valid
        if (hitTestInfo.ColumnIndex >= 0 && hitTestInfo.RowIndex >= 0)
        {
          //// dgv_TrafficData.Rows[hitTestInfo.RowIndex].Selected = true;
          cms_DataGrid_RightMouseButton.Show(this.dgv_TrafficData, new Point(e.X, e.Y));
        }
      }
    }



    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.domainLayer.EmptyRecordList();
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PluginIpAccountingUC_MouseDown(object sender, MouseEventArgs e)
    {
      try
      {
        DataGridView.HitTestInfo hti = this.dgv_TrafficData.HitTest(e.X, e.Y);

        if (hti.RowIndex >= 0)
        {
          this.dgv_TrafficData.ClearSelection();
          this.dgv_TrafficData.Rows[hti.RowIndex].Selected = true;
          this.dgv_TrafficData.CurrentCell = this.dgv_TrafficData.Rows[hti.RowIndex].Cells[0];
        }
      }
      catch (Exception ex)
      {
        this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
        this.dgv_TrafficData.ClearSelection();
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RB_Service_Click(object sender, EventArgs e)
    {
      this.accountingOutputType = "-p";

      this.dgv_TrafficData.Columns.Clear();

      DataGridViewTextBoxColumn columnServiceName = new DataGridViewTextBoxColumn();
      columnServiceName.DataPropertyName = "Basis";
      columnServiceName.Name = "Basis";
      columnServiceName.HeaderText = "Service";
      columnServiceName.ReadOnly = true;
      //// columnServiceName.Width = 120;
      columnServiceName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      columnServiceName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.dgv_TrafficData.Columns.Add(columnServiceName);


      DataGridViewTextBoxColumn columnPacketCounter = new DataGridViewTextBoxColumn();
      columnPacketCounter.DataPropertyName = "PacketCounter";
      columnPacketCounter.Name = "PacketCounter";
      columnPacketCounter.HeaderText = "No. packets";
      columnPacketCounter.ReadOnly = true;
      columnPacketCounter.Width = 120;
      columnPacketCounter.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.dgv_TrafficData.Columns.Add(columnPacketCounter);
      this.dgv_TrafficData.Columns["PacketCounter"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

      DataGridViewTextBoxColumn columnDataVolume = new DataGridViewTextBoxColumn();
      columnDataVolume.DataPropertyName = "DataVolume";
      columnDataVolume.Name = "DataVolume";
      columnDataVolume.HeaderText = "Data volume";
      columnDataVolume.ReadOnly = true;
      columnDataVolume.Width = 120;
      columnDataVolume.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.dgv_TrafficData.Columns.Add(columnDataVolume);
      this.dgv_TrafficData.Columns["DataVolume"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

      DataGridViewTextBoxColumn columnLastUpdate = new DataGridViewTextBoxColumn();
      columnLastUpdate.DataPropertyName = "LastUpdate";
      columnLastUpdate.Name = "LastUpdate";
      columnLastUpdate.HeaderText = "Last Update";
      columnLastUpdate.ReadOnly = true;
      columnLastUpdate.Width = 267;
      //// columnLastUpdate.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      columnLastUpdate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.dgv_TrafficData.Columns.Add(columnLastUpdate);
    }



    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RB_LocalIP_Click(object sender, EventArgs e)
    {
      this.accountingOutputType = "-s";

      this.dgv_TrafficData.Columns.Clear();
      DataGridViewTextBoxColumn columnServiceName = new DataGridViewTextBoxColumn();
      columnServiceName.DataPropertyName = "Basis";
      columnServiceName.Name = "Basis";
      columnServiceName.HeaderText = "Local IP";
      columnServiceName.ReadOnly = true;
      //// columnServiceName.Width = 120;
      columnServiceName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      columnServiceName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.dgv_TrafficData.Columns.Add(columnServiceName);

      DataGridViewTextBoxColumn columnPacketCounter = new DataGridViewTextBoxColumn();
      columnPacketCounter.DataPropertyName = "PacketCounter";
      columnPacketCounter.Name = "PacketCounter";
      columnPacketCounter.HeaderText = "No. packets";
      columnPacketCounter.ReadOnly = true;
      columnPacketCounter.Width = 120;
      columnPacketCounter.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.dgv_TrafficData.Columns.Add(columnPacketCounter);
      this.dgv_TrafficData.Columns["PacketCounter"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

      DataGridViewTextBoxColumn columnDataVolume = new DataGridViewTextBoxColumn();
      columnDataVolume.DataPropertyName = "DataVolume";
      columnDataVolume.Name = "DataVolume";
      columnDataVolume.HeaderText = "Data volume";
      columnDataVolume.ReadOnly = true;
      columnDataVolume.Width = 120;
      columnDataVolume.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.dgv_TrafficData.Columns.Add(columnDataVolume);
      this.dgv_TrafficData.Columns["DataVolume"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

      DataGridViewTextBoxColumn columnLastUpdate = new DataGridViewTextBoxColumn();
      columnLastUpdate.DataPropertyName = "LastUpdate";
      columnLastUpdate.Name = "LastUpdate";
      columnLastUpdate.HeaderText = "Last Update";
      columnLastUpdate.ReadOnly = true;
      columnLastUpdate.Width = 267;
      //// columnLastUpdate.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      columnLastUpdate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.dgv_TrafficData.Columns.Add(columnLastUpdate);
    }



    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RB_RemoteIP_Click(object sender, EventArgs e)
    {
      this.accountingOutputType = "-d";

      this.dgv_TrafficData.Columns.Clear();
      DataGridViewTextBoxColumn columnServiceName = new DataGridViewTextBoxColumn();
      columnServiceName.DataPropertyName = "Basis";
      columnServiceName.Name = "Basis";
      columnServiceName.HeaderText = "Remote IP";
      columnServiceName.ReadOnly = true;
      //// columnServiceName.Width = 120;
      columnServiceName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      columnServiceName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.dgv_TrafficData.Columns.Add(columnServiceName);


      DataGridViewTextBoxColumn columnPacketCounter = new DataGridViewTextBoxColumn();
      columnPacketCounter.DataPropertyName = "PacketCounter";
      columnPacketCounter.Name = "PacketCounter";
      columnPacketCounter.HeaderText = "No. packets";
      columnPacketCounter.ReadOnly = true;
      columnPacketCounter.Width = 120;
      columnPacketCounter.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.dgv_TrafficData.Columns.Add(columnPacketCounter);
      this.dgv_TrafficData.Columns["PacketCounter"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

      DataGridViewTextBoxColumn columnDataVolume = new DataGridViewTextBoxColumn();
      columnDataVolume.DataPropertyName = "DataVolume";
      columnDataVolume.Name = "DataVolume";
      columnDataVolume.HeaderText = "Data volume";
      columnDataVolume.ReadOnly = true;
      columnDataVolume.Width = 120;
      columnDataVolume.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.dgv_TrafficData.Columns.Add(columnDataVolume);
      this.dgv_TrafficData.Columns["DataVolume"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

      DataGridViewTextBoxColumn columnLastUpdate = new DataGridViewTextBoxColumn();
      columnLastUpdate.DataPropertyName = "LastUpdate";
      columnLastUpdate.Name = "LastUpdate";
      columnLastUpdate.HeaderText = "Last Update";
      columnLastUpdate.ReadOnly = true;
      columnLastUpdate.Width = 267;
      columnLastUpdate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.dgv_TrafficData.Columns.Add(columnLastUpdate);


      //// cDataArray = new BindingList<AccountingItem>();
      //// dgv_TrafficData.DataSource = cDataArray;
      this.accountingRecords = new BindingList<AccountingItem>();
      this.dgv_TrafficData.DataSource = this.accountingRecords;
    }

    #endregion

  }
}
