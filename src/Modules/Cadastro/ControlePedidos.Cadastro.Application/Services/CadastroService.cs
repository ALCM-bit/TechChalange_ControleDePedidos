using ControlePedidos.Cadastro.Application.Abstractions;
using ControlePedidos.Cadastro.Application.DTO;
using ControlePedidos.Cadastro.Domain.Abstractions;
using ControlePedidos.Cadastro.Domain.ValueObjects;

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
            var cadastroDomain = new Domain.Entities.Cadastro(null, 
                                                              new Email(cadastro.Email),
                                                              new CPF(cadastro.CPF),
                                                              cadastro.Nome);

            await _cadastroRepository.CadastrarAsync(cadastroDomain);

            return true;
        }

        public async Task<CadastroResponse> ObterCadastroAsync(string cpf)
        {
            if(cpf is not null)
            {
                cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            }

            var cadastro = await _cadastroRepository.ObterCadastroAsync(cpf);

            if(cadastro is null)
            {
                return null!;
            }

            var cadastroResponse = new CadastroResponse 
            { 
                CPF = cadastro.CPF.Numero,
                Nome = cadastro.Nome,
                Email = cadastro.Email.Endereco,
            };

            return cadastroResponse;
        }
    }
}
