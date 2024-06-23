using CadastroPedidos.Pedido.Application.Abstractions;
using ControlePedidos.Pedido.Domain.Abstractions;
using Mapster;
using Entity = ControlePedidos.Pedido.Domain.Entities;

namespace CadastroPedidos.Pedido.Application.UseCases.ObterPedido;

public class ObterPedidoUseCase : IUseCase<ObterPedidoRequest, ObterPedidoResponse>
{
    private readonly IPedidoRepository _pedidoRepository;

    public ObterPedidoUseCase(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public async Task<ObterPedidoResponse> ExecuteAsync(ObterPedidoRequest request)
    {
        Entity.Pedido pedido = await _pedidoRepository.ObterPedidoAsync(request.Id);

        if (pedido is null)
        {
            return null!;
        }

        var pedidoResponse = pedido.Adapt<ObterPedidoResponse>();

        return pedidoResponse;
    }
}
