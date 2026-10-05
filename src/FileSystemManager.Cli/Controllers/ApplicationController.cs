using FileSystemManager.Core.Commands;
using FileSystemManager.Cli.Mapping;
using FileSystemManager.Cli.Output;
using FileSystemManager.Cli.Parsing;

namespace FileSystemManager.Cli.Controllers;

public class ApplicationController
{
    private readonly ICommandMapper _commandMapper;
    private readonly CommandParser _commandParser;
    private readonly IOutputWriter _outputWriter;

    public ApplicationController(ICommandMapper commandMapper, IOutputWriter outputWriter)
    {
        _commandMapper = commandMapper;
        _outputWriter = outputWriter;
        _commandParser = new CommandParser();
    }

    public void ProcessCommand(string input)
    {
        try
        {
            ParsedCommand parsedCommand = _commandParser.Parse(input);
            ICommand? command = _commandMapper.Map(parsedCommand);

            if (command == null)
            {
                _outputWriter.WriteLine($"Unknown command: {parsedCommand.Name}");
                return;
            }

            CommandResult result = command.Execute();

            if (!string.IsNullOrEmpty(result.Message))
            {
                if (result.IsSuccess)
                {
                    _outputWriter.WriteLine(result.Message);
                }
                else
                {
                    _outputWriter.WriteLine($"Error: {result.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            _outputWriter.WriteLine($"Error: {ex.Message}");
        }
    }
}