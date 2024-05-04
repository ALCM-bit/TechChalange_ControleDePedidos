using ControlePedidos.Pedido.Domain.Abstractions;
using ControlePedidos.Pedido.Infrastructure.Repositories.MongoDB.Contexts;
using ControlePedidos.Pedido.Infrastructure.Repositories.MongoDB.Models;
using MongoDB.Driver;

namespace ControlePedidos.Pedido.Infrastructure.Repositories.MongoDB.Persistence;

public class PedidoRepository : IPedidoRepository
{
    private readonly PedidoDbContext _context;

    public PedidoRepository(PedidoDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.Pedido> ObterPedidoAsync(string idPedido)
    {
        var filter = Builders<PedidoModel>.Filter.Eq(p => p.Id, idPedido);

        var result = await _context.Pedido.Find(filter).Limit(1).FirstOrDefaultAsync();

        // TODO: Criar mapeamento/conversão model -> entity        
        return null;
    }

    public Task<IEnumerable<Domain.Entities.Pedido>> ObterTodosPedidosAsync()
    {
        throw new NotImplementedException();
    }

    public Task<string> CriarPedidoAsync(Domain.Entities.Pedido pedido)
    {
        throw new NotImplementedException();
    }

    public Task AtualizarPedidoAsync(Domain.Entities.Pedido pedido)
    {
        throw new NotImplementedException();
    }
}
