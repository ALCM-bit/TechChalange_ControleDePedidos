namespace ControlePedidos.Cadastro.Application.UseCases.ObterCadastro;

public class ObterCadastroRequest
{
    public ObterCadastroRequest(string cpf)
    {
        CPF = cpf.Trim().Replace(".", "").Replace("-", "");
    }
    public string CPF { get; set; }
}