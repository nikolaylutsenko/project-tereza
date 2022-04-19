using System.Linq.Expressions;
using FluentResults;

namespace Project.Tereza.Core.Interfaces
{
    public interface IService<T>
    {
        Task<Result<T>> GetAsync(Guid id);
        Task<Result<IEnumerable<T>>> GetAllAsync(Expression<Func<T, bool>>? properties = null);
        Task<Result> AddAsync(T item);
        Task<Result> UpdateAsync(T item);
        Task<Result> DeleteAsync(T item);
        Task<Result<(IEnumerable<T> items, int count)>> FindAsync(ISpecification<T>? specification = null, string? includes = null);
    }
}