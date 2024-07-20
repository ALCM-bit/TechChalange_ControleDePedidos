using ControlePedidos.Cadastro.Domain.ValueObjects;
using ControlePedidos.Common.Entities;
using ControlePedidos.Common.Exceptions;

namespace ControlePedidos.Cadastro.Domain.Entities
{
    public class Cadastro : Entity, IAggregationRoot
    {
        public Cadastro(string id,DateTime dataDeCriacao, Email email, CPF cpf, string nome) : base(id, dataDeCriacao)
        {
            Email = email;
            CPF = cpf;
            Nome = nome;

            Validate();
        }

        public Email Email { get; set; }
        public CPF CPF { get; private set; }
        public string Nome { get; private set; }

        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Nome))
            {
                throw new DomainNotificationException("O nome ? obrigat?rio.");
            }
        }
    }
}
