namespace FileSystemManager.Core.FileSystem;

public class LocalFileSystem : IFileSystem
{
    public IEnumerable<FileSystemItem> GetDirectoryItems(string path, int depth = 1)
    {
        if (depth <= 0) yield break;

        foreach (string directory in Directory.EnumerateDirectories(path))
        {
            var info = new DirectoryInfo(directory);
            yield return new FileSystemItem(Path.GetFileName(directory), directory, true);
            if (depth > 1)
            {
                foreach (FileSystemItem subItem in GetDirectoryItems(directory, depth - 1))
                {
                    yield return subItem;
                }
            }
        }

        foreach (string file in Directory.EnumerateFiles(path))
        {
            yield return new FileSystemItem(Path.GetFileName(file), file, false);
        }
    }

    public void CreateDirectory(string path)
    {
        try
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erorr while creating the directory: {ex.Message}");
            throw;
        }
    }

    public void DeleteDirectory(string path)
    {
        try
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erorr while deleting the directory: {ex.Message}");
            throw;
        }
    }

    public void MoveFile(string source, string destination)
    {
        if (File.Exists(source))
        {
            File.Move(source, destination);
        }
    }

    public void CopyFile(string source, string destination)
    {
        File.Copy(source, destination);
    }

    public void DeleteFile(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public void RenameFile(string source, string newName)
    {
        if (!File.Exists(source))
            return;

        string directory = Path.GetDirectoryName(source) ?? throw new ArgumentException("Invalid source path");
        string destination = Path.Combine(directory, newName);

        File.Move(source, destination);
    }

    public bool DirectoryExists(string path)
    {
        return Directory.Exists(path);
    }

    public bool FileExists(string path)
    {
        return File.Exists(path);
    }

    public string ReadFileContent(string path)
    {
        return File.ReadAllText(path);
    }
}