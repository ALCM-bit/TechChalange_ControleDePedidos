using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ControlePedidos.Produto.Infrastructure.Repositories.MongoDB.Models;

[BsonIgnoreExtraElements]
public class BaseModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public BaseModel(string id)
    {
        Id = string.IsNullOrWhiteSpace(id) ? ObjectId.GenerateNewId().ToString() : id;
        DataCriacao = DateTime.Now;
    }

    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }
}
