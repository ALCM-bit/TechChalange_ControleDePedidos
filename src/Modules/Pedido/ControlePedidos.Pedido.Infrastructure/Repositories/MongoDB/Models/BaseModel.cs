using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ControlePedidos.Pedido.Infrastructure.Repositories.MongoDB.Models;

[BsonIgnoreExtraElements]
internal class BaseModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }

    public BaseModel(string id)
    {
        Id = string.IsNullOrWhiteSpace(id) ? ObjectId.GenerateNewId().ToString() : id;
    }
}
