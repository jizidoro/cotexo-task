using hexagonal.Application.Bases.Interfaces;
using hexagonal.Domain.Bases;

namespace hexagonal.Application.Components.BookComponent.Core;

public interface IUcBookDelete
{
    Task<ISingleResult<Entity>> Execute(int id);
}