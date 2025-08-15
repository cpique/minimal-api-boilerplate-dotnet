namespace MinimalApiBoilerplate.Application.Validators;

public interface IValidator<T>
{
    List<string> Validate(T instance);
}
