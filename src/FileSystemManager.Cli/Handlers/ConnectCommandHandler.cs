using FileSystemManager.Core.Commands;
using FileSystemManager.Core.FileSystem;
using FileSystemManager.Cli.Parsing;

namespace FileSystemManager.Cli.Handlers;

public class ConnectCommandHandler : BaseCommandHandler
{
    private readonly FileSystemContext _context;
    private readonly IFileSystemFactory _factory;

    public ConnectCommandHandler(
        FileSystemContext context,
        IFileSystemFactory factory)
    {
        _context = context;
        _factory = factory;
    }

    public override ICommand? Handle(ParsedCommand command)
    {
        if (command.Name != "connect")
        {
            return base.Handle(command);
        }

        if (command.Args.Length == 0)
        {
            throw new ArgumentException("Address required for connect");
        }

        string address = command.Args[0];
        string mode = "local";

        for (int i = 0; i < command.Args.Length; i++)
        {
            if (command.Args[i] == "-m" && i + 1 < command.Args.Length)
            {
                mode = command.Args[i + 1];
                break;
            }
        }

        return new ConnectCommand(_context, address, _factory, mode);
    }
}