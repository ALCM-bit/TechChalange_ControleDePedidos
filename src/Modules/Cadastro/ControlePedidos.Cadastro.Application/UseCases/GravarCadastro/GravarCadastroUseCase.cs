using ControlePedidos.Cadastro.Application.Abstractions;
using ControlePedidos.Cadastro.Application.DTO;
using ControlePedidos.Cadastro.Domain.Abstractions;
using ControlePedidos.Cadastro.Domain.ValueObjects;
using Mapster;

namespace ControlePedidos.Cadastro.Application.UseCases.GravarCadastro;

public class GravarCadastroUseCase : IUseCase<GravarCadastroRequest>
{
    private readonly ICadastroRepository _cadastroRepository;

    public GravarCadastroUseCase(ICadastroRepository cadastroRepository)
    {
        _cadastroRepository = cadastroRepository;
    }

    public async Task ExecuteAsync(GravarCadastroRequest request)
    {
        var cadastroDomain = new Domain.Entities.Cadastro(null,
            DateTime.UtcNow,
            new Email(request.Email),
            new CPF(request.CPF),
            request.Nome);

        await _cadastroRepository.CadastrarAsync(cadastroDomain);
    }
}