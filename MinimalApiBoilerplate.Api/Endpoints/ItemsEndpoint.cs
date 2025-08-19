using Microsoft.AspNetCore.Mvc;
using MinimalApiBoilerplate.Api.Validators;
using MinimalApiBoilerplate.Application.Requests;
using MinimalApiBoilerplate.Application.Responses;
using MinimalApiBoilerplate.Application.Services;
using MinimalApiBoilerplate.Application.Validators;
using System.Net;

namespace MinimalApiBoilerplate.Api.Endpoints;

public static class ItemsEndpoint
{
    private const string BaseRoute = "/api/items";

    public static void ConfigureEndpoints(WebApplication app)
    {
        app.MapGet(BaseRoute, GetAllAsync);

        app.MapGet($"{BaseRoute}/{{id:guid}}", GetByIdAsync);

        app.MapGet($"{BaseRoute}/by-user/{{userId:guid}}", GetByUserIdAsync);

        app.MapPost(BaseRoute, CreateAsync);

        app.MapPut($"{BaseRoute}/{{id:guid}}", UpdateAsync);

        app.MapDelete($"{BaseRoute}/{{id:guid}}", DeleteAsync);
    }

    private static async Task<IResult> GetAllAsync(IItemService service, HttpContext context, ILogger<Program> logger)
    {
        //TODO determine how to pass an id here
        var items = await service.GetAllAsync();

        ApiResponse response = new()
        {
            Result = items,
            IsSuccess = true,
            StatusCode = HttpStatusCode.OK
        };

        logger.LogDebug("Endpoint executed successfully.");
        return Results.Ok(response);
    }

    private static async Task<IResult> GetByIdAsync(IItemService service, HttpContext context, ILogger<Program> logger, Guid id)
    {
        var item = await service.GetByIdAsync(id);

        if (item == null) return Results.NotFound();

        ApiResponse response = new()
        {
            Result = item,
            IsSuccess = true,
            StatusCode = HttpStatusCode.OK
        };

        logger.LogDebug("Endpoint executed successfully.");
        return Results.Ok(response);
    }

    private static async Task<IResult> GetByUserIdAsync(IItemService service, HttpContext context, ILogger<Program> logger, Guid userId)
    {
        var item = await service.GetByUserIdAsync(userId);

        if (item == null) return Results.NotFound();

        ApiResponse response = new()
        {
            Result = item,
            IsSuccess = true,
            StatusCode = HttpStatusCode.OK
        };

        logger.LogDebug("Endpoint executed successfully.");
        return Results.Ok(response);
    }

    private static async Task<IResult> CreateAsync(IItemService service, HttpContext context, IValidator<CreateItemRequest> validator, ILogger<Program> logger, [FromBody] CreateItemRequest request)
    {
        if (!request.TryValidate(validator, out var error)) return error!;
        await service.AddAsync(request);
        logger.LogDebug("Endpoint executed successfully.");
        return Results.Created();
    }

    private static async Task<IResult> UpdateAsync(IItemService service, HttpContext context, IValidator<UpdateItemRequest> validator, ILogger<Program> logger, [FromBody] UpdateItemRequest request)
    {
        if (!request.TryValidate(validator, out var error)) return error!;
        //TODO make sure they update only what they can
        await service.UpdateAsync(request);
        logger.LogDebug("Endpoint executed successfully.");
        return Results.Ok();
    }

    private static async Task<IResult> DeleteAsync(IItemService service, HttpContext context, ILogger<Program> logger, Guid id)
    {
        //TODO make sure they delete only what they can
        await service.DeleteAsync(id);
        logger.LogDebug("Endpoint executed successfully.");
        return Results.NoContent();
    }
}

