using System.Linq;
using MDEvents.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MDEvents.Services.Specification
{
    public static class SpecificationEvaluator<TEntity> where TEntity : class
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
        {
            var query = inputQuery;

            if (specification?.Criteria != null && query != null)
            {
                query = query.Where(specification.Criteria);
            }

            if (specification.Skip != null && specification.Take != null)
            {
                query = query
                    .Skip(specification.Skip)
                    .Take(specification.Take);
            }

            return query;
        }
        
        public static (IQueryable<TEntity>, int) GetTupleQueryInt(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
        {
            var query = inputQuery;
            var count = 0;

            if (specification?.Criteria != null && query != null)
            {
                query = query.Where(specification.Criteria);
            }

            count = query.Count();

            if (specification.Skip != null && specification.Take != null)
            {
                query = query
                    .Skip(specification.Skip)
                    .Take(specification.Take);
            }

            return (query, count);
        }
    }
}