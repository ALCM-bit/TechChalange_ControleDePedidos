using CadastroPedidos.Pedido.Application.Abstractions;
using CadastroPedidos.Pedido.Application.DTO;
using ControlePedidos.Common.Exceptions;
using ControlePedidos.Pedido.Domain.Abstractions;
using ControlePedidos.Pedido.Domain.Entities;
using ControlePedidos.Pedido.Domain.Factories;
using Mapster;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;
using Entity = ControlePedidos.Pedido.Domain.Entities;

namespace CadastroPedidos.Pedido.Application.Services;

public class PedidoApplicationService : IPedidoApplicationService
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IProdutoExternalRepository _pedidoExternalRepository;

    public PedidoApplicationService(IPedidoRepository pedidoRepository, IProdutoExternalRepository pedidoExternalRepository)
    {
        _pedidoRepository = pedidoRepository;
        _pedidoExternalRepository = pedidoExternalRepository;
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
        var itensPedido = await CriarItensPedido(pedidoRequest.Itens);

        var pedido = new Entity.Pedido(string.Empty, string.Empty, pedidoRequest.IdCliente, null, DateTime.UtcNow, null, itensPedido);

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

        if (pedidoRequest.Itens is not null)
        {
            var itensPedido = await CriarItensPedido(pedidoRequest.Itens);

            pedido.AtualizarItens(itensPedido);
        }

        if (pedidoRequest.Status is not null)
        {
            pedido.AtualizarStatus(pedidoRequest.Status.Value);
        }

        await _pedidoRepository.AtualizarPedidoAsync(pedido);
    }

    private async Task<List<ItemPedido>> CriarItensPedido(List<ItemPedidoRequest> itemRequests)
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

    public async Task<string> CheckoutPedido(string id)
    {
        Entity.Pedido pedido = await _pedidoRepository.ObterPedidoAsync(id);

        if (pedido is null)
        {
            throw new ApplicationNotificationException("Pedido não encontrado");
        }

        MercadoPagoConfig.AccessToken = Environment.GetEnvironmentVariable("MercadoPagoToken"); ;

        if (pedido.Itens is null)
        {
            throw new ApplicationNotificationException("Pedido não possui itens para realizar o checkout");
        }

        List<PreferenceItemRequest> itensPedido = new List<PreferenceItemRequest>();

        foreach (ItemPedido item in pedido.Itens)
        {
            itensPedido.Add(new PreferenceItemRequest
            {
                Id = item.Id,
                Title = item.Nome,
                Quantity = item.Quantidade,
                UnitPrice = item.Preco
            });
        }

        PreferenceRequest request = new PreferenceRequest
        {
            //TODO - preencher back urls para atualizacao do status de pagamento do pedido
            ExternalReference = pedido.Id,
            Items = itensPedido
        };

        PreferenceClient client = new PreferenceClient();

        try
        {
            Preference preference = client.Create(request);
            return preference.SandboxInitPoint;
        }catch
        {
            throw;
        }
    }
}
