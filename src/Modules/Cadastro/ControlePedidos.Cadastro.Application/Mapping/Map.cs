using ControlePedidos.Cadastro.Application.UseCases.ObterCadastro;
using ControlePedidos.Common.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlePedidos.Cadastro.Application.Mapping;

public class Map : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Domain.Entities.Cadastro, ObterCadastroResponse>()
            .Map(dest => dest.CPF, src => src.CPF.Numero)
            .Map(dest => dest.Email, src => src.Email.Endereco);
    }
}
