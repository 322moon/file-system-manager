using FileSystemManager.Cli.Proccess;

namespace FileSystemManager.Cli;

public static class Program
{
    public static void Main()
    {
        var startup = new Startup();
        startup.Configure();
    }
}