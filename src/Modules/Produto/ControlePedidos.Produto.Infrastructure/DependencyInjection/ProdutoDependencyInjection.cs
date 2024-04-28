using ControlePedidos.Produto.Infrastructure.Repositories.MongoDB.Contexts;
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

    }

    private static void RegisterContexts(this IServiceCollection services)
    {
        services.AddScoped<ProdutoDbContext>();
    }
}
