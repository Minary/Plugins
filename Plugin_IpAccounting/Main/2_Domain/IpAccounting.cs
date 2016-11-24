namespace Minary.Plugin.Main.IpAccounting.Domain
{
  using Minary.Plugin.Main.IpAccounting.DataTypes;
  using MinaryLib.Plugin;
  using System.Collections.Generic;

  public class IpAccounting : IObservable
  {

    #region MEMBERS

    private static IpAccounting instance;
    private Infrastructure.IpAccounting infrastructureLayer;
    private List<IObserver> observerList;
    private List<AccountingItem> recordList;
    private IPlugin plugin;
    private IpAccountingConfig config;

    #endregion


    #region PROPERTIES

    public List<AccountingItem> RecordList { get { return (this.recordList); } private set { } }

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="IpAccounting"/> class.
    ///
    /// </summary>
    private IpAccounting(IpAccountingConfig config, IPlugin plugin)
    {
      this.plugin = plugin;
      this.config = config;
      this.recordList = new List<AccountingItem>();
      this.observerList = new List<IObserver>();
      this.infrastructureLayer = Infrastructure.IpAccounting.GetInstance(config, plugin, ref this.recordList);
    }


    /// <summary>
    /// Create single instance
    /// </summary>
    /// <returns></returns>
    public static IpAccounting GetInstance(IpAccountingConfig config, IPlugin plugin)
    {
      if (instance == null)
      {
        instance = new IpAccounting(config, plugin);
      }

      return (instance);
    }

    #endregion


    #region RECORDS

    /// <summary>
    ///
    /// </summary>
    /// <param name="record"></param>
    public void AddRecord(AccountingItem record)
    {
      this.recordList.Add(record);
      this.Notify();
    }


    /// <summary>
    ///
    /// </summary>
    public void EmptyRecordList()
    {
      this.infrastructureLayer.OnStopAttack();
      this.recordList.Clear();
      this.Notify();
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="index"></param>
    public void RemoveRecordAt(int index)
    {
      this.recordList.RemoveAt(index);
      this.Notify();
    }

    #endregion


    #region TEMPLATE
    

    #endregion


    #region EVENTS

    /// <summary>
    ///
    /// </summary>
    public void OnInit()
    {
      this.infrastructureLayer.OnInit();
      this.recordList.Clear();
      this.Notify();
    }

    /// <summary>
    ///
    /// </summary>
    public void OnReset()
    {
      this.infrastructureLayer.OnReset();
    }

    /// <summary>
    ///
    /// </summary>
    public void OnStartAttack(IpAccountingConfig config)
    {
      this.infrastructureLayer.OnStartAttack(config);
    }

    /// <summary>
    ///
    /// </summary>
    public void OnStopAttack()
    {
      this.infrastructureLayer.OnStopAttack();
    }

    #endregion


    #region OBSERVABLE INTERFACE METHODS

    public void AddObserver(IObserver observerObject)
    {
      this.observerList.Add(observerObject);
    }


    public void Notify()
    {
      foreach (IObserver tmpObserver in this.observerList)
      {
        tmpObserver.Update(this.recordList);
      }
    }

    #endregion

  }
}
