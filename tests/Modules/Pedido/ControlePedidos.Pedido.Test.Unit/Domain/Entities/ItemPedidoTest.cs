using ControlePedidos.Common.Exceptions;
using ControlePedidos.Pedido.Domain.Entities;
using ControlePedidos.Pedido.Domain.Enums;

namespace ControlePedidos.Pedido.Test.Unit.Domain.Entities;

public class ItemPedidoTest : BaseUnitTest
{
    private static ItemPedido CriarItemPedidoPadrao()
    {
        string? id = GerarIdValido();
        DateTime dataCriacao = DateTime.UtcNow;
        string produtoId = GerarIdValido();
        string nome = GerarIdValido().Substring(0, 5);
        string tipoProduto = "Lanche";
        TamanhoProduto tamanhoProduto = TamanhoProduto.M;
        decimal preco = (decimal)new Random().NextDouble() * (100 - 1) + 1;
        int quantidade = (int)new Random().NextInt64(1, 3);
        string observacao = GerarIdValido().Substring(0, 5);

        return new(id, dataCriacao, produtoId, nome, tipoProduto, tamanhoProduto, Math.Round(preco, 2), quantidade, observacao);
    }

    #region Validations

    [Fact]
    public void Validate_Should_CriarInstancia_When_ParametrosValidos()
    {
        // Arrange
        string? id = GerarIdValido();
        DateTime dataCriacao = DateTime.UtcNow;
        string produtoId = GerarIdValido();
        string nome = GerarIdValido().Substring(0, 5);
        string tipoProduto = "Lanche";
        TamanhoProduto tamanhoProduto = TamanhoProduto.M;
        decimal preco = (decimal)new Random().NextDouble() * (100 - 1) + 1;
        int quantidade = (int)new Random().NextInt64(1, 3);
        string observacao = GerarIdValido().Substring(0, 5);

        // Act
        var item = new ItemPedido(id, dataCriacao, produtoId, nome, tipoProduto, tamanhoProduto, Math.Round(preco, 2), quantidade, observacao);

        // Assert
        Assert.NotNull(item);
    }

    [Fact]
    public void Validate_Should_ThrowDomainNotificationException_When_QuantidadeMenorQueUm()
    {
        // Arrange
        int quantidade = 0;

        // Act
        Action act = () => new ItemPedido(null!, DateTime.UtcNow, GerarIdValido(), "NomeTeste", "Sobremesa", TamanhoProduto.M, 10, quantidade, string.Empty);

        // Assert
        var notificationException = Assert.Throws<DomainNotificationException>(act);
    }

    #endregion
}
