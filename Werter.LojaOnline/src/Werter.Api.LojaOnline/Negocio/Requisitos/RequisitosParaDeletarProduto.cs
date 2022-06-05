using FluentValidation;

namespace Werter.Api.LojaOnline.Negocio.Requisitos
{
    public class RequisitosParaDeletarProduto : RequisitosBase
    {
        public Guid Id { get; set; }

        public override bool EstaValido()
        {
            base.EstaValido();

            var resultado = new Validacao().Validate(this);
            if (ResultadoValidacao == null)
                ResultadoValidacao = resultado;

            ResultadoValidacao.Errors.AddRange(resultado.Errors);
            return ResultadoValidacao.IsValid;
        }

        private sealed class Validacao : AbstractValidator<RequisitosParaDeletarProduto>
        {
            public Validacao()
            {
                RuleFor(x => x.Id)
                    .NotEmpty()
                    .WithMessage("Id não pode ser nulo");
            }
        }
    }
}
