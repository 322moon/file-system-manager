using FileSystemManager.Core.FileSystem;

namespace FileSystemManager.Core.Commands;

public class FileCopyCommand : ICommand
{
    private readonly FileSystemContext _context;
    private readonly string _sourcePath;
    private readonly string _destinationPath;

    public FileCopyCommand(FileSystemContext context, string sourcePath, string destinationPath)
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

            _context.FileSystem?.CopyFile(absoluteSourcePath, absoluteDestinationPath);
            return CommandResult.Success($"File copied from {_sourcePath} to {_destinationPath}");
        }
        catch (Exception ex)
        {
            return CommandResult.Failure($"Copy failed: {ex.Message}");
        }
    }
}