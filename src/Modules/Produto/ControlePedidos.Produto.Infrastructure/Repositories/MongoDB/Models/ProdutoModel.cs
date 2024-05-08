using ControlePedidos.Produto.Domain.Enums;

namespace ControlePedidos.Produto.Infrastructure.Repositories.MongoDB.Models;

internal class ProdutoModel : BaseModel
{
    public ProdutoModel(string id) : base(id)
    {
    }

    public string Nome { get; private set; }
    public decimal Preco { get; private set; }
    public TipoProduto TipoProduto { get; private set; }
    public string Descricao { get; private set; }
}
