namespace ControlePedidos.Produto.Domain.Entities
{
    public class Produto : Entity, IAggregationRoot
    {
        public string Nome { get; private set; }
        public decimal Preco { get; private set; }
        public TipoProduto TipoProduto { get; private set; }
        public string Descricao { get; private set; }
        public bool Ativo { get; set; }

        public Produto(string id, string nome, decimal preco, TipoProduto tipoProduto, string descricao, DateTime dataCriacao, bool ativo) : base(id, dataCriacao)
        {
            Nome = nome;
            Preco = preco;
            TipoProduto = tipoProduto;
            Descricao = descricao;
            Ativo = ativo; 

            Validate();
        }

        protected override void Validate() { }

        // Definir os métodos de validação

        public void Criar()
        {
            if (string.IsNullOrEmpty(Nome))
            {
                throw new DomainNotificationException("Nome do produto é obrigatório");
            }

            if (Preco <= 0)
            {
                throw new DomainNotificationException("Preço do produto é obrigatório");
            }

            if (TipoProduto == null)
            {
                throw new DomainNotificationException("Tipo do produto é obrigatório");
            }

            if (string.IsNullOrEmpty(Descricao))
            {
                throw new DomainNotificationException("Descrição do produto é obrigatório");
            }

            Validate();
        }
    }
}
