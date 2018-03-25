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
    /*
     *  De HTTPS requests worden door de HTTPS||00-11-22...  verfout
     *  omdat daar de bronpoort uitgewisseld wordt!!!
     *  Dat gaat bij deze plugin dan mis!
     * 
     */
    [TestMethod]
    public void Add_System()
    {
      //SystemRecord record = new SystemRecord("00-11-22-33-44-55", "192.168.1.100", "Firefox", "Intel", "Windows", "24-03-2018 18:08:21");
      //this.inst.AddRecord(record);
      this.inst.OnNewData("HTTPREQ||11-22-33-44-55-66||192.168.10.10||1234||8.8.8.8||80||" +
                          "GET /index.html HTTP/1.1..Host: host.com....<html><body>hello world</body></html>");
      this.inst.ProcessEntries();

      Assert.IsTrue(this.inst.SystemRecords.Count == 1);
    }

    #endregion

  }
}
