using CadastroPedidos.Produto.Application.UseCases.GravarProduto;
using ControlePedidos.Common.Entities;
using ControlePedidos.Produto.Domain.Abstractions;
using ControlePedidos.Produto.Domain.Enums;
using Moq;
using Entity = ControlePedidos.Produto.Domain.Entities;

namespace ControlePedidos.Produto.Test.Unit.Application.UseCases;

public class GravarProdutoTest
{

    private readonly GravarProdutosUseCase _useCase;
    private readonly Mock<IProdutoRepository> _produtoRepositoryMock;

    private readonly List<KeyValuePair<string, decimal>> Tamanho = new List<KeyValuePair<string, decimal>>(){
                new KeyValuePair<string, decimal>("P", 10),
                new KeyValuePair<string, decimal>("M", 15),
                new KeyValuePair<string, decimal>("G", 20)
     };

    public GravarProdutoTest()
    {
        _produtoRepositoryMock = new();
        _useCase = new(_produtoRepositoryMock.Object);
    
    }

    [Fact]
    public async Task AdicionarProdutoAsync_ShouldAddNewProducts()
    {
        // Arrange
        var listaProdutos = new List<GravarProdutosRequest>
            {
                new GravarProdutosRequest
                {
                    Nome = "Produto 1",
                    TamanhoPreco = Tamanho,
                    TipoProduto = TipoProduto.Acompanhamento,
                    Descricao = "Descrição do Produto 1"
                }
            };

        var novosProdutos = listaProdutos.Select(produto => new Entity.Produto(string.Empty, produto.Nome, produto.TamanhoPreco, produto.TipoProduto, produto.Descricao, DateTime.UtcNow, true)).ToList();

        _produtoRepositoryMock.Setup(x => x.AdicionarProdutoAsync(novosProdutos)).Returns(Task.CompletedTask);

        // Act
        await _useCase.ExecuteAsync(listaProdutos);

        _produtoRepositoryMock.Verify(x => x.AdicionarProdutoAsync(It.Is<List<Entity.Produto>>(p =>
            p.Count == 1 && p.FirstOrDefault(x => x.Nome == listaProdutos[0].Nome) != null
        )), Times.Once);

    }

}
