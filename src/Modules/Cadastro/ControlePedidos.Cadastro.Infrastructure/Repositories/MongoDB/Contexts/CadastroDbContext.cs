using ControlePedidos.Cadastro.Infrastructure.Repositories.MongoDB.Models;
using MongoDB.Driver;
using System.Configuration;

namespace ControlePedidos.Cadastro.Infrastructure.Repositories.MongoDB.Contexts;

public class CadastroDbContext
{
    public IMongoClient Client { get; }
    internal IMongoDatabase Database { get; }

    #region Collections

    public IMongoCollection<CadastroModel> Cadastro => Database.GetCollection<CadastroModel>(Collections.Cadastro);

    #endregion

    public CadastroDbContext()
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
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__ControlePedidosDB");
        return Environment.ExpandEnvironmentVariables(connectionString!);
    }
}
