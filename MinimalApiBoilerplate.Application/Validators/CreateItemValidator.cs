using MinimalApiBoilerplate.Application.Requests;

namespace MinimalApiBoilerplate.Application.Validators;

public class CreateItemValidator : IValidator<CreateItemRequest>
{
    private const int MIN_LENGTH = 3;

    public List<string> Validate(CreateItemRequest request)
    {
        var errors = new List<string>();

        if (request.UserId == Guid.Empty)
            errors.Add($"{nameof(request.UserId)} is required.");

        if (string.IsNullOrEmpty(request.Name))
            errors.Add($"{nameof(request.Name)} is required.");

        if (request.Name.Length < MIN_LENGTH)
            errors.Add($"{nameof(request.Name)} should be at least {MIN_LENGTH} characters.");

        if (string.IsNullOrEmpty(request.Code))
            errors.Add($"{nameof(request.Code)} is required.");

        return errors;
    }
}
