using FluentValidation;

namespace Werter.Api.LojaOnline.Negocio.Requisitos
{
    public sealed class RequisitosParaCadastrarProduto : RequisitosProdutoBase
    {
        public override bool EstaValido()
        {
            ResultadoValidacao = new ValidacaoDeProduto().Validate(this);
            return ResultadoValidacao.IsValid;
        }

        private sealed class ValidacaoDeProduto : AbstractValidator<RequisitosParaCadastrarProduto>
        {
            public ValidacaoDeProduto()
            {
                RuleFor(x => x.Nome)
                    .Must(x => !string.IsNullOrEmpty(x)).WithMessage($"O campo {nameof(Nome)} nome do produto é um campo obrigátorio")
                    .MaximumLength(100).WithMessage($"O campo {nameof(Nome)} não pode conter mais de 100 caracteres");


                RuleFor(x => x.Descricao)
                    .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Nome do produto é um campo obrigátorio")
                    .MaximumLength(300).WithMessage($"O campo {nameof(Descricao)} não pode conter mais de 300 caracteres");

                RuleFor(x => x.Valor)
                    .NotNull().WithMessage($"O campo {nameof(Valor)} não pode ser nulo");

                RuleFor(x => x.Ativo)
                    .NotNull().WithMessage($"O campo {nameof(Ativo)} não pode ser nulo");

                RuleFor(x => x.QuantidadeEmEstoque)
                    .NotNull().WithMessage($"O campo {nameof(QuantidadeEmEstoque)} não pode ser nulo")
                    .Must(x => x >= 0).WithMessage($"O campo {nameof(QuantidadeEmEstoque)} não pode ter uma quantidade negativa");

            }
        }
    }
}
