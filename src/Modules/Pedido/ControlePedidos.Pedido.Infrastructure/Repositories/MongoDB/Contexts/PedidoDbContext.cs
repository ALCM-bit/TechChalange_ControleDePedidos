using ControlePedidos.Pedido.Infrastructure.Repositories.MongoDB.Models;
using MongoDB.Driver;
using System.Configuration;

namespace ControlePedidos.Pedido.Infrastructure.Repositories.MongoDB.Contexts;

public class PedidoDbContext
{
    public IMongoClient Client { get; }
    internal IMongoDatabase Database { get; }

    #region Collections

    internal IMongoCollection<PedidoModel> Pedido => Database.GetCollection<PedidoModel>(Collections.Pedido);

    #endregion

    public PedidoDbContext()
    {
        MongoUrl url;

        try
        {
            url = new(GetConnectionString());
        }
        catch
        {
            throw new Exception("Conexão Inválida");
        }

        var mongoSettings = MongoClientSettings.FromUrl(url);

        Client = new MongoClient(mongoSettings);
        Database = Client.GetDatabase("controlePedidosDB");
    }

    private static string GetConnectionString()
    {
        var connectionString = ConfigurationManager.ConnectionStrings["ControlePedidosDB"].ConnectionString;
        return Environment.ExpandEnvironmentVariables(connectionString);
    }
}
