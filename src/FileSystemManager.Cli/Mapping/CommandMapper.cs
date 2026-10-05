using FileSystemManager.Core.Commands;
using FileSystemManager.Core.FileSystem;
using FileSystemManager.Core.Output;
using FileSystemManager.Cli.Handlers;
using FileSystemManager.Cli.Parsing;

namespace FileSystemManager.Cli.Mapping;

public class CommandMapper : ICommandMapper
{
    protected ICommandHandler CommandHandlerChain { get; private set; }

    public CommandMapper(
        FileSystemContext context,
        IFormatter formatter,
        IFileSystemFactory fileSystemFactory,
        FileContentPresenterFactory fileContentPresenterFactory)
    {
        var connectHandler = new ConnectCommandHandler(context, fileSystemFactory);
        var disconnectHandler = new DisconnectCommandHandler(context);
        var treeHandler = new TreeHandler(context, formatter);
        var fileHandler = new FileHandler(context, fileContentPresenterFactory);

        connectHandler
            .SetNext(disconnectHandler)
            .SetNext(treeHandler)
            .SetNext(fileHandler);

        CommandHandlerChain = connectHandler;
    }

    public ICommand? Map(ParsedCommand command)
    {
        return CommandHandlerChain.Handle(command);
    }
}