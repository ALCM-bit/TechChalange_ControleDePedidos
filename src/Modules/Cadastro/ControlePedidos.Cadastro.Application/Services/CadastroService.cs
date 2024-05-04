using ControlePedidos.Cadastro.Application.Abstractions;
using ControlePedidos.Cadastro.Application.DTO;
using ControlePedidos.Cadastro.Domain.Abstractions;
using Mapster;

namespace ControlePedidos.Cadastro.Application.Services
{
    public class CadastroService : ICadastroService
    {
        private readonly ICadastroRepository _cadastroRepository;

        public CadastroService(ICadastroRepository cadastroRepository)
        {
            _cadastroRepository = cadastroRepository;
        }

        public async Task<bool> CadastrarAsync(CadastroRequest cadastro)
        {
            var cadastroDomain = cadastro.Adapt<Domain.Entities.Cadastro>();
            await _cadastroRepository.CadastrarAsync(cadastroDomain);

            return true;
        }

        public async Task<CadastroResponse> ObterCadastroAsync(string Id)
        {
            var cadastro = await _cadastroRepository.ObterCadastroAsync(Id);

            var cadastroResponse = cadastro.Adapt<CadastroResponse>();

            return cadastroResponse;
        }

        public async Task<IList<CadastroResponse>> ObterTodosCadastrosAsync()
        {
            var cadastros = await _cadastroRepository.ObterTodosCadastrosAsync();

            var cadastroResponses = cadastros.Adapt<List<CadastroResponse>>();

            return cadastroResponses;
        }
    }
}
