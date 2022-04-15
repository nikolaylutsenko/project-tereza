using System.Linq.Expressions;
using FluentResults;

namespace Project.Tereza.Core.Interfaces
{
    public interface IService<T>
    {
        Task<T> GetAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> properties = null);
        Task<Result> AddAsync(T item);
        Task<Result> UpdateAsync(T item);
        Task<Result> DeleteAsync(T item);
    }
}