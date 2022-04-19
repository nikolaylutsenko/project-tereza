using System.Linq.Expressions;
using FluentResults;
using MDEvents.Services.Specification;
using Microsoft.EntityFrameworkCore;
using Project.Tereza.Core.Entities;
using Project.Tereza.Core.Interfaces;
using Project.Tereza.Infrastructure.DBContext;
using Serilog;

namespace Project.Tereza.Services
{
    public class Service<T> : IService<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly ILogger _logger;

        public Service(AppDbContext context, ILogger logger)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _logger = logger;
        }

        public async Task<Result<T>> GetAsync(Guid id)
        {
            var item = await _dbSet.FindAsync(id);

            if (item is null)
            {
                _logger.Error($"Item with id {id} not found");
                return Result.Fail($"Item with id {id} not found");
            }

            return Result.Ok(item);
        }

        public async Task<Result<IEnumerable<T>>> GetAllAsync(Expression<Func<T, bool>>? properties = null)
        {
            IEnumerable<T> result = Enumerable.Empty<T>();

            try
            {
                result = await _dbSet.Where(properties ?? (x => true)).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception has been appeared in {nameof(GetAllAsync)} - {ex.Message}");
                return Result.Fail(ex.Message);
            }

            return Result.Ok(result);
        }

        public async Task<Result> AddAsync(T item)
        {
            var result = Result.Fail(string.Empty);

            if (item == null)
            {
                _logger.Warning($"Try add null item {nameof(item)}");
                return Result.Fail($"You provide null item");
            }

            try
            {
                await _dbSet.AddAsync(item);
                await _context.SaveChangesAsync();

                result = Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception been taking exception while try commit changes - {ex.Message}");
                result = Result.Fail(ex.Message);
            }

            return result;
        }

        public async Task<Result> UpdateAsync(T item)
        {
            var result = Result.Fail(string.Empty);

            if (item == null)
            {
                _logger.Error($"Try to {nameof(UpdateAsync)} call with {nameof(item)} but provide null");
                return Result.Fail("You provide null item");
            }

            try
            {
                _dbSet.Update(item);
                await _context.SaveChangesAsync();

                result = Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.Error($"Execution {nameof(UpdateAsync)} thrown exception while trying commit changes - {ex.Message}");
                result = Result.Fail(ex.Message);
            }

            return result;
        }

        public async Task<Result> DeleteAsync(T item)
        {
            var result = Result.Fail(string.Empty);

            if (item == null)
            {
                _logger.Error($"Try to {nameof(DeleteAsync)} call with {nameof(item)} but provide null");
                return Result.Fail("You provide null item");
            }

            try
            {
                _dbSet.Remove(item);
                await _context.SaveChangesAsync();

                result = Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.Error($"Execution {nameof(DeleteAsync)} been taking exception while try commit changes - {ex.Message}");
                result = Result.Fail(ex.Message);
            }

            return result;
        }

        public async Task<Result<(IEnumerable<T> items, int count)>> FindAsync(ISpecification<T>? specification = null, string? includes = null)
        {
            (IQueryable<T>? items, int count) tuple = (null, 0);

            try
            {
                IQueryable<T> items = _context.Set<T>();

                if (!string.IsNullOrEmpty(includes))
                {
                    var listOfIncludes = includes.Split(';').ToList();
                    foreach (var includeItem in listOfIncludes)
                    {
                        items = items.Include(includeItem);
                    }

                    items = items.AsQueryable();
                }

                // if specification is null it mean that we need entire list of entities
                if (specification is null)
                {
                    return Result.Ok<(IEnumerable<T> items, int count)>(new(items, items.Count()));
                }

                tuple = SpecificationEvaluator<T>.GetTupleQueryInt(items, specification);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

                return Result.Fail(ex.Message);
            }

            var resul = await tuple.items.ToListAsync();

            return Result.Ok<(IEnumerable<T> items, int count)>((resul, tuple.count));
        }
    }
}