using FileSystemManager.Cli.Controllers;

namespace FileSystemManager.Cli.Interfaces;

public class ConsoleInterface
{
    private readonly ApplicationController _controller;

    public ConsoleInterface(ApplicationController controller)
    {
        _controller = controller;
    }

    public void Run()
    {
        Console.WriteLine("File System Manager (type 'exit' to quit)");

        while (true)
        {
            Console.Write("> ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                continue;
            }

            if (input == "exit")
            {
                break;
            }

            _controller.ProcessCommand(input);
        }
    }
}