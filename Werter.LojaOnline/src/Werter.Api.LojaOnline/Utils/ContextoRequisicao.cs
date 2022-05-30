namespace Werter.Api.LojaOnline.Utils
{
    public class ContextoRequisicao
    {
        public Guid CorrelationId { get; set; }
        public CancellationToken CancellationToken { get; set; }

        public ContextoRequisicao()
        {
            CorrelationId = Guid.NewGuid();
            CancellationToken = new CancellationToken();
        }
    }
}
