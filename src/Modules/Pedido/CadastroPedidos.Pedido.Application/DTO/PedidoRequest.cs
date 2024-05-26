namespace CadastroPedidos.Pedido.Application.DTO;

public class PedidoRequest
{
    public string? IdCliente { get; set; }
    public List<ItemPedidoRequest> Itens { get; set; } = new();
}

public class ItemPedidoRequest
{
    public string ProdutoId { get; set; } = string.Empty;
    public string Tamanho { get; set; }
    public int Quantidade { get; set; }
    public string? Observacao { get; set; }
}
