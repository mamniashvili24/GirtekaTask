using MediatR;
using System.Net;
using Domain.Models.Csv;
using Microsoft.Extensions.Options;
using Domain.ConfigurationSettings;

namespace Application.Queries.DownloadCsvFiles;

public class DownloadCsvFilesQueryHandler : IRequestHandler<DownloadCsvFilesQuery, DownloadedCsvFile>
{
    private readonly ElectricityUrlOptions _electricityUrlOptions;

    public DownloadCsvFilesQueryHandler(IOptions<ElectricityUrlOptions> electricityUrlOptions)
    {
        _electricityUrlOptions = electricityUrlOptions.Value;
    }

    public async Task<DownloadedCsvFile> Handle(DownloadCsvFilesQuery request, CancellationToken cancellationToken)
    {
        var fileNames = new List<string>();
        var urls = request.Url.Paths.Skip(request.Url.Paths.Length - 2).Take(2);

        using var webClient = new WebClient();

        foreach (var url in urls)
        {
            var fileName = url.Split("/").LastOrDefault();
            fileNames.Add(fileName);
            webClient.DownloadFile(_electricityUrlOptions.BaseAddress + url, fileName);
        }

        return new(fileNames.ToArray());
    }
}