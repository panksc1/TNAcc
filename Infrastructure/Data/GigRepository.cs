using Core.Entities;
using Core.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GigRepository : IGigRepository
    {
        private readonly AccContext _context;
        public GigRepository(AccContext context)
        {
            this._context = context;
        }

        public async Task<IReadOnlyList<Entity>> GetEntitiesAsync()
        {
            return await this._context.Entities.ToListAsync();
        }

        public async Task<IReadOnlyList<Gig>> GetGigsAsync()
        {
            return await this._context.Gigs
                .Include(g => g.Venue)
                .ToListAsync();
        }

        public async Task<Payable> GetPayableByIdAsync(int id)
        {
            return await this._context.Payables
                .Include(p => p.Gig)
                .Include(p => p.Entity)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Payable>> GetPayablesAsync()
        {
            return await this._context.Payables
                .Include(p => p.Gig)
                .Include(p => p.Entity)
                .ToListAsync();
        }

        public async Task<Receivable> GetReceivableByIdAsync(int id)
        {
            return await this._context.Receivables
                .Include(r => r.Gig)
                .Include(r => r.Entity)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IReadOnlyList<Receivable>> GetReceivablesAsync()
        {
            return await this._context.Receivables
                .Include(r => r.Gig)
                .Include(r => r.Entity)
                .ToListAsync();
        }

        public async Task<Venue> GetVenueByIdAsync(int id) {
            return await this._context.Venues
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IReadOnlyList<Venue>> GetVenuesAsync() {
            return await this._context.Venues
                .OrderBy(v => v.Name)
                .ToListAsync();
        }
    }
}