using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroPedidos.Produto.Application.Abstractions;

public interface IUseCase<TRequest, TResponse>
{
    public Task<TResponse> ExecuteAsync(TRequest request);
}

public interface IUseCase<TRequest>
{
    public Task ExecuteAsync(TRequest request);
}
