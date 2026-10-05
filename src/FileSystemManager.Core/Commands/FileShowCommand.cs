using FileSystemManager.Core.FileSystem;
using FileSystemManager.Core.Output;

namespace FileSystemManager.Core.Commands;

public class FileShowCommand : ICommand
{
    private readonly FileSystemContext _context;
    private readonly string _path;
    private readonly IFileContentPresenter _presenter;

    public FileShowCommand(FileSystemContext context, string path, IFileContentPresenter presenter)
    {
        _context = context;
        _path = path;
        _presenter = presenter;
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

            string absolutePath = _context.GetAbsolutePath(_path);
            string content = _context.FileSystem.ReadFileContent(absolutePath);
            string presentedContent = _presenter.Present(content);

            return CommandResult.Success(presentedContent);
        }
        catch (Exception ex)
        {
            return CommandResult.Failure(ex.Message);
        }
    }
}