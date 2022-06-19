using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Werter.Api.LojaOnline.Utils;
using Werter.LojaOnline.Dominio.Modelos;

namespace Werter.Api.LojaOnline.Dados
{
    public class LojaOnlineContext : DbContext, IUnitOfWork
    {

        public DbSet<Produto>? Produtos { get; set; }

        public LojaOnlineContext(DbContextOptions<LojaOnlineContext> options) : base(options) {}


        public async Task<bool> Commit(CancellationToken cancellationToken)
            => await SaveChangesAsync(cancellationToken) > 0;





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();


            var todasAsPropriedades = modelBuilder.Model.GetEntityTypes()
                .SelectMany(x =>
                    x.GetProperties().Where(p => p.ClrType == typeof(string)));

            foreach (var property in todasAsPropriedades)
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LojaOnlineContext).Assembly);

        }

    }



    public class LojaOnlineContextFactory : IDesignTimeDbContextFactory<LojaOnlineContext>
    {
        public LojaOnlineContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<LojaOnlineContext>();
            builder
                .UseSqlServer("Server=localhost, 1433;" +
                              "Database=LojaOnline;" +
                              "User Id=sa;" +
                              "Password=!123Senha;" +
                              "Application Name=\"LojaOnline\";pooling=true;")
                .LogTo(Console.WriteLine)
                .EnableSensitiveDataLogging();

            return new LojaOnlineContext(builder.Options);
        }
    }
}
