using FluentValidation;
using Werter.Api.LojaOnline.Utils;

namespace Werter.Api.LojaOnline.Negocio.Requisitos
{
    public class RequisitosParaAlterarProduto : RequisitosProdutoBase
    {
        [NaoDeveAtualizar]
        public Guid Id { get; set; }

        public override bool EstaValido()
        {
            ResultadoValidacao = new ValidacaoDeProduto().Validate(this);
            return ResultadoValidacao.IsValid;
        }

        private sealed class ValidacaoDeProduto : AbstractValidator<RequisitosParaAlterarProduto>
        {
            public ValidacaoDeProduto()
            {
                When(x => !string.IsNullOrEmpty(x.Nome), () =>
                {
                    RuleFor(x => x.Nome)
                        .MaximumLength(100).WithMessage($"O campo {nameof(Nome)} não pode conter mais de 100 caracteres");

                });

                When(x => !string.IsNullOrEmpty(x.Descricao), () =>
                {
                    RuleFor(x => x.Descricao)
                        .MaximumLength(300).WithMessage($"O campo {nameof(Descricao)} não pode conter mais de 300 caracteres");

                });

                When(x => x.Valor != null, () =>
                {
                    RuleFor(x => x.Valor)
                        .NotNull().WithMessage($"O campo {nameof(Valor)} não pode ser nulo");
                });

                When(x => x.Ativo != null, () =>
                {
                    RuleFor(x => x.Ativo)
                        .NotNull().WithMessage($"O campo {nameof(Ativo)} não pode ser nulo");

                });

                When(x => x.QuantidadeEmEstoque != null, () =>
                {
                    RuleFor(x => x.QuantidadeEmEstoque)
                        .NotNull().WithMessage($"O campo {nameof(QuantidadeEmEstoque)} não pode ser nulo")
                        .Must(x => x >= 0).WithMessage($"O campo {nameof(QuantidadeEmEstoque)} não pode ter uma quantidade negativa");

                });

            }
        }

    }
}
