namespace MyToDo.HttpContracts.Tags;

public sealed record UpdateTagDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}
