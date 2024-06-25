using CadastroPedidos.Produto.Application.DTO;
using CadastroPedidos.Produto.Application.UseCases.AtualizarProduto;
using CadastroPedidos.Produto.Application.UseCases.GravarProduto;
using ControlePedidos.Common.Entities;
using ControlePedidos.Produto.Domain.Abstractions;
using ControlePedidos.Produto.Domain.Enums;
using Moq;
using Entity = ControlePedidos.Produto.Domain.Entities;

namespace ControlePedidos.Produto.Test.Unit.Application.UseCases;

public class AtualizarProdutoTest
{
    private readonly AtualizarProdutoUseCase _useCase;
    private readonly Mock<IProdutoRepository> _produtoRepositoryMock;

    private readonly List<KeyValuePair<string, decimal>> Tamanho = new List<KeyValuePair<string, decimal>>(){
                new KeyValuePair<string, decimal>("P", 10),
                new KeyValuePair<string, decimal>("M", 15),
                new KeyValuePair<string, decimal>("G", 20)
     };

    public AtualizarProdutoTest()
    {
        _produtoRepositoryMock = new();
        _useCase = new(_produtoRepositoryMock.Object);
    }

    [Fact]
    public async Task AtualizarProdutoAsync_ShouldUpdateExistingProduct()
    {
        // Arrange
        var id = "1";

        new KeyValuePair<string, decimal>() { };
        var produtoAntigo = new Entity.Produto(id, "Produto Antigo", Tamanho, TipoProduto.Bebida, "Descrição do Produto Antigo", DateTime.UtcNow, true);
        var produtoAtualizado = new AtualizarProdutoRequest
        {
            Id = id,
            Nome = "Produto Atualizado",
            TamanhoPreco = Tamanho,
            TipoProduto = TipoProduto.Bebida,
            Descricao = "Descrição do Produto Atualizado",
            Ativo = false
        };

        _produtoRepositoryMock.Setup(x => x.ObterProdutoAsync(id)).ReturnsAsync(produtoAntigo);
        _produtoRepositoryMock.Setup(x => x.AtualizarProdutoAsync(It.IsAny<Entity.Produto>())).Verifiable();

        // Act
        await _useCase.ExecuteAsync(produtoAtualizado);

        // Assert
        _produtoRepositoryMock.Verify(x => x.ObterProdutoAsync(id), Times.Once);
        _produtoRepositoryMock.Verify(x => x.AtualizarProdutoAsync(It.Is<Entity.Produto>(p =>
            p.Id == id &&
            p.Nome == produtoAtualizado.Nome &&
            p.TamanhoPreco == produtoAtualizado.TamanhoPreco &&
            p.TipoProduto == produtoAtualizado.TipoProduto &&
            p.Descricao == produtoAtualizado.Descricao &&
            p.DataCriacao == produtoAntigo.DataCriacao &&
            p.Ativo == produtoAtualizado.Ativo
        )), Times.Once);
    }

}
