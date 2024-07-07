namespace Api.Entity;

public class Business(int id, string name)
{
    public int Id { get; private set; } = id;
    public string Name { get; private set; } = name;
}