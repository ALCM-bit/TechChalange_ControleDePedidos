using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlePedidos.Cadastro.Domain.ValueObjects;

namespace ControlePedidos.Cadastro.Domain.Entities
{
    public class Cliente
    {
        public Cliente()
        {
            
        }
        public Cliente(Email email, CPF cpf, string nome)
        {
            Email = email;
            CPF = cpf;
            Nome = nome;
        }
        
        public Email Email { get; set; }
        public CPF CPF { get; private set; }
        public string Nome { get; set; }
    }
}
