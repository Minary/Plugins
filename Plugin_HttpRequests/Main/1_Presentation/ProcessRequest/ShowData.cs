using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Minary.Plugin.Main
{
  public partial class ShowData : Form
  {

    #region MEMBERS

    private string url;

    #endregion


    #region PUBLIC

    public ShowData(string url)
    {
      InitializeComponent();

      this.url = url;

      // Start fetching data
      Task.Run(() => {
        this.FetchData();
      });

      this.KeyUp += this.ShowData_KeyUp;
    }

    #endregion


    #region PRIVATE

    private void FetchData()
    {
      var outputString = new StringBuilder();
      outputString.Append($"URL: {this.url}\r\n");

      try
      {
        HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(this.url);
        WebResponse response = httpReq.GetResponse();

        outputString.Append("Status: " + ((HttpWebResponse)response).StatusDescription + "\r\n");
        outputString.Append($"ContentLength: {response.ContentLength}\r\n");

        Stream dataStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(dataStream);
        
        string line = string.Empty;
        int dataRead = 0;
        int maxDataLength = 10 * 1024 * 1024; // Max. is 10mb
        while ((line = reader.ReadLine()) != null && line.Length > 0)
        {
          dataRead += line.Length;
          outputString.Append($"{line}\r\n");

          if (dataRead > maxDataLength)
          {
            outputString.Append($"\r\n\r\n Maximum data length of {maxDataLength} reached\r\n");
            outputString.Append($"Abort reading.\r\n");
            break;
          }
        }

        outputString.Append($"\r\n\r\nTotal bytes received: {dataRead}\r\n");

        reader.Close();
        response.Close();
      }
      catch (Exception ex)
      {
        outputString.Append($"OOPS! Something went wrong!\r\n\r\n{ex.Message}");
      }

      this.rtb_Response.BeginInvoke((MethodInvoker)delegate 
      {
        this.rtb_Response.Text = outputString.ToString();
      });
    }


    #endregion


    #region PROTECTED

    protected void ShowData_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
      {
        this.Close();
      }
    }

    #endregion

  }
}
