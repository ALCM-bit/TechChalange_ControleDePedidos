namespace ControlePedidos.Cadastro.Application.UseCases.GravarCadastro;

public class GravarCadastroRequest
{
    public string Email { get; set; }
    public string CPF { get; set; }
    public string Nome { get; set; }
}