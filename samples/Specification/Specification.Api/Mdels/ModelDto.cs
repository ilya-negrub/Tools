namespace Specification.Api.Mdels;

public class ModelDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateTimeOffset Date { get; set; }
}