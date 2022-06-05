using Exceptionless;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Werter.Api.LojaOnline.Dados;
using Werter.Api.LojaOnline.Negocio.Exceptions;

namespace Werter.Api.LojaOnline.Configuracoes
{
    public static class PrepararDb
    {

        public static void RodarMigrationInicial(IApplicationBuilder app)
        {

            try
            {
                using var scopo = app.ApplicationServices.CreateScope();
                RodarMigrations(scopo);
            }
            catch (Exception exception) 
            {
                exception
                    .ToExceptionless()
                    .SetMessage("Ocorreu um erro ao tentar rodar as migrations")
                    .Submit();
            }
        }


        private static void RodarMigrations(IServiceScope scopo)
        {
            var contexto = TentaObterDbContext(scopo);

            var bancoNaoExiste = !contexto.Database.GetService<IRelationalDatabaseCreator>().Exists();
            var temMigrationsPendendente = contexto.Database.GetPendingMigrations().Any();
            var temAlgoPendente = bancoNaoExiste || temMigrationsPendendente;

            if (!temAlgoPendente) return;


            contexto.Database.Migrate();
        }

        private static LojaOnlineContext TentaObterDbContext(IServiceScope scopo)
        {
            var servico = scopo.ServiceProvider.GetService<LojaOnlineContext>();
            if (servico == null)
                throw new InfraestruturaException("Contexto LojaOnline não esta definido");

            return servico;
        }

    }
}
