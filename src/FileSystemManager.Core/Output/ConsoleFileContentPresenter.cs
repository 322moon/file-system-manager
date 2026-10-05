namespace FileSystemManager.Core.Output;

public class ConsoleFileContentPresenter : IFileContentPresenter
{
    public string Present(string content)
    {
        return content;
    }
}