using CadastroPedidos.Produto.Api;
using CadastroPedidos.Produto.Application.Abstractions;
using CadastroPedidos.Produto.Application.DTO;
using CadastroPedidos.Produto.Application.UseCases.AtualizarProduto;
using CadastroPedidos.Produto.Application.UseCases.DeletarProduto;
using CadastroPedidos.Produto.Application.UseCases.GravarProduto;
using CadastroPedidos.Produto.Application.UseCases.ObterProduto;
using CadastroPedidos.Produto.Application.UseCases.ObterTodosProdutos;
using ControlePedidos.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ControlePedidos.API.Controllers;

[Route("api/produtos")]
public class ProdutosController : BaseController
{
    private readonly IProdutosApi _produtosApi;
    private readonly IUseCase<ObterProdutoRequest, ObterProdutoResponse> _obterProdutoUseCase;
    private readonly IUseCase<IEnumerable<GravarProdutosRequest>> _gravarProdutosUseCase;
    private readonly IUseCase<ObterTodosProdutosRequest, IEnumerable<ObterTodosProdutosResponse>> _obterTodosProdutosUseCase;
    private readonly IUseCase<AtualizarProdutoRequest> _atualizarProdutoUseCase;
    private readonly IUseCase<DeletarProdutoRequest> _deletarProdutoUseCase;

    public ProdutosController(
        IProdutosApi produtosApi,
        IUseCase<ObterProdutoRequest, ObterProdutoResponse> obterProdutoUseCase,
        IUseCase<IEnumerable<GravarProdutosRequest>> gravarProdutosUseCase,
        IUseCase<ObterTodosProdutosRequest, IEnumerable<ObterTodosProdutosResponse>> obterTodosProdutosUseCase,
        IUseCase<AtualizarProdutoRequest> atualizarProdutoUseCase,
        IUseCase<DeletarProdutoRequest> deletarProdutoUseCase)
    {
        _produtosApi = produtosApi;
        _obterProdutoUseCase = obterProdutoUseCase;
        _gravarProdutosUseCase = gravarProdutosUseCase;
        _obterTodosProdutosUseCase = obterTodosProdutosUseCase;
        _atualizarProdutoUseCase = atualizarProdutoUseCase;
        _deletarProdutoUseCase = deletarProdutoUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarProdutosAsync([FromBody] IEnumerable<GravarProdutosRequest> produto)
    {
        try
        {
            await _gravarProdutosUseCase.ExecuteAsync(produto);

            return Ok();
        }
        catch (NotificationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{nameof(ProdutosController)}[{AdicionarProdutosAsync}] - Unexpected Error - [{ex.Message}]");
            return BadRequest(new { error = "Ocorreu um erro inesperado" });
        }
    }

    [HttpPut]
    public async Task<IActionResult> AtualizarProdutoAsync([FromBody] AtualizarProdutoRequest request)
    {
        try
        {
            await _atualizarProdutoUseCase.ExecuteAsync(request);

            return Ok();
        }
        catch (NotificationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{nameof(ProdutosController)}[{AtualizarProdutoAsync}] - Unexpected Error - [{ex.Message}]");
            return BadRequest(new { error = "Ocorreu um erro inesperado" });
        }
    }

    [HttpGet("obterProduto")]
    public async Task<IActionResult> ObterProdutoAsync([FromQuery] ObterProdutoRequest request)
    {
        try
        {
            ObterProdutoResponse produto = await _obterProdutoUseCase.ExecuteAsync(request);

            if (produto is null)
            {
                return NotFound();
            }

            return Ok(produto);
        }
        catch (NotificationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{nameof(ProdutosController)}[{ObterProdutoAsync}] - Unexpected Error - [{ex.Message}]");
            return BadRequest(new { error = "Ocorreu um erro inesperado" });
        }
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodosTiposProdutoAsync([FromQuery] ObterTodosProdutosRequest request)
    {
        IEnumerable<ObterTodosProdutosResponse> produtos = await _obterTodosProdutosUseCase.ExecuteAsync(request);

        return Ok(produtos);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoverProdutoAsync([FromQuery] DeletarProdutoRequest request)
    {
        try
        {
            await _deletarProdutoUseCase.ExecuteAsync(request);

            return Ok();
        }
        catch (NotificationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{nameof(ProdutosController)}[{RemoverProdutoAsync}] - Unexpected Error - [{ex.Message}]");
            return BadRequest(new { error = "Ocorreu um erro inesperado" });
        }
    }
}
