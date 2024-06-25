using CadastroPedidos.Produto.Application.UseCases.GravarProduto;
using CadastroPedidos.Produto.Application.UseCases.ObterTodosProdutos;
using ControlePedidos.Common.Entities;
using ControlePedidos.Produto.Domain.Abstractions;
using ControlePedidos.Produto.Domain.Enums;
using Moq;
using Entity = ControlePedidos.Produto.Domain.Entities;

namespace ControlePedidos.Produto.Test.Unit.Application.UseCases;

public class ObterTodosProdutosTest
{
    private readonly ObterTodosProdutosUseCase _useCase;
    private readonly Mock<IProdutoRepository> _produtoRepositoryMock;

    private readonly List<KeyValuePair<string, decimal>> Tamanho = new List<KeyValuePair<string, decimal>>(){
                new KeyValuePair<string, decimal>("P", 10),
                new KeyValuePair<string, decimal>("M", 15),
                new KeyValuePair<string, decimal>("G", 20)
     };

    public ObterTodosProdutosTest()
    {
        _produtoRepositoryMock = new();
        _useCase = new(_produtoRepositoryMock.Object);
    }

    [Fact]
    public async Task ObterTodosTiposProdutoAsync_ShouldReturnListOfProductResponse()
    {
        // Arrange
        var tipoProduto = TipoProduto.Lanche;
        var ativo = true;
        var retornarTodos = false;
        var produtos = new List<Entity.Produto>
            {
                new Entity.Produto("1", "Produto 1", Tamanho, TipoProduto.Lanche, "Descrição do Produto 1", DateTime.UtcNow, true),
                new Entity.Produto("2", "Produto 2", Tamanho, TipoProduto.Sobremesa, "Descrição do Produto 2", DateTime.UtcNow, true)
            };

        var produtoResponse = produtos.Select(p => new ProdutoResponse
        {
            Id = p.Id,
            Nome = p.Nome,
            TamanhoPreco = (List<KeyValuePair<string, decimal>>)p.TamanhoPreco,
            TipoProduto = p.TipoProduto,
            Descricao = p.Descricao,
            DataCriacao = p.DataCriacao,
            Ativo = p.Ativo
        }).ToList();

        _produtoRepositoryMock.Setup(x => x.ObterTodosTiposProdutoAsync(tipoProduto, ativo, retornarTodos)).ReturnsAsync(produtos);

        // Act
        var result = await _useCase.ExecuteAsync(new() { TipoProduto = tipoProduto, Ativo = ativo, RetornarTodos = retornarTodos});

        // Assert
        Assert.Equal(produtoResponse.Count, result.Count());
        for (int i = 0; i < produtoResponse.Count; i++)
        {
            Assert.Equal(produtoResponse[i].Id, result.ElementAt(i).Id);
            Assert.Equal(produtoResponse[i].Nome, result.ElementAt(i).Nome);
            Assert.Equal(produtoResponse[i].TamanhoPreco, result.ElementAt(i).TamanhoPreco);
            Assert.Equal(produtoResponse[i].TipoProduto, result.ElementAt(i).TipoProduto);
            Assert.Equal(produtoResponse[i].Descricao, result.ElementAt(i).Descricao);
            Assert.Equal(produtoResponse[i].DataCriacao, result.ElementAt(i).DataCriacao);
            Assert.Equal(produtoResponse[i].Ativo, result.ElementAt(i).Ativo);
        }
    }
}
