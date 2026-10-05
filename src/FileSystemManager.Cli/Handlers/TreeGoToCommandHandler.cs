using FileSystemManager.Core.Commands;
using FileSystemManager.Core.FileSystem;
using FileSystemManager.Cli.Parsing;

namespace FileSystemManager.Cli.Handlers;

public class TreeGoToCommandHandler : BaseCommandHandler
{
    private readonly FileSystemContext _context;

    public TreeGoToCommandHandler(FileSystemContext context)
    {
        _context = context;
    }

    public override ICommand? Handle(ParsedCommand command)
    {
        if (command.Name != "goto")
        {
            return base.Handle(command);
        }

        if (command.Args.Length == 0)
            throw new ArgumentException("Path required for tree goto");

        string path = command.Args[0];
        return new TreeGoToCommand(_context, path);
    }
}