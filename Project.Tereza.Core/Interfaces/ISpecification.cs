using System.Linq.Expressions;

namespace Project.Tereza.Core.Interfaces
{
    public interface ISpecification<T>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        Expression<Func<T, bool>>? Criteria { get; }
    }
}
