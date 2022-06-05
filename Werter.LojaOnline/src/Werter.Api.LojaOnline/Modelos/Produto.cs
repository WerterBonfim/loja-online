using Werter.Api.LojaOnline.Dados.Repositorio;
using Werter.Api.LojaOnline.Negocio.Requisitos;

namespace Werter.Api.LojaOnline.Modelos
{
    public class Produto : EntidadeBase, IAggregateRoot
    {
        public string? Nome { get; private set; }
        public string? Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public bool Ativo { get; private set; } = true;
        public int QuantidadeEmEstoque { get; private set; }

        // EF
        public Produto() {}
        public Produto(string nome, string descricao, decimal valor, int qtdEstoque)
        {
            Nome = nome;
            Valor = valor;
            Descricao = descricao;
            QuantidadeEmEstoque = qtdEstoque;
        }


        public void AtualizarProduto(RequisitosParaAlterarProduto requisitos)
        {
            if (!string.IsNullOrEmpty(requisitos.Nome))
                Nome = requisitos.Nome;
        }
    }
}
