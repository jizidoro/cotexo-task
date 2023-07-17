using System.Linq.Expressions;
using hexagonal.Domain.Bases.Interfaces;

namespace hexagonal.Data.Bases;

public interface IRedisRepository<TEntity>
    where TEntity : IEntity
{
    Task CommitChangesAsync();
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
}