using ControlePedidos.Cadastro.Application.Abstractions;
using ControlePedidos.Cadastro.Domain.Abstractions;
using ControlePedidos.Cadastro.Domain.ValueObjects;
using Mapster;

namespace ControlePedidos.Cadastro.Application.UseCases.ObterCadastro;

public class ObterCadastroUseCase : IUseCase<ObterCadastroRequest, ObterCadastroResponse>
{
    private readonly ICadastroRepository _cadastroRepository;

    public ObterCadastroUseCase(ICadastroRepository cadastroRepository)
    {
        _cadastroRepository = cadastroRepository;
    }
    public async Task<ObterCadastroResponse> ExecuteAsync(ObterCadastroRequest request)
    {
        var cadastro = await _cadastroRepository.ObterCadastroAsync(request.CPF.Trim().Replace(".", "").Replace("-", ""));

        if (cadastro is null)
        {
            return null;
        }

        var response = cadastro.Adapt<ObterCadastroResponse>();
        return response;
    }
}