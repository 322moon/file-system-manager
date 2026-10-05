namespace FileSystemManager.Cli.Parsing;

public class CommandParser
{
    public ParsedCommand Parse(string command)
    {
        string [] tokens = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (tokens.Length == 0)
        {
            throw new ArgumentException("Empty command");
        }

        string commandName = tokens[0];
        string[] args = tokens.Skip(1).ToArray();
        return new ParsedCommand(commandName, args);
    }
}