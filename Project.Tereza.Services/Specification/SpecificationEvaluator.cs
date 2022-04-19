using Project.Tereza.Core.Interfaces;

namespace MDEvents.Services.Specification
{
    public static class SpecificationEvaluator<T> where T : class
    {
        public static IQueryable<T>? GetQuery(IQueryable<T>? inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery;

            if (specification?.Criteria != null && query != null)
            {
                query = query.Where(specification.Criteria);
            }

            if (specification?.Skip != null && specification?.Take != null)
            {
                query = query?
                    .Skip(specification.Skip)
                    .Take(specification.Take);
            }

            return query;
        }

        public static (IQueryable<T>, int) GetTupleQueryInt(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery;
            var count = 0;

            if (specification?.Criteria != null && query != null)
            {
                query = query.Where(specification.Criteria);
            }

            count = query.Count();

            if (specification?.Skip != null && specification?.Take != null)
            {
                query = query
                    .Skip(specification.Skip)
                    .Take(specification.Take);
            }

            return (query, count);
        }
    }
}