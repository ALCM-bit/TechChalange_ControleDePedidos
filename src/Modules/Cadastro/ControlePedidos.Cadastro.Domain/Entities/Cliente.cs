using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlePedidos.Cadastro.Domain.Entities
{
    public class Cliente
    {
        public Cliente(string email, string cpf, string nome)
        {
            Email = email;
            CPF = cpf;
            Nome = nome;
        }
        public string Email { get; set; }
        public string CPF { get; private set; }
        public string Nome { get; set; }
    }
}
