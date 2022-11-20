using MediatR;
using Domain.Models.Csv;

namespace Application.Commands.WriteInDatabaseFromCsv;

public class WriteInDatabaseFromCsvCommand : IRequest
{
	public WriteInDatabaseFromCsvCommand() { }

	public WriteInDatabaseFromCsvCommand(DownloadedCsvFile csvPath)
	{
		CsvPath = csvPath;
	}

	public WriteInDatabaseFromCsvCommand(string[] paths)
	{
		CsvPath = new(paths);
	}

    public DownloadedCsvFile CsvPath { get; set; }
}