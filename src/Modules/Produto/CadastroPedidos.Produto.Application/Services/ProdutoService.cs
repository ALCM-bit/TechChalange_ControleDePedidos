using CadastroPedidos.Produto.Application.Abstractions;
using CadastroPedidos.Produto.Application.DTO;
using ControlePedidos.Common.Entities;
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

        public async Task AdicionarProdutoAsync(IEnumerable<ProdutoRequest> listaProdutos)
        {
            var novosProdutos = new List<Entity.Produto>();
            foreach (var produto in listaProdutos)
            {
                novosProdutos.Add(new Entity.Produto(string.Empty, produto.Nome, produto.Preco, produto.TipoProduto, produto.Descricao, DateTime.UtcNow, true));
            }

            await _produtoRepository.AdicionarProdutoAsync(novosProdutos);
        }

        public async Task AtualizarProdutoAsync(string id, AtualizaProdutoRequest produto)
        {
            var produtoAntigo = await _produtoRepository.ObterProdutoAsync(id);

            if (produtoAntigo is null)
                throw new ApplicationNotificationException("Produto não encontrado");
            else
            {
                var produtoAtualizado = new Entity.Produto(produtoAntigo.Id!, produto.Nome, produto.Preco, produto.TipoProduto, produto.Descricao, produtoAntigo.DataCriacao, produto.Ativo);

                await _produtoRepository.AtualizarProdutoAsync(produtoAtualizado);
            }
        }

        public async Task<ProdutoResponse> ObterProdutoAsync(string id)
        {
            var produto = await _produtoRepository.ObterProdutoAsync(id);

            if (produto is null)
                return null!;

            var produtoResponse = produto.Adapt<ProdutoResponse>();

            return produtoResponse;
        }

        public async Task<IEnumerable<ProdutoResponse>> ObterTodosTiposProdutoAsync(TipoProduto tipoProduto, bool ativo, bool retornarTodos)
        {
            var pedidos = await _produtoRepository.ObterTodosTiposProdutoAsync(tipoProduto, ativo, retornarTodos);

            var pedidoResponse = pedidos.Adapt<List<ProdutoResponse>>();

            return pedidoResponse;
        }

        public async Task RemoverProdutoAsync(string id)
        {
            var produto = await _produtoRepository.ObterProdutoAsync(id);

            if (produto is null)
                throw new ApplicationNotificationException("Produto não encontrado");

            await _produtoRepository.RemoverProdutoAsync(produto);
        }
    }
}
