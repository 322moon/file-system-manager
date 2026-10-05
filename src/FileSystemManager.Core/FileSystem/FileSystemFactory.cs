namespace FileSystemManager.Core.FileSystem;

public class FileSystemFactory : IFileSystemFactory
{
    public IFileSystem Create(string mode)
    {
        return mode switch
        {
            "local" => new LocalFileSystem(),
            _ => throw new ArgumentException($"Unsupported file system: {mode}"),
        };
    }
}