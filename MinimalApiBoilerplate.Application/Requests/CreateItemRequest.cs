namespace MinimalApiBoilerplate.Application.Requests;

public class CreateItemRequest
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Code { get; set; } = default!;
}
