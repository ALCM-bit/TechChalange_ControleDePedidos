using CadastroPedidos.Pedido.Application.Abstractions;
using CadastroPedidos.Pedido.Application.UseCases.AtualizarPedido;
using CadastroPedidos.Pedido.Application.UseCases.CriarPedido;
using CadastroPedidos.Pedido.Application.UseCases.ObterPedido;
using CadastroPedidos.Pedido.Application.UseCases.ObterTodosPedidos;
using CadastroPedidos.Pedido.Application.UseCases.ProcessarPagamento;
using ControlePedidos.API.DTO;
using ControlePedidos.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ControlePedidos.API.Controllers;

[Route("api/pedidos")]
public class PedidosController : BaseController
{
    private readonly IUseCase<ObterPedidoRequest, ObterPedidoResponse> _obterPedidoUseCase;
    private readonly IUseCase<ObterTodosPedidosRequest, ObterTodosPedidosResponse> _obterTodosPedidosUseCase;    
    private readonly IUseCase<CriarPedidoRequest, CriarPedidoResponse> _criarPedidoUseCase;
    private readonly IUseCase<AtualizarPedidoRequest> _atualizarPedidoUseCase;
    private readonly IUseCase<ProcessarPagamentoPedidoRequest, ProcessarPagamentoPedidoResponse> _processarPagamentoPedidoCase;

    public PedidosController(IUseCase<ObterPedidoRequest, ObterPedidoResponse> obterPedidoUseCase,
                             IUseCase<ObterTodosPedidosRequest, ObterTodosPedidosResponse> obterTodosPedidosUseCase,
                             IUseCase<CriarPedidoRequest, CriarPedidoResponse> criarPedidoUseCase,
                             IUseCase<AtualizarPedidoRequest> atualizarPedidoUseCase,
                             IUseCase<ProcessarPagamentoPedidoRequest, ProcessarPagamentoPedidoResponse> processarPagamentoPedidoCase)
    {
        _obterPedidoUseCase = obterPedidoUseCase;
        _obterTodosPedidosUseCase = obterTodosPedidosUseCase;
        _criarPedidoUseCase = criarPedidoUseCase;
        _atualizarPedidoUseCase = atualizarPedidoUseCase;
        _processarPagamentoPedidoCase = processarPagamentoPedidoCase;
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

    [HttpGet("notificacoes/pagamento")]
    public async Task<ActionResult<SituacaoPagamentoDto>> ProcessarPagamento([FromQuery] SituacaoPagamentoDto situacaoPagamento)
    {
        try
        {
            ProcessarPagamentoPedidoRequest request = new() { IdPedido = situacaoPagamento.IdPedido, Status = situacaoPagamento.Status };

            ProcessarPagamentoPedidoResponse response = await _processarPagamentoPedidoCase.ExecuteAsync(request);

            // Redirecionamento para a página que irá exibir a situação do pagamento
            return Redirect($"https://controle-pedidos/pagamento?status={response.Status}");
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
}
