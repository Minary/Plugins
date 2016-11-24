namespace Minary.Plugin.Main.Infrastructure
{
  using System;
  using System.Collections;
  using System.IO;
  using System.Text.RegularExpressions;

  public class MacVendor
  {

    #region MEMBERS

    private static MacVendor instance;
    private string macVendorList = @"data\MACVendors.txt";
    private Hashtable macVendorMap;

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="MacVendor"/> class.
    ///
    /// </summary>
    public MacVendor()
    {
      this.macVendorMap = new Hashtable();
      this.LoadMacVendorList();
    }


    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public static MacVendor GetInstance()
    {
      if (instance == null)
      {
        instance = new MacVendor();
      }

      return instance;
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="macAddress"></param>
    /// <returns></returns>
    public string GetVendorByMac(string macAddress)
    {
      string retVal = string.Empty;
      string vendor = string.Empty;
      Match match;

      if (!string.IsNullOrEmpty(macAddress))
      {
        // Determine vendor
        vendor = string.Empty;
        if ((match = Regex.Match(macAddress, @"([\da-f]{1,2})[:\-]{1}([\da-f]{1,2})[:\-]{1}([\da-f]{1,2})[:\-]{1}.*", RegexOptions.IgnoreCase)).Success)
        {
          string oct1 = match.Groups[1].Value.ToString();
          string oct2 = match.Groups[2].Value.ToString();
          string oct3 = match.Groups[3].Value.ToString();
          string tmp = string.Format("{0}{1}{2}", oct1, oct2, oct3).ToLower();

          retVal = this.macVendorMap.ContainsKey(tmp) ? this.macVendorMap[tmp].ToString() : string.Empty;
        }
      }

      return retVal;
    }

    #endregion


    #region PRIVATE

    /// <summary>
    ///
    /// </summary>
    private void LoadMacVendorList()
    {
      string tmpLine = string.Empty;
      StreamReader streamReader = null;
      char[] delimiters = "\t ".ToCharArray();
      string macAddress = string.Empty;
      string vendorName = string.Empty;

      try
      {
        streamReader = new StreamReader(this.macVendorList);
        while ((tmpLine = streamReader.ReadLine()) != null)
        {
          tmpLine = tmpLine.Trim();

          try
          {
            string[] splitter = tmpLine.Split(delimiters, 2);


            if (splitter.Length == 2)
            {
              macAddress = splitter[0].ToLower();
              vendorName = splitter[1];
              this.macVendorMap.Add(macAddress, vendorName);
            }
          }
          catch (Exception)
          {
            ////            LogConsole.Main.LogConsole.LogInstance.pushMsg(string.Format("Unable to load MAC/Vendor pair: {0}/{1}   ({2})", tmpLine, macAddress, vendorName));
          }
        }
      }
      catch (FileNotFoundException)
      {
        ////        LogConsole.Main.LogConsole.LogInstance.pushMsg(string.Format("{0} not found!", macVendorList));
      }
      catch (Exception)
      {
        ////        LogConsole.Main.LogConsole.LogInstance.pushMsg(string.Format("Error occurred while opening {0}: {1}", macVendorList, ex.StackTrace));
      }
      finally
      {
        if (streamReader != null)
        {
          streamReader.Close();
        }
      }
    }

    #endregion

  }
}