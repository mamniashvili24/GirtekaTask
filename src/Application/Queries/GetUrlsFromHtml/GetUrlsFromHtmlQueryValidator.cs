using FluentValidation;

namespace Application.Queries.GetUrlsFromHtml;

public class GetUrlsFromHtmlQueryValidator : AbstractValidator<GetUrlsFromHtmlQuery>
{
    public GetUrlsFromHtmlQueryValidator()
    {
        RuleFor(o => o.Html).NotNull().NotEmpty();
    }
}