using System.Linq.Expressions;
using FluentResults;
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

        public async Task<T> GetAsync(Guid id)
        {
            var item = await _dbSet.FindAsync(id);

            if (item is null)
            {
                _logger.Error($"Item with id {id} not found");
            }

            return item;
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> properties = null)
        {
            IEnumerable<T> result = Enumerable.Empty<T>();

            try
            {
                result = await _dbSet.Where(properties ?? (x => true)).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception has been appeared in {nameof(GetAllAsync)} - {ex.Message}");
            }

            return result;
        }

        public async Task<Result<T>> AddAsync(T item)
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
    }
}