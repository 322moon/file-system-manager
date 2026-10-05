using FileSystemManager.Core.Commands;
using FileSystemManager.Core.FileSystem;
using FileSystemManager.Cli.Parsing;

namespace FileSystemManager.Cli.Handlers;

public class FileRenameCommandHandler : BaseCommandHandler
{
    private readonly FileSystemContext _context;

    public FileRenameCommandHandler(FileSystemContext context)
    {
        _context = context;
    }

    public override ICommand? Handle(ParsedCommand command)
    {
        if (command.Name != "rename")
        {
            return base.Handle(command);
        }

        if (command.Args.Length < 2)
        {
            throw new ArgumentException("Path and new name required for file rename");
        }

        string path = command.Args[0];
        string newName = command.Args[1];
        return new FileRenameCommand(_context, path, newName);
    }
}