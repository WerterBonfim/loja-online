using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Werter.Api.LojaOnline.Dados.Repositorio;
using Werter.LojaOnline.Dominio.Modelos;

namespace Werter.Api.LojaOnline.Dados
{
    public sealed class MapeamentoDaEntidadeDeProdutos : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(c => c.QuantidadeEmEstoque)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.Ativo)
                .IsRequired();

            builder.Property(c => c.Valor)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(c => c.DataHoraCadastro)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(c => c.DataHoraAlterado)                
                .HasColumnType("datetime");

            builder.ToTable("Produtos");
        }
    }

    public interface IProdutoRepositorio : IRepositorio<Produto>
    {
        Task<bool> SalvarAsync(CancellationToken cancellation);
    }

    public sealed class ProdutosRepositorio : RepositorioBase<Produto>, IProdutoRepositorio
    {
        private readonly LojaOnlineContext _lojaOnlineContext;

        public ProdutosRepositorio(LojaOnlineContext contexto) : base(contexto)
        {
            _lojaOnlineContext = contexto;
        }

        public async Task<bool> SalvarAsync(CancellationToken cancellation)
            => await _lojaOnlineContext.SaveChangesAsync(cancellation) > 0;

    }
}
