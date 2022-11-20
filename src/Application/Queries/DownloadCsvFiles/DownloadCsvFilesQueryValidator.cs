using FluentValidation;

namespace Application.Queries.DownloadCsvFiles;

public class DownloadCsvFilesQueryValidator : AbstractValidator<DownloadCsvFilesQuery>
{
    public DownloadCsvFilesQueryValidator()
    {
        RuleFor(o => o.Url).NotNull();
        RuleFor(o => o.Url.Paths).NotNull().NotEmpty();
    }
}