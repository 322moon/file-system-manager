using FileSystemManager.Core.Commands;
using FileSystemManager.Core.FileSystem;
using FileSystemManager.Core.Output;
using FileSystemManager.Cli.Parsing;

namespace FileSystemManager.Cli.Handlers;

public class TreeListCommandHandler : BaseCommandHandler
{
    private readonly FileSystemContext _context;
    private readonly IFormatter _formatter;

    public TreeListCommandHandler(FileSystemContext context, IFormatter formatter)
    {
        _context = context;
        _formatter = formatter;
    }

    public override ICommand? Handle(ParsedCommand command)
    {
        if (command.Name != "list")
        {
            return base.Handle(command);
        }

        int depth = 1;

        for (int i = 0; i < command.Args.Length; i++)
        {
            if (command.Args[i] == "-d" && i + 1 < command.Args.Length)
            {
                if (int.TryParse(command.Args[i + 1], out int parsedDepth) && parsedDepth > 0)
                {
                    depth = parsedDepth;
                }
                else
                {
                    throw new ArgumentException("Depth must be a positive integer");
                }

                break;
            }
        }

        return new TreeListCommand(_context, depth, _formatter);
    }
}