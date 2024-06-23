using CadastroPedidos.Pedido.Application.Abstractions;
using CadastroPedidos.Pedido.Application.DTO;
using ControlePedidos.Common.Exceptions;
using ControlePedidos.Pedido.Domain.Abstractions;
using ControlePedidos.Pedido.Domain.Entities;
using ControlePedidos.Pedido.Domain.Factories;

namespace CadastroPedidos.Pedido.Application.Services;

public class PedidoApplicationService : IPedidoApplicationService
{
    private readonly IProdutoExternalRepository _pedidoExternalRepository;

    public PedidoApplicationService(IProdutoExternalRepository pedidoExternalRepository)
    {
        _pedidoExternalRepository = pedidoExternalRepository;
    }

    public async Task<List<ItemPedido>> GerarItensPedidoAsync(List<ItemPedidoRequestDto> itemRequests)
    {
        var errorMessages = new List<string>();
        var itensPedido = new List<ItemPedido>();

        var produtosAtivos = await _pedidoExternalRepository.ObterTodosProdutosAsync(true);

        foreach (var item in itemRequests)
        {
            try
            {
                var produto = produtosAtivos.FirstOrDefault(p => p.Id == item.ProdutoId);

                var itemPedido = ItemPedidoFactory.Criar(produto!, null!, item.Tamanho, item.Quantidade, item.Observacao!);

                itensPedido.Add(itemPedido);
            }
            catch (NotificationException ex)
            {
                errorMessages.Add($"Produto [{item.ProdutoId}] - [{ex.Message}]");
            }
            catch (Exception)
            {
                errorMessages.Add($"Produto [{item.ProdutoId}] - Erro inesperado");
            }
        }

        if (errorMessages.Any())
        {
            throw new ApplicationNotificationException(string.Join(',', errorMessages));
        }

        return itensPedido;
    }
}
