namespace ControlePedidos.Pedido.Domain.Abstractions;

public interface IProdutoExternalRepository
{
    Task<IEnumerable<Produto>> ObterTodosProdutosAsync(bool apenasAtivos);
}
