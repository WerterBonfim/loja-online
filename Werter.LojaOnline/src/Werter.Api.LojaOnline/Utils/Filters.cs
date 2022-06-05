using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Werter.Api.LojaOnline.Negocio.Exceptions;

namespace Werter.Api.LojaOnline.Utils
{
    public class ValidacaoDoModelAsync : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
                context.Result = new BadRequestObjectResult(context.ModelState);


            await next();

        }
    }

    public class FiltroDeExceptionInterceptorAttribute : ExceptionFilterAttribute, IAsyncExceptionFilter
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.GetType() == typeof(InfraestruturaException))            
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            

            return base.OnExceptionAsync(context);
        }

        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
        }
    }
}
