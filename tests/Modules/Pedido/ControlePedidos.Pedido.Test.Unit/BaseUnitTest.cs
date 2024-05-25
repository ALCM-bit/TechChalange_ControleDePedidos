namespace ControlePedidos.Pedido.Test.Unit;

public abstract class BaseUnitTest
{
    public static string GerarIdValido()
    {
        return Guid.NewGuid().ToString().Replace("-","").Substring(0, 24);
    }
}
