using MediatR;
using Domain.Common.Error.Messages;
using Domain.ConfigurationSettings;
using Microsoft.Extensions.Options;

namespace Application.Queries.ReadHtmlFromUrl;

public class ReadHtmlFromUrlQueryHandler : IRequestHandler<ReadHtmlFromUrlQuery, string>
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<ElectricityUrlOptions> _electricityUrlOptions;

    public ReadHtmlFromUrlQueryHandler(IHttpClientFactory httpClientFactory, IOptions<ElectricityUrlOptions> electricityUrlOptions)
    {
        _httpClientFactory = httpClientFactory;
        _electricityUrlOptions = electricityUrlOptions;
    }

    public async Task<string> Handle(ReadHtmlFromUrlQuery request, CancellationToken cancellationToken)
    {
        var response = await _httpClientFactory
            .CreateClient("Electricity")
            .GetAsync(_electricityUrlOptions.Value.BaseAddress + _electricityUrlOptions.Value.EndPoint
            , cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(CommonErrors.ReadingPageError);
        }

        var result = await response.Content.ReadAsStringAsync(cancellationToken);

        return result;
    }
}