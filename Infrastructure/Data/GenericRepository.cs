using System.Reflection;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AccContext _context;
        public GenericRepository(AccContext context)
        {
            this._context = context;
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await this._context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            // Apply Specification returns the IQueryable and the First or Default is where
            // the query is actually executed 
            return await ApplySpecification(spec).FirstOrDefaultAsync();

        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await this._context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> AddEntityAsync(T entity)
        {
            var tracked = await this._context.Set<T>().AddAsync(entity);
            await this._context.SaveChangesAsync();
            return await this._context.Set<T>().FirstOrDefaultAsync(t => t.Id == tracked.Entity.Id);
        }

        public async Task<T> DeleteEntityAsync(int id)
        {
            var deleted = await this._context.Set<T>().FindAsync(id);
            if(deleted == null) 
            {
                return null;
            }

            this._context.Set<T>().Remove(deleted);
            await this._context.SaveChangesAsync();

            return deleted;
        }

        public async Task<T> UpdateEntityAsync(T entity)
        {
            var original = await this._context.Set<T>().FirstOrDefaultAsync(e => e.Id == entity.Id);
            if(original == null)
            {
                return null;
            }

            var sourceProps = typeof (T).GetProperties().Where(x => x.CanRead).ToList();
            var destProps = typeof (T).GetProperties().Where(x => x.CanWrite).ToList();

            foreach(var sourceProp in sourceProps)
            {
                if(destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    if(p.CanWrite) {
                        p.SetValue(original, sourceProp.GetValue(entity, null), null);
                    }
                }
            }

            this._context.Set<T>().Update(original);
            await this._context.SaveChangesAsync();

            var updated = await this._context.Set<T>().FirstOrDefaultAsync(e => e.Id == entity.Id);

            if(updated == null)
            {
                return null;
            }
            return updated;
        }

         private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(this._context.Set<T>().AsQueryable(), spec);
        }
    }
}