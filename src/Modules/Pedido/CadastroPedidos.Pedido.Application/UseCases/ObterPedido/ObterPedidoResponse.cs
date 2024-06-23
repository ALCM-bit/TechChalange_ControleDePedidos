using CadastroPedidos.Pedido.Application.DTO;
using ControlePedidos.Pedido.Domain.Enums;
using System.Text.Json.Serialization;

namespace CadastroPedidos.Pedido.Application.UseCases.ObterPedido;

public class ObterPedidoResponse
{
    public string Id { get; set; } = string.Empty;
    public string Codigo { get; set; } = string.Empty;
    public string? IdCliente { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public StatusPedido? Status { get; set; } = null;

    public DateTime DataCriacao { get; set; }
    public DateTime? DataFinalizacao { get; set; }
    public decimal Total { get; set; }
    public List<ItemPedidoResponseDto> Itens { get; set; } = new();
}
