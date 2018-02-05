namespace Minary.Plugin.Main
{
  using System.Windows.Forms;


  public partial class ShowRequest : Form
  {

    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="ShowRequest"/> class.
    ///
    /// </summary>
    /// <param name="content"></param>
    public ShowRequest(string content)
    {
      this.InitializeComponent();

      this.tb_Request.Text = content;
      this.tb_Request.Select(0, 0);

      this.KeyUp += this.ShowRequest_KeyUp;
    }

    #endregion


    #region PROTECTED

    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ShowRequest_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
      {
        this.Close();
      }
    }

    #endregion

  }
}
