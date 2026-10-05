using FileSystemManager.Core.FileSystem;

namespace FileSystemManager.Core.Commands;

public class FileDeleteCommand : ICommand
{
    private readonly FileSystemContext _context;
    private readonly string _path;

    public FileDeleteCommand(FileSystemContext context, string path)
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

            string absolutePath = _context.GetAbsolutePath(_path);
            _context.FileSystem?.DeleteFile(absolutePath);
            return CommandResult.Success($"File deleted: {_path}");
        }
        catch (Exception ex)
        {
            return CommandResult.Failure($"Delete failed: {ex.Message}");
        }
    }
}