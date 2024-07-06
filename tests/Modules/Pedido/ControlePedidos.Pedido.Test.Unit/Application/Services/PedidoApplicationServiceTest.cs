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
        _pedidoApplicationService = new PedidoApplicationService(_produtoExternalRepository.Object);

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
        string tamanhoProduto = "M";
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
        Dictionary<string, decimal> tamanhoPreco = new() { { "M", Math.Round(preco, 2) } };
        string descricao = GerarIdValido().Substring(0, 5);
        bool ativo = true;

        return new Produto(id, nome, tamanhoPreco, tipoProduto, descricao, dataCriacao, ativo);
    }

    #endregion

    #region GerarItensPedidoAsync

    [Fact]
    public async Task GerarItensPedidoAsync_Should_RetornarItensPedido_When_ItensValidos()
    {
        // Arrange
        var produto = CriarProdutoPadrao();

        var novoItemPedido = new ItemPedidoRequestDto()
        {
            Observacao = null,
            ProdutoId = produto.Id!,
            Quantidade = 1,
            Tamanho = "M"
        };

        _produtoExternalRepository.Setup(x => x.ObterTodosProdutosAsync(true)).ReturnsAsync([produto]);
        
        // Act
        var response = await _pedidoApplicationService.GerarItensPedidoAsync([novoItemPedido]);

        // Assert
        _produtoExternalRepository.Verify(x => x.ObterTodosProdutosAsync(true), Times.Once);
        Assert.Single(response);
        Assert.Equal(produto.Id, response.FirstOrDefault()?.ProdutoId);
    }

    [Fact]
    public async Task GerarItensPedidoAsync_Should_ThrowNotificationException_When_ProdutoNaoEncontrado()
    {
        // Arrange
        var produto = CriarProdutoPadrao();

        var novoItemPedido = new ItemPedidoRequestDto()
        {
            Observacao = null,
            ProdutoId = produto.Id!,
            Quantidade = 1,
            Tamanho = "M"
        };

        _produtoExternalRepository.Setup(x => x.ObterTodosProdutosAsync(true)).ReturnsAsync([]);

        // Act
        Task act = _pedidoApplicationService.GerarItensPedidoAsync([novoItemPedido]);

        var ex = await Assert.ThrowsAsync<ApplicationNotificationException>(async () => await act);

        // Assert
        _produtoExternalRepository.Verify(x => x.ObterTodosProdutosAsync(true), Times.Once);

        Assert.NotNull(ex);
        Assert.Contains("inativo ou não encontrado", ex.Message, StringComparison.InvariantCultureIgnoreCase);
    }

    #endregion
}
