using Domain.Models.Csvl;

namespace Domain.Models.Csv;

public class DownloadedCsvFile : CsvBase
{
	public DownloadedCsvFile() { }

    public DownloadedCsvFile(string[] paths)
    {
        Paths = paths;
    }
}