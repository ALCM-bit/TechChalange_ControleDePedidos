using ControlePedidos.Cadastro.Application.Abstractions;
using ControlePedidos.Cadastro.Application.UseCases.GravarCadastro;
using ControlePedidos.Cadastro.Application.UseCases.ObterCadastro;
using ControlePedidos.Cadastro.Domain.Abstractions;
using ControlePedidos.Cadastro.Infrastructure.Repositories;
using ControlePedidos.Cadastro.Infrastructure.Repositories.ClienteRepositories;
using ControlePedidos.Cadastro.Infrastructure.Repositories.MongoDB.Contexts;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ControlePedidos.Cadastro.Infrastructure.DependencyInjection;

public static class CadastroDependencyInjection
{
    public static void AddCadastro(this IServiceCollection services, IConfiguration configuration)
    {
        RegisterContexts(services);
        RegisterServices(services, configuration);
        TypeAdapterConfig.GlobalSettings.Scan(AppDomain.CurrentDomain.Load("ControlePedidos.Cadastro.Application"));
        
    }

    private static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICadastroRepository, CadastroRepository>();
        services.AddScoped<IUseCase<ObterCadastroRequest, ObterCadastroResponse>, ObterCadastroUseCase>();
        services.AddScoped<IUseCase<GravarCadastroRequest>, GravarCadastroUseCase>();
    }

    private static void RegisterContexts(this IServiceCollection services)
    {
        MongoDBRigistror.RegisterDocumentResolver();
        services.AddScoped<CadastroDbContext>();
    }
}
