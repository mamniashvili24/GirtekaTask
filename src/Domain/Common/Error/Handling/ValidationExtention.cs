using Microsoft.AspNetCore.Mvc;
using Domain.Common.Error.Messages;

namespace Domain.Common.Error.Handling;

public static class ValidationExtention
{
    public static ValidationProblemDetails ToProblemDetails(this Exception ex)
    {
        var error = new ValidationProblemDetails
        {
            Status = 400,
            Detail = ex.Message,
            Title = CommonErrors.SomethingWentWrong,
            Type = "https://www.rfc-editor.org/rfc/rfc5378.html"
        };

        return error;
    }
}