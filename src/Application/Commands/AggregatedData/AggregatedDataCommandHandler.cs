using MediatR;
using Application.Queries.ReadHtmlFromUrl;
using Application.Queries.GetUrlsFromHtml;
using Application.Queries.DownloadCsvFiles;
using Application.Commands.WriteInDatabaseFromCsv;

namespace Application.Commands.AggregatedData;

public class AggregatedDataCommandHandler : IRequestHandler<AggregatedDataCommand>
{
    private readonly ISender _sender;

    public AggregatedDataCommandHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task<Unit> Handle(AggregatedDataCommand request, CancellationToken cancellationToken)
    {
        var html = await _sender.Send(new ReadHtmlFromUrlQuery(), cancellationToken);
        var urls = await _sender.Send(new GetUrlsFromHtmlQuery(html), cancellationToken);

        urls.Paths = urls.Paths.Where(o => o.Contains(".csv")).ToArray();
        
        var filePat = await _sender.Send(new DownloadCsvFilesQuery(urls), cancellationToken);
        await _sender.Send(new WriteInDatabaseFromCsvCommand(filePat), cancellationToken);

        return new();
    }
}