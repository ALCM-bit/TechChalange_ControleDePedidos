using ControlePedidos.Common.Exceptions;
using ControlePedidos.Pedido.Domain.Entities;
using ControlePedidos.Pedido.Domain.Enums;

namespace ControlePedidos.Pedido.Test.Unit.Domain.Entities;

public class PedidoTest : BaseUnitTest
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
        int quantidade = (int)new Random().NextInt64(1,3);
        string observacao = GerarIdValido().Substring(0, 5);

        return new(id, dataCriacao, produtoId, nome, tipoProduto, tamanhoProduto, Math.Round(preco, 2), quantidade, observacao);
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
        Action act = () => new Entity.Pedido(null, null, null, StatusPedido.Finalizado, dataCriacao, dataFinalizacao, [CriarItemPedidoPadrao()]);

        // Assert
        var notificationException = Assert.Throws<DomainNotificationException>(act);
    }

    #endregion

    [Fact]
    public void ObterTotal_Should_RetornarSomaDoValorDosProdutos()
    {
        // Arrange
        var pedido = CriarPedidoPadrao();
        decimal total = pedido.Itens.Sum(i => i.Subtotal);

        // Act
        // Assert
        Assert.Equal(total, pedido.Total);
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
        var pedido = new Entity.Pedido(null, null, null, status, DateTime.UtcNow, null, [CriarItemPedidoPadrao()]);

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
        var pedido = new Entity.Pedido(null, null, null, status, DateTime.UtcNow, null, [CriarItemPedidoPadrao()]);

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
        var pedido = new Entity.Pedido(null, null, null, status, DateTime.UtcNow, null, [CriarItemPedidoPadrao()]);

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
        var pedido = new Entity.Pedido(null, null, null, status, DateTime.UtcNow, null, [CriarItemPedidoPadrao()]);

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
        var pedido = new Entity.Pedido(null, null, null, status, DateTime.UtcNow, null, [CriarItemPedidoPadrao()]);

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
        var pedido = new Entity.Pedido(null, null, null, status, DateTime.UtcNow, null, [CriarItemPedidoPadrao()]);

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
        var pedido = new Entity.Pedido(null, null, null, status, DateTime.UtcNow, null, [CriarItemPedidoPadrao()]);

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
        var pedido = new Entity.Pedido(null, null, null, statusAtual, DateTime.UtcNow, null, [CriarItemPedidoPadrao()]);

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
        var pedido = new Entity.Pedido(null, null, null, statusAtual, DateTime.UtcNow, null, [CriarItemPedidoPadrao()]);

        // Act
        Action act = () => pedido.AtualizarStatus(statusNovo);

        // Assert
        var notificationException = Assert.Throws<DomainNotificationException>(act);
    }

    #endregion
}
