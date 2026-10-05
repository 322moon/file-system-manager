using FileSystemManager.Core.FileSystem;

namespace FileSystemManager.Core.Commands;

public class ConnectCommand : ICommand
{
    private readonly FileSystemContext _context;
    private readonly string _address;
    private readonly IFileSystemFactory _fileSystemFactory;
    private readonly string _mode;

    public ConnectCommand(
        FileSystemContext context,
        string address,
        IFileSystemFactory fileSystemFactory,
        string mode = "local")
    {
        _context = context;
        _address = address;
        _fileSystemFactory = fileSystemFactory;
        _mode = mode;
    }

    public CommandResult Execute()
    {
        try
        {
            if (_context.IsConnected)
            {
                return CommandResult.Failure("Already connected");
            }

            if (!Path.IsPathRooted(_address))
            {
                return CommandResult.Failure("Path must be absolute");
            }

            IFileSystem fileSystem = _fileSystemFactory.Create(_mode);
            _context.Connect(fileSystem, _address);

            return CommandResult.Success("Connected");
        }
        catch (Exception ex)
        {
            return CommandResult.Failure($"Connection failed: {ex.Message}");
        }
    }
}