using FileSystemManager.Core.Commands;
using FileSystemManager.Core.FileSystem;
using FileSystemManager.Core.Output;
using FileSystemManager.Cli.Parsing;

namespace FileSystemManager.Cli.Handlers;

public class FileShowCommandHandler : BaseCommandHandler
{
    private readonly FileContentPresenterFactory _fileContentPresenterFactory;
    private readonly FileSystemContext _context;

    public FileShowCommandHandler(FileContentPresenterFactory fileContentPresenterFactory, FileSystemContext context)
    {
        _fileContentPresenterFactory = fileContentPresenterFactory;
        _context = context;
    }

    public override ICommand? Handle(ParsedCommand command)
    {
        if (command.Name != "show")
        {
            return base.Handle(command);
        }

        if (command.Args.Length == 0)
        {
            throw new ArgumentException("Path required for file show");
        }

        string path = command.Args[0];
        string mode = "console";

        for (int i = 1; i < command.Args.Length; i++)
        {
            if (command.Args[i] == "-m" && i + 1 < command.Args.Length)
            {
                mode = command.Args[i + 1];
                break;
            }
        }

        IFileContentPresenter presenter = _fileContentPresenterFactory.Create(mode);
        return new FileShowCommand(_context, path, presenter);
    }
}