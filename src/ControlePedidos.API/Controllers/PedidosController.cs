using CadastroPedidos.Pedido.Application.Abstractions;
using CadastroPedidos.Pedido.Application.DTO;
using CadastroPedidos.Pedido.Application.UseCases.ObterPedido;
using ControlePedidos.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ControlePedidos.API.Controllers;

[Route("api/pedidos")]
public class PedidosController : BaseController
{
    private readonly IUseCase<ObterPedidoRequest, ObterPedidoResponse> _obterPedidoUseCase;
    private readonly IPedidoApplicationService _pedidoService;

    public PedidosController(IPedidoApplicationService pedidoService,
                             IUseCase<ObterPedidoRequest, ObterPedidoResponse> obterPedidoUseCase)
    {
        _pedidoService = pedidoService;
        _obterPedidoUseCase = obterPedidoUseCase;
    }

    // TODO: Criar objeto de retorno padrão
    // TODO: Adicionar Autenticação

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PedidoResponse>>> ObterTodosPedidos()
    {
        try
        {
            IEnumerable<PedidoResponse> pedidos = await _pedidoService.ObterTodosPedidosAsync();

            return Ok(pedidos);
        }
        catch (NotificationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{nameof(PedidosController)}[{ObterTodosPedidos}] - Unexpected Error - [{ex.Message}]");
            return BadRequest(new { error = "Ocorreu um erro inesperado" });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ObterPedidoResponse>> ObterPedido(string id)
    {
        try
        {
            var request = new ObterPedidoRequest() { Id = id };

            ObterPedidoResponse pedido = await _obterPedidoUseCase.ExecuteAsync(request);

            if (pedido is null)
            {
                return NotFound();
            }

            return Ok(pedido);
        }
        catch (NotificationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{nameof(PedidosController)}[{ObterPedido}] - Unexpected Error - [{ex.Message}]");
            return BadRequest(new { error = "Ocorreu um erro inesperado" });
        }            
    }

    [HttpPost]
    public async Task<ActionResult<PedidoResponse>> CriarPedido(PedidoRequest pedido)
    {
        try
        {
            string id = await _pedidoService.CriarPedidoAsync(pedido);

            // TODO: Criar DTO de response
            return Ok(new { Id = id });
        }
        catch (NotificationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{nameof(PedidosController)}[{CriarPedido}] - Unexpected Error - [{ex.Message}]");
            return BadRequest(new { error = "Ocorreu um erro inesperado" });
        }
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<PedidoResponse>> AtualizarPedido([FromRoute] string id, [FromBody] AtualizarPedidoRequest pedido)
    {
        try
        {
            await _pedidoService.AtualizarPedidoAsync(id, pedido);

            return NoContent();
        }
        catch (NotificationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{nameof(PedidosController)}[{AtualizarPedido}] - Unexpected Error - [{ex.Message}]");
            return BadRequest(new { error = "Ocorreu um erro inesperado" });
        }
    }

    [HttpPatch("{id}/checkout")]
    public async Task<ActionResult<PagamentoResponse>> CheckoutPedido([FromRoute] string id)
    {
        try
        {
            var response = await _pedidoService.CheckoutPedido(id);

            if (response.IsNullOrEmpty())
            {
                return BadRequest(new { error = "Ocorreu um erro inesperado ao contatar provedor de pagamento" });
            }
            return new PagamentoResponse(){
                Url = response
            };
        }
        catch (NotificationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{nameof(PedidosController)}[{AtualizarPedido}] - Unexpected Error - [{ex.Message}]");
            return BadRequest(new { error = "Ocorreu um erro inesperado" });
        }
    }
}
