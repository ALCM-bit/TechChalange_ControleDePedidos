using CadastroPedidos.Pedido.Application.DTO;
using ControlePedidos.Pedido.Domain.Entities;

namespace CadastroPedidos.Pedido.Application.Abstractions;

public interface IPedidoApplicationService
{
    Task<List<ItemPedido>> GerarItensPedidoAsync(List<ItemPedidoRequestDto> itemRequests);
}
