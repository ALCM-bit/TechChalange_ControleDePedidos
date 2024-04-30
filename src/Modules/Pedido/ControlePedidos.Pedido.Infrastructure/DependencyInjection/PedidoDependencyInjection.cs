using ControlePedidos.Pedido.Infrastructure.Repositories.MongoDB.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ControlePedidos.Pedido.Infrastructure.DependencyInjection;

public static class PedidoDependencyInjection
{
    public static void AddPedido(this IServiceCollection services, IConfiguration configuration)
    {
        RegisterContexts(services);
        RegisterServices(services, configuration);
    }

    private static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {

    }

    private static void RegisterContexts(this IServiceCollection services)
    {
        services.AddScoped<PedidoDbContext>();
    }
}
