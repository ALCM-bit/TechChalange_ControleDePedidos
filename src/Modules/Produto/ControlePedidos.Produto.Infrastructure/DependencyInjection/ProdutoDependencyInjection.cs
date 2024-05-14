using CadastroPedidos.Produto.Application.Abstractions;
using CadastroPedidos.Produto.Application.Services;
using ControlePedidos.Produto.Domain.Abstractions;
using ControlePedidos.Produto.Infrastructure.Repositories.MongoDB;
using ControlePedidos.Produto.Infrastructure.Repositories.MongoDB.Contexts;
using ControlePedidos.Produto.Infrastructure.Repositories.MongoDB.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ControlePedidos.Produto.Infrastructure.DependencyInjection;

public static class ProdutoDependencyInjection
{
    public static void AddProduto(this IServiceCollection services, IConfiguration configuration)
    {
        RegisterContexts(services);
        RegisterServices(services, configuration);
    }

    private static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
    }

    private static void RegisterContexts(this IServiceCollection services)
    {
        MongoDbRegistror.RegisterDocumentResolver();
        services.AddScoped<ProdutoDbContext>();
    }
}
