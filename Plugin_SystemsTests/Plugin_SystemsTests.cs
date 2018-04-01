using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minary.Plugin.Main.Systems.DataTypes;
using MinaryLib;
using Minary.Plugin.Main;

namespace Plugin_SystemsTests
{
  [TestClass]
  public class Plugin_SystemsTests
  {

    #region MEMBERS

    private PluginProperties properties;
    private Minary.Plugin.Main.Plugin_Systems inst;
    private Moq.Mock<MinaryLib.Plugin.IPluginHost> minaryHost;

    #endregion



    [TestInitialize()]
    public void Initialize()
    {
      this.minaryHost = new Moq.Mock<MinaryLib.Plugin.IPluginHost>();
      this.properties = new PluginProperties();

      this.properties.HostApplication = (MinaryLib.Plugin.IPluginHost)this.minaryHost.Object;
      this.properties.ApplicationBaseDir = @"c:\temp\";
      this.properties.PluginBaseDir = @"c:\temp\";
      this.properties.PatternSubDir = @"patterns\";

      this.inst = new Minary.Plugin.Main.Plugin_Systems(properties);
    }


    #region TESTS

    [TestMethod]
    public void Add_System()
    {
      this.inst.OnNewData("HTTPREQ||11-22-33-44-55-66||192.168.10.10||1234||8.8.8.8||80||" +
                          "GET /index.html HTTP/1.1..Host: host.com....<html><body>hello world</body></html>");
      this.inst.ProcessEntries();

      Assert.IsTrue(this.inst.SystemRecords.Count == 1);
    }


    [TestMethod]
    public void Update_System_HTTPREQ()
    {
      // Add initial record
      this.inst.OnNewData("HTTPREQ||11-22-33-44-55-66||192.168.10.10||1234||8.8.8.8||80||" +
                          "GET /index.html HTTP/1.1..Host: host.com....<html><body>hello world</body></html>");
      this.inst.ProcessEntries();
      Assert.IsTrue(this.inst.SystemRecords.Count == 1);

      // Add secondnd record
      var timestampOld = this.inst.SystemRecords[0].LastSeen;
      System.Threading.Thread.Sleep(2000);
      this.inst.OnNewData("HTTPREQ||11-22-33-44-55-66||192.168.10.10||1234||8.8.8.8||80||" +
                                "GET /index.html HTTP/1.1..Host: host.com....<html><body>hello world</body></html>");
      this.inst.ProcessEntries();
      var timestampNew = this.inst.SystemRecords[0].LastSeen;

      Assert.IsTrue(timestampOld != timestampNew);
    }


    [TestMethod]
    public void Update_System_HTTPS()
    {
      // Add initial record
      this.inst.OnNewData("HTTPS||11-22-33-44-55-66||192.168.10.10||1234||8.8.8.8||443||" +
                          "CONNECT:8.8.8.8");
      this.inst.ProcessEntries();
      Assert.IsTrue(this.inst.SystemRecords.Count == 1);

      // Add secondnd record
      var timestampOld = this.inst.SystemRecords[0].LastSeen;
      System.Threading.Thread.Sleep(2000);
      this.inst.OnNewData("HTTPS||11-22-33-44-55-66||192.168.10.10||1234||8.8.8.8||443||" +
                          "CONNECT:8.8.8.8");
      this.inst.ProcessEntries();
      var timestampNew = this.inst.SystemRecords[0].LastSeen;

      Assert.IsTrue(timestampOld != timestampNew);
    }


    [TestMethod]
    public void Update_System_DNSREQ()
    {
      // Add initial record
      this.inst.OnNewData("DNSREQ||11-22-33-44-55-66||192.168.10.10||1234||8.8.8.8||53||" +
                          "minary.io");
      this.inst.ProcessEntries();
      Assert.IsTrue(this.inst.SystemRecords.Count == 1);

      // Add secondnd record
      var timestampOld = this.inst.SystemRecords[0].LastSeen;
      System.Threading.Thread.Sleep(2000);
      this.inst.OnNewData("DNSREQ||11-22-33-44-55-66||192.168.10.10||1234||8.8.8.8||53||" +
                          "minary.io");
      this.inst.ProcessEntries();
      var timestampNew = this.inst.SystemRecords[0].LastSeen;
      
      Assert.IsTrue(timestampOld != timestampNew);
    }


    #endregion

  }
}
