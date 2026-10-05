using FileSystemManager.Core.Commands;
using FileSystemManager.Core.FileSystem;
using FileSystemManager.Cli.Parsing;

namespace FileSystemManager.Cli.Handlers;

public class DisconnectCommandHandler : BaseCommandHandler
{
    private readonly FileSystemContext _context;

    public DisconnectCommandHandler(FileSystemContext context)
    {
        _context = context;
    }

    public override ICommand? Handle(ParsedCommand command)
    {
        if (command.Name != "disconnect")
        {
            return base.Handle(command);
        }

        return new DisconnectCommand(_context);
    }
}