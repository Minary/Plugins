namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.Firewall.DataTypes;
  using System;
  using System.Text.RegularExpressions;

  public partial class Plugin_Firewall
  {

    #region GUI RECORDS METHODS


    /// <summary>
    ///
    /// </summary>
    /// <param name="protocol"></param>
    /// <param name="srcIp"></param>
    /// <param name="dstIp"></param>
    /// <param name="srcPortLowerStr"></param>
    /// <param name="srcPortUpperStr"></param>
    /// <param name="dstPortLowerStr"></param>
    /// <param name="dstPortUpperStr"></param>
    private delegate void EvaluateAndAddRecordDelegate(string protocol, string srcIP, string dstIP, string srcPortLowerStr, string srcPortUpperStr, string dstPortLowerStr, string dstPortUpperStr);
    public void EvaluateAndAddRecord(string protocol, string srcIp, string dstIp, string srcPortLowerStr, string srcPortUpperStr, string dstPortLowerStr, string dstPortUpperStr)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new EvaluateAndAddRecordDelegate(this.EvaluateAndAddRecord), new object[] { protocol, srcIp, dstIp, srcPortLowerStr, srcPortUpperStr, dstPortLowerStr, dstPortUpperStr });
        return;
      }

      int firstVisibleRowTop = -1;
      int srcPortLower = 0;
      int srcPortUpper = 0;
      int dstPortLower = 0;
      int dstPortUpper = 0;
      string id = string.Empty;
      string errorMessage = string.Empty;

      // Set default values where necessary
      if (string.IsNullOrEmpty(srcIp))
      {
        srcIp = "0.0.0.0";
      }

      if (string.IsNullOrEmpty(dstIp))
      {
        dstIp = "0.0.0.0";
      }

      if (string.IsNullOrEmpty(srcPortLowerStr))
      {
        srcPortLowerStr = "0";
      }

      if (string.IsNullOrEmpty(srcPortUpperStr))
      {
        srcPortUpperStr = "0";
      }

      if (string.IsNullOrEmpty(dstPortLowerStr))
      {
        dstPortLowerStr = "0";
      }

      if (string.IsNullOrEmpty(dstPortUpperStr))
      {
        dstPortUpperStr = "0";
      }

      // Parse ports
      try
      {
        srcPortLower = int.Parse(srcPortLowerStr);
        srcPortUpper = int.Parse(srcPortUpperStr);
        dstPortLower = int.Parse(dstPortLowerStr);
        dstPortUpper = int.Parse(dstPortUpperStr);
      }
      catch (Exception)
      {
        throw new Exception("Check the firewall rule port settings.");
      }

      // Arrange port settings
      if (srcPortLower == 0 && srcPortUpper > 0)
      {
        srcPortLower = srcPortUpper;
        srcPortLowerStr = srcPortUpperStr;
      }

      if (srcPortUpper == 0 && srcPortLower > 0)
      {
        srcPortUpper = srcPortLower;
        srcPortUpperStr = srcPortLowerStr;
      }

      if (dstPortLower == 0 && dstPortUpper > 0)
      {
        dstPortLower = dstPortUpper;
        dstPortLowerStr = dstPortUpperStr;
      }

      if (dstPortUpper == 0 && dstPortLower > 0)
      {
        dstPortUpper = dstPortLower;
        dstPortUpperStr = dstPortLowerStr;
      }

      // Check IP addresses/port format
      if (!Regex.Match(srcIp, @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$").Success)
      {
        errorMessage = "Something is wrong with the source IP";
      }
      else if (!Regex.Match(dstIp, @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$").Success)
      {
        errorMessage = "Something is wrong with the destination IP";
      }
      else if (!Regex.Match(srcPortLowerStr, @"^\d{1,5}$").Success || int.Parse(srcPortLowerStr) < 0 || int.Parse(srcPortLowerStr) > 65535)
      {
        errorMessage = "Something is wrong with the source port (lower)";
      }
      else if (!Regex.Match(srcPortUpperStr, @"^\d{1,5}$").Success || int.Parse(srcPortUpperStr) < 0 || int.Parse(srcPortUpperStr) > 65535)
      {
        errorMessage = "Something is wrong with the source port (upper)";
      }
      else if (!Regex.Match(dstPortLowerStr, @"^\d{1,5}$").Success || int.Parse(dstPortLowerStr) < 0 || int.Parse(dstPortLowerStr) > 65535)
      {
        errorMessage = "Something is wrong with the destination port (lower)";
      }
      else if (!Regex.Match(dstPortUpperStr, @"^\d{1,5}$").Success || int.Parse(dstPortUpperStr) < 0 || int.Parse(dstPortUpperStr) > 65535)
      {
        errorMessage = "Something is wrong with the destination port (upper)";
      }
      else if (dstPortLower > dstPortUpper)
      {
        errorMessage = "Lower destination port is greater than the upper port";
      }
      else if (srcPortLower > srcPortUpper)
      {
        errorMessage = "Lower source port is greater than the upper port";
      }

      if (errorMessage.Length > 0)
      {
        throw new Exception(errorMessage);
      }


      // Verify firewall rule ID for uniqueness
      id = string.Format("{0}{1}{2}{3}{4}{5}{6}", protocol, dstIp, dstPortLowerStr, dstPortUpperStr, srcIp, srcPortLowerStr, srcPortUpperStr);

      foreach (FirewallRuleRecord tmpRule in this.firewallRules)
        if (tmpRule.ID == id)
        {
          throw new Exception("This rule already exists");
        }

      // Memorize DataGridView position and selection
      firstVisibleRowTop = this.dgv_FWRules.FirstDisplayedScrollingRowIndex;

      // Add new rule to DataGridView
      lock (this)
      {
        this.dgv_FWRules.SuspendLayout();
        try
        {
          this.firewallRules.Insert(0, new FirewallRuleRecord(protocol, srcIp, srcPortLowerStr, srcPortUpperStr, dstIp, dstPortLowerStr, dstPortUpperStr));
        }
        catch (Exception)
        {
        }

        this.dgv_FWRules.ResumeLayout();
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    private delegate void ClearRecordListDelegate();
    private void ClearRecordList()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new ClearRecordListDelegate(this.ClearRecordList), new object[] { });
        return;
      }

      lock (this)
      {
        this.dgv_FWRules.SuspendLayout();

        try
        {
          this.firewallRules.Clear();
        }
        catch (Exception)
        {
        }

        this.dgv_FWRules.ResumeLayout();
      }
    }




    /// <summary>
    ///
    /// </summary>
    private delegate void DeleteSelectedRecordDelegate();
    private void DeleteSelectedRecord()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new DeleteSelectedRecordDelegate(this.DeleteSelectedRecord), new object[] { });
        return;
      }

      bool isLastLine = false;
      int firstVisibleRowTop = -1;
      int lastRowIndex = -1;
      int selectedRowIndex = -1;


      lock (this)
      {

        if (this.dgv_FWRules.CurrentRow != null && this.dgv_FWRules.CurrentRow == this.dgv_FWRules.Rows[this.dgv_FWRules.Rows.Count - 1])
        {
          isLastLine = true;
        }

        firstVisibleRowTop = this.dgv_FWRules.FirstDisplayedScrollingRowIndex;
        lastRowIndex = this.dgv_FWRules.Rows.Count - 1;

        if (this.dgv_FWRules.CurrentCell != null)
        {
          selectedRowIndex = this.dgv_FWRules.CurrentCell.RowIndex;
        }

        this.dgv_FWRules.SuspendLayout();

        try
        {
          int currentIndex = this.dgv_FWRules.CurrentCell.RowIndex;
          //// string lHostName = DGV_Spoofing.Rows[currentIndex].Cells["HostName"].Value.ToString();
          this.firewallRules.RemoveAt(currentIndex);
        }
        catch (Exception ex)
        {
          this.pluginProperties.HostApplication.LogMessage("{0}: {1}", this.Config.PluginName, ex.Message);
        }

        // Selected cell/row
        try
        {
          if (selectedRowIndex >= 0)
          {
            this.dgv_FWRules.CurrentCell = this.dgv_FWRules.Rows[selectedRowIndex].Cells[0];
          }
        }
        catch (Exception)
        {
        }

        // Reset position
        try
        {
          if (firstVisibleRowTop >= 0)
          {
            this.dgv_FWRules.FirstDisplayedScrollingRowIndex = firstVisibleRowTop;
          }
        }
        catch (Exception)
        {
        }

        this.dgv_FWRules.ResumeLayout();
      }
    }

    #endregion

  }
}
