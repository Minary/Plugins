using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minary.Plugin.Main.HttpsRequest.DataTypes;
using MinaryLib;


namespace Minary.Plugin.Main.Tests
{

  [TestClass]
  public class Plugin_HttpsRequestsTests
  {

    #region MEMBERS

    private PluginProperties properties;
    private Plugin_HttpsRequests inst;
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
      this.properties.PatternSubDir = @"plugins\";

      this.inst = new Plugin_HttpsRequests(this.properties);
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
      Assert.IsTrue(this.inst.FoundHttpsRequests.Count == 0);
    }


    [TestMethod()]
    public void ProcessData_HTTPS_OPEN()
    {
      this.inst.OnNewData("HTTPS||11-22-33-44-55-66||192.168.10.10||1234||8.8.8.8||443||CONNECT:104.244.42.129");
      this.inst.ProcessEntries();

      Assert.IsTrue(this.inst.DataBatch.Count == 0);
      Assert.IsTrue(this.inst.FoundHttpsRequests.Count == 1);
    }


    [TestMethod()]
    public void ProcessData_DNS_REQUEST()
    {
      this.inst.OnNewData("DNSREP||11-22-33-44-55-66||8.8.8.8||53||192.168.10.10|||1234||www.twitter.com,104.244.42.129");
      this.inst.ProcessEntries();

      Assert.IsTrue(this.inst.DataBatch.Count == 0);
      Assert.IsTrue(this.inst.DnsCache.Count > 0);
      Assert.IsTrue(this.inst.IpCache.Count == 0);
      Assert.IsTrue(this.inst.FoundHttpsRequests.Count == 0);
    }


    [TestMethod()]
    public void ProcessData_DNS_and_HTTP_REQUEST()
    {
      this.inst.OnNewData("DNSREP||11-22-33-44-55-66||8.8.8.8||53||192.168.10.10||1234||www.twitter.com,104.244.42.129");
      this.inst.OnNewData("HTTPS||11-22-33-44-55-66||192.168.10.10||1234||104.244.42.129||443||CONNECT:104.244.42.129"); // www.twitter.com
      this.inst.ProcessEntries();

      Assert.IsTrue(this.inst.DataBatch.Count == 0);
      Assert.IsTrue(this.inst.DnsCache.Count == 1);
      Assert.IsTrue(this.inst.IpCache.Count == 1);
      Assert.IsTrue(this.inst.FoundHttpsRequests.Count == 1);
    }


    [TestMethod()]
    public void ProcessData_DNS_and_HTTP_REQUEST_use_cache()
    {
      this.inst.OnNewData("DNSREP||11-22-33-44-55-66||8.8.8.8||53||192.168.10.10||1234||www.twitter.com,104.244.42.129");
      this.inst.OnNewData("HTTPS||11-22-33-44-55-66||192.168.10.10||1234||104.244.42.129||443||CONNECT:104.244.42.129"); // www.twitter.com
      this.inst.OnNewData("HTTPS||11-22-33-44-55-66||192.168.10.10||1234||104.244.42.129||443||CONNECT:104.244.42.129");
      this.inst.ProcessEntries();

      Assert.IsTrue(this.inst.DataBatch.Count == 0);
      Assert.IsTrue(this.inst.DnsCache.Count == 1);
      Assert.IsTrue(this.inst.IpCache.Count == 1);
      Assert.IsTrue(this.inst.FoundHttpsRequests.Count == 2);
    }

    #endregion

  }
}
