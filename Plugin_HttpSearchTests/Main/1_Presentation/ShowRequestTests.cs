using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;


namespace Minary.Plugin.Main.Tests
{
  [TestClass()]
  public class ShowRequestTests
  {

    #region TESTS
    
    [TestMethod()]
    public void ShowRequestTest()
    {
      var httpData = "POST /login.php HTTP/1.1..Host: host.com....param1=someValue&user=joos&param2=anotherValue&pass=randomletters&param3=blah";
      var hostRegex = ".*";
      var pathRegex = ".*";
      var dataRegex = @"user=(\w+)\b.*&pass=(\w+)";

      ShowRequest inst = new ShowRequest(httpData, hostRegex, pathRegex, dataRegex);
      string rtfOutput = inst.GenerateRtf(httpData, dataRegex);
      
      Assert.IsTrue(Regex.Match(rtfOutput, @"user=\s*\\b\s*joos\s*\\b\s*").Success == true);
      Assert.IsTrue(Regex.Match(rtfOutput, @"pass=\s*\\b\s*randomletters\s*\\b\s*").Success == true);
    }

    #endregion

  }
}
