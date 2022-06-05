using FluentValidation.Results;
using Werter.Api.LojaOnline.Negocio.Requisitos;

namespace Werter.Api.LojaOnline.Negocio.Servicos
{
    public interface IServicoBase<T> where T : RequisitosBase
    {
        Task<ValidationResult> LidarCom(T requisitos, CancellationToken cancellationToken);
            
    }
}
