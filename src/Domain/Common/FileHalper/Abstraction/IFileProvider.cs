namespace Domain.Common.FileHalper.Abstraction;

public interface IFileProvider
{
    bool Exists(string path);

    void Delete(string path);

    StreamReader GetStreamReader(string path);
}