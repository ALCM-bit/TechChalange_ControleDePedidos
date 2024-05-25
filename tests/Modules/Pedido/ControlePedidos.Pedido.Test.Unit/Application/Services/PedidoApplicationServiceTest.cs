using CadastroPedidos.Pedido.Application.Abstractions;
using CadastroPedidos.Pedido.Application.DTO;
using CadastroPedidos.Pedido.Application.Services;
using ControlePedidos.Common.Exceptions;
using ControlePedidos.Pedido.Domain.Abstractions;
using ControlePedidos.Pedido.Domain.Entities;
using ControlePedidos.Pedido.Domain.Enums;
using Moq;

namespace ControlePedidos.Pedido.Test.Unit.Application.Services;

public class PedidoApplicationServiceTest : BaseUnitTest
{
    private readonly IPedidoApplicationService _pedidoApplicationService;
    private readonly Mock<IProdutoExternalRepository> _produtoExternalRepository;
    private readonly Mock<IPedidoRepository> _pedidoRepositoryMock;

    public PedidoApplicationServiceTest()
    {
        _pedidoRepositoryMock = new();
        _produtoExternalRepository = new();
        _pedidoApplicationService = new PedidoApplicationService(_pedidoRepositoryMock.Object, _produtoExternalRepository.Object);

        _produtoExternalRepository.Setup(repo => repo.ObterTodosProdutosAsync(true)).ReturnsAsync([]);
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
        string nome = GerarIdValido().Substring(0,5);
        string tipoProduto = "Lanche";
        TamanhoProduto tamanhoProduto = TamanhoProduto.M;
        decimal preco = (decimal) new Random().NextDouble() * (100 - 1) + 1;
        int quantidade = (int) new Random().NextInt64(1, 3);
        string observacao = GerarIdValido().Substring(0,5);

        return new ItemPedido(id, dataCriacao, produtoId, nome, tipoProduto, tamanhoProduto, Math.Round(preco, 2), quantidade, observacao);
    }

    private static Produto CriarProdutoPadrao()
    {
        string? id = GerarIdValido();
        DateTime dataCriacao = DateTime.UtcNow;
        string nome = GerarIdValido().Substring(0, 5);
        string tipoProduto = "Lanche";
        decimal preco = (decimal)new Random().NextDouble() * (100 - 1) + 1;
        Dictionary<TamanhoProduto, decimal> tamanhoPreco = new() { { TamanhoProduto.M, Math.Round(preco, 2) } };
        string descricao = GerarIdValido().Substring(0, 5);
        bool ativo = true;

        return new Produto(id, nome, tamanhoPreco, tipoProduto, descricao, dataCriacao, ativo);
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
        var response = await _pedidoApplicationService.ObterPedidoAsync(pedido.Id!);

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
        var response = await _pedidoApplicationService.ObterPedidoAsync(idPedido);

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
        var response = await _pedidoApplicationService.ObterTodosPedidosAsync();

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
        var produto = CriarProdutoPadrao();

        var novoItemPedido = new ItemPedidoRequest()
        {
            Id = null,
            Observacao = null,
            ProdutoId = produto.Id!,
            Quantidade = 1,
            Tamanho = TamanhoProduto.P
        };

        var request = new PedidoRequest()
        {
            IdCliente = null,
            Itens = [novoItemPedido]
        };

        _produtoExternalRepository.Setup(x => x.ObterTodosProdutosAsync(true)).ReturnsAsync([produto]);
        _pedidoRepositoryMock.Setup(x => x.CriarPedidoAsync(It.IsAny<Entity.Pedido>())).ReturnsAsync(() => "id_teste");
        
        // Act
        var response = await _pedidoApplicationService.CriarPedidoAsync(request);

        // Assert
        _produtoExternalRepository.Verify(x => x.ObterTodosProdutosAsync(true), Times.Once);
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
        Task act = _pedidoApplicationService.AtualizarPedidoAsync(idPedido, null!);

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
            Status = status
        };

        _pedidoRepositoryMock.Setup(x => x.ObterPedidoAsync(It.IsAny<string>())).ReturnsAsync(pedido);

        // Act
        await _pedidoApplicationService.AtualizarPedidoAsync(pedido.Id!, request);

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

        var novoItemPedido = new ItemPedidoRequest()
        {
            Id = null,
            Observacao = null,
            ProdutoId = produto.Id!,
            Quantidade = 1,
            Tamanho = TamanhoProduto.P
        };

        var request = new AtualizarPedidoRequest()
        {
            Itens = new List<ItemPedidoRequest>{ novoItemPedido }
        };

        _pedidoRepositoryMock.Setup(x => x.ObterPedidoAsync(pedido.Id!)).ReturnsAsync(pedido);
        _produtoExternalRepository.Setup(x => x.ObterTodosProdutosAsync(true)).ReturnsAsync([produto]);

        // Act
        await _pedidoApplicationService.AtualizarPedidoAsync(pedido.Id!, request);

        // Assert
        _pedidoRepositoryMock.Verify(x => x.ObterPedidoAsync(pedido.Id!), Times.Once);
        _pedidoRepositoryMock.Verify(x => x.AtualizarPedidoAsync(pedido), Times.Once);
        Assert.Single(pedido.Itens);
        Assert.Equal(novoItemPedido.Id, pedido.Itens.FirstOrDefault()?.Id);
    }

    #endregion
}
