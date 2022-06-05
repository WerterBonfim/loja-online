using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;
using Werter.Api.LojaOnline.Utils;

namespace Werter.Api.LojaOnline.Negocio.Requisitos
{
    public abstract class RequisitosBase
    {
        [SwaggerIgnore]
        [BindNever]// Faz o swagger ignorar essa propriedade
        public ValidationResult? ResultadoValidacao { get; set; }

        public virtual bool EstaValido() => throw new NotImplementedException();
    }
}
