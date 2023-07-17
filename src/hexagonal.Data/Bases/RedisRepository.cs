using System.Linq.Expressions;
using System.Text.Json;
using hexagonal.Domain.Bases;
using StackExchange.Redis;

namespace hexagonal.Data.Bases;

public class RedisRepository<TEntity> : IRedisRepository<TEntity>
    where TEntity : Entity
{
    private readonly IDatabase _database;

    public RedisRepository(IConnectionMultiplexer connectionMultiplexer)
    {
        _database = connectionMultiplexer.GetDatabase();
    }

    public Task CommitChangesAsync()
    {
        // Redis operates in a way where changes are committed as soon as they are made.
        return Task.CompletedTask;
    }

    public async Task Add(TEntity obj)
    {
        var serializedValue = JsonSerializer.Serialize(obj);
        await _database.StringSetAsync(obj.Id.ToString(), serializedValue);
    }

    public async Task AddAll(IList<TEntity> obj)
    {
        // Redis does not natively support batch insert operations, 
        // so we add them one by one.
        foreach (var entity in obj)
        {
            await Add(entity);
        }
    }

    public void Update(TEntity obj)
    {
        // In this case, Update is the same as Add.
        Add(obj).Wait();
    }

    public void UpdateAll(IList<TEntity> obj)
    {
        // Update all can be implemented in a similar way to AddAll.
        foreach (var entity in obj)
        {
            Update(entity);
        }
    }

    public void Remove(int id)
    {
        _database.KeyDeleteAsync(id.ToString());
    }

    public void RemoveAll(IList<int>? ids)
    {
        if (ids == null)
            return;

        foreach (var id in ids)
        {
            Remove(id);
        }
    }

    public async Task<TEntity?> GetById(int id)
    {
        var value = await _database.StringGetAsync(id.ToString());
        return value.IsNullOrEmpty ? null : JsonSerializer.Deserialize<TEntity>(value);
    }

    public Task<TEntity?> GetById(int id, params string[] includes)
    {
        // Redis does not support "includes" as it is a key-value store, 
        // so we just get the entity by id.
        return GetById(id);
    }

    public Task<TEntity?> GetById(int id, Expression<Func<TEntity, TEntity>> projection)
    {
        // Redis does not support projections, so we just get the entity by id.
        return GetById(id);
    }

    public Task<TEntity?> GetById(int id, Expression<Func<TEntity, TEntity>>? projection,
        params string[]? includes)
    {
        // Redis does not support projections or includes, so we just get the entity by id.
        return GetById(id);
    }
}