using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minary.Plugin.Main.HttpSearch.DataTypes.Class;
using MinaryLib;


namespace Minary.Plugin.Main.Tests
{

  [TestClass()]
  public class Plugin_HttpSearchTests 
  {

    #region MEMBERS

    private PluginProperties properties;
    private Plugin_HttpSearch inst;
    private Moq.Mock<MinaryLib.Plugin.IPluginHost> minaryHost;

    #endregion


    #region TESTS

    [TestInitialize()]
    public void Initialize()
    {
      this.minaryHost = new Moq.Mock<MinaryLib.Plugin.IPluginHost>();
      this.properties = new PluginProperties();

      this.properties.HostApplication = (MinaryLib.Plugin.IPluginHost)this.minaryHost.Object;
      this.properties.ApplicationBaseDir = @"c:\temp\";
      this.properties.PluginBaseDir = @"c:\temp\";
      this.properties.PatternSubDir = @"patterns\";

      this.inst = new Plugin_HttpSearch(properties);      
    }


    [TestMethod()]
    public void AddDataRecord()
    {
      this.inst.OnNewData("Testrecord");
      Assert.IsTrue(this.inst.DataBatch.Count > 0);
    }


    [TestMethod()]
    public void ProcessData_NoPatterns()
    {
      this.inst.OnNewData("Testrecord");
      this.inst.ProcessEntries();
      Assert.IsTrue(this.inst.DataBatch.Count == 0);
      Assert.IsTrue(this.inst.HttpFindingRedcords.Count == 0);
    }


    [TestMethod()]
    public void ProcessData_NonMatchingPatterns()
    {
      RecordHttpSearch pattern = new RecordHttpSearch("GET", @"invalidhost\.com", "/invalid/path/.*", "invalidDataRegex");
      this.inst.HttpSearchRecords.Add(pattern);
      this.inst.OnNewData("Testrecord");
      this.inst.ProcessEntries();

      Assert.IsTrue(this.inst.DataBatch.Count == 0);
      Assert.IsTrue(this.inst.HttpSearchRecords.Count == 1);
      Assert.IsTrue(this.inst.HttpFindingRedcords.Count == 0);
    }

    
    [TestMethod()]
    public void ProcessData_MatchingMethod()
    {
      RecordHttpSearch pattern = new RecordHttpSearch("GET", @".*", "/.*", ".*");
      this.inst.AddRecord(pattern);
      this.inst.OnNewData("TCP||11-22-33-44-55-66||192.168.10.10||1234||8.8.8.8||80||" +
                          "GET /index.html HTTP/1.1..Host: host.com....<html><body>hello world</body></html>");
      this.inst.ProcessEntries();
      
      Assert.IsTrue(this.inst.HttpSearchRecords.Count == 1);
      Assert.IsTrue(this.inst.HttpFindingRedcords.Count == 1);
    }
    

    [TestMethod()]
    public void ProcessData_MatchingHost()
    {
      RecordHttpSearch pattern = new RecordHttpSearch("GET", @"intranet\.host\.com", "/.*", ".*");
      this.inst.AddRecord(pattern);
      this.inst.OnNewData("TCP||11-22-33-44-55-66||192.168.10.10||1234||8.8.8.8||80||" +
                          "GET /index.html HTTP/1.1..Host: intranet.host.com....<html><body>hello world</body></html>");
      this.inst.ProcessEntries();

      Assert.IsTrue(this.inst.HttpSearchRecords.Count == 1);
      Assert.IsTrue(this.inst.HttpFindingRedcords.Count == 1);
    }


    [TestMethod()]
    public void ProcessData_MatchingPath()
    {
      RecordHttpSearch pattern = new RecordHttpSearch("GET", ".*", @"/subdir/index\.html", ".*");
      this.inst.AddRecord(pattern);
      this.inst.OnNewData("TCP||11-22-33-44-55-66||192.168.10.10||1234||8.8.8.8||80||" +
                          "GET /subdir/index.html HTTP/1.1..Host: host.com....<html><body>hello world</body></html>");
      this.inst.ProcessEntries();

      Assert.IsTrue(this.inst.HttpSearchRecords.Count == 1);
      Assert.IsTrue(this.inst.HttpFindingRedcords.Count == 1);
    }


    [TestMethod()]
    public void ProcessData_MatchingData()
    {
      RecordHttpSearch pattern = new RecordHttpSearch("GET", ".*", ".*", "world");
      this.inst.AddRecord(pattern);
      this.inst.OnNewData("TCP||11-22-33-44-55-66||192.168.10.10||1234||8.8.8.8||80||" +
                          "GET /subdir/index.html HTTP/1.1..Host: host.com....<html><body>hello world</body></html>");
      this.inst.ProcessEntries();

      Assert.IsTrue(this.inst.HttpSearchRecords.Count == 1);
      Assert.IsTrue(this.inst.HttpFindingRedcords.Count == 1);
    }


    [TestMethod()]
    public void ProcessData_SearchUsernameAndPassword()
    {
      RecordHttpSearch pattern = new RecordHttpSearch("POST", ".*", ".*", @"user=(\w+)\b.*&pass=(\w)");
      this.inst.AddRecord(pattern);
      this.inst.OnNewData("TCP||11-22-33-44-55-66||192.168.10.10||1234||8.8.8.8||80||" +
                          "POST /login.php HTTP/1.1..Host: host.com....param1=someValue&user=joos&param2=anotherValue&pass=randomletters&param3=blah");
      this.inst.ProcessEntries();

      Assert.IsTrue(this.inst.HttpSearchRecords.Count == 1);
      Assert.IsTrue(this.inst.HttpFindingRedcords.Count == 1);
    }


    [TestMethod()]
    public void ProcessData_InvalidDataPattern()
    {
      RecordHttpSearch pattern = new RecordHttpSearch("GET", ".*", ".*", @"[world");
      Assert.ThrowsException<System.Exception>(() => this.inst.AddRecord(pattern));
    }

    #endregion

  }
}

