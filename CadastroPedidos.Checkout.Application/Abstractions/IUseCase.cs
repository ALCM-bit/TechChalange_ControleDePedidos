namespace CadastroPedidos.Checkout.Application.Abstractions;

public interface IUseCase<TRequest>
{
    Task ExecuteAsync(TRequest request);
}

public interface IUseCase<TRequest, TResponse>
{
    Task<TResponse> ExecuteAsync(TRequest request);
}
