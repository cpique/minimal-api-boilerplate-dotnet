using MinimalApiBoilerplate.Application.Validators;

namespace MinimalApiBoilerplate.Api.Validators;

public static class ValidationExtensions
{
    public static bool TryValidate<T>(this T request, IValidator<T> validator, out IResult? errorResult)
    {
        var errors = validator.Validate(request);
        if (errors.Count != 0)
        {
            errorResult = Results.BadRequest(new { Errors = errors });
            return false;
        }

        errorResult = null;
        return true;
    }
}
