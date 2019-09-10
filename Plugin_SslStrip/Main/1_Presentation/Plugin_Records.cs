namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.SslStrip.DataTypes;
  using System;
  using System.Text.RegularExpressions;

  public partial class Plugin_SslStrip
  {

    #region GUI RECORDS METHODS

    /// <summary>
    ///
    /// </summary>
    /// <param name="pRecord"></param>
    private delegate void AddRecordDelegate(SslStripRecord record);
    private void AddRecord(SslStripRecord record)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new AddRecordDelegate(this.AddRecord), new object[] { record });
        return;
      }

      if (record == null)
      {
        return;
      }

      // Update DataGridView
      this.dgv_SslStrippingTargets.SuspendLayout();

      lock (this)
      {
        // Verify if record already exists
        foreach (SslStripRecord tmpRecord in this.sslStripRecords)
        {
          if (tmpRecord.HostName == record.HostName && tmpRecord.ContentType == record.ContentType)
          {
            throw new Exception("A record with this host name already exists.");
          }
        }

        // Verify if HostName is correct
        if (!Regex.Match(record.HostName, @"^[\w\d\-_\.\*]+\.[\*\w]{1,10}$", RegexOptions.IgnoreCase).Success)
        {
          throw new Exception("Something is wrong with the host name.");
        }

        // Verify if ContentType is correct
        if (!Regex.Match(record.ContentType, @"^[\w\d\-_]+\/[\w\d\-_]+$", RegexOptions.IgnoreCase).Success)
        {
          throw new Exception("Something is wrong with the content type definition.");
        }

        this.sslStripRecords.Insert(0, record);
        this.dgv_SslStrippingTargets.ResumeLayout();
      }
    }



    /// <summary>
    ///
    /// </summary>
    /// <param name="pIndex"></param>
    public delegate void RemoveRecordAtDelegate(int index);
    public void RemoveRecordAt(int index)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new RemoveRecordAtDelegate(this.RemoveRecordAt), new object[] { index });
        return;
      }

      lock (this)
      {
        this.dgv_SslStrippingTargets.SuspendLayout();

        try
        {
          this.sslStripRecords.RemoveAt(index);
        }
        catch (Exception)
        {
        }

        this.dgv_SslStrippingTargets.ResumeLayout();
      }
    }


    /// <summary>
    ///
    /// </summary>
    private delegate void ClearRecordListDelegate();
    public void ClearRecordList()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new ClearRecordListDelegate(this.ClearRecordList), new object[] { });
        return;
      }

      lock (this)
      {
        this.dgv_SslStrippingTargets.SuspendLayout();

        try
        {
          this.sslStripRecords.Clear();
        }
        catch (Exception)
        {
        }

        this.dgv_SslStrippingTargets.ResumeLayout();
      }
    }

    #endregion

  }
}