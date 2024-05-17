using ControlePedidos.Common.Exceptions;
using ControlePedidos.Pedido.Domain.Enums;

namespace ControlePedidos.Pedido.Test.Unit.Domain.Entities;

public class PedidoTest
{
    // TODO: Atualizar testes quando modulo de Produtos pronto

    #region Setup Methods

    private static Entity.Pedido CriarPedidoPadrao()
    {
        string? id = null;
        string? codigo = null;
        string? idCliente = null;
        StatusPedido? status = null;
        DateTime dataCriacao = DateTime.UtcNow;
        DateTime? dataFinalizacao = null;

        return new(id, codigo, idCliente, status, dataCriacao, dataFinalizacao);
    }

    #endregion

    #region Validations

    [Fact]
    public void Validate_Should_ThrowDomainNotificationException_When_DataFinalizacaoMenorQueDataCriacao()
    {
        // Arrange
        DateTime dataFinalizacao = DateTime.UtcNow;
        DateTime dataCriacao = dataFinalizacao.AddMinutes(1);

        // Act
        Action act = () => new Entity.Pedido(null, null, null, StatusPedido.Finalizado, dataCriacao, dataFinalizacao);

        // Assert
        var notificationException = Assert.Throws<DomainNotificationException>(act);
    }

    #endregion

    [Fact]
    public void ObterTotal_Should_RetornarSomaDoValorDosProdutos()
    {
        // Arrange
        var pedido = CriarPedidoPadrao();

        // Act
        double total = pedido.ObterTotal();

        // Assert
        Assert.Equal(100, total);
    }

    #region ConfirmarPedido

    [Fact]
    public void ConfirmarPedido_Should_AlterarStatusParaRecebido_When_StatusNulo()
    {
        // Arrange
        var pedido = CriarPedidoPadrao();

        // Act
        pedido.ConfirmarPedido();

        // Assert
        Assert.Equal(StatusPedido.Recebido, pedido.Status);
    }

    [Fact]
    public void ConfirmarPedido_Should_ThrowDomainNotificationException_When_StatusNaoNulo()
    {
        // Arrange
        StatusPedido status = StatusPedido.Preparando;
        var pedido = new Entity.Pedido(null, null, null, status, DateTime.UtcNow, null);

        // Act
        Action act = () => pedido.ConfirmarPedido();

        // Assert
        var notificationException = Assert.Throws<DomainNotificationException>(act);
    }

    #endregion

    #region IniciarPreparo

    [Fact]
    public void IniciarPreparo_Should_AlterarStatusParaPreparando_When_StatusRecebido()
    {
        // Arrange
        StatusPedido status = StatusPedido.Recebido;
        var pedido = new Entity.Pedido(null, null, null, status, DateTime.UtcNow, null);

        // Act
        pedido.IniciarPreparo();

        // Assert
        Assert.Equal(StatusPedido.Preparando, pedido.Status);
    }

    [Fact]
    public void IniciarPreparo_Should_ThrowDomainNotificationException_When_StatusDiferenteDeRecebido()
    {
        // Arrange
        StatusPedido status = StatusPedido.Pronto;
        var pedido = new Entity.Pedido(null, null, null, status, DateTime.UtcNow, null);

        // Act
        Action act = () => pedido.IniciarPreparo();

        // Assert
        var notificationException = Assert.Throws<DomainNotificationException>(act);
    }

    #endregion

    #region FinalizarPreparo

    [Fact]
    public void FinalizarPreparo_Should_AlterarStatusParaPreparando_When_StatusPreparando()
    {
        // Arrange
        StatusPedido status = StatusPedido.Preparando;
        var pedido = new Entity.Pedido(null, null, null, status, DateTime.UtcNow, null);

        // Act
        pedido.FinalizarPreparo();

        // Assert
        Assert.Equal(StatusPedido.Pronto, pedido.Status);
    }

    [Fact]
    public void IniciarPreparo_Should_ThrowDomainNotificationException_When_StatusDiferenteDePreparando()
    {
        // Arrange
        StatusPedido status = StatusPedido.Recebido;
        var pedido = new Entity.Pedido(null, null, null, status, DateTime.UtcNow, null);

        // Act
        Action act = () => pedido.FinalizarPreparo();

        // Assert
        var notificationException = Assert.Throws<DomainNotificationException>(act);
    }

    #endregion

    #region FinalizarPedido

    [Fact]
    public void FinalizarPedido_Should_AlterarStatusParaPreparando_When_StatusPronto()
    {
        // Arrange
        StatusPedido status = StatusPedido.Pronto;
        var pedido = new Entity.Pedido(null, null, null, status, DateTime.UtcNow, null);

        // Act
        pedido.FinalizarPedido();

        // Assert
        Assert.Equal(StatusPedido.Finalizado, pedido.Status);
        Assert.NotNull(pedido.DataAtualizacao);
    }

    [Fact]
    public void FinalizarPedido_Should_ThrowDomainNotificationException_When_StatusDiferenteDePronto()
    {
        // Arrange
        StatusPedido status = StatusPedido.Recebido;
        var pedido = new Entity.Pedido(null, null, null, status, DateTime.UtcNow, null);

        // Act
        Action act = () => pedido.FinalizarPedido();

        // Assert
        var notificationException = Assert.Throws<DomainNotificationException>(act);
    }

    #endregion

    #region AtualizarStatus

    [Theory]
    [InlineData(null, StatusPedido.Recebido)]
    [InlineData(StatusPedido.Recebido, StatusPedido.Preparando)]
    [InlineData(StatusPedido.Preparando, StatusPedido.Pronto)]
    [InlineData(StatusPedido.Pronto, StatusPedido.Finalizado)]
    public void AtualizarStatus_Should_AlterarStatus_When_StatusNovoValido(StatusPedido? statusAtual, StatusPedido statusNovo)
    {
        // Arrange
        var pedido = new Entity.Pedido(null, null, null, statusAtual, DateTime.UtcNow, null);

        // Act
        pedido.AtualizarStatus(statusNovo);

        // Assert
        Assert.Equal(statusNovo, pedido.Status);
    }

    [Theory]
    [InlineData(StatusPedido.Pronto, StatusPedido.Recebido)]
    [InlineData(null, StatusPedido.Preparando)]
    [InlineData(StatusPedido.Finalizado, StatusPedido.Pronto)]
    [InlineData(StatusPedido.Recebido, StatusPedido.Finalizado)]
    public void AtualizarStatus_Should_ThrowDomainNotificationException_When_StatusNovoInvalido(StatusPedido? statusAtual, StatusPedido statusNovo)
    {
        // Arrange
        var pedido = new Entity.Pedido(null, null, null, statusAtual, DateTime.UtcNow, null);

        // Act
        Action act = () => pedido.AtualizarStatus(statusNovo);

        // Assert
        var notificationException = Assert.Throws<DomainNotificationException>(act);
    }

    #endregion
}
