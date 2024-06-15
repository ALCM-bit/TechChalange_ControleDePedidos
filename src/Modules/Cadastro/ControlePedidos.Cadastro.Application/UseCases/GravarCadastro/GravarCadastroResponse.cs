namespace ControlePedidos.Cadastro.Application.UseCases.GravarCadastro;

public class GravarCadastroResponse
{
   public GravarCadastroResponse(bool gravado)
   {
      Gravado = gravado;
   }
   public bool Gravado { get; set; }
}