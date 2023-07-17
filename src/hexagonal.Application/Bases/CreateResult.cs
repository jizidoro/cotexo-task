using hexagonal.Domain.Bases;
using hexagonal.Domain.Enums;

namespace hexagonal.Application.Bases;

public class CreateResult<TEntity> : SingleResult<TEntity>
    where TEntity : Entity
{
    public CreateResult()
    {
        Code = (int) EnumResponse.Created;
        Success = true;
        Message = "";
    }

    public CreateResult(bool success, string? message)
    {
        Code = success ? (int) EnumResponse.Created : (int) EnumResponse.NotFound;
        Success = success;
        Message = message;
    }
}