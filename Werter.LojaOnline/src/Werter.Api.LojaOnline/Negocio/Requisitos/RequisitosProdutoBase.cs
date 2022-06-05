using FluentValidation;

namespace Werter.Api.LojaOnline.Negocio.Requisitos
{
    public abstract class RequisitosProdutoBase : RequisitosBase
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal? Valor { get; set; }
        public bool? Ativo { get; set; }
        public int? QuantidadeEmEstoque { get; set; }

       
    }
}
