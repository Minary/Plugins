namespace Minary.Plugin
{
  using System;
  using System.Drawing;
  using System.Windows.Forms;

  public partial class Main_Notes : Form
  {
    public Main_Notes()
    {
      this.InitializeComponent();
    }

    public void SetText(string data)
    {
      this.tb_Data.Text = data;
      this.tb_Data.Select(0, 0);
    }

    public void AppendText(string data)
    {
      this.tb_Data.Text += data;
      this.tb_Data.Select(0, 0);

      this.HighlightPhrase(this.tb_Data, "Cookies");
      this.HighlightPhrase(this.tb_Data, "System");
      this.HighlightPhrase(this.tb_Data, "Website");
    }


    public void HighlightPhrase(RichTextBox richTextBox, string search)
    {
      int pos = richTextBox.SelectionStart;
      string s = richTextBox.Text;
      Font font = new Font("Verdana", 8, FontStyle.Bold);

      for (int ix = 0;;)
      {
        int jx = s.IndexOf(search, ix, StringComparison.CurrentCultureIgnoreCase);
        if (jx < 0)
        {
          break;
        }

        richTextBox.SelectionStart = jx;
        richTextBox.SelectionLength = search.Length;
        richTextBox.SelectionFont = font;

        ix = jx + 1;
      }

      richTextBox.SelectionStart = pos;
      richTextBox.SelectionLength = 0;
    }
  }
}
