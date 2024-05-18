using ControlePedidos.Cadastro.Application.DTO;

namespace ControlePedidos.Cadastro.Application.Abstractions
{
    public interface ICadastroService
    {
        Task<bool> CadastrarAsync(CadastroRequest cadastro);
        Task<CadastroResponse> ObterCadastroAsync(string cpf);
    }
}
