using ControlePedidos.Pedido.Domain.Entities;
using ControlePedidos.Pedido.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ControlePedidos.Pedido.Infrastructure.Repositories.MongoDB.Models;

[BsonIgnoreExtraElements]
internal class ItemPedidoModel
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string ItemId { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string ProdutoId { get; set; }

    public string Nome { get; set; } = string.Empty;
    public string TipoProduto { get; set; } = string.Empty;
    public TamanhoProduto Tamanho { get; set; }
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }
    public string? Observacao { get; set; }
    public decimal Subtotal { get; set; }
    public DateTime DataCriacao { get; set; }

    public ItemPedidoModel(string id)
    {
        ItemId = string.IsNullOrWhiteSpace(id) ? ObjectId.GenerateNewId().ToString() : id;
    }

    internal static ItemPedido MapToDomain(ItemPedidoModel model)
    {
        if (model is null) return null!;

        return new ItemPedido(model.ItemId,
                              model.DataCriacao,
                              model.ProdutoId.ToString(),
                              model.Nome,
                              model.TipoProduto,
                              model.Tamanho,
                              model.Preco,
                              model.Quantidade,
                              model.Observacao);
    }

    internal static ItemPedidoModel MapFromDomain(ItemPedido entity)
    {
        if (entity is null) return null!;

        return new ItemPedidoModel(entity.Id!)
        {
            ProdutoId = entity.ProdutoId,
            Nome = entity.Nome,
            TipoProduto = entity.TipoProduto,
            Tamanho = entity.Tamanho,
            Preco = entity.Preco,
            Quantidade = entity.Quantidade,
            Observacao = entity.Observacao,
            Subtotal = entity.Subtotal
        };
    }

    internal static IEnumerable<ItemPedido> MapToDomain(IEnumerable<ItemPedidoModel> models)
    {
        var mapList = new List<ItemPedido>();

        if (models is null || !models.Any()) return Enumerable.Empty<ItemPedido>();

        foreach (var model in models)
        {
            try
            {
                mapList.Add(MapToDomain(model));
            }
            catch { }
        }

        return mapList;
    }

    internal static IEnumerable<ItemPedidoModel> MapFromDomain(IEnumerable<ItemPedido> entities)
    {
        var mapList = new List<ItemPedidoModel>();

        if (entities is null || !entities.Any()) return Enumerable.Empty<ItemPedidoModel>();

        foreach (var model in entities)
        {
            try
            {
                mapList.Add(MapFromDomain(model));
            }
            catch { }
        }

        return mapList;
    }
}
