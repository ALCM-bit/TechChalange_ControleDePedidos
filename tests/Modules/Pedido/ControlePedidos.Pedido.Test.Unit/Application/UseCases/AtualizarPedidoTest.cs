using CadastroPedidos.Pedido.Application.Abstractions;
using CadastroPedidos.Pedido.Application.DTO;
using CadastroPedidos.Pedido.Application.UseCases.AtualizarPedido;
using ControlePedidos.Common.Exceptions;
using ControlePedidos.Pedido.Domain.Abstractions;
using ControlePedidos.Pedido.Domain.Entities;
using ControlePedidos.Pedido.Domain.Enums;
using Moq;

namespace ControlePedidos.Pedido.Test.Unit.Application.UseCases;
public class AtualizarPedidoTest : BaseUnitTest
{
    private readonly Mock<IPedidoApplicationService> _pedidoApplicationServiceMock;
    private readonly Mock<IPedidoRepository> _pedidoRepositoryMock;
    private readonly AtualizarPedidoUseCase _useCase;

    public AtualizarPedidoTest()
    {
        _pedidoApplicationServiceMock = new();
        _pedidoRepositoryMock = new();
        _useCase = new(_pedidoApplicationServiceMock.Object, _pedidoRepositoryMock.Object);
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
    public async Task AtualizarPedidoAsync_Should_ThrowApplicationNotificationException_When_PedidoNaoEncontrado()
    {
        // Arrange
        string idPedido = "test";

        var request = new AtualizarPedidoRequest()
        {
            IdPedido = idPedido
        };

        _pedidoRepositoryMock.Setup(x => x.ObterPedidoAsync(request.IdPedido)).ReturnsAsync(() => null!);

        // Act
        Task act = _useCase.ExecuteAsync(request);

        // Assert
        await Assert.ThrowsAsync<ApplicationNotificationException>(async () => await act);
        _pedidoRepositoryMock.Verify(x => x.ObterPedidoAsync(idPedido), Times.Once);
        _pedidoRepositoryMock.Verify(x => x.AtualizarPedidoAsync(It.IsAny<Entity.Pedido>()), Times.Never);
    }

    [Theory]
    [InlineData(StatusPedido.Recebido)]
    public async Task AtualizarPedidoAsync_Should_AtualizarStatus_When_PedidoEncontrado(StatusPedido status)
    {
        // Arrange
        var pedido = CriarPedidoPadrao();
        var request = new AtualizarPedidoRequest()
        {
            IdPedido = pedido.Id!,
            Status = status
        };

        _pedidoRepositoryMock.Setup(x => x.ObterPedidoAsync(request.IdPedido)).ReturnsAsync(pedido);

        // Act
        await _useCase.ExecuteAsync(request);

        // Assert
        _pedidoRepositoryMock.Verify(x => x.AtualizarPedidoAsync(pedido), Times.Once);
        Assert.Equal(status, pedido.Status);
    }

    [Fact]
    public async Task AtualizarPedidoAsync_Should_AtualizarItensPedido_When_PedidoEncontrado()
    {
        // Arrange
        var pedido = CriarPedidoPadrao();
        var produto = CriarProdutoPadrao();

        var novoItemPedido = new ItemPedidoRequestDto()
        {
            Observacao = null,
            ProdutoId = produto.Id!,
            Quantidade = 1,
            Tamanho = "M"
        };

        var request = new AtualizarPedidoRequest()
        {
            IdPedido = pedido.Id!,
            Itens = new List<ItemPedidoRequestDto> { novoItemPedido }
        };

        _pedidoRepositoryMock.Setup(x => x.ObterPedidoAsync(pedido.Id!)).ReturnsAsync(pedido);
        _pedidoApplicationServiceMock.Setup(x => x.GerarItensPedidoAsync(It.IsAny<List<ItemPedidoRequestDto>>())).ReturnsAsync([CriarItemPedidoPadrao()]);

        // Act
        await _useCase.ExecuteAsync(request);

        // Assert
        _pedidoRepositoryMock.Verify(x => x.ObterPedidoAsync(pedido.Id!), Times.Once);
        _pedidoRepositoryMock.Verify(x => x.AtualizarPedidoAsync(pedido), Times.Once);
        Assert.Single(pedido.Itens);
    }
}
