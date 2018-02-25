namespace Minary.Plugin.Main
{
  using System;
  using System.Collections.Generic;
  using System.Text;
  using System.Text.RegularExpressions;
  using System.Windows.Forms;


  public partial class ShowRequest : Form
  {

    #region MEMBERS
    
    private Dictionary<string, List<string>> httpHeaders = new Dictionary<string, List<string>>();
    private string httpContentData = string.Empty;
    private string hostRegex = string.Empty;
    private string pathRegex = string.Empty;
    private string dataRegex = string.Empty;
    private string headerData = string.Empty;
    private string rawHttpData = string.Empty;
    private string[] httpHeaderLines;

    #endregion


    #region PUBLIC

    public ShowRequest(string rawHttpData, string hostRegex, string pathRegex, string dataRegex)
    {
      this.InitializeComponent();

      this.hostRegex = hostRegex;
      this.pathRegex = pathRegex;
      this.dataRegex = dataRegex;
      this.rawHttpData = rawHttpData;

      this.InitRawData(rawHttpData);
      var rtfFormatedText = this.GenerateRtf(this.rawHttpData, dataRegex);

      this.rtb_Request.Rtf = rtfFormatedText;
      this.rtb_Request.Select(0, 0);
    }

    #endregion


    #region PROTECTED
    
    protected override bool ProcessDialogKey(Keys keyData)
    {
      if (keyData == Keys.Escape)
      {
        this.Close();
        return false;
      }

      return base.ProcessDialogKey(keyData);
    }
    
    #endregion


    #region PRIVATE

    public string GenerateRtf(string rawRequest, string dataRegex)
    {
      var formatedData = string.Empty;

      if (string.IsNullOrEmpty(rawRequest) == true)
      {
        return formatedData;
      }
            
      string httpRequestLine = this.GetHttpRrequestLine(rawRequest);
      string httpHeaders = this.GenerateHttpHeaders();
      string httpContent = this.GenerateHttpContent(dataRegex);
      formatedData = string.Format(@"{{\rtf1\ansi \b {0} \b0 \line {1}\line\line{2} END }} ",
                                    httpRequestLine, 
                                    httpHeaders,
                                    httpContent);

      return formatedData;
    }

    

    private void InitRawData(string rawRequest)
    {
      int headerEndIndex = -1;
      if (Regex.Match(rawRequest, @"HTTP\/\d\.\d.*\.{4}", RegexOptions.Multiline | RegexOptions.Singleline).Success == true)
      {
        headerEndIndex = rawRequest.IndexOf("....", 0);
      }
      else if (Regex.Match(rawRequest, @"HTTP\/\d\.\d.*(\r\n){2}", RegexOptions.Multiline | RegexOptions.Singleline).Success == true)
      {
        headerEndIndex = rawRequest.IndexOf("\r\n\r\n", 0);
      }
      else if (Regex.Match(rawRequest, @"HTTP\/\d\.\d.*(\n){2}", RegexOptions.Multiline | RegexOptions.Singleline).Success == true)
      {
        headerEndIndex = rawRequest.IndexOf("\n\n", 0);
      }
      else
      {
        throw new Exception("The request data structure is invalid.");
      }

      this.headerData = rawRequest.Substring(0, headerEndIndex);
      this.httpContentData = rawRequest.Substring(headerEndIndex + 4);
      this.headerData = this.headerData.TrimStart(new char[] { '.', ' ', '\t', '\r', '\n' });
      this.httpHeaderLines = this.headerData.Split(new string[] { "..", "\r\n", "\n" }, StringSplitOptions.None);
      
      this.ParseHttpHeaders();
    }


    private void ParseHttpHeaders()
    {
      for (int i = 0; i < this.httpHeaderLines.Length; i++)
      {
        // Memorize header name/value pair when recognized
        var pattern = @"(\w+)\s*\:(.*)";
        MatchCollection matches = Regex.Matches(this.httpHeaderLines[i], pattern);
        if (matches.Count <= 0)
        {
          continue;
        }

        string name = matches[0].Groups[1].Value;
        string value = matches[0].Groups[2].Value;

        if (this.httpHeaders.ContainsKey(name) == false)
        {
          this.httpHeaders.Add(name, new List<string>());
        }

        this.httpHeaders[name].Add(value);
      }
    }


    private string GetHttpRrequestLine(string rawRequest)
    {
      var retVal = string.Empty;
      var pattern = @"^(GET|POST|PUT|HEAD|DELETE|OPTIONS|PROXY)\s+([^\s]+)\s+(HTTP\/\d\.\d)";

      MatchCollection matches = Regex.Matches(rawRequest, pattern);
      if (matches.Count <= 0)
      {
        return retVal;
      }

      string method = matches[0].Groups[1].Value;
      string path = matches[0].Groups[2].Value;
      string version = matches[0].Groups[3].Value;

      retVal = $"{method} {path} {version}";

      return retVal;
    }


    private string GenerateHttpHeaders()
    {
      var retVal = string.Empty;

      foreach (var name in this.httpHeaders.Keys)
      {
        foreach (var value in this.httpHeaders[name])
        {
          retVal += $@"\b {name} \b0 : {value} \line ";
        }
      }

      return retVal;
    }


    private string GenerateHttpContent(string dataRegex)
    {
      var sb = new StringBuilder(this.httpContentData);
      RegexOptions regOpts = RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline;
      Match match = Regex.Match(httpContentData, dataRegex, regOpts);

      if (match.Success == false ||
          match.Groups.Count <= 0)
      {
        return sb.ToString();
      }

      // Run through the finding list in reverse order.
      // We're doing this because we're going to change the content
      // and change the string's length which preserves the indices.
      for (int ctr = match.Groups.Count - 1; ctr >= 1; ctr--)
        {
          sb.Remove(match.Groups[ctr].Index, match.Groups[ctr].Length);
          sb.Insert(match.Groups[ctr].Index, string.Format(@" \b {0}\b0 ", match.Groups[ctr].Value));
        }

      return sb.ToString();
    }

    #endregion

  }
}
