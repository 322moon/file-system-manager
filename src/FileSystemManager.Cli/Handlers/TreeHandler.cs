using FileSystemManager.Core.Commands;
using FileSystemManager.Core.FileSystem;
using FileSystemManager.Core.Output;
using FileSystemManager.Cli.Parsing;

namespace FileSystemManager.Cli.Handlers;

public class TreeHandler : BaseCommandHandler
{
    protected ICommandHandler TreeHandlersChain { get; private set; }

    private readonly FileSystemContext _context;
    private readonly IFormatter _formatter;

    public TreeHandler(FileSystemContext context, IFormatter formatter)
    {
        _context = context;
        _formatter = formatter;

        var gotoHandler = new TreeGoToCommandHandler(_context);
        var listHandler = new TreeListCommandHandler(_context, _formatter);
        gotoHandler.SetNext(listHandler);

        TreeHandlersChain = gotoHandler;
    }

    public override ICommand? Handle(ParsedCommand command)
    {
        if (command.Name != "tree")
        {
            return base.Handle(command);
        }

        string subCommand = command.Args[0];
        string [] subArgs = command.Args.Skip(1).ToArray();
        var subParsedCommand = new ParsedCommand(subCommand, subArgs);

        return TreeHandlersChain.Handle(subParsedCommand);
    }
}