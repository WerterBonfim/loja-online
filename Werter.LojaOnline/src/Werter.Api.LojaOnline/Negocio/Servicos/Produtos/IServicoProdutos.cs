using FluentValidation.Results;
using Werter.Api.LojaOnline.Negocio.Requisitos;

namespace Werter.Api.LojaOnline.Negocio.Servicos.Produtos
{
    public interface IServicoProdutos
    {
        Task<ValidationResult?> LidarCom(RequisitosParaAlterarProduto requisitos, CancellationToken cancellationToken);
        Task<ValidationResult?> LidarCom(RequisitosParaCadastrarProduto requisitos, CancellationToken cancellationToken);
        Task<ValidationResult?> LidarCom(RequisitosParaDeletarProduto requisitos, CancellationToken cancellationToken);
    }
}