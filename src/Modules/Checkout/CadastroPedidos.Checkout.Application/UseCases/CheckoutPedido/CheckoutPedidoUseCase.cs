using CadastroPedidos.Checkout.Application.Abstractions;
using ControlePedidos.Common.Exceptions;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;

namespace CadastroPedidos.Checkout.Application.UseCases.CheckoutPedido;

public class CheckoutPedidoUseCase : IUseCase<CheckoutPedidoRequest, CheckoutPedidoResponse>
{
    public CheckoutPedidoUseCase()
    {
    }

    public async Task<CheckoutPedidoResponse> ExecuteAsync(CheckoutPedidoRequest request)
    {
        try
        {
            MercadoPagoConfig.AccessToken = Environment.GetEnvironmentVariable("MercadoPagoToken");

            if (request.Itens is null)
            {
                throw new ApplicationNotificationException("Pedido não possui itens para realizar o checkout");
            }

            List<PreferenceItemRequest> itensPedido = new List<PreferenceItemRequest>();

            foreach (var item in request.Itens)
            {
                itensPedido.Add(new PreferenceItemRequest
                {
                    Id = item.Id,
                    Title = item.Nome,
                    Quantity = item.Quantidade,
                    UnitPrice = item.Valor
                });
            }

            PreferenceRequest preferenceRequest = new PreferenceRequest
            {
                //TODO - preencher back urls para atualizacao do status de pagamento do pedido
                ExternalReference = request.IdPedido,
                Items = itensPedido,
                AutoReturn = "approved",
                BackUrls = new()
                {
                    Pending = "https://controle-pedidos/api/pedidos/notificacoes/pagamento",
                    Success = "https://controle-pedidos/api/pedidos/notificacoes/pagamento",
                    Failure = "https://controle-pedidos/api/pedidos/notificacoes/pagamento"
                }
            };

            PreferenceClient client = new PreferenceClient();

            Preference preference = client.Create(preferenceRequest);

            return new CheckoutPedidoResponse()
            { 
                UrlPagamento = preference.SandboxInitPoint
            };
        }
        catch (Exception e)
        {
            throw new ApplicationNotificationException($"Não foi possivel realizar o checkout do pedido:{request.IdPedido}");
        }
    }
}
