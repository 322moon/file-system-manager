using FileSystemManager.Core.FileSystem;
using FileSystemManager.Core.Output;

namespace FileSystemManager.Core.Commands;

public class TreeListCommand : ICommand
{
    private readonly int _depth;
    private readonly IFormatter _formatter;
    private readonly FileSystemContext _context;

    public TreeListCommand(FileSystemContext context, int depth, IFormatter formatter)
    {
        _depth = depth;
        _context = context;
        _formatter = formatter;
    }

    public CommandResult Execute()
    {
        try
        {
            if (!_context.IsConnected)
            {
                return CommandResult.Failure("Not connected to a file system");
            }

            if (_context.FileSystem is null)
            {
                return CommandResult.Failure("Not connected to a file system");
            }

            if (_context.CurrentPath is null)
            {
                return CommandResult.Failure("Current path not set");
            }

            IEnumerable<FileSystemItem> items = _context.FileSystem.GetDirectoryItems(_context.CurrentPath, _depth);
            string output = _formatter.Format(items, _depth);

            return CommandResult.Success(output);
        }
        catch (Exception ex)
        {
            return CommandResult.Failure($"Failed to change directory: {ex.Message}");
        }
    }
}