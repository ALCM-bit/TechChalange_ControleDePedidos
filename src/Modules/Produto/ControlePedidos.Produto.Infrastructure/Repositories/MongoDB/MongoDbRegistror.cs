using MongoDB.Bson.Serialization.Conventions;

namespace ControlePedidos.Produto.Infrastructure.Repositories.MongoDB;

public class MongoDbRegistror
{
    public static void RegisterDocumentResolver()
    {
        var pack = new ConventionPack
        {
            new CamelCaseElementNameConvention()
        };

        ConventionRegistry.Register("Camel Case", pack, t => t.FullName.Contains(".MongoDB.Models"));
    }
}