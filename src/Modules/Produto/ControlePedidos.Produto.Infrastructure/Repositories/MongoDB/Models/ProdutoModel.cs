using ControlePedidos.Produto.Domain.Enums;

namespace ControlePedidos.Produto.Infrastructure.Repositories.MongoDB.Models;

internal class ProdutoModel : BaseModel
{
    public ProdutoModel(string id) : base(id)
    {
    }

    public string Nome { get; set; }
    public IEnumerable<KeyValuePair<string, decimal>> TamanhoPreco { get; set; }
    public TipoProduto TipoProduto { get; set; }
    public string Descricao { get; set; }
    public bool Ativo { get; set; }

    internal static ProdutoModel MapFromDomain(Domain.Entities.Produto produto)
    {
        if (produto is null) return null;

        return new ProdutoModel(produto.Id)
        {
            Nome = produto.Nome,
            TipoProduto = produto.TipoProduto,
            TamanhoPreco = produto.TamanhoPreco,
            Descricao = produto.Descricao,
            DataCriacao = produto.DataCriacao,
            DataAtualizacao = produto.DataAtualizacao,
            Ativo = produto.Ativo
        };
    }

    internal static IEnumerable<ProdutoModel> MapFromDomain(IEnumerable<Domain.Entities.Produto> produtos)
    {
        if (produtos is null || !produtos.Any()) return Enumerable.Empty<ProdutoModel>();

        return produtos.Select(MapFromDomain).ToList();
    }

    internal static Domain.Entities.Produto MapToDomain(ProdutoModel produto)
    {
        if (produto is null) return null;

        var produtoMapeado = new Domain.Entities.Produto(produto.Id,
                                     produto.Nome,
                                     produto.TamanhoPreco,
                                     produto.TipoProduto,
                                     produto.Descricao,
                                     produto.DataCriacao,
                                     produto.Ativo);

        produtoMapeado.DataAtualizacao = produto.DataAtualizacao;

        return produtoMapeado;
    }

    internal static IEnumerable<Domain.Entities.Produto> MapToDomain(IEnumerable<ProdutoModel> produtos)
    {
        if (produtos is null || !produtos.Any()) return Enumerable.Empty<Domain.Entities.Produto>();

        return produtos.Select(MapToDomain).ToList();
    }
}
