using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Domain.Common.Error.Messages;

public static class CommonErrors
{
    private static readonly string ErrorCodeFragment = nameof(CommonErrors) + "_" + nameof(Errors) + "_";
    
    public static string ReadingPageError => ErrorCodeFragment + nameof(ReadingPageError);
    
    public static string SomethingWentWrong => ErrorCodeFragment + nameof(SomethingWentWrong);
}