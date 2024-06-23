using CadastroPedidos.Produto.Application.Abstractions;
using ControlePedidos.Produto.Domain.Abstractions;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroPedidos.Produto.Application.UseCases.ObterProduto
{
    public class ObterProdutoUseCase : IUseCase<ObterProdutoRequest, ObterProdutoResponse>
    {
        private readonly IProdutoRepository _produtoRepository;

        public ObterProdutoUseCase(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }
        public async  Task<ObterProdutoResponse> ExecuteAsync(ObterProdutoRequest request)
        {
            var produto = await _produtoRepository.ObterProdutoAsync(request.IdProduto);

            if (produto is null)
            {
                return null;
            }

            var produtoResponse = produto.Adapt<ObterProdutoResponse>();

            return produtoResponse;
        }
    }
}
