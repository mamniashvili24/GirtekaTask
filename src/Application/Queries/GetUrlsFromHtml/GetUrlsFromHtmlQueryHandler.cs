using MediatR;
using Domain.Models;
using System.Text.RegularExpressions;

namespace Application.Queries.GetUrlsFromHtml;

public class GetUrlsFromHtmlQueryHandler : IRequestHandler<GetUrlsFromHtmlQuery, Urls>
{
    public async Task<Urls> Handle(GetUrlsFromHtmlQuery request, CancellationToken cancellationToken)
    {
        var rejex = new Regex(@"(?inx)
            <a \s [^>]*
                href \s* = \s*
                    (?<q> ['""] )
                        (?<url> [^""]+ )
                    \k<q>
            [^>]* >");

        var urls = rejex.Matches(request.Html)
            .Select(o => o.Groups["url"].ToString())
            .ToArray();

        return new(urls);
    }
}