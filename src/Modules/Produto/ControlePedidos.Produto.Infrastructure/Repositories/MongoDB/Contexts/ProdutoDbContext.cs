using ControlePedidos.Produto.Infrastructure.Repositories.MongoDB.Models;
using MongoDB.Driver;
using System.Configuration;

namespace ControlePedidos.Produto.Infrastructure.Repositories.MongoDB.Contexts;
public class ProdutoDbContext
{
    public IMongoClient Client { get; }
    internal IMongoDatabase Database { get; }

    #region Collections

    internal IMongoCollection<ProdutoModel> Pedido => Database.GetCollection<ProdutoModel>(Collections.Produto);

    #endregion

    public ProdutoDbContext()
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