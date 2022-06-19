using Microsoft.EntityFrameworkCore;
using Werter.Api.LojaOnline.Dados;
using Werter.Api.LojaOnline.Negocio.Servicos.Produtos;
using Werter.Api.LojaOnline.Utils;

namespace Werter.Api.LojaOnline.Configuracoes
{
    public static class ConfiguracaoDaparametroscaoDeDependencia
    {
        public static void AdicionarInjecaoDeDependencia(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<ValidacaoDoModelAsync>();
    
            services.AddDbContext<LojaOnlineContext>(options =>
                options
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution)
                .EnableSensitiveDataLogging()
                .LogTo(Console.Write, LogLevel.Information)
            );


            services.AddScoped<FiltroDeExceptionInterceptorAttribute>(x 
                => new FiltroDeExceptionInterceptorAttribute(x.GetService<ILogger>()!));
            services.AddScoped<IProdutoRepositorio, ProdutosRepositorio>();
            services.AddScoped<IServicoProdutos, ServicoParaLidarComProdutos>();

        }
    }
}
