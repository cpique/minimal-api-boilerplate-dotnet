using MinimalApiBoilerplate.Application.Requests;
using MinimalApiBoilerplate.Application.Validators;

namespace MinimalApiBoilerplate.Api.Configuration;

public static class ValidatorsConfiguration
{
    public static void ConfigureValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateItemRequest>, CreateItemValidator>();
        services.AddScoped<IValidator<UpdateItemRequest>, UpdateItemValidator>();
    }
}