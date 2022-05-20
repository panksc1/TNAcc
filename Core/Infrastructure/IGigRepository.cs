using Core.Entities;

namespace Core.Infrastructure
{
    public interface IGigRepository
    {
        Task<Payable> GetPayableByIdAsync(int id);
        Task<IReadOnlyList<Payable>> GetPayablesAsync();
        
        Task<Receivable> GetReceivableByIdAsync(int id);
        Task<IReadOnlyList<Receivable>> GetReceivablesAsync();

        Task<IReadOnlyList<Gig>> GetGigsAsync();
        Task<IReadOnlyList<Entity>> GetEntitiesAsync();
    }
}