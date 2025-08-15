namespace MinimalApiBoilerplate.Application.Responses;


public class ItemResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string CreatedAt { get; set; } = default!;
    public string UpdatedAt { get; set; } = default!;
}