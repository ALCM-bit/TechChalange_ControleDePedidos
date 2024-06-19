using CadastroPedidos.Pedido.Application.UseCases.ObterPedido;

namespace CadastroPedidos.Pedido.Application.UseCases.ObterTodosPedidos;

public class ObterTodosPedidosResponse
{
    public List<ObterPedidoResponse> Pedidos { get; set; } = [];
}
