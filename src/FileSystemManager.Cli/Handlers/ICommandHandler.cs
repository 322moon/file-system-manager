using FileSystemManager.Core.Commands;
using FileSystemManager.Cli.Parsing;

namespace FileSystemManager.Cli.Handlers;

public interface ICommandHandler
{
    ICommandHandler SetNext(ICommandHandler nextHandler);

    ICommand? Handle(ParsedCommand command);
}