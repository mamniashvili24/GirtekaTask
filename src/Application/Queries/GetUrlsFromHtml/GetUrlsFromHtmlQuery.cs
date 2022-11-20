using MediatR;
using Domain.Models;

namespace Application.Queries.GetUrlsFromHtml;

public class GetUrlsFromHtmlQuery : IRequest<Urls>
{
	public GetUrlsFromHtmlQuery() { }

	public GetUrlsFromHtmlQuery(string html)
	{
		Html = html;
	}

    public string Html { get; set; }
}