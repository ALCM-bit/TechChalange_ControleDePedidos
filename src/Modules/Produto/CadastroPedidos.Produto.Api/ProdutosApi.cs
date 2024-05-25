using CadastroPedidos.Produto.Application.Abstractions;
using ControlePedidos.Produto.Domain.Enums;

namespace CadastroPedidos.Produto.Api;

public class ProdutosApi : IProdutosApi
{
    private readonly IProdutoService _produtoService;

    public ProdutosApi(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    public async Task<IEnumerable<ProdutoResponse>> ObterTodosTiposProdutoAsync(string tipoProduto, bool ativo, bool retornarTodos = false)
    {
        var tipoProdutoParsed = Enum.TryParse<TipoProduto>(tipoProduto, out var outTipoProduto) ? outTipoProduto : (TipoProduto?) null;

        return await _produtoService.ObterTodosTiposProdutoAsync(tipoProdutoParsed, ativo, retornarTodos);
    }
}
