using Exceptionless;

namespace Werter.Api.LojaOnline.Configuracoes
{
    public static class ConfiguracaoDoExceptionLess
    {
        public static void AdicionarConfiguracaoDoExceptionLess(this IServiceCollection services, ConfigurationManager configuration)
        {
            //var key = configuration["ExceptionlessKey"];
            //services.AddExceptionless(key);
            services.AddExceptionless(x =>
            {
                x.ApiKey = "5Nf6oPNMW3XQJFO0Hl9wFAFXW4e2Ge6DAfmFSEDf";
                x.ServerUrl = "http://localhost:5000";
            });
        }

        public static void UsarExceptionLess(this IApplicationBuilder app)
        {
            app.UseExceptionless();
        }
    }
}
