namespace ControlePedidos.Common.Entities;

public abstract class ValueObject
{
    protected ValueObject()
    { }

    protected abstract void Validate();
}
