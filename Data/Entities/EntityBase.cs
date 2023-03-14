namespace FilmApp.Data.Entities;

public abstract class EntityBase : IEntity
{
    public int Id { get; set; }

    public override string ToString() => $"Id: {Id}";
}
