using Domain.Common.FileHalper.Abstraction;

namespace Domain.Common.FileHalper.Implementation;

public class FileProvider : IFileProvider
{
    public void Delete(string path) => File.Delete(path);

    public bool Exists(string path) => File.Exists(path);

    public StreamReader GetStreamReader(string path) => new StreamReader(File.OpenRead(path));
}