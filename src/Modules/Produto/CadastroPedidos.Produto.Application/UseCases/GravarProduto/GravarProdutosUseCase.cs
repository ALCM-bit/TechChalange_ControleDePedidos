using CadastroPedidos.Produto.Application.Abstractions;
using ControlePedidos.Produto.Domain.Abstractions;
using System.Collections.Generic;
using Entity = ControlePedidos.Produto.Domain.Entities;

namespace CadastroPedidos.Produto.Application.UseCases.GravarProduto;

public class GravarProdutosUseCase : IUseCase<IEnumerable<GravarProdutosRequest>>
{
    private readonly IProdutoRepository _produtoRepository;

    public GravarProdutosUseCase(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task ExecuteAsync(IEnumerable<GravarProdutosRequest> request)
    {
        var novosProdutos = request.Select(produto => new Entity.Produto(string.Empty, produto.Nome, produto.TamanhoPreco, produto.TipoProduto, produto.Descricao, DateTime.UtcNow, true)).ToList();

        await _produtoRepository.AdicionarProdutoAsync(novosProdutos);
    }
}
