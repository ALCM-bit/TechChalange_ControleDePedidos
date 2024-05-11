namespace ControlePedidos.Common.Exceptions;

public class DomainException : NotificationException
{
    public DomainException(string message) : base(message)
    {

    }
}
