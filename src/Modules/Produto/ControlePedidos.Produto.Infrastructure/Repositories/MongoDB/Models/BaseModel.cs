﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ControlePedidos.Produto.Infrastructure.Repositories.MongoDB.Models;

[BsonIgnoreExtraElements]
public class BaseModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
}