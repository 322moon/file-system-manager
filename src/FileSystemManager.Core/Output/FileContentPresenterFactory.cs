namespace FileSystemManager.Core.Output;

public class FileContentPresenterFactory
{
    public IFileContentPresenter Create(string mode)
    {
        return mode switch
        {
            "console" => new ConsoleFileContentPresenter(),
            _ => throw new ArgumentException($"Unsupported mode: {mode}"),
        };
    }
}