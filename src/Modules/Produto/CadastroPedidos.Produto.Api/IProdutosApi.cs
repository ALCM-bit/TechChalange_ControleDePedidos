namespace CadastroPedidos.Produto.Api;

public interface IProdutosApi
{
    Task<IEnumerable<ProdutoResponse>> ObterTodosTiposProdutoAsync(string tipoProduto, bool ativo, bool retornarTodos = false);
}
