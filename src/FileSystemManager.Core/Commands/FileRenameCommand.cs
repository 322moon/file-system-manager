using FileSystemManager.Core.FileSystem;

namespace FileSystemManager.Core.Commands;

public class FileRenameCommand : ICommand
{
    private readonly FileSystemContext _context;
    private readonly string _path;
    private readonly string _newName;

    public FileRenameCommand(FileSystemContext context, string path, string newName)
    {
        _context = context;
        _path = path;
        _newName = newName;
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
            _context.FileSystem?.RenameFile(absolutePath, _newName);
            return CommandResult.Success($"File renamed to {_newName}");
        }
        catch (Exception ex)
        {
            return CommandResult.Failure($"Rename failed: {ex.Message}");
        }
    }
}