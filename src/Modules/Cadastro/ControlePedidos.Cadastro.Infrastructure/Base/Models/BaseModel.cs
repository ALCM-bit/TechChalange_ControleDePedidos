﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ControlePedidos.Cadastro.Infrastructure.Repositories.MongoDB.Models;

public class BaseModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public BaseModel(string id)
    {
        Id = string.IsNullOrWhiteSpace(id) ? ObjectId.GenerateNewId().ToString() : id;
    }
}
