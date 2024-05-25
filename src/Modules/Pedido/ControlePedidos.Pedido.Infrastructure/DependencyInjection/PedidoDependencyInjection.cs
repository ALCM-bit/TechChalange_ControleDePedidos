using CadastroPedidos.Pedido.Application.Abstractions;
using CadastroPedidos.Pedido.Application.Services;
using ControlePedidos.Pedido.Domain.Abstractions;
using ControlePedidos.Pedido.Infrastructure.Repositories.Http;
using ControlePedidos.Pedido.Infrastructure.Repositories.MongoDB;
using ControlePedidos.Pedido.Infrastructure.Repositories.MongoDB.Contexts;
using ControlePedidos.Pedido.Infrastructure.Repositories.MongoDB.Persistence;
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
        services.AddScoped<IPedidoRepository, PedidoRepository>();
        services.AddScoped<IProdutoExternalRepository, ProdutoExternalRepository>();
        services.AddScoped<IPedidoApplicationService, PedidoApplicationService>();
    }
    
    private static void RegisterContexts(this IServiceCollection services)
    {
        MongoDbRegistror.RegisterDocumentResolver();
        services.AddScoped<PedidoDbContext>();
    }
}
