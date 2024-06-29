﻿using CadastroPedidos.Produto.Api;
using CadastroPedidos.Produto.Application.Abstractions;
using CadastroPedidos.Produto.Application.UseCases.AtualizarProduto;
using CadastroPedidos.Produto.Application.UseCases.DeletarProduto;
using CadastroPedidos.Produto.Application.UseCases.GravarProduto;
using CadastroPedidos.Produto.Application.UseCases.ObterProduto;
using CadastroPedidos.Produto.Application.UseCases.ObterTodosProdutos;
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
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IProdutosApi, ProdutosApi>();

        services.AddScoped<IUseCase<ObterProdutoRequest, ObterProdutoResponse>, ObterProdutoUseCase>();
        services.AddScoped<IUseCase<IEnumerable<GravarProdutosRequest>>, GravarProdutosUseCase>();
        services.AddScoped<IUseCase<ObterTodosProdutosRequest, IEnumerable<ObterTodosProdutosResponse>>, ObterTodosProdutosUseCase>();
        services.AddScoped<IUseCase<AtualizarProdutoRequest>, AtualizarProdutoUseCase>();
        services.AddScoped<IUseCase<DeletarProdutoRequest>, DeletarProdutoUseCase>();


    }

    private static void RegisterContexts(this IServiceCollection services)
    {
        MongoDbRegistror.RegisterDocumentResolver();
        services.AddScoped<ProdutoDbContext>();
    }
}
