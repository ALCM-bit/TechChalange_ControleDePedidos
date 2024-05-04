using CadastroPedidos.Pedido.Application.DTO;
using ControlePedidos.Pedido.Domain.Enums;

namespace CadastroPedidos.Pedido.Application.Abstractions;

public interface IPedidoService
{
    Task<PedidoResponse> ObterPedidoAsync(string idPedido);
    Task<IEnumerable<PedidoResponse>> ObterTodosPedidosAsync();
    Task<string> CriarPedidoAsync(PedidoRequest pedido);
    Task AtualizarStatusAsync(string id, StatusPedido status);
}
