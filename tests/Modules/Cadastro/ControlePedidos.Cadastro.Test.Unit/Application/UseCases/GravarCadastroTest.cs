using ControlePedidos.Cadastro.Application.UseCases.GravarCadastro;
using ControlePedidos.Cadastro.Application.UseCases.ObterCadastro;
using ControlePedidos.Cadastro.Domain.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlePedidos.Cadastro.Test.Unit.Application.UseCases;

public class GravarCadastroTest
{

    private readonly GravarCadastroUseCase _useCase;
    private readonly Mock<ICadastroRepository> _cadastroRepositoryMock;

    public GravarCadastroTest()
    {
        _cadastroRepositoryMock = new();
        _useCase = new(_cadastroRepositoryMock.Object);
    }

    [Fact]
    public async Task CriarCadastroAsync_Should_Retornar_When_CadastroCriado()
    {
        var request = new GravarCadastroRequest()
        {
            CPF = "17993850002",
            Email = "felipe@gamil.com",
            Nome = "Felipe",
        };

        _cadastroRepositoryMock.Setup(x => x.CadastrarAsync(It.IsAny<Cadastro.Domain.Entities.Cadastro>()));

        var response = await _useCase.ExecuteAsync(request);

        _cadastroRepositoryMock.Verify(x => x.CadastrarAsync(It.IsAny<Cadastro.Domain.Entities.Cadastro>()), Times.Once);

        Assert.NotNull(response);
    }
}
