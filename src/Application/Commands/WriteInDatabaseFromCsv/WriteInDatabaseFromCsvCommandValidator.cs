using FluentValidation;

namespace Application.Commands.WriteInDatabaseFromCsv;

public class WriteInDatabaseFromCsvCommandValidator : AbstractValidator<WriteInDatabaseFromCsvCommand>
{
    public WriteInDatabaseFromCsvCommandValidator()
    {
        RuleFor(o => o.CsvPath).NotNull();
        RuleFor(o => o.CsvPath.Paths).NotNull().NotEmpty();
    }
}