using FileSystemManager.Core.FileSystem;
using System.Text;

namespace FileSystemManager.Core.Output;

public class ConsoleTreeFormatter : IFormatter
{
    private readonly string _fileIcon;
    private readonly string _directoryIcon;
    private readonly string _indent;

    public ConsoleTreeFormatter(
        string fileIcon = "[F] ",
        string directoryIcon = "[D] ",
        string indent = "    ")
    {
        _fileIcon = fileIcon;
        _directoryIcon = directoryIcon;
        _indent = indent;
    }

    public string Format(IEnumerable<FileSystemItem> items, int depth)
    {
        var result = new StringBuilder();
        var itemList = items.ToList();
        IEnumerable<FileSystemItem> rootItems = itemList.Where(item =>
            Path.GetDirectoryName(item.AbsolutePath) == Path.GetDirectoryName(itemList.First().AbsolutePath));

        FormatItems(rootItems, depth, 0, string.Empty, result, itemList);
        return result.ToString();
    }

    public void FormatItems(
        IEnumerable<FileSystemItem> items,
        int maxDepth,
        int currentDepth,
        string prefix,
        StringBuilder result,
        IEnumerable<FileSystemItem> allItems)
    {
        if (currentDepth >= maxDepth) return;
        var itemList = items.ToList();

        for (int i = 0; i < itemList.Count; i++)
        {
            FileSystemItem item = itemList[i];
            bool isLast = i == itemList.Count - 1;
            bool isDirectory = item.IsDirectory;

            string icon = isDirectory ? _directoryIcon : _fileIcon;
            string currentPrefix = isLast ? "└── " : "├── ";
            result.AppendLine($"{prefix}{currentPrefix}{icon}{item.Name}");

            if (isDirectory && currentDepth + 1 < maxDepth)
            {
                string nextPrefix = prefix + (isLast ? _indent : "│   ");
                IEnumerable<FileSystemItem> childItems = allItems.Where(child =>
                    Path.GetDirectoryName(child.AbsolutePath) == item.AbsolutePath);

                FormatItems(childItems, maxDepth, currentDepth + 1, nextPrefix, result, allItems);
            }
        }
    }
}