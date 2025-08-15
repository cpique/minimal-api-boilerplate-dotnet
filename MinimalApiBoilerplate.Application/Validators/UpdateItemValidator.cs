using MinimalApiBoilerplate.Application.Common;
using MinimalApiBoilerplate.Application.Requests;

namespace MinimalApiBoilerplate.Application.Validators;

public class UpdateItemValidator : IValidator<UpdateItemRequest>
{
    public List<string> Validate(UpdateItemRequest request)
    {
        var errors = new List<string>();

        if (request.Id == Guid.Empty)
            errors.Add($"{nameof(request.Id)} is required.");

        if (request.UserId == Guid.Empty)
            errors.Add($"{nameof(request.UserId)} is required.");

        if (string.IsNullOrEmpty(request.Name))
            errors.Add($"{nameof(request.Name)} is required.");

        if (request.Name.Length < ValidationConstants.ItemConstants.MIN_LENGTH)
            errors.Add($"{nameof(request.Name)} should be at least {ValidationConstants.ItemConstants.MIN_LENGTH} characters.");

        if (string.IsNullOrEmpty(request.Code))
            errors.Add($"{nameof(request.Code)} is required.");

        return errors;
    }
}