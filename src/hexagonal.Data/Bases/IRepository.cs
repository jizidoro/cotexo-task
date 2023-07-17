using System.Linq.Expressions;
using hexagonal.Domain.Bases.Interfaces;

namespace hexagonal.Data.Bases;

public interface IRepository<TEntity> : IDisposable
    where TEntity : IEntity
{
    Task CommitChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task Add(TEntity obj);
    Task AddAll(IList<TEntity> obj);
    void Update(TEntity obj);
    void UpdateAll(IList<TEntity> obj);
    void Remove(int id);
    void RemoveAll(IList<int>? ids);
    Task<TEntity?> GetById(int id);
    Task<TEntity?> GetById(int id, params string[] includes);
    Task<TEntity?> GetById(int id, Expression<Func<TEntity, TEntity>> projection);

    Task<TEntity?> GetById(int id, Expression<Func<TEntity, TEntity>>? projection,
        params string[]? includes);

    Task<TEntity?> GetByValue(string value);
    Task<TEntity?> GetByValue(string value, Expression<Func<TEntity, TEntity>>? projection);
    Task<bool> ValueExists(int id, string value);
    Task<bool> IsUnique(Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntity> GetAll();
    IQueryable<TEntity> GetAllAsNoTracking();
    IEnumerable<TEntity> GetAllAsNoTracking(Expression<Func<TEntity, TEntity>> projection);
    Task<TEntity?> GetByPredicate(Expression<Func<TEntity, bool>> predicate);
}