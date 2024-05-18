using CadastroPedidos.Pedido.Application.Abstractions;
using CadastroPedidos.Pedido.Application.DTO;
using ControlePedidos.Common.Exceptions;
using ControlePedidos.Pedido.Domain.Abstractions;
using Mapster;
using Entity = ControlePedidos.Pedido.Domain.Entities;

namespace CadastroPedidos.Pedido.Application.Services;

public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _pedidoRepository;

    public PedidoService(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public async Task<PedidoResponse> ObterPedidoAsync(string idPedido)
    {
        Entity.Pedido pedido = await _pedidoRepository.ObterPedidoAsync(idPedido);
         
        if (pedido is null)
        {
            return null!;
        }

        var pedidoResponse = pedido.Adapt<PedidoResponse>();

        return pedidoResponse;
    }

    public async Task<IEnumerable<PedidoResponse>> ObterTodosPedidosAsync()
    {
        IEnumerable<Entity.Pedido> pedidos = await _pedidoRepository.ObterTodosPedidosAsync();

        var pedidoResponse = pedidos.Adapt<List<PedidoResponse>>();

        return pedidoResponse;
    }

    public async Task<string> CriarPedidoAsync(PedidoRequest pedidoRequest)
    {
        var pedido = new Entity.Pedido(string.Empty, string.Empty, pedidoRequest.IdCliente, null, DateTime.UtcNow, null);

        await _pedidoRepository.CriarPedidoAsync(pedido);

        return pedido.Codigo;
    }

    public async Task AtualizarPedidoAsync(string id, AtualizarPedidoRequest pedidoRequest)
    {
        Entity.Pedido pedido = await _pedidoRepository.ObterPedidoAsync(id);

        if (pedido is null)
        {
            throw new ApplicationNotificationException("Pedido não encontrado");
        }

        pedido.AtualizarStatus(pedidoRequest.Status);

        await _pedidoRepository.AtualizarPedidoAsync(pedido);
    }
}
