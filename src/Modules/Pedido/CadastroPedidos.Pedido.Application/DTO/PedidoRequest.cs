using ControlePedidos.Pedido.Domain.Enums;
using System.Text.Json.Serialization;

namespace CadastroPedidos.Pedido.Application.DTO;

public class PedidoRequest
{
    public string? IdCliente { get; set; }
    public List<ItemPedidoRequest> Itens { get; set; } = new();
}

public class ItemPedidoRequest
{
    public string? Id { get; set; }
    public string ProdutoId { get; set; } = string.Empty;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TamanhoProduto Tamanho { get; set; }
    public int Quantidade { get; set; }
    public string? Observacao { get; set; }
}
