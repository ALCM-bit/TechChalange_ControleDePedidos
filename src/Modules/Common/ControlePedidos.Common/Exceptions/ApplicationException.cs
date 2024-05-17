namespace ControlePedidos.Common.Exceptions;
public class ApplicationErrorException : NotificationException
{
    public ApplicationErrorException(string message) : base(message)
    {

    }
}
