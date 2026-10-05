using FileSystemManager.Core.Commands;
using FileSystemManager.Cli.Parsing;

namespace FileSystemManager.Cli.Mapping;

public interface ICommandMapper
{
    ICommand? Map(ParsedCommand command);
}