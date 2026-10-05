namespace FileSystemManager.Core.Commands;

public interface ICommand
{
    CommandResult Execute();
}