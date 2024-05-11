using ControlePedidos.Pedido.Domain.Enums;

namespace CadastroPedidos.Pedido.Application.DTO;

public class PedidoResponse
{
    public string Id { get; set; } = string.Empty;
    public string Codigo { get; set; } = string.Empty;
    public string? IdCliente { get; set; }
    public StatusPedido? Status { get; set; } = null;
    public DateTime DataCriacao { get; set; }
    public DateTime? DataFinalizacao { get; set; }

    // TODO: Retornar produtos
}
