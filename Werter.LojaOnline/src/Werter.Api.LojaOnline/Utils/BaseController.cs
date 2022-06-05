using Exceptionless;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Werter.Api.LojaOnline.Negocio.Exceptions;

namespace Werter.Api.LojaOnline.Utils
{
    [ApiController]
    //[ServiceFilter(typeof(ValidacaoDoModelAsync))]
    public class BaseController : Controller
    {
        private readonly ICollection<string> _erros = new List<string>();

        protected IActionResult RespostaPersonalizada(Exception exception, string mensagem)
        {
            AdicionarErro(mensagem);

            exception
                .ToExceptionless()
                .SetMessage(mensagem)
                .Submit();

            if (exception is LojaOnlineException)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return RespostaPersonalizada();
        }

        protected ActionResult RespostaPersonalizada(object? resultado = null)
        {
            if (OperacaoValida())
                return Ok(resultado);

            var erros = new Dictionary<string, string[]>
            {
                {"Mensagens", _erros.ToArray()}
            };

            return BadRequest(new ValidationProblemDetails(erros));
        }

        protected IActionResult RespostaPersonalizada(ValidationResult? validationResult)
        {
            if (validationResult != null)                
                foreach (var error in validationResult.Errors)
                    AdicionarErro(error.ErrorMessage);

            return RespostaPersonalizada();
        }

        protected IActionResult RespostaPersonalizada(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(x => x.Errors);
            foreach (var erro in erros)
                AdicionarErro(erro.ErrorMessage);

            return RespostaPersonalizada();
        }

        protected bool OperacaoValida()
        {
            return !_erros.Any();
        }

        protected void AdicionarErro(string erro)
        {
            _erros.Add(erro);
        }

        protected void LimparErros()
        {
            _erros.Clear();
        }
    }
}
