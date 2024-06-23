using CadastroPedidos.Produto.Application.Abstractions;
using ControlePedidos.Produto.Domain.Abstractions;
using ControlePedidos.Produto.Domain.Enums;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroPedidos.Produto.Application.UseCases.ObterTodosProdutos;

public class ObterTodosProdutosUseCase : IUseCase<ObterTodosProdutosRequest, IEnumerable<ObterTodosProdutosResponse>>
{
    private readonly IProdutoRepository _produtoRepository;

    public ObterTodosProdutosUseCase(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }
    public async Task<IEnumerable<ObterTodosProdutosResponse>> ExecuteAsync(ObterTodosProdutosRequest request)
    {
        var produtos = await _produtoRepository.ObterTodosTiposProdutoAsync(request.TipoProduto, request.Ativo, request.RetornarTodos);

        var produtoResponse = produtos.Adapt<IEnumerable<ObterTodosProdutosResponse>>();

        return produtoResponse;
    }

}
