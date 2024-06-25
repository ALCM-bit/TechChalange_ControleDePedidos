using CadastroPedidos.Produto.Application.UseCases.GravarProduto;
using CadastroPedidos.Produto.Application.UseCases.ObterProduto;
using ControlePedidos.Produto.Domain.Abstractions;
using ControlePedidos.Produto.Domain.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity = ControlePedidos.Produto.Domain.Entities;

namespace ControlePedidos.Produto.Test.Unit.Application.UseCases;

public class ObterProdutoTest
{
    private readonly ObterProdutoUseCase _useCase;
    private readonly Mock<IProdutoRepository> _produtoRepositoryMock;

    private readonly List<KeyValuePair<string, decimal>> Tamanho = new List<KeyValuePair<string, decimal>>(){
                new KeyValuePair<string, decimal>("P", 10),
                new KeyValuePair<string, decimal>("M", 15),
                new KeyValuePair<string, decimal>("G", 20)
     };

    public ObterProdutoTest()
    {
        _produtoRepositoryMock = new();
        _useCase = new(_produtoRepositoryMock.Object);
    }

    [Fact]
    public async Task ObterProdutoAsync_ShouldReturnProductResponse()
    {
        // Arrange
        var id = "1";
        var date = DateTime.UtcNow;
        var produto = new Entity.Produto(id, "Produto 1", Tamanho, TipoProduto.Bebida, "Descrição do Produto 1", date, true);
        var produtoResponse = new ProdutoResponse
        {
            Id = id,
            Nome = "Produto 1",
            TamanhoPreco = Tamanho,
            TipoProduto = TipoProduto.Bebida,
            Descricao = "Descrição do Produto 1",
            DataCriacao = date,
            Ativo = true
        };

        _produtoRepositoryMock.Setup(x => x.ObterProdutoAsync(id)).ReturnsAsync(produto);

        // Act
        var result = await _useCase.ExecuteAsync(new() { IdProduto = id});

        // Assert
        Assert.Equal(produtoResponse.Id, result.Id);
        Assert.Equal(produtoResponse.Nome, result.Nome);
        Assert.Equal(produtoResponse.TamanhoPreco, result.TamanhoPreco);
        Assert.Equal(produtoResponse.TipoProduto, result.TipoProduto);
        Assert.Equal(produtoResponse.Descricao, result.Descricao);
        Assert.Equal(produtoResponse.DataCriacao, result.DataCriacao);
        Assert.Equal(produtoResponse.Ativo, result.Ativo);
    }
}
