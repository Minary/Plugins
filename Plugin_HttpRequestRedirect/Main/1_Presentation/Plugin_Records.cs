namespace Minary.Plugin.Main
{
  using Minary.Plugin.Main.RequestRedirect.DataTypes;
  using System;
  using System.IO;
  using System.Text.RegularExpressions;


  public partial class Plugin_HttpRequestRedirect
  {

    #region GUI RECORDS METHODS

    /// <summary>
    ///
    /// </summary>
    /// <param name="pRecord"></param>
    private delegate void AddRecordDelegate(string requestedResource, string replacementResource, string redirectType, string redirectDescription);
    private void AddRecord(string requestedResource, string replacementResource, string redirectType, string redirectDescription)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new AddRecordDelegate(this.AddRecord), new object[] { requestedResource, replacementResource, redirectType, redirectDescription });
        return;
      }

      string requestedScheme = string.Empty;
      string requestedHost = string.Empty;
      string requestedPath = string.Empty;


      // Verify if requested URL resource is valid
      Uri requestedUri;
      bool isValidUri = Uri.TryCreate(requestedResource, UriKind.Absolute, out requestedUri);

      if (!isValidUri)
      {
        throw new Exception("The requested resource URL is invalid");
      }

      if (requestedUri.Scheme != Uri.UriSchemeHttp && requestedUri.Scheme != Uri.UriSchemeHttps)
      {
        throw new Exception("The requested URL scheme is invalid.");
      }

      if (string.IsNullOrEmpty(requestedUri.Host) || string.IsNullOrWhiteSpace((requestedUri.Host)))
      {
        throw new Exception("The requested URL host is invalid.");
      }

      if (string.IsNullOrEmpty(requestedUri.PathAndQuery) || string.IsNullOrWhiteSpace((requestedUri.PathAndQuery)))
      {
        throw new Exception("The requested URL path is invalid.");
      }

      requestedScheme = requestedUri.Scheme;
      requestedHost = requestedUri.Host;
      requestedPath = requestedUri.PathAndQuery;

      // Verify if replacement URL resource is valid
      Uri replacementUri;
      bool isValidReplUri = Uri.TryCreate(replacementResource, UriKind.Absolute, out replacementUri);

      if (!isValidReplUri)
      {
        throw new Exception("The replacement resource URL is invalid");
      }

      if (replacementUri.Scheme != Uri.UriSchemeHttp && replacementUri.Scheme != Uri.UriSchemeHttps)
      {
        throw new Exception("The replacement URL scheme is invalid.");
      }

      if (string.IsNullOrEmpty(replacementUri.Host) || string.IsNullOrWhiteSpace((replacementUri.Host)))
      {
        throw new Exception("The replacement URL host is invalid.");
      }

      if (string.IsNullOrEmpty(replacementUri.PathAndQuery) || string.IsNullOrWhiteSpace((replacementUri.PathAndQuery)))
      {
        throw new Exception("The replacement URL path is invalid.");
      }

      // Verify if record already exists
      foreach (RequestRedirectRecord tmpRecord in this.requestRedirectRecords)
      {
        if (tmpRecord.RequestedHost == requestedHost && tmpRecord.RequestedPath == requestedPath)
        {
          throw new Exception("A record with this host name already exists.");
        }
      }

      // Verify if host name is correct
      if (!Regex.Match(requestedHost, @"^[\w\d\-_\.]+\.[a-z]{2,10}$", RegexOptions.IgnoreCase).Success)
      {
        throw new Exception("Something is wrong with the host name.");
      }

      // Verify if path is correct
      if (!Regex.Match(requestedPath, @"^/[^\s]*$", RegexOptions.IgnoreCase).Success)
      {
        throw new Exception("Something is wrong with the path.");
      }

      lock (this)
      {
        RequestRedirectRecord newRecord = new RequestRedirectRecord(requestedScheme, requestedHost, requestedPath, replacementResource, redirectType, redirectDescription);

        this.dgv_RequestRedirectURLs.SuspendLayout();
        this.requestRedirectRecords.Insert(0, newRecord);
        this.dgv_RequestRedirectURLs.ResumeLayout();
      }
    }

    #endregion

  }
}
