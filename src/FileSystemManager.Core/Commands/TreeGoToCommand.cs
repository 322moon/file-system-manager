using FileSystemManager.Core.FileSystem;

namespace FileSystemManager.Core.Commands;

public class TreeGoToCommand : ICommand
{
    private readonly FileSystemContext _context;
    private readonly string _path;

    public TreeGoToCommand(FileSystemContext context, string path)
    {
        _context = context;
        _path = path;
    }

    public CommandResult Execute()
    {
        try
        {
            if (!_context.IsConnected)
            {
                return CommandResult.Failure("Not connected to a file system");
            }

            _context.ChangeCurrentPath(_path);
            return CommandResult.Success($"Current directory changed to {_context.CurrentPath}");
        }
        catch (Exception ex)
        {
            return CommandResult.Failure($"Failed to change directory: {ex.Message}");
        }
    }
}