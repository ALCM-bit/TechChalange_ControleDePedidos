using CadastroPedidos.Produto.Application.Abstractions;
using ControlePedidos.Common.Exceptions;
using ControlePedidos.Produto.Domain.Abstractions;
using ControlePedidos.Produto.Domain.Enums;
using Mapster;
using Entity = ControlePedidos.Produto.Domain.Entities;

namespace CadastroPedidos.Produto.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        //  Implementar os métodos da interface
        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }                                                                                                                                                                       

        public async Task AdicionarProdutoAsync(ProdutoRequest produto)
        {
            var novoProduto = new Entity.Produto(string.Empty, produto.Nome, produto.Preco, produto.TipoProduto, produto.Descricao);

            await _produtoRepository.AdicionarProdutoAsync(novoProduto);
        }

        public async Task AtualizarProdutoAsync(string id, ProdutoRequest produto)
        {
            Entity.Produto produtoAntigo = await _produtoRepository.ObterProdutoAsync(id);

            if (produtoAntigo is null)
            {
                throw new ApplicationNotificationException("Pedido não encontrado");
            }
        }

        public async Task<ProdutoResponse> ObterProdutoAsync(string id)
        {
            Entity.Produto produto = await _produtoRepository.ObterProdutoAsync(id);

            if (produto is null)
            {
                return null!;
            }

            var produtoResponse = produto.Adapt<ProdutoResponse>();

            return produtoResponse;
        }

        public Task<IEnumerable<ProdutoResponse>> ObterTodosTiposProdutoAsync(TipoProduto tipoProduto)
        {
            throw new NotImplementedException();
        }

        public Task RemoverProdutoAsync(string id)
        {
            // Implementar a lógica de remoção
            throw new NotImplementedException();
        }
    }
}
