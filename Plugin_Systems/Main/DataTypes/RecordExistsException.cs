namespace Minary.Plugin.Main.Systems.DataTypes
{
  public class RecordExistsException : System.Exception
  {
    public RecordExistsException() : base() { }
    public RecordExistsException(string message) : base(message) { }
  }
}