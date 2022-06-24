using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria); // p => p.ProductTypeId == id
            }

            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy); 
            }

            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending); 
            }

            //The paging operators need to come after any sorting and filtering operators
            // so that the filtering and sorting is applied to the entire set of results.
            if(spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            // Takes our two include statements and aggregate them and passes into our query
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            var secondaryResult = spec.StringIncludes.Aggregate(query, (current, include) => current.Include(include));

            return secondaryResult;
        }

    }
}