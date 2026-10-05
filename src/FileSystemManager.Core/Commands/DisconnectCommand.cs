using FileSystemManager.Core.FileSystem;

namespace FileSystemManager.Core.Commands;

public class DisconnectCommand : ICommand
{
    private readonly FileSystemContext _context;

    public DisconnectCommand(FileSystemContext context)
    {
        _context = context;
    }

    public CommandResult Execute()
    {
        try
        {
            if (!_context.IsConnected)
            {
                return CommandResult.Failure("Not connected to a file system");
            }

            _context.Disconnect();
            return CommandResult.Success("Disconnected");
        }
        catch (Exception ex)
        {
            return CommandResult.Failure($"Disconnect failed: {ex.Message}");
        }
    }
}