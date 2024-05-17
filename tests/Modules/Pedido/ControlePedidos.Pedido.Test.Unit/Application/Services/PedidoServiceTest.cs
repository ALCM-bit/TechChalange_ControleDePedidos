using CadastroPedidos.Pedido.Application.Abstractions;
using CadastroPedidos.Pedido.Application.DTO;
using CadastroPedidos.Pedido.Application.Services;
using ControlePedidos.Common.Exceptions;
using ControlePedidos.Pedido.Domain.Abstractions;
using ControlePedidos.Pedido.Domain.Enums;
using Moq;

namespace ControlePedidos.Pedido.Test.Unit.Application.Services;

public class PedidoServiceTest
{
    // TODO: Atualizar testes quando modulo de Produtos pronto

    private readonly IPedidoService _pedidoService;
    private readonly Mock<IPedidoRepository> _pedidoRepositoryMock;

    public PedidoServiceTest()
    {
        _pedidoRepositoryMock = new();
        _pedidoService = new PedidoService(_pedidoRepositoryMock.Object);
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

        return new(id, codigo, idCliente, status, dataCriacao, dataFinalizacao);
    }

    #endregion

    #region ObterPedidoAsync

    [Fact]
    public async Task ObterPedidoAsync_Should_RetornarPedido_When_PedidoEncontrado()
    {
        // Arrange
        var pedido = CriarPedidoPadrao();

        _pedidoRepositoryMock.Setup(x => x.ObterPedidoAsync(pedido.Id!)).ReturnsAsync(pedido);

        // Act
        var response = await _pedidoService.ObterPedidoAsync(pedido.Id!);

        // Assert
        _pedidoRepositoryMock.Verify(x => x.ObterPedidoAsync(pedido.Id!), Times.Once);
    }

    [Fact]
    public async Task ObterPedidoAsync_Should_RetornarNulo_When_PedidoNaoEncontrado()
    {
        // Arrange
        string idPedido = "test";
        _pedidoRepositoryMock.Setup(x => x.ObterPedidoAsync(It.IsAny<string>())).ReturnsAsync(() => null!);

        // Act
        var response = await _pedidoService.ObterPedidoAsync(idPedido);

        // Assert
        _pedidoRepositoryMock.Verify(x => x.ObterPedidoAsync(idPedido), Times.Once);
        Assert.Null(response);
    }

    #endregion

    #region ObterTodosPedidosAsync

    [Fact]
    public async Task ObterTodosPedidosAsync_Should_RetornarListaDePedido_When_ExistirPedidos()
    {
        // Arrange
        var pedido1 = CriarPedidoPadrao();
        var pedido2 = CriarPedidoPadrao();

        _pedidoRepositoryMock.Setup(x => x.ObterTodosPedidosAsync()).ReturnsAsync([pedido1, pedido2]);

        // Act
        var response = await _pedidoService.ObterTodosPedidosAsync();

        // Assert
        _pedidoRepositoryMock.Verify(x => x.ObterTodosPedidosAsync(), Times.Once);
        Assert.Equal(2, response.Count());
    }

    #endregion

    #region CriarPedidoAsync

    [Fact]
    public async Task CriarPedidoAsync_Should_RetornarCodigoPedido_When_PedidoCriado()
    {
        // Arrange
        var request = new PedidoRequest()
        {
            IdCliente = null
        };

        _pedidoRepositoryMock.Setup(x => x.CriarPedidoAsync(It.IsAny<Entity.Pedido>())).ReturnsAsync(() => "id_teste");

        // Act
        var response = await _pedidoService.CriarPedidoAsync(request);

        // Assert
        _pedidoRepositoryMock.Verify(x => x.CriarPedidoAsync(It.IsAny<Entity.Pedido>()), Times.Once);
        Assert.Equal(5, response.Length);
    }

    #endregion

    #region AtualizarPedidoAsync

    [Fact]
    public async Task AtualizarPedidoAsync_Should_ThrowApplicationNotificationException_When_PedidoNaoEncontrado()
    {
        // Arrange
        string idPedido = "test";
        _pedidoRepositoryMock.Setup(x => x.ObterPedidoAsync(It.IsAny<string>())).ReturnsAsync(() => null!);

        // Act
        Task act = _pedidoService.AtualizarPedidoAsync(idPedido, null!);

        // Assert
        await Assert.ThrowsAsync<ApplicationNotificationException>(async () => await act);
        _pedidoRepositoryMock.Verify(x => x.ObterPedidoAsync(idPedido), Times.Once);
        _pedidoRepositoryMock.Verify(x => x.AtualizarPedidoAsync(It.IsAny<Entity.Pedido>()), Times.Never);
    }

    [Theory]
    [InlineData(StatusPedido.Recebido)]
    public async Task AtualizarPedidoAsync_Should_AtualizarPedido_When_PedidoEncontrado(StatusPedido status)
    {
        // Arrange
        var pedido = CriarPedidoPadrao();
        var request = new AtualizarPedidoRequest()
        {
            Status = status
        };

        _pedidoRepositoryMock.Setup(x => x.ObterPedidoAsync(It.IsAny<string>())).ReturnsAsync(pedido);
        //_pedidoRepositoryMock.Setup(x => x.AtualizarPedidoAsync(pedido)).ReturnsAsync(() => Task.CompletedTask);

        // Act
        await _pedidoService.AtualizarPedidoAsync(pedido.Id!, request);

        // Assert
        _pedidoRepositoryMock.Verify(x => x.AtualizarPedidoAsync(pedido), Times.Once);
        Assert.Equal(status, pedido.Status);
    }

    #endregion
}
