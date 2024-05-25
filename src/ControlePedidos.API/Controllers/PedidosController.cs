using CadastroPedidos.Pedido.Application.Abstractions;
using CadastroPedidos.Pedido.Application.DTO;
using ControlePedidos.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ControlePedidos.API.Controllers;

[Route("api/pedidos")]
public class PedidosController : BaseController
{
    private readonly IPedidoApplicationService _pedidoService;

    public PedidosController(IPedidoApplicationService pedidoService)
    {
        _pedidoService = pedidoService;
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
    public async Task<ActionResult<PedidoResponse>> ObterPedido(string id)
    {
        try
        {
            PedidoResponse pedido = await _pedidoService.ObterPedidoAsync(id);

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
            string codigoPedido = await _pedidoService.CriarPedidoAsync(pedido);

            // TODO: Criar DTO de response
            return Ok(new { codigo = codigoPedido });
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
}
