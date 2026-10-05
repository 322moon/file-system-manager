using FileSystemManager.Core.Commands;
using FileSystemManager.Cli.Parsing;

namespace FileSystemManager.Cli.Handlers;

public abstract class BaseCommandHandler : ICommandHandler
{
    private ICommandHandler? _nextHandler;

    public ICommandHandler SetNext(ICommandHandler nextHandler)
    {
        _nextHandler = nextHandler;
        return nextHandler;
    }

    public virtual ICommand? Handle(ParsedCommand command)
    {
        return _nextHandler?.Handle(command);
    }
}