using hexagonal.Domain.Bases;
using hexagonal.Domain.Enums;

namespace hexagonal.Application.Bases;

public class ErrorResult<TEntity> : SingleResult<TEntity>
    where TEntity : Entity
{
    public ErrorResult()
    {
        Code = (int) EnumResponse.ErrorBusinessValidation;
        Success = true;
        Message = "";
    }

    public ErrorResult(bool success, string? message)
    {
        Code = (int) EnumResponse.ErrorBusinessValidation;
        Success = success;
        Message = message;
    }
}