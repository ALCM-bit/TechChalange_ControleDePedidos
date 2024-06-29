using CadastroPedidos.Produto.Application.Abstractions;
using CadastroPedidos.Produto.Application.UseCases.ObterTodosProdutos;
using ControlePedidos.Produto.Domain.Enums;

namespace CadastroPedidos.Produto.Api;

public class ProdutosApi : IProdutosApi
{
    private readonly IUseCase<ObterTodosProdutosRequest, IEnumerable<ObterTodosProdutosResponse>> _obterTodosProdutosUseCase;

    public ProdutosApi(
         IUseCase<ObterTodosProdutosRequest, IEnumerable<ObterTodosProdutosResponse>> obterTodosProdutosUseCase)
    {
        _obterTodosProdutosUseCase = obterTodosProdutosUseCase;
    }

    public async Task<IEnumerable<ObterTodosProdutosResponse>> ObterTodosTiposProdutoAsync(string tipoProduto, bool ativo, bool retornarTodos = false)
    {
        var tipoProdutoParsed = Enum.TryParse<TipoProduto>(tipoProduto, out var outTipoProduto) ? outTipoProduto : (TipoProduto?) null;

        return await _obterTodosProdutosUseCase.ExecuteAsync(new() { TipoProduto = tipoProdutoParsed, Ativo = ativo, RetornarTodos = retornarTodos});
    }
}
