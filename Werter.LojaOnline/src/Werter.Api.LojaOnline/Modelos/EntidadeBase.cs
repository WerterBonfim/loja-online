namespace Werter.Api.LojaOnline.Modelos
{
    public abstract class EntidadeBase 
    {
        public Guid Id { get; set; }
        public DateTime DataHoraCadastro { get; set; } = DateTime.Now;
        public DateTime? DataHoraAlterado { get; set; }

        protected EntidadeBase()
        {
            Id = Guid.NewGuid();
        }
    }
}
