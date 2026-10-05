namespace FileSystemManager.Core.FileSystem;

public interface IFileSystemFactory
{
    IFileSystem Create(string mode);
}