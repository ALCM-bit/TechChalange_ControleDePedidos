using ControlePedidos.Produto.Infrastructure.Repositories.MongoDB.Models;
using MongoDB.Driver;

namespace ControlePedidos.Produto.Infrastructure.Repositories.MongoDB.Contexts;
public class ProdutoDbContext
{
    public IMongoClient Client { get; }
    internal IMongoDatabase Database { get; }

    #region Collections

    internal IMongoCollection<ProdutoModel> Produto => Database.GetCollection<ProdutoModel>(Collections.Produto);

    #endregion

    public ProdutoDbContext()
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__ControlePedidosDB");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception("Invalid Connection String");
        }

        var mongoSettings = MongoClientSettings.FromConnectionString(connectionString);

        Client = new MongoClient(mongoSettings);
        Database = Client.GetDatabase("controleProdutoDB");
    }
}
