using CadastroPedidos.Produto.Api;
using ControlePedidos.Pedido.Domain.Abstractions;
using ControlePedidos.Pedido.Infrastructure.Repositories.Http.DTO;
using Mapster;

namespace ControlePedidos.Pedido.Infrastructure.Repositories.Http;

public class ProdutoExternalRepository : IProdutoExternalRepository
{
    private readonly IProdutosApi _produtosApi;

    public ProdutoExternalRepository(IProdutosApi produtosApi)
    {
        _produtosApi = produtosApi;
    }

    public async Task<IEnumerable<Domain.Entities.Produto>> ObterTodosProdutosAsync(bool apenasAtivos)
    {
        var result = await _produtosApi.ObterTodosTiposProdutoAsync(null!, apenasAtivos, false);

        var models = result.Adapt<List<ProdutoApiDto>>();

        return ProdutoApiDto.MapToDomain(models);
    }
}
