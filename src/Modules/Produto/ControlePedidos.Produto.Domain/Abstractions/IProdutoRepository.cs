namespace ControlePedidos.Produto.Domain.Abstractions
{
    public interface IProdutoRepository
    {
        // Definir os métodos da interface
        Task<Entities.Produto> ObterProdutoAsync(string id);
        Task<IEnumerable<Entities.Produto>> ObterTodosTiposProdutoAsync(TipoProduto tipoProduto);
        Task AdicionarProdutoAsync(Entities.Produto produto);
        Task AtualizarProdutoAsync(Entities.Produto produto);
        Task RemoverProdutoAsync(Entities.Produto produto);
    }
}
