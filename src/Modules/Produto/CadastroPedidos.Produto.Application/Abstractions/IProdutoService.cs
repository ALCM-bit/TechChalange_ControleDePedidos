using CadastroPedidos.Produto.Application.DTO;
using ControlePedidos.Produto.Domain.Enums;

namespace CadastroPedidos.Produto.Application.Abstractions;

public interface IProdutoService
{
    Task<ProdutoResponse> ObterProdutoAsync(string id);
    Task<IEnumerable<ProdutoResponse>> ObterTodosTiposProdutoAsync(TipoProduto? tipoProduto, bool ativo, bool retornarTodos);
    Task AdicionarProdutoAsync(IEnumerable<ProdutoRequest> produto);
    Task AtualizarProdutoAsync(string id, AtualizaProdutoRequest produto);
    Task RemoverProdutoAsync(string id);
}
