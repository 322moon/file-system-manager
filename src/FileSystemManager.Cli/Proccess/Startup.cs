using FileSystemManager.Core.FileSystem;
using FileSystemManager.Core.Output;
using FileSystemManager.Cli.Controllers;
using FileSystemManager.Cli.Interfaces;
using FileSystemManager.Cli.Mapping;
using FileSystemManager.Cli.Output;

namespace FileSystemManager.Cli.Proccess;

public class Startup
{
    public void Configure()
    {
        var context = new FileSystemContext();

        IFormatter formatter = new ConsoleTreeFormatter();

        IOutputWriter outputWriter = new ConsoleOutputWriter();

        IFileSystemFactory fileSystemFactory = new FileSystemFactory();

        var fileContentPresenterFactory = new FileContentPresenterFactory();

        var mapper = new CommandMapper(
            context,
            formatter,
            fileSystemFactory,
            fileContentPresenterFactory);

        var controller = new ApplicationController(mapper,  outputWriter);

        var app = new ConsoleInterface(controller);
        app.Run();
    }
}