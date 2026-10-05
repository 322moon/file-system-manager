using FileSystemManager.Core.Commands;
using FileSystemManager.Core.FileSystem;
using FileSystemManager.Cli.Parsing;

namespace FileSystemManager.Cli.Handlers;

public class FileDeleteCommandHandler : BaseCommandHandler
{
    private readonly FileSystemContext _context;

    public FileDeleteCommandHandler(FileSystemContext context)
    {
        _context = context;
    }

    public override ICommand? Handle(ParsedCommand command)
    {
        if (command.Name != "delete")
        {
            return base.Handle(command);
        }

        if (command.Args.Length == 0)
        {
            throw new ArgumentException("Path required for file delete");
        }

        string path = command.Args[0];
        return new FileDeleteCommand(_context, path);
    }
}