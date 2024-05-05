namespace CadastroPedidos.Produto.Application.Abstractions
{
    public interface IProdutoService
    {
        // Definir os métodos da interface
        Task<ProdutoResponse> ObterProdutoAsync(string idProduto);
        Task<IEnumerable<ProdutoResponse>> ObterTodosTiposProdutoAsync();
        Task AdicionarProdutoAsync(ProdutoResponse produto);
        Task AtualizarProdutoAsync(ProdutoResponse produto);
        Task RemoverProdutoAsync(string idProduto);
    }
}
