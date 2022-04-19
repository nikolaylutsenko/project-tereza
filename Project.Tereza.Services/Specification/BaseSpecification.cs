using System.Linq.Expressions;
using Project.Tereza.Core.Interfaces;

namespace MDEvents.Services.Specification
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        protected BaseSpecification()
        {
        }

        protected BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public int Skip { get; set; }
        public int Take { get; set; }
        public Expression<Func<T, bool>>? Criteria { get; set; }
    }
}