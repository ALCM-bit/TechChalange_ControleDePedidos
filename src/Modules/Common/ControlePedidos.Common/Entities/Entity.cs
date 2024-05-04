namespace ControlePedidos.Common.Entities;

public abstract class Entity
{
    public string Id { get; } = string.Empty;

    protected Entity(string id)
    {
        Id = id;
    }

    protected abstract void Validate();
}
