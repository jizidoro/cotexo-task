using hexagonal.Domain.Bases.Interfaces;

namespace hexagonal.Application.Bases.Interfaces;

public interface ISingleResult<TEntity> : IResult
    where TEntity : IEntity
{
    TEntity? Data { get; set; }
}