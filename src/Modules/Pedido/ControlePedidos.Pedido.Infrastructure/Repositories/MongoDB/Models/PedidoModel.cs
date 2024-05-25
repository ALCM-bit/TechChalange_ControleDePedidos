using ControlePedidos.Pedido.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace ControlePedidos.Pedido.Infrastructure.Repositories.MongoDB.Models;

[BsonIgnoreExtraElements]
internal class PedidoModel : BaseModel
{
    public string Codigo { get; set; } = string.Empty;
    public string? IdCliente { get; set; }
    public StatusPedido? Status { get; set; }
    public DateTime? DataFinalizacao { get; set; }
    public IEnumerable<ItemPedidoModel> Itens { get; set; } = [];

    public PedidoModel(string id) : base(id)
    { }

    internal static PedidoModel MapFromDomain(Domain.Entities.Pedido pedido)
    {
        if (pedido is null) return null!;

        return new PedidoModel(pedido.Id!)
        {
            Codigo = pedido.Codigo,
            IdCliente = pedido.IdCliente,
            Status = pedido.Status,
            DataCriacao = pedido.DataCriacao,
            DataFinalizacao = pedido.DataAtualizacao,
            Itens = ItemPedidoModel.MapFromDomain(pedido.Itens)
        };
    }

    internal static Domain.Entities.Pedido MapToDomain(PedidoModel pedidoModel)
    {
        if (pedidoModel is null) return null!;

        return new Domain.Entities.Pedido(pedidoModel.Id,
                                          pedidoModel.Codigo,
                                          pedidoModel.IdCliente,
                                          pedidoModel.Status,
                                          pedidoModel.DataCriacao,
                                          pedidoModel.DataFinalizacao,
                                          ItemPedidoModel.MapToDomain(pedidoModel.Itens));
    }

    internal static IEnumerable<Domain.Entities.Pedido> MapToDomain(IEnumerable<PedidoModel> pedidoModel)
    {
        var mapList = new List<Domain.Entities.Pedido>();

        if (pedidoModel is null || !pedidoModel.Any()) return mapList;

        foreach (var model in pedidoModel)
        {
            try
            {
                mapList.Add(MapToDomain(model));
            }
            catch { }
        }

        return mapList;
    }
}
