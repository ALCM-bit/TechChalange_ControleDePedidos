using CadastroPedidos.Checkout.Application.Abstractions;
using CadastroPedidos.Checkout.Application.Service;
using CadastroPedidos.Checkout.Application.UseCases.CheckoutPedido;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CadastroPedidos.Checkout.Infrastructure.DependencyInjection;

public static class CheckDependencyInjection
{
    public static void AddCheckout(this IServiceCollection services, IConfiguration configuration)
    {
        RegisterServices(services, configuration);
        RegisterUseCases(services);
    }

    private static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICheckoutApplicationService, CheckoutApplicationService>();
    }

    private static void RegisterUseCases(this IServiceCollection services)
    {
        services.AddScoped<IUseCase<CheckoutPedidoRequest, CheckoutPedidoResponse>, CheckoutPedidoUseCase>();
    }
}
