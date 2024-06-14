namespace CadastroPedidos.Pedido.Application.Abstractions;

public interface IUseCase<TRequest,TResponse>
{
    Task<TResponse> ExecuteAsync(TRequest request);
}
