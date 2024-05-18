using CadastroPedidos.Produto.Application.Abstractions;
using CadastroPedidos.Produto.Application.DTO;
using CadastroPedidos.Produto.Application.Services;
using ControlePedidos.Produto.Domain.Abstractions;
using ControlePedidos.Produto.Domain.Enums;
using Moq;
using Entity = ControlePedidos.Produto.Domain.Entities;

namespace CadastroPedidos.Produto.Application.Tests.Services
{
    public class ProdutoServiceTests
    {
        private readonly Mock<IProdutoRepository> _produtoRepositoryMock;
        private readonly IProdutoService _produtoService;

        private readonly List<KeyValuePair<string, decimal>> Tamanho = new List<KeyValuePair<string, decimal>>(){
                new KeyValuePair<string, decimal>("P", 10),
                new KeyValuePair<string, decimal>("M", 15),
                new KeyValuePair<string, decimal>("G", 20)
            };

        public ProdutoServiceTests()
        {
            _produtoRepositoryMock = new Mock<IProdutoRepository>();
            _produtoService = new ProdutoService(_produtoRepositoryMock.Object);
          
        }

        [Fact]
        public async Task AdicionarProdutoAsync_ShouldAddNewProducts()
        {
            // Arrange
            var listaProdutos = new List<ProdutoRequest>
            {
                new ProdutoRequest
                {
                    Nome = "Produto 1",
                    TamanhoPreco = Tamanho,
                    TipoProduto = TipoProduto.Acompanhamento,
                    Descricao = "Descrição do Produto 1"
                }
            };

            var novosProdutos = listaProdutos.Select(produto => new Entity.Produto(string.Empty, produto.Nome, produto.TamanhoPreco, produto.TipoProduto, produto.Descricao, DateTime.UtcNow, true)).ToList();

            _produtoRepositoryMock.Setup(x => x.AdicionarProdutoAsync(novosProdutos)).Returns(Task.CompletedTask); ;

            // Act
             await _produtoService.AdicionarProdutoAsync(listaProdutos);

            // Assert
            _produtoRepositoryMock.Verify(x => x.AdicionarProdutoAsync(novosProdutos), Times.Once);

        }

        [Fact]
        public async Task AtualizarProdutoAsync_ShouldUpdateExistingProduct()
        {
            // Arrange
            var id = "1";

            new KeyValuePair<string, decimal>() { };
            var produtoAntigo = new Entity.Produto(id, "Produto Antigo", Tamanho, TipoProduto.Bebida, "Descrição do Produto Antigo", DateTime.UtcNow, true);
            var produtoAtualizado = new AtualizaProdutoRequest
            {
                Nome = "Produto Atualizado",
                TamanhoPreco = Tamanho,
                TipoProduto = TipoProduto.Bebida,
                Descricao = "Descrição do Produto Atualizado",
                Ativo = false
            };

            _produtoRepositoryMock.Setup(x => x.ObterProdutoAsync(id)).ReturnsAsync(produtoAntigo);
            _produtoRepositoryMock.Setup(x => x.AtualizarProdutoAsync(It.IsAny<Entity.Produto>())).Verifiable();

            // Act
            await _produtoService.AtualizarProdutoAsync(id, produtoAtualizado);

            // Assert
            _produtoRepositoryMock.Verify(x => x.ObterProdutoAsync(id), Times.Once);
            _produtoRepositoryMock.Verify(x => x.AtualizarProdutoAsync(It.Is<Entity.Produto>(p =>
                p.Id == id &&
                p.Nome == produtoAtualizado.Nome &&
                p.TamanhoPreco == produtoAtualizado.TamanhoPreco &&
                p.TipoProduto == produtoAtualizado.TipoProduto &&
                p.Descricao == produtoAtualizado.Descricao &&
                p.DataCriacao == produtoAntigo.DataCriacao &&
                p.Ativo == produtoAtualizado.Ativo
            )), Times.Once);
        }

        [Fact]
        public async Task ObterProdutoAsync_ShouldReturnProductResponse()
        {
            // Arrange
            var id = "1";
            var date = DateTime.UtcNow;
            var produto = new Entity.Produto(id, "Produto 1", Tamanho, TipoProduto.Bebida, "Descrição do Produto 1", date, true);
            var produtoResponse = new ProdutoResponse
            {
                Id = id,
                Nome = "Produto 1",
                TamanhoPreco = Tamanho,
                TipoProduto = TipoProduto.Bebida,
                Descricao = "Descrição do Produto 1",
                DataCriacao = date,
                Ativo = true
            };

            _produtoRepositoryMock.Setup(x => x.ObterProdutoAsync(id)).ReturnsAsync(produto);

            // Act
            var result = await _produtoService.ObterProdutoAsync(id);

            // Assert
            Assert.Equal(produtoResponse.Id, result.Id);
            Assert.Equal(produtoResponse.Nome, result.Nome);
            Assert.Equal(produtoResponse.TamanhoPreco, result.TamanhoPreco);
            Assert.Equal(produtoResponse.TipoProduto, result.TipoProduto);
            Assert.Equal(produtoResponse.Descricao, result.Descricao);
            Assert.Equal(produtoResponse.DataCriacao, result.DataCriacao);
            Assert.Equal(produtoResponse.Ativo, result.Ativo);
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
            var result = await _produtoService.ObterTodosTiposProdutoAsync(tipoProduto, ativo, retornarTodos);

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

        [Fact]
        public async Task RemoverProdutoAsync_ShouldRemoveExistingProduct()
        {
            // Arrange
            var id = "1";
            var produto = new Entity.Produto(id, "Produto 1", Tamanho, TipoProduto.Lanche, "Descrição do Produto 1", DateTime.UtcNow, true);

            _produtoRepositoryMock.Setup(x => x.ObterProdutoAsync(id)).ReturnsAsync(produto);
            _produtoRepositoryMock.Setup(x => x.RemoverProdutoAsync(produto)).Verifiable();

            // Act
            await _produtoService.RemoverProdutoAsync(id);

            // Assert
            _produtoRepositoryMock.Verify(x => x.ObterProdutoAsync(id), Times.Once);
            _produtoRepositoryMock.Verify(x => x.RemoverProdutoAsync(produto), Times.Once);
        }
    }
}
