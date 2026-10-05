namespace FileSystemManager.Core.FileSystem;

public class FileSystemContext
{
    public IFileSystem? FileSystem { get; private set; }

    public string? RootPath { get; private set; }

    public string? CurrentPath { get; private set; }

    public bool IsConnected => FileSystem != null;

    public void Connect(IFileSystem? fileSystem, string absolutePath)
    {
        FileSystem = fileSystem;
        RootPath = Path.GetFullPath(absolutePath);
        CurrentPath = RootPath;
    }

    public void Disconnect()
    {
        FileSystem = null;
        RootPath = null;
        CurrentPath = null;
    }

    public string GetAbsolutePath(string path)
    {
        if (Path.IsPathRooted(path))
        {
            return Path.GetFullPath(path);
        }

        if (CurrentPath == null)
        {
            throw new InvalidOperationException("Not connected");
        }

        return Path.GetFullPath(Path.Combine(CurrentPath, path));
    }

    public bool IsPathWithinRoot(string path)
    {
        if (RootPath == null)
        {
            return false;
        }

        string absolutePath = Path.GetFullPath(path);
        return absolutePath.StartsWith(RootPath,  StringComparison.OrdinalIgnoreCase);
    }

    public void ChangeCurrentPath(string path)
    {
        string absolutePath = Path.GetFullPath(path);

        if (!IsPathWithinRoot(absolutePath))
        {
            throw new ArgumentException("Path is outside the root directory");
        }

        if (FileSystem == null || !FileSystem.DirectoryExists(absolutePath))
        {
            throw new DirectoryNotFoundException($"Directory not found: {absolutePath}");
        }

        CurrentPath = absolutePath;
    }
}