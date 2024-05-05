using CadastroPedidos.Produto.Application.Abstractions;
using ControlePedidos.Produto.Domain.Abstractions;
using Mapster;

namespace CadastroPedidos.Produto.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        //  Implementar os métodos da interface
    }
}
