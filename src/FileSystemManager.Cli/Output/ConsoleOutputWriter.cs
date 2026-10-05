namespace FileSystemManager.Cli.Output;

public class ConsoleOutputWriter : IOutputWriter
{
    public void Write(string text) => Console.Write(text);

    public void WriteLine(string text) => Console.WriteLine(text);
}