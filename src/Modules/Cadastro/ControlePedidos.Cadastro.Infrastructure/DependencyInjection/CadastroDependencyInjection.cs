using ControlePedidos.Cadastro.Infrastructure.Repositories.MongoDB.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ControlePedidos.Cadastro.Infrastructure.DependencyInjection;

public static class CadastroDependencyInjection
{
    public static void AddCadastro(this IServiceCollection services, IConfiguration configuration)
    {
        RegisterContexts(services);
        RegisterServices(services, configuration);
    }

    private static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {

    }

    private static void RegisterContexts(this IServiceCollection services)
    {
        services.AddScoped<CadastroDbContext>();
    }
}
