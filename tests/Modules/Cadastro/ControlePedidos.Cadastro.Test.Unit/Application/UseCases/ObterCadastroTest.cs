using ControlePedidos.Cadastro.Application.UseCases.ObterCadastro;
using ControlePedidos.Cadastro.Domain.Abstractions;
using ControlePedidos.Cadastro.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlePedidos.Cadastro.Test.Unit.Application.UseCases;

public class ObterCadastroTest
{
    private readonly ObterCadastroUseCase _useCase;
    private readonly Mock<ICadastroRepository> _cadastroRepositoryMock;
    public ObterCadastroTest()
    {
        _cadastroRepositoryMock = new();
        _useCase = new(_cadastroRepositoryMock.Object);
    }

    private static Domain.Entities.Cadastro CriarCadastro()
    {
        string id = Guid.NewGuid().ToString();
        string nome = "Felipe";
        string email = "felipe@gmail.com";
        string cpf = "17993850002";

        return new Domain.Entities.Cadastro(
            id,
            DateTime.UtcNow,
            new Domain.ValueObjects.Email(email),
            new Domain.ValueObjects.CPF(cpf),
            nome);
    }

    [Fact]
    public async Task ExecuteAsync_Should_RetornarCadastro_When_CadastroEncontrado()
    {
        // Arrange
        var cadastro = CriarCadastro();

        _cadastroRepositoryMock.Setup(x => x.ObterCadastroAsync(cadastro.CPF.Numero!)).ReturnsAsync(cadastro);

        // Act
        var response = await _useCase.ExecuteAsync(new ObterCadastroRequest() { CPF = cadastro.CPF.Numero });

        // Assert
        _cadastroRepositoryMock.Verify(x => x.ObterCadastroAsync(cadastro.CPF.Numero!), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_Should_RetornarNulo_When_PedidoNaoEncontrado()
    {
        // Arrange
        string cpf = "test";
        _cadastroRepositoryMock.Setup(x => x.ObterCadastroAsync(It.IsAny<string>())).ReturnsAsync(() => null!);

        // Act
        var response = await _useCase.ExecuteAsync(new ObterCadastroRequest() { CPF = cpf });

        // Assert
        _cadastroRepositoryMock.Verify(x => x.ObterCadastroAsync(cpf), Times.Once);
        Assert.Null(response);
    }
}
