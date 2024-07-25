using CadastroPedidos.Pedido.Application.Abstractions;
using CadastroPedidos.Pedido.Application.Services;
using CadastroPedidos.Pedido.Application.UseCases.AtualizarPedido;
using CadastroPedidos.Pedido.Application.UseCases.CriarPedido;
using CadastroPedidos.Pedido.Application.UseCases.ObterPedido;
using CadastroPedidos.Pedido.Application.UseCases.ObterTodosPedidos;
using CadastroPedidos.Pedido.Application.UseCases.ProcessarPagamento;
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
        RegisterUseCases(services);
    }

    private static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPedidoRepository, PedidoRepository>();
        services.AddScoped<IProdutoExternalRepository, ProdutoExternalRepository>();
        services.AddScoped<IPedidoApplicationService, PedidoApplicationService>();
    }

    private static void RegisterUseCases(this IServiceCollection services)
    {
        services.AddScoped<IUseCase<ObterPedidoRequest, ObterPedidoResponse>, ObterPedidoUseCase>();
        services.AddScoped<IUseCase<ObterTodosPedidosRequest, ObterTodosPedidosResponse>, ObterTodosPedidosUseCase>();
        services.AddScoped<IUseCase<CriarPedidoRequest, CriarPedidoResponse>, CriarPedidoUseCase>();
        services.AddScoped<IUseCase<AtualizarPedidoRequest>, AtualizarPedidoUseCase>();
        services.AddScoped<IUseCase<ProcessarPagamentoPedidoRequest, ProcessarPagamentoPedidoResponse>, ProcessarPagamentoPedidoUseCase>();
    }

    private static void RegisterContexts(this IServiceCollection services)
    {
        MongoDbRegistror.RegisterDocumentResolver();
        services.AddScoped<PedidoDbContext>();
    }
}
