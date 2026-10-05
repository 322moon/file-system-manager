using FileSystemManager.Core.Commands;
using FileSystemManager.Core.FileSystem;
using FileSystemManager.Cli.Parsing;

namespace FileSystemManager.Cli.Handlers;

public class FileMoveCommandHandler : BaseCommandHandler
{
    private readonly FileSystemContext _context;

    public FileMoveCommandHandler(FileSystemContext context)
    {
        _context = context;
    }

    public override ICommand? Handle(ParsedCommand command)
    {
        if (command.Name != "move")
        {
            return base.Handle(command);
        }

        if (command.Args.Length < 2)
        {
            throw new ArgumentException("Source and destination paths required for file move");
        }

        string sourcePath = command.Args[0];
        string destinationPath = command.Args[1];
        return new FileMoveCommand(_context, sourcePath, destinationPath);
    }
}