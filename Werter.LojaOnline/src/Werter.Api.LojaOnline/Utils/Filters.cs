using Exceptionless;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Werter.LojaOnline.Compartilhado.DomainObjects;

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
        private readonly ILogger _logger;

        public FiltroDeExceptionInterceptorAttribute(ILogger logger)
        {
            _logger = logger;
        }
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            context
                .Exception
                .ToExceptionless()
                .Submit();
            
            _logger.LogError(AppLogEvents.Error, context.Exception, null);

            context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            return base.OnExceptionAsync(context);
        }

        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
        }
    }
}