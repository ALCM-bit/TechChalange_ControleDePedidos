using CadastroPedidos.Produto.Api;
using CadastroPedidos.Produto.Application.Abstractions;
using CadastroPedidos.Produto.Application.DTO;
using CadastroPedidos.Produto.Application.UseCases.GravarProduto;
using CadastroPedidos.Produto.Application.UseCases.ObterProduto;
using ControlePedidos.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ControlePedidos.API.Controllers;

[Route("api/produtos")]
public class ProdutosController : BaseController
{
    private readonly IProdutosApi _produtosApi;
    private readonly IUseCase<ObterProdutoRequest, ObterProdutoResponse> _obterProdutoUseCase;
    private readonly IUseCase<IEnumerable<GravarProdutosRequest>, GravarProdutosResponse> _gravarProdutosUseCase;
    private readonly IProdutoService _produtoService;

    public ProdutosController(
        IProdutoService produtoService, IProdutosApi produtosApi,
        IUseCase<ObterProdutoRequest, ObterProdutoResponse> obterProdutoUseCase,
        IUseCase<IEnumerable<GravarProdutosRequest>, GravarProdutosResponse> gravarProdutosUseCase)
    {
        _produtoService = produtoService;
        _produtosApi = produtosApi;
        _obterProdutoUseCase = obterProdutoUseCase;
        _gravarProdutosUseCase = gravarProdutosUseCase;
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

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarProdutoAsync(string id, [FromBody] AtualizaProdutoRequest produto)
    {
        try
        {
            await _produtoService.AtualizarProdutoAsync(id, produto);

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
    public async Task<IActionResult> ObterTodosTiposProdutoAsync([FromQuery] string tipoProduto, [FromQuery] bool ativo, [FromQuery] bool retornarTodos = false)
    {
        IEnumerable <ProdutoResponse> produtos = await _produtosApi.ObterTodosTiposProdutoAsync(tipoProduto!, ativo, retornarTodos);

        return Ok(produtos);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoverProdutoAsync(string id)
    {
        try
        {
            await _produtoService.RemoverProdutoAsync(id);

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
