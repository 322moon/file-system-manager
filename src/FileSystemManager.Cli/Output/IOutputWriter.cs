namespace FileSystemManager.Cli.Output;

public interface IOutputWriter
{
    void Write(string text);

    void WriteLine(string text);
}