using ControlePedidos.Produto.Domain.Abstractions;
using ControlePedidos.Produto.Domain.Enums;
using ControlePedidos.Produto.Infrastructure.Repositories.MongoDB.Contexts;

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

        public Task AdicionarProdutoAsync(Domain.Entities.Produto produto)
        {
            throw new NotImplementedException();
        }

        public Task AtualizarProdutoAsync(Domain.Entities.Produto produto)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Produto> ObterProdutoAsync(string idProduto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Domain.Entities.Produto>> ObterTodosTiposProdutoAsync(TipoProduto tipoProduto)
        {
            throw new NotImplementedException();
        }

        public Task RemoverProdutoAsync(Domain.Entities.Produto produto)
        {
            throw new NotImplementedException();
        }
    }
}
