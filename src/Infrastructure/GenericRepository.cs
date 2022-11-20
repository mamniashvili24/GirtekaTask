using Infrastructure.Database;
using Domain.Common.Database.Abstractions;

namespace Infrastructure;

public class GenericRepository<T> : IRepository<T> where T : class
{
    private readonly ElectricityDbContext _context;

    public GenericRepository(ElectricityDbContext context)
    {
        _context = context;
    }
    
    public IAsyncEnumerable<T> GetAllAsyncEnumerable()
    {
        var result = _context.Set<T>()
                    .AsAsyncEnumerable();

        return result;
    }

    public virtual async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await _context.AddRangeAsync(entities, cancellationToken);
    }

    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await _context.SaveChangesAsync(cancellationToken);

        return result;
    }
}