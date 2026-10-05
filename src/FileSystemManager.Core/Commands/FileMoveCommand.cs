using FileSystemManager.Core.FileSystem;

namespace FileSystemManager.Core.Commands;

public class FileMoveCommand : ICommand
{
    private readonly FileSystemContext _context;
    private readonly string _sourcePath;
    private readonly string _destinationPath;

    public FileMoveCommand(FileSystemContext context, string sourcePath, string destinationPath)
    {
        _context = context;
        _sourcePath = sourcePath;
        _destinationPath = destinationPath;
    }

    public CommandResult Execute()
    {
        try
        {
            if (!_context.IsConnected)
            {
                return CommandResult.Failure("Not connected to a file system");
            }

            string absoluteSourcePath = _context.GetAbsolutePath(_sourcePath);
            string absoluteDestinationPath = _context.GetAbsolutePath(_destinationPath);

            _context.FileSystem?.MoveFile(absoluteSourcePath, absoluteDestinationPath);
            return CommandResult.Success($"File moved from {_sourcePath} to {_destinationPath}");
        }
        catch (Exception ex)
        {
            return CommandResult.Failure($"Move failed: {ex.Message}");
        }
    }
}