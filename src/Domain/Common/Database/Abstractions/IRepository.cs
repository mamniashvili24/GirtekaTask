using System.Linq.Expressions;

namespace Domain.Common.Database.Abstractions;

public interface IRepository<T> where T : class
{    
    IAsyncEnumerable<T> GetAllAsyncEnumerable();

    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}