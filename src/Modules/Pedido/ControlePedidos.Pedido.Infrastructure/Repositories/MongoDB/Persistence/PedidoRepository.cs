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
    
        return PedidoModel.MapToDomain(result);
    }

    public async Task<IEnumerable<Domain.Entities.Pedido>> ObterTodosPedidosAsync()
    {
        var filter = Builders<PedidoModel>.Filter.Empty;
        var sort = Builders<PedidoModel>.Sort.Descending(x => x.Id);

        var result = await _context.Pedido.Find(filter).Sort(sort).ToListAsync();

        return PedidoModel.MapToDomain(result);
    }

    public async Task<string> CriarPedidoAsync(Domain.Entities.Pedido pedido)
    {
        var model = PedidoModel.MapFromDomain(pedido);

        await _context.Pedido.InsertOneAsync(model);

        return model.Id;
    }

    public Task AtualizarPedidoAsync(Domain.Entities.Pedido pedido)
    {
        var model = PedidoModel.MapFromDomain(pedido);

        var filter = Builders<PedidoModel>.Filter.Eq(p => p.Id, pedido.Id);

        var update = Builders<PedidoModel>.Update
                                           .Set(p => p.Status, pedido.Status)
                                           .Set(p => p.DataFinalizacao, pedido.DataAtualizacao);

        return _context.Pedido.UpdateOneAsync(filter, update);
    }
}
