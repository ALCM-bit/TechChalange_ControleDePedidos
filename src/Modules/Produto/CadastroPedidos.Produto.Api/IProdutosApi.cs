using CadastroPedidos.Produto.Application.UseCases.ObterTodosProdutos;

namespace CadastroPedidos.Produto.Api;

public interface IProdutosApi
{
    Task<IEnumerable<ObterTodosProdutosResponse>> ObterTodosTiposProdutoAsync(string tipoProduto, bool ativo, bool retornarTodos = false);
}
