namespace ControlePedidos.Common.Entities;

public abstract class ValueObjectBase
{
    protected ValueObjectBase()
    { }

    public abstract bool Validate();
}
