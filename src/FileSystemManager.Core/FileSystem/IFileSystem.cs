namespace FileSystemManager.Core.FileSystem;

public interface IFileSystem
{
    IEnumerable<FileSystemItem> GetDirectoryItems(string path, int depth);

    void CreateDirectory(string path);

    void DeleteDirectory(string path);

    void MoveFile(string source, string destination);

    void CopyFile(string source, string destination);

    void DeleteFile(string path);

    void RenameFile(string source, string newName);

    bool DirectoryExists(string path);

    bool FileExists(string path);

    string ReadFileContent(string path);
}