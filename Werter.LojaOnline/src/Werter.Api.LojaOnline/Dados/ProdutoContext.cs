using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Werter.Api.LojaOnline.Utils;

namespace Werter.Api.LojaOnline.Dados
{
    public class ProdutoContext : DbContext, IUnitOfWork
    {
        private readonly ILogger<ProdutoContext> _logger;
        private readonly ContextoRequisicao _contextoRequisicao;

        public ProdutoContext(
            DbContextOptions<ProdutoContext> options,
            ILogger<ProdutoContext> logger,
            ContextoRequisicao contextoRequisicao
            ) : base(options)
        {
            _logger = logger;
            _contextoRequisicao = contextoRequisicao;
        }

        public async Task<bool> Commit(CancellationToken cancellationToken)
            => await SaveChangesAsync(cancellationToken) > 0;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _logger.LogInformation("Ignorando ValidationResult como Model");
            modelBuilder.Ignore<ValidationResult>();

            _logger.LogInformation("Aplicando convensoes definidas (varchar(100))");

            var todasAsPropriedades = modelBuilder.Model.GetEntityTypes()
                .SelectMany(x =>
                    x.GetProperties().Where(p => p.ClrType == typeof(string)));

            foreach (var property in todasAsPropriedades)
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProdutoContext).Assembly);

        }

    }
}
