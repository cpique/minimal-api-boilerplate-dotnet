using Microsoft.AspNetCore.Diagnostics;

namespace MinimalApiBoilerplate.Api.Middleware;

public static class ExceptionHandlerMiddleware
{
    public static void AddGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (exceptionFeature is not null)
                {
                    var response = new
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "An unexpected error occurred."
                    };

                    //TODO: Maybe log error message, or stack trace

                    await context.Response.WriteAsJsonAsync(response);
                }
            });
        });
    }
}