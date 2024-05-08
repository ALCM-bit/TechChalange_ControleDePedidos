using ControlePedidos.Produto.Domain.Enums;

namespace CadastroPedidos.Produto.Application.Abstractions
{
    public interface IProdutoService
    {
        // Definir os métodos da interface
        Task<ProdutoResponse> ObterProdutoAsync(string id);
        Task<IEnumerable<ProdutoResponse>> ObterTodosTiposProdutoAsync(TipoProduto tipoProduto);
        Task AdicionarProdutoAsync(ProdutoRequest produto);
        Task AtualizarProdutoAsync(string id, ProdutoRequest produto);
        Task RemoverProdutoAsync(string id);
    }
}
