using ControlePedidos.Produto.Domain.Enums;

namespace ControlePedidos.Produto.Infrastructure.Repositories.MongoDB.Models;

internal class ProdutoModel(string id) : BaseModel(id)
{
    internal static ProdutoModel MapFromDomain(Domain.Entities.Produto produto)
    {
        if (produto is null) return null!;

        return new ProdutoModel(produto.Id!)
        {
            Id = produto.Id!,
            Nome = produto.Nome,
            TipoProduto = produto.TipoProduto,
            TamanhoPreco = produto.TamanhoPreco,
            Descricao = produto.Descricao,
            DataCriacao = produto.DataCriacao,
            DataAtualizacao = produto.DataAtualizacao,
            Ativo = produto.Ativo
        };
    }
    internal static IEnumerable<ProdutoModel> MapFromDomain(IEnumerable<Domain.Entities.Produto> produto)
    {
        var mapList = new List<ProdutoModel>();

        if (produto is null || !produto.Any()) return mapList;

        foreach (var model in produto)
        {
            mapList.Add(MapFromDomain(model));
        }

        return mapList;
    }

    internal static Domain.Entities.Produto MapToDomain(ProdutoModel produto)
    {
        if (produto is null) return null!;

        var produtoMapeado = new Domain.Entities.Produto(produto.Id!,
                                         produto.Nome,
                                         produto.TamanhoPreco,
                                         produto.TipoProduto,
                                         produto.Descricao,
                                         produto.DataCriacao,
                                         produto.Ativo);

        produtoMapeado.DataAtualizacao = produto.DataAtualizacao;

        return produtoMapeado;
    }

    internal static IEnumerable<Domain.Entities.Produto> MapToDomain(IEnumerable<ProdutoModel> produto)
    {
        var mapList = new List<Domain.Entities.Produto>();

        if (produto is null || !produto.Any()) return mapList;

        foreach (var model in produto)
        {
            mapList.Add(MapToDomain(model));
        }

        return mapList;
    }
    public string Nome { get; private set; }
    public IEnumerable<KeyValuePair<string, decimal>> TamanhoPreco { get; private set; }
    public TipoProduto TipoProduto { get; private set; }
    public string Descricao { get; private set; }
    public bool Ativo { get; private set; }

}
