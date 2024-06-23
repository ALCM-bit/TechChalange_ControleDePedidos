using CadastroPedidos.Produto.Application.Abstractions;
using ControlePedidos.Common.Entities;
using ControlePedidos.Common.Exceptions;
using ControlePedidos.Produto.Domain.Abstractions;
using Entity = ControlePedidos.Produto.Domain.Entities;

namespace CadastroPedidos.Produto.Application.UseCases.AtualizarProduto;

public class AtualizarProdutoUseCase : IUseCase<AtualizarProdutoRequest>
{
    private readonly IProdutoRepository _produtoRepository;

    public AtualizarProdutoUseCase(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }
    public async Task ExecuteAsync(AtualizarProdutoRequest request)
    {
        var produtoAntigo = await _produtoRepository.ObterProdutoAsync(request.Id);

        if (produtoAntigo is null)
        {
            throw new ApplicationNotificationException("Produto não encontrado");
        }

        var produtoAtualizado = new Entity.Produto(produtoAntigo.Id!, request.Nome, request.TamanhoPreco, request.TipoProduto, request.Descricao, produtoAntigo.DataCriacao, request.Ativo);

        await _produtoRepository.AtualizarProdutoAsync(produtoAtualizado);
    }
}
