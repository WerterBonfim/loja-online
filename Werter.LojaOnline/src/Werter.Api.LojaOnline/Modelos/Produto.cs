namespace Werter.Api.LojaOnline.Modelos
{
    public class Produto : EntidadeBase
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public bool Ativo { get; set; } = true;
        public int QuantidadeEmEstoque { get; set; }


        public Produto() {}
        public Produto(string nome, string descricao, decimal valor, int qtdEstoque)
        {
            Nome = nome;
            Valor = valor;
            Descricao = descricao;
            QuantidadeEmEstoque = qtdEstoque;
        }
    }
}
