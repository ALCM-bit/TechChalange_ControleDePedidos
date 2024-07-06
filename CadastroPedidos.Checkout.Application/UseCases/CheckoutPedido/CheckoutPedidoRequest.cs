namespace CadastroPedidos.Checkout.Application.UseCases.CheckoutPedido;

public class CheckoutPedidoRequest
{
    public string IdPedido { get; set; } = string.Empty;
    public List<Item> Itens { get; set; } = new List<Item>();
}

public class Item
{
    public string Id { get; set; }
    public string Nome { get; set; }
    public int Quantidade { get; set; }
    public decimal Valor { get; set; }
}
