namespace Minary.Plugin.Main.Systems.DataTypes
{
  public class RecordException : System.Exception
  {
    public RecordException() : base() { }
    public RecordException(string message) : base(message) { }
  }
}