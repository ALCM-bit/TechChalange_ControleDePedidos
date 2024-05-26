using CadastroPedidos.Pedido.Application.DTO;
using ControlePedidos.Pedido.Domain.Enums;

namespace CadastroPedidos.Pedido.Application.Abstractions;

public interface IPedidoApplicationService
{
    Task<PedidoResponse> ObterPedidoAsync(string idPedido);
    Task<IEnumerable<PedidoResponse>> ObterTodosPedidosAsync();

    /// <summary>
    /// Cria um Pedido
    /// </summary>
    /// <param name="pedido"></param>
    /// <returns>Código do Pedido</returns>
    Task<string> CriarPedidoAsync(PedidoRequest pedido);
    Task AtualizarPedidoAsync(string id, AtualizarPedidoRequest pedidoRequest);
    Task<string> CheckoutPedido(string id);
}
