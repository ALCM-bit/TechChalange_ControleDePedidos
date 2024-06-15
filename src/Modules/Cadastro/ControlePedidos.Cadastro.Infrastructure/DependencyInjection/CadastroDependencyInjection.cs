using ControlePedidos.Cadastro.Application.Abstractions;
using ControlePedidos.Cadastro.Application.Services;
using ControlePedidos.Cadastro.Application.UseCases.GravarCadastro;
using ControlePedidos.Cadastro.Application.UseCases.ObterCadastro;
using ControlePedidos.Cadastro.Domain.Abstractions;
using ControlePedidos.Cadastro.Infrastructure.Repositories;
using ControlePedidos.Cadastro.Infrastructure.Repositories.ClienteRepositories;
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
        services.AddScoped<ICadastroRepository, CadastroRepository>();
        services.AddScoped<ICadastroService, CadastroService>();

        services.AddScoped<IUseCase<ObterCadastroRequest, ObterCadastroResponse>, ObterCadastroUseCase>();
        services.AddScoped<IUseCase<GravarCadastroRequest, GravarCadastroResponse>, GravarCadastroUseCase>();
    }

    private static void RegisterContexts(this IServiceCollection services)
    {
        MongoDBRigistror.RegisterDocumentResolver();
        services.AddScoped<CadastroDbContext>();
    }
}
