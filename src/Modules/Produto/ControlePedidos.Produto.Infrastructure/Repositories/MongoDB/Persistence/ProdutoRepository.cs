using ControlePedidos.Produto.Domain.Abstractions;
using ControlePedidos.Produto.Domain.Enums;
using ControlePedidos.Produto.Infrastructure.Repositories.MongoDB.Contexts;
using ControlePedidos.Produto.Infrastructure.Repositories.MongoDB.Models;
using MongoDB.Driver;

namespace ControlePedidos.Produto.Infrastructure.Repositories.MongoDB.Persistence
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ProdutoDbContext _context;

        public ProdutoRepository(ProdutoDbContext context)
        {
            _context = context;
        }

        // Definir a regra dos métodos

        public async Task AdicionarProdutoAsync(IEnumerable<Domain.Entities.Produto> produto)
        {
            var model = ProdutoModel.MapFromDomain(produto);

            await _context.Produto.InsertManyAsync(model);          
        }

        public Task AtualizarProdutoAsync(Domain.Entities.Produto produto)
        {
            var filter = Builders<ProdutoModel>.Filter.Eq(p => p.Id, produto.Id);
            var update = Builders<ProdutoModel>.Update
                                           .Set(p => p.Nome, produto.Nome)
                                           .Set(p => p.Descricao, produto.Descricao)
                                           .Set(p => p.TipoProduto, produto.TipoProduto)
                                           .Set(p => p.Preco, produto.Preco)
                                           .Set(p => p.DataAtualizacao, DateTime.UtcNow);

            return _context.Produto.UpdateOneAsync(filter, update);
        }

        public async Task<Domain.Entities.Produto> ObterProdutoAsync(string idProduto)
        {
            var filter = Builders<ProdutoModel>.Filter.Eq(p => p.Id, idProduto);

            var result = await _context.Produto.Find(filter).Limit(1).FirstOrDefaultAsync();

            return ProdutoModel.MapToDomain(result);
        }

        public async Task<IEnumerable<Domain.Entities.Produto>> ObterTodosTiposProdutoAsync(TipoProduto tipoProduto, bool ativo, bool retornarTodos)
        {
            var filter = Builders<ProdutoModel>.Filter.Eq(p => p.TipoProduto, tipoProduto);
            if (!retornarTodos)
                filter &= Builders<ProdutoModel>.Filter.Eq(p => p.Ativo, ativo);

            var sort = Builders<ProdutoModel>.Sort.Descending(x => x.Id);

            var result = await _context.Produto.Find(filter).Sort(sort).ToListAsync();

            return ProdutoModel.MapToDomain(result);
        }

        public async Task RemoverProdutoAsync(Domain.Entities.Produto produto)
        {
            var filter = Builders<ProdutoModel>.Filter.Eq(p => p.Id, produto.Id);

            await _context.Produto.DeleteOneAsync(filter);
        }
    }
}
