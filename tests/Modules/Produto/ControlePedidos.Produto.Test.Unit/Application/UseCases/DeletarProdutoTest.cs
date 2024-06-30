using CadastroPedidos.Produto.Application.UseCases.DeletarProduto;
using CadastroPedidos.Produto.Application.UseCases.GravarProduto;
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

public class DeletarProdutoTest
{

    private readonly DeletarProdutoUseCase _useCase;
    private readonly Mock<IProdutoRepository> _produtoRepositoryMock;

    private readonly List<KeyValuePair<string, decimal>> Tamanho = new List<KeyValuePair<string, decimal>>(){
                new KeyValuePair<string, decimal>("P", 10),
                new KeyValuePair<string, decimal>("M", 15),
                new KeyValuePair<string, decimal>("G", 20)
     };

    public DeletarProdutoTest()
    {
        _produtoRepositoryMock = new();
        _useCase = new(_produtoRepositoryMock.Object);

    }

    [Fact]
    public async Task RemoverProdutoAsync_ShouldRemoveExistingProduct()
    {
        // Arrange
        var id = "1";
        var produto = new Entity.Produto(id, "Produto 1", Tamanho, TipoProduto.Lanche, "Descrição do Produto 1", DateTime.UtcNow, true);

        _produtoRepositoryMock.Setup(x => x.ObterProdutoAsync(id)).ReturnsAsync(produto);
        _produtoRepositoryMock.Setup(x => x.RemoverProdutoAsync(produto)).Verifiable();

        // Act
        await _useCase.ExecuteAsync(new() { Id = id});

        // Assert
        _produtoRepositoryMock.Verify(x => x.ObterProdutoAsync(id), Times.Once);
        _produtoRepositoryMock.Verify(x => x.RemoverProdutoAsync(produto), Times.Once);
    }
}
