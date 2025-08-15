namespace MinimalApiBoilerplate.Domain;

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string CreatedAt { get; set; } = DateTime.UtcNow.ToString("o");
    public string UpdatedAt { get; set; } = DateTime.UtcNow.ToString("o");
}
