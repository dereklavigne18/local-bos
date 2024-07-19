namespace Api.Entity;

public class Business(Guid id, string name)
{
    public Guid Id { get; private set; } = id;
    public string Name { get; set; } = name;
}