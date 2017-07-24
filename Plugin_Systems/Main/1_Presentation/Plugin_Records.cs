namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.Systems.DataTypes;
  using System;
  using System.Linq;
  using System.Text.RegularExpressions;

  public partial class Plugin_Systems
  {

    #region GUI RECORDS METHODS

    /// <summary>
    ///
    /// </summary>
    public delegate void ClearRecordListDelegate();
    public void ClearRecordList()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new ClearRecordListDelegate(ClearRecordList), new object[] { });
        return;
      }

      lock (this)
      {
        this.dgv_Systems.SuspendLayout();

        try
        {
          this.systemRecords.Clear();
        }
        catch (Exception)
        {
        }

        this.dgv_Systems.ResumeLayout();
      }
    }


    /// <summary>
    ///
    /// </summary>
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
        this.dgv_Systems.SuspendLayout();

        try
        {
          this.systemRecords.RemoveAt(index);
        }
        catch (Exception)
        {
        }

        this.dgv_Systems.ResumeLayout();
      }
    }


    /// <summary>
    ///
    /// </summary>
    public delegate void AddRecordDelegate(SystemRecord record);
    public void AddRecord(SystemRecord record)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new AddRecordDelegate(this.AddRecord), new object[] { record });
        return;
      }

      if (record == null)
      {
        throw new RecordException("The record is invalid");
      }

      if (!Regex.Match(record.SrcMac.Trim(), @"^[\da-f]{1,2}[\-:][\da-f]{1,2}[\-:][\da-f]{1,2}[\-:][\da-f]{1,2}[\-:][\da-f]{1,2}[\-:][\da-f]{1,2}$", RegexOptions.IgnoreCase).Success)
      {
        throw new RecordException("Something is wrong with the MAC address");
      }

      if (!Regex.Match(record.SrcIp.Trim(), @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$", RegexOptions.IgnoreCase).Success)
      {
        throw new RecordException("Something is wrong with the IP address");
      }

      if (this.systemRecords.ToList().FindAll(elem => record.Id == elem.Id).Count() > 0)
      {
        throw new RecordExistsException(string.Format("System ({0}) already exists.", record.Id));
      }

      // Add new record to the data grid view
      lock (this)
      {
        this.dgv_Systems.SuspendLayout();
        this.systemRecords.Add(record);

        // Resize the DGV to the defined maximum size. \
        while (this.systemRecords.Count > MAX_TABLE_ROWS)
        {
          this.systemRecords.RemoveAt(0);
        }

        this.dgv_Systems.ResumeLayout();
      }
    }

    #endregion

  }
}