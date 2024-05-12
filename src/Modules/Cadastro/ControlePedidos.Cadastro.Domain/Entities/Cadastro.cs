using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlePedidos.Cadastro.Domain.ValueObjects;
using ControlePedidos.Common.Entities;
using ControlePedidos.Common.Exceptions;

namespace ControlePedidos.Cadastro.Domain.Entities
{
    public class Cadastro : Entity, IAggregationRoot
    {
        public Cadastro(string id, Email email, CPF cpf, string nome) : base(id)
        {
            Email = email;
            CPF = cpf;
            Nome = nome;

            Validate();
        }

        public Email Email { get; set; }
        public CPF CPF { get; private set; }
        public string Nome { get; set; }

        protected override void Validate()
        {
            if (!string.IsNullOrWhiteSpace(CPF.Numero))
            {
                var cpfOk = CPF.Validate();
                if (!cpfOk)
                {
                    throw new DomainException("O CPF informado é inválido.");
                }
            }

         
            if (!string.IsNullOrWhiteSpace(Email.Endereco))
            {
                var email = Email.Validate();
                if (!email)
                {
                    throw new DomainException("O e-mail informado é inválido.");
                }
            }

            if (string.IsNullOrWhiteSpace(Nome))
            {
                throw new DomainException("O nome é obrigatório.");
            }
        }
    }
}
