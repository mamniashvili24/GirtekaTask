using MediatR;
using Domain.Models;
using Domain.Models.Csv;

namespace Application.Queries.DownloadCsvFiles;

public class DownloadCsvFilesQuery : IRequest<DownloadedCsvFile>
{
	public DownloadCsvFilesQuery() { }

	public DownloadCsvFilesQuery(Urls url)
	{
		Url = url;
	}

	public DownloadCsvFilesQuery(string[] paths)
	{
		Url = new(paths);
	}

    public Urls Url { get; set; }

    public object Headers { get; internal set; }
}