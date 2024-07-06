using CadastroPedidos.Checkout.Application.Abstractions;
using CadastroPedidos.Checkout.Application.UseCases.CheckoutPedido;
using ControlePedidos.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ControlePedidos.API.Controllers;


[Route("api/checkout")]
public class CheckoutController : BaseController
{
    private readonly ICheckoutApplicationService _checkoutService;
    private readonly IUseCase<CheckoutPedidoRequest, CheckoutPedidoResponse> _checkoutPedidoUseCase;

    public CheckoutController(
                             IUseCase<CheckoutPedidoRequest, CheckoutPedidoResponse> checkoutPedidoUseCase)
    {
        _checkoutPedidoUseCase = checkoutPedidoUseCase;
    }

    [HttpPatch]
    public async Task<ActionResult<CheckoutPedidoResponse>> CheckoutPedido(CheckoutPedidoRequest checkoutPedidoRequest)
    {
        try
        {
            CheckoutPedidoResponse response = await _checkoutPedidoUseCase.ExecuteAsync(checkoutPedidoRequest);

            if (response.UrlPagamento.IsNullOrEmpty())
            {
                return BadRequest(new { error = "Ocorreu um erro inesperado ao contatar provedor de pagamento" });
            }

            return response;
        }
        catch (NotificationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{nameof(PedidosController)}[{checkoutPedidoRequest.IdPedido}] - Unexpected Error - [{ex.Message}]");
            return BadRequest(new { error = "Ocorreu um erro inesperado" });
        }
    }
}

