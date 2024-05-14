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
        Database = Client.GetDatabase("controleProdutoDB");
    }

    private static string GetConnectionString()
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__ControlePedidosDB");
        return Environment.ExpandEnvironmentVariables(connectionString);
    }
}