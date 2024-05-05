namespace ControlePedidos.Common.Entities;

public abstract class Entity
{
    public string Id { get; set; } = string.Empty;

    protected Entity(string id)
    {
        Id = id;
    }

    protected abstract void Validate();
}
