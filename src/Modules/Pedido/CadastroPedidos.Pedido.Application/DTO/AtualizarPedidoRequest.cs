using ControlePedidos.Pedido.Domain.Enums;

namespace CadastroPedidos.Pedido.Application.DTO;

public class AtualizarPedidoRequest
{
    public StatusPedido Status { get; set; }
}
