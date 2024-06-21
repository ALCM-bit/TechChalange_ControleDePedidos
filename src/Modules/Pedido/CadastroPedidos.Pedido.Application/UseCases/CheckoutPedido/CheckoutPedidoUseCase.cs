using CadastroPedidos.Pedido.Application.Abstractions;
using ControlePedidos.Common.Exceptions;
using ControlePedidos.Pedido.Domain.Abstractions;
using ControlePedidos.Pedido.Domain.Entities;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;
using Entity = ControlePedidos.Pedido.Domain.Entities;

namespace CadastroPedidos.Pedido.Application.UseCases.CheckoutPedido;

public class CheckoutPedidoUseCase : IUseCase<CheckoutPedidoRequest, CheckoutPedidoResponse>
{
    private readonly IPedidoRepository _pedidoRepository;

    public CheckoutPedidoUseCase(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public async Task<CheckoutPedidoResponse> ExecuteAsync(CheckoutPedidoRequest request)
    {
        try
        {
            Entity.Pedido pedido = await _pedidoRepository.ObterPedidoAsync(request.IdPedido);

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

            PreferenceRequest preferenceRequest = new PreferenceRequest
            {
                //TODO - preencher back urls para atualizacao do status de pagamento do pedido
                ExternalReference = pedido.Id,
                Items = itensPedido
            };

            PreferenceClient client = new PreferenceClient();

            Preference preference = client.Create(preferenceRequest);

            return new CheckoutPedidoResponse()
            { 
                UrlPagamento = preference.SandboxInitPoint
            };
        }
        catch
        {
            throw;
        }
    }
}
