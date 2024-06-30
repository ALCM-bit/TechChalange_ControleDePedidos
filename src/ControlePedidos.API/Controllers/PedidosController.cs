using CadastroPedidos.Pedido.Application.Abstractions;
using CadastroPedidos.Pedido.Application.DTO;
using CadastroPedidos.Pedido.Application.UseCases.AtualizarPedido;
using CadastroPedidos.Pedido.Application.UseCases.CheckoutPedido;
using CadastroPedidos.Pedido.Application.UseCases.CriarPedido;
using CadastroPedidos.Pedido.Application.UseCases.ObterPedido;
using CadastroPedidos.Pedido.Application.UseCases.ObterTodosPedidos;
using ControlePedidos.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ControlePedidos.API.Controllers;

[Route("api/pedidos")]
public class PedidosController : BaseController
{
    private readonly IUseCase<ObterPedidoRequest, ObterPedidoResponse> _obterPedidoUseCase;
    private readonly IUseCase<ObterTodosPedidosRequest, ObterTodosPedidosResponse> _obterTodosPedidosUseCase;    
    private readonly IUseCase<CheckoutPedidoRequest, CheckoutPedidoResponse> _checkoutPedidoUseCase;
    private readonly IUseCase<CriarPedidoRequest, CriarPedidoResponse> _criarPedidoUseCase;
    private readonly IUseCase<AtualizarPedidoRequest> _atualizarPedidoUseCase;

    public PedidosController(IUseCase<ObterPedidoRequest, ObterPedidoResponse> obterPedidoUseCase,
                             IUseCase<ObterTodosPedidosRequest, ObterTodosPedidosResponse> obterTodosPedidosUseCase,
                             IUseCase<CheckoutPedidoRequest, CheckoutPedidoResponse> checkoutPedidoUseCase,
                             IUseCase<CriarPedidoRequest, CriarPedidoResponse> criarPedidoUseCase,
                             IUseCase<AtualizarPedidoRequest> atualizarPedidoUseCase)
    {
        _obterPedidoUseCase = obterPedidoUseCase;
        _obterTodosPedidosUseCase = obterTodosPedidosUseCase;
        _checkoutPedidoUseCase = checkoutPedidoUseCase;
        _criarPedidoUseCase = criarPedidoUseCase;
        _atualizarPedidoUseCase = atualizarPedidoUseCase;
    }

    // TODO: Criar objeto de retorno padrão
    // TODO: Adicionar Autenticação

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ObterPedidoResponse>>> ObterTodosPedidos()
    {
        try
        {
            ObterTodosPedidosResponse response = await _obterTodosPedidosUseCase.ExecuteAsync(null!);

            return Ok(response.Pedidos);
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
    public async Task<ActionResult<CriarPedidoResponse>> CriarPedido(CriarPedidoRequest pedido)
    {
        try
        {
            CriarPedidoResponse response = await _criarPedidoUseCase.ExecuteAsync(pedido);

            return Ok(response);
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
    public async Task<ActionResult> AtualizarPedido([FromRoute] string id, [FromBody] AtualizarPedidoRequest pedido)
    {
        try
        {
            var request = new AtualizarPedidoRequest()
            {
                IdPedido = id,
                Status = pedido.Status,
                Itens = pedido.Itens
            };

            await _atualizarPedidoUseCase.ExecuteAsync(request);

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
    public async Task<ActionResult<CheckoutPedidoResponse>> CheckoutPedido([FromRoute] string id)
    {
        try
        {
            var request = new CheckoutPedidoRequest() { IdPedido = id };

            CheckoutPedidoResponse response = await _checkoutPedidoUseCase.ExecuteAsync(request);

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
            Console.WriteLine($"[{nameof(PedidosController)}[{AtualizarPedido}] - Unexpected Error - [{ex.Message}]");
            return BadRequest(new { error = "Ocorreu um erro inesperado" });
        }
    }
}
