using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlePedidos.Cadastro.Application.Services
{
    public interface IClienteValidationService
    {
        bool ValidarCPF(string cpf);
        bool ValidarEmail(string email);
    }
}