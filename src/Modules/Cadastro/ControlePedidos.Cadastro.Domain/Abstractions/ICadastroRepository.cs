namespace ControlePedidos.Cadastro.Domain.Abstractions
{
    public interface ICadastroRepository
    {
        Task<Entities.Cadastro> ObterCadastroAsync(string cpf);
        Task<IList<Entities.Cadastro>> ObterTodosCadastrosAsync();
        Task CadastrarAsync(Entities.Cadastro cadastro);
    }
}