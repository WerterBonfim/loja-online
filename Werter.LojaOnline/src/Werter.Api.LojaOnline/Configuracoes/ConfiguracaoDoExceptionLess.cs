using Exceptionless;

namespace Werter.Api.LojaOnline.Configuracoes
{
    public static class ConfiguracaoDoExceptionLess
    {
        public static void AdicionarConfiguracaoDoExceptionLess(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddExceptionless(x =>
            {
                x.ApiKey = "YevJIwCdeSKGFPe9tBvFnLN9Juy5W5Tmcau8JkXf";
                x.ServerUrl = "http://localhost:5000";
            });
        }

        public static void UsarExceptionLess(this IApplicationBuilder app)
        {
            app.UseExceptionless();
        }
    }
}
