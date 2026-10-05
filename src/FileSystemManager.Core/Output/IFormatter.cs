using FileSystemManager.Core.FileSystem;

namespace FileSystemManager.Core.Output;

public interface IFormatter
{
    string Format(IEnumerable<FileSystemItem> items, int depth);
}