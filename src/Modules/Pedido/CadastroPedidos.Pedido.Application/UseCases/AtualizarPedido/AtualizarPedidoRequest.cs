using CadastroPedidos.Pedido.Application.DTO;
using ControlePedidos.Pedido.Domain.Enums;
using System.Text.Json.Serialization;

namespace CadastroPedidos.Pedido.Application.UseCases.AtualizarPedido;

public class AtualizarPedidoRequest
{
    public string IdPedido { get; set; } = string.Empty;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public StatusPedido? Status { get; set; }

    public List<ItemPedidoRequestDto>? Itens { get; set; }
}
