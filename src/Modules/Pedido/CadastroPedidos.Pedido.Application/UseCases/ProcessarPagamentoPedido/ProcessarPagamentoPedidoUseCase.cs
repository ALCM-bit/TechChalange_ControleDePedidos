using CadastroPedidos.Pedido.Application.Abstractions;
using ControlePedidos.Common.Exceptions;
using ControlePedidos.Pedido.Domain.Abstractions;
using ControlePedidos.Pedido.Domain.Enums;
using Entity = ControlePedidos.Pedido.Domain.Entities;

namespace CadastroPedidos.Pedido.Application.UseCases.ProcessarPagamento;

public class ProcessarPagamentoPedidoUseCase : IUseCase<ProcessarPagamentoPedidoRequest, ProcessarPagamentoPedidoResponse>
{
    private readonly IPedidoRepository _pedidoRepository;

    public ProcessarPagamentoPedidoUseCase(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public async Task<ProcessarPagamentoPedidoResponse> ExecuteAsync(ProcessarPagamentoPedidoRequest request)
    {
        StatusPagamento statusPagamento = StatusPagamento.Pendente;

        Entity.Pedido pedido = await _pedidoRepository.ObterPedidoAsync(request.IdPedido);

        if (pedido is null)
        {
            throw new ApplicationNotificationException("Pedido não encontrado");
        }

        if (string.Equals(request.Status, "approved", StringComparison.InvariantCultureIgnoreCase))
        {
            statusPagamento = StatusPagamento.Pago;

            pedido.ConfirmarPedido();

            await _pedidoRepository.AtualizarPedidoAsync(pedido);
        }

        return new()
        { 
            Status = statusPagamento
        };
    }
}
