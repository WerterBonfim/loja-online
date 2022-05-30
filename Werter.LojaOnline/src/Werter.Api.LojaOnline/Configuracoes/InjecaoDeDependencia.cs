using Werter.Api.LojaOnline.Dados;
using Werter.Api.LojaOnline.Utils;

namespace Werter.Api.LojaOnline.Configuracoes
{
    public static class ConfiguracaoDaInjecaoDeDependencia
    {
        public static void ConfigurarInjecaoDeDepencias(this IServiceCollection services)
        {
            services.AddScoped<ContextoRequisicao>();

            services.AddScoped<ProdutoContext>();
        }
    }
}
