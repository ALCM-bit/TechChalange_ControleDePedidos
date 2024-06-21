using CadastroPedidos.Pedido.Application.Abstractions;
using CadastroPedidos.Pedido.Application.DTO;
using CadastroPedidos.Pedido.Application.UseCases.CriarPedido;
using ControlePedidos.Pedido.Domain.Abstractions;
using ControlePedidos.Pedido.Domain.Entities;
using ControlePedidos.Pedido.Domain.Enums;
using Moq;

namespace ControlePedidos.Pedido.Test.Unit.Application.UseCases;

public class CriarPedidoTest : BaseUnitTest
{
    private readonly Mock<IPedidoApplicationService> _pedidoApplicationServiceMock;
    private readonly Mock<IPedidoRepository> _pedidoRepositoryMock;
    private readonly CriarPedidoUseCase _useCase;

    public CriarPedidoTest()
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
    public async Task ExecuteAsync_Should_RetornarCodigoPedido_When_PedidoCriado()
    {
        // Arrange
        var novoItemPedido = new ItemPedidoRequestDto()
        {
            Observacao = null,
            ProdutoId = GerarIdValido(),
            Quantidade = 1,
            Tamanho = "M"
        };

        var request = new CriarPedidoRequest()
        {
            IdCliente = null,
            Itens = [novoItemPedido]
        };

        _pedidoApplicationServiceMock.Setup(x => x.GerarItensPedidoAsync(request.Itens)).ReturnsAsync([CriarItemPedidoPadrao()]);
        _pedidoRepositoryMock.Setup(x => x.CriarPedidoAsync(It.IsAny<Entity.Pedido>())).ReturnsAsync(() => "id_teste");

        // Act
        var response = await _useCase.ExecuteAsync(request);

        // Assert
        _pedidoRepositoryMock.Verify(x => x.CriarPedidoAsync(It.IsAny<Entity.Pedido>()), Times.Once);
        Assert.Equal("id_teste", response.Id);
    }
}
