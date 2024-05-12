using CadastroPedidos.Produto.Application.Abstractions;
using CadastroPedidos.Produto.Application.DTO;
using ControlePedidos.Common.Exceptions;
using ControlePedidos.Produto.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ControlePedidos.API.Controllers;

[Route("api/produtos")]
public class ProdutosController : BaseController
{
    private readonly IProdutoService _produtoService;
    public ProdutosController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarProdutoAsync([FromBody] IEnumerable<ProdutoRequest> produto)
    {
        try
        {
            await _produtoService.AdicionarProdutoAsync(produto);

            return Ok();
        }
        catch (NotificationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{nameof(ProdutosController)}[{AdicionarProdutoAsync}] - Unexpected Error - [{ex.Message}]");
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

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterProdutoAsync(string id)
    {
        try
        {
            ProdutoResponse produto = await _produtoService.ObterProdutoAsync(id);

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
    public async Task<IActionResult> ObterTodosTiposProdutoAsync([FromQuery] TipoProduto tipoProduto, [FromQuery] bool ativo, bool retornarTodos = false)
    {
        IEnumerable<ProdutoResponse> produtos = await _produtoService.ObterTodosTiposProdutoAsync(tipoProduto, ativo, retornarTodos);

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
