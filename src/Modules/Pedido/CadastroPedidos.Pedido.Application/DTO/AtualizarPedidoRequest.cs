using ControlePedidos.Pedido.Domain.Enums;
using System.Text.Json.Serialization;

namespace CadastroPedidos.Pedido.Application.DTO;

public class AtualizarPedidoRequest
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public StatusPedido? Status { get; set; }

    public List<ItemPedidoRequest>? Itens { get; set; }
}
