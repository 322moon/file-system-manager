using FileSystemManager.Core.Commands;
using FileSystemManager.Core.FileSystem;
using FileSystemManager.Cli.Parsing;

namespace FileSystemManager.Cli.Handlers;

public class FileCopyCommandHandler : BaseCommandHandler
{
    private readonly FileSystemContext _context;

    public FileCopyCommandHandler(FileSystemContext context)
    {
        _context = context;
    }

    public override ICommand? Handle(ParsedCommand command)
    {
        if (command.Name != "copy")
        {
            return base.Handle(command);
        }

        if (command.Args.Length < 2)
        {
            throw new ArgumentException("Source and destination paths required for file copy");
        }

        string sourcePath = command.Args[0];
        string destinationPath = command.Args[1];
        return new FileCopyCommand(_context, sourcePath, destinationPath);
    }
}