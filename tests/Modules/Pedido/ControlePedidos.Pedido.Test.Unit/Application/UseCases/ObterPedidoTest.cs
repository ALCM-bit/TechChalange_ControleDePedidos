using CadastroPedidos.Pedido.Application.UseCases.ObterPedido;
using ControlePedidos.Pedido.Domain.Abstractions;
using ControlePedidos.Pedido.Domain.Entities;
using ControlePedidos.Pedido.Domain.Enums;
using Moq;

namespace ControlePedidos.Pedido.Test.Unit.Application.UseCases;

public class ObterPedidoTest : BaseUnitTest
{
    private readonly ObterPedidoUseCase _useCase;
    private readonly Mock<IPedidoRepository> _pedidoRepositoryMock;

    public ObterPedidoTest()
    {
        _pedidoRepositoryMock = new();
        _useCase = new(_pedidoRepositoryMock.Object);
    }

    #region Setup Methods

    private static Entity.Pedido CriarPedidoPadrao()
    {
        string? id = "663ec4510724f79af50364f0";
        string? codigo = null;
        string? idCliente = null;
        StatusPedido? status = null;
        DateTime dataCriacao = DateTime.UtcNow;
        DateTime? dataFinalizacao = null;
        var itens = new List<ItemPedido>() { CriarItemPedidoPadrao() };

        return new(id, codigo, idCliente, status, dataCriacao, dataFinalizacao, itens);
    }

    private static ItemPedido CriarItemPedidoPadrao()
    {
        string? id = GerarIdValido();
        DateTime dataCriacao = DateTime.UtcNow;
        string produtoId = GerarIdValido();
        string nome = GerarIdValido().Substring(0, 5);
        string tipoProduto = "Lanche";
        string tamanhoProduto = "M";
        decimal preco = (decimal)new Random().NextDouble() * (100 - 1) + 1;
        int quantidade = (int)new Random().NextInt64(1, 3);
        string observacao = GerarIdValido().Substring(0, 5);

        return new ItemPedido(id, dataCriacao, produtoId, nome, tipoProduto, tamanhoProduto, Math.Round(preco, 2), quantidade, observacao);
    }

    private static Produto CriarProdutoPadrao()
    {
        string? id = GerarIdValido();
        DateTime dataCriacao = DateTime.UtcNow;
        string nome = GerarIdValido().Substring(0, 5);
        string tipoProduto = "Lanche";
        decimal preco = (decimal)new Random().NextDouble() * (100 - 1) + 1;
        Dictionary<string, decimal> tamanhoPreco = new() { { "M", Math.Round(preco, 2) } };
        string descricao = GerarIdValido().Substring(0, 5);
        bool ativo = true;

        return new Produto(id, nome, tamanhoPreco, tipoProduto, descricao, dataCriacao, ativo);
    }

    #endregion

    [Fact]
    public async Task ExecuteAsync_Should_RetornarPedido_When_PedidoEncontrado()
    {
        // Arrange
        var pedido = CriarPedidoPadrao();

        _pedidoRepositoryMock.Setup(x => x.ObterPedidoAsync(pedido.Id!)).ReturnsAsync(pedido);

        // Act
        var response = await _useCase.ExecuteAsync(new() { Id = pedido.Id! });

        // Assert
        _pedidoRepositoryMock.Verify(x => x.ObterPedidoAsync(pedido.Id!), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_Should_RetornarNulo_When_PedidoNaoEncontrado()
    {
        // Arrange
        string idPedido = "test";
        _pedidoRepositoryMock.Setup(x => x.ObterPedidoAsync(It.IsAny<string>())).ReturnsAsync(() => null!);

        // Act
        var response = await _useCase.ExecuteAsync(new() { Id = idPedido });

        // Assert
        _pedidoRepositoryMock.Verify(x => x.ObterPedidoAsync(idPedido), Times.Once);
        Assert.Null(response);
    }
}
