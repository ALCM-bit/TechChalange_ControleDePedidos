using CadastroPedidos.Produto.Application.Abstractions;
using ControlePedidos.Common.Exceptions;
using ControlePedidos.Produto.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroPedidos.Produto.Application.UseCases.DeletarProduto;

public class DeletarProdutoUseCase : IUseCase<DeletarProdutoRequest, DeletarProdutoResponse>
{
    private readonly IProdutoRepository _produtoRepository;

    public DeletarProdutoUseCase(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }
    public async Task<DeletarProdutoResponse> ExecuteAsync(DeletarProdutoRequest request)
    {
        var produto = await _produtoRepository.ObterProdutoAsync(request.Id);

        if (produto is null)
        {
            throw new ApplicationNotificationException("Produto não encontrado");
        }

        await _produtoRepository.RemoverProdutoAsync(produto);

        return new DeletarProdutoResponse();
    }
}
