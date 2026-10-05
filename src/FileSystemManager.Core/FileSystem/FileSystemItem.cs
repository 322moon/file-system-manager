namespace FileSystemManager.Core.FileSystem;

public class FileSystemItem
{
    public string? Name { get; }

    public string? AbsolutePath { get; }

    public bool IsDirectory { get; }

    public FileSystemItem(string? name, string? absolutePath, bool isDirectory)
    {
        Name = name;
        AbsolutePath = absolutePath;
        IsDirectory = isDirectory;
    }
}