﻿namespace ControlePedidos.Produto.Domain.Entities
{
    public class Produto : Entity, IAggregationRoot
    {
        public string Nome { get; private set; }
        public decimal Preco { get; private set; }
        public TipoProduto TipoProduto { get; private set; }
        public string Descricao { get; private set; }

        public Produto(string id, string nome, decimal preco, TipoProduto tipoProduto, string descricao) : base(id)
        {
            Nome = nome;
            Preco = preco;
            TipoProduto = tipoProduto;
            Descricao = descricao;

            Validate();
        }

        protected override void Validate() { }

        // Definir os métodos de validação

        public void Criar()
        {
            if (string.IsNullOrEmpty(Nome))
            {
                throw new DomainException("Nome do produto é obrigatório");
            }

            if (Preco <= 0)
            {
                throw new DomainException("Preço do produto é obrigatório");
            }

            if (TipoProduto == null)
            {
                throw new DomainException("Tipo do produto é obrigatório");
            }

            if (string.IsNullOrEmpty(Descricao))
            {
                throw new DomainException("Descrição do produto é obrigatório");
            }

            Validate();
        }
    }
}
