namespace ControlePedidos.Common.Entities;

public abstract class ValueObject
{
    protected ValueObject()
    { }

    public abstract bool Validate();
}
