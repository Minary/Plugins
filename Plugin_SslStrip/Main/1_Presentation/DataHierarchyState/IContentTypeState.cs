namespace Minary.Plugin.Main
{
  public interface IContentTypeState
  {
    ContextType UsedContextType { get; }
    string UsedContentType { get; }
  }
}