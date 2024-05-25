using ControlePedidos.Pedido.Domain.Enums;
using System.Text.Json.Serialization;

namespace CadastroPedidos.Pedido.Application.DTO;

public class PedidoResponse
{
    public string Id { get; set; } = string.Empty;
    public string Codigo { get; set; } = string.Empty;
    public string? IdCliente { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public StatusPedido? Status { get; set; } = null;

    public DateTime DataCriacao { get; set; }
    public DateTime? DataFinalizacao { get; set; }
    public decimal Total { get; set; }
    public List<ItemPedidoResponse> Itens { get; set; } = new();
}

public class ItemPedidoResponse
{
    //public string? Id { get; set; }
    public string ProdutoId { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string TipoProduto { get; set; } = string.Empty;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TamanhoProduto Tamanho { get; set; }
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }
    public string? Observacao { get; set; }
    public decimal Subtotal { get; set; }
}
