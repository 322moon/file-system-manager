using FileSystemManager.Cli.Parsing;
using Xunit;

namespace FileSystemManager.Tests.Tests;

public class CommandParserTests
{
    private readonly CommandParser _parser;

    public CommandParserTests()
    {
        _parser = new CommandParser();
    }

    [Theory]
    [InlineData("connect /Users", "connect", new[] { "/Users" })]
    [InlineData("tree list -d 2", "tree", new[] { "list", "-d", "2" })]
    [InlineData("file show document.txt -m console", "file", new[] { "show", "document.txt", "-m", "console" })]
    [InlineData("file move source.txt destination.txt", "file", new[] { "move", "source.txt", "destination.txt" })]
    [InlineData("disconnect", "disconnect", new string[0])]
    [InlineData("tree goto folder", "tree", new[] { "goto", "folder" })]
    public void Parse_ValidCommand_ReturnsCorrectParsedCommand(
        string input,
        string expectedName,
        string[] expectedArgs)
    {
        ParsedCommand result = _parser.Parse(input);

        Assert.Equal(expectedName, result.Name);
        Assert.Equal(expectedArgs, result.Args);
    }

    [Fact]
    public void Parse_EmptyCommand_ThrowsArgumentException()
    {
        string input = string.Empty;

        Assert.Throws<ArgumentException>(() => _parser.Parse(input));
    }

    [Fact]
    public void Parse_WhitespaceCommand_ThrowsArgumentException()
    {
        string input = "   ";

        Assert.Throws<ArgumentException>(() => _parser.Parse(input));
    }
}