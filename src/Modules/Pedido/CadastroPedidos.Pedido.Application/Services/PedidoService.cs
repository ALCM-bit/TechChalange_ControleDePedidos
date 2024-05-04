using CadastroPedidos.Pedido.Application.Abstractions;
using CadastroPedidos.Pedido.Application.DTO;
using ControlePedidos.Common.Exceptions;
using ControlePedidos.Pedido.Domain.Abstractions;
using ControlePedidos.Pedido.Domain.Enums;
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
        var codigoPedido = Guid.NewGuid().ToString().Substring(0, 5);

        var pedido = new Entity.Pedido(string.Empty, codigoPedido, DateTime.UtcNow, pedidoRequest.IdCliente);

        string id = await _pedidoRepository.CriarPedidoAsync(pedido);

        return id;
    }

    public async Task ConfirmarPedidoAsync(string id)
    {
        Entity.Pedido pedido = await _pedidoRepository.ObterPedidoAsync(id);

        if (pedido is null)
        {
            throw new ApplicationErrorException("Pedido não encontrado");
        }

        pedido.ConfirmarPedido();

        await _pedidoRepository.AtualizarPedidoAsync(pedido);
    }

    public async Task AtualizarStatusAsync(string id, StatusPedido status)
    {
        Entity.Pedido pedido = await _pedidoRepository.ObterPedidoAsync(id);

        if (pedido is null)
        {
            throw new ApplicationErrorException("Pedido não encontrado");
        }

        switch (status)
        {
            case StatusPedido.Recebido:
                pedido.ConfirmarPedido();
                break;

            case StatusPedido.Preparando:
                pedido.IniciarPreparo();
                break;

            case StatusPedido.Pronto:
                pedido.FinalizarPreparo();
                break;

            case StatusPedido.Finalizado:
                pedido.FinalizarPedido();
                break;

            default:
                throw new ApplicationErrorException("Status inválido");
        }

        await _pedidoRepository.AtualizarPedidoAsync(pedido);
    }
}
