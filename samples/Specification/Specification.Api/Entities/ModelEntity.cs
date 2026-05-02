namespace Specification.Api.Entities;

public class ModelEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateTimeOffset Date { get; set; }
}