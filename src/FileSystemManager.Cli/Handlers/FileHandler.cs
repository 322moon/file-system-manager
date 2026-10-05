using FileSystemManager.Core.Commands;
using FileSystemManager.Core.FileSystem;
using FileSystemManager.Core.Output;
using FileSystemManager.Cli.Parsing;

namespace FileSystemManager.Cli.Handlers;

public class FileHandler : BaseCommandHandler
{
    protected ICommandHandler SubCommandHandlerChain { get; private set; }

    private readonly FileSystemContext _context;
    private readonly FileContentPresenterFactory _fileContentPresenterFactory;

    public FileHandler(FileSystemContext context, FileContentPresenterFactory fileContentPresenterFactory)
    {
        _context = context;
        _fileContentPresenterFactory = fileContentPresenterFactory;

        var copyHandler = new FileCopyCommandHandler(_context);
        var showHandler = new FileShowCommandHandler(_fileContentPresenterFactory, _context);
        var moveHandler = new FileMoveCommandHandler(_context);
        var renameHandler = new FileRenameCommandHandler(_context);
        var deleteHandler = new FileDeleteCommandHandler(_context);

        copyHandler
            .SetNext(showHandler)
            .SetNext(moveHandler)
            .SetNext(renameHandler)
            .SetNext(deleteHandler);
        SubCommandHandlerChain = copyHandler;
    }

    public override ICommand? Handle(ParsedCommand command)
    {
        if (command.Name != "file")
        {
            return base.Handle(command);
        }

        string subCommand = command.Args[0];
        string [] subArgs = command.Args.Skip(1).ToArray();
        var subParsedCommand = new ParsedCommand(subCommand, subArgs);

        return SubCommandHandlerChain.Handle(subParsedCommand);
    }
}