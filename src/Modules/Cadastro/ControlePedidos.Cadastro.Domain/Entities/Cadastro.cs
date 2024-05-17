using ControlePedidos.Cadastro.Domain.ValueObjects;
using ControlePedidos.Common.Entities;
using ControlePedidos.Common.Exceptions;

namespace ControlePedidos.Cadastro.Domain.Entities
{
    public class Cadastro : Entity, IAggregationRoot
    {
        public Cadastro(string id, Email email, CPF cpf, string nome) : base(id, DateTime.UtcNow)
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
            if (string.IsNullOrWhiteSpace(Nome))
            {
                throw new DomainException("O nome é obrigatório.");
            }
        }
    }
}
