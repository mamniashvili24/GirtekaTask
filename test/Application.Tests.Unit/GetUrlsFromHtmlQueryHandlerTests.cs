using Xunit;
using FluentAssertions;
using Application.Queries.GetUrlsFromHtml;

namespace Application.Tests.Unit;

public class GetUrlsFromHtmlQueryHandlerTests
{
    private readonly GetUrlsFromHtmlQueryHandler _sut;

    public GetUrlsFromHtmlQueryHandlerTests()
    {
        _sut = new();
    }

    [Theory]
    [InlineData(
        "<p>" +
            "random text" +
        "</p>", 0)]

    [InlineData(
        "<html>" +
            "<p>" +
                "random text" +
            "</p>" +
        "</html>", 0)]
    [InlineData(
        "<p>" +
            "<a href=\"fb.com\"> " +
                "fb.com " +
            "</a>" +
        "</p>", 1)]
    [InlineData(
        "<p>" +
            "<a href=\"fb.com\">" +
                " fb.com " +
            "</a>" +
        "</p>" +
        "<p>" +
            "<a href=\"google.com\"> " +
                "google.com " +
            "</a>" +
        "</p>", 2)]
    public async Task Handle_ParsHtml_WhenHtmlAndCheckUrlCount(string html, int countOfLinks)
    {
        // Act
        var result = await _sut.Handle(new GetUrlsFromHtmlQuery(html), default);

        // Assert
        result.Paths.Length.Should().Be(countOfLinks);
    }
}