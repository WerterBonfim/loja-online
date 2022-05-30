namespace Werter.Api.LojaOnline.Modelos
{
    public abstract class EntidadeBase 
    {
        public Guid Id { get; set; }
        public DateTime DataHoraCadastro { get; set; }
        public DateTime DataHoraAlterado { get; set; }

        public EntidadeBase()
        {
            Id = Guid.NewGuid();
        }
    }
}
