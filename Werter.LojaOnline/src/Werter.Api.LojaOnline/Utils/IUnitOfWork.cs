namespace Werter.Api.LojaOnline.Utils
{
    public interface IUnitOfWork
    {
        Task<bool> Commit(CancellationToken cancellationToken);
    }
}
