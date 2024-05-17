namespace ControlePedidos.Pedido.Domain.Abstractions;

public interface IPedidoRepository
{
    Task<Entities.Pedido> ObterPedidoAsync(string idPedido);
    Task<IEnumerable<Entities.Pedido>> ObterTodosPedidosAsync();
    Task<string> CriarPedidoAsync(Entities.Pedido pedido);
    Task AtualizarPedidoAsync(Entities.Pedido pedido);
}
