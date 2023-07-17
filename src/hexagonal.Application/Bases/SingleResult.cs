using hexagonal.Application.Bases.Interfaces;
using hexagonal.Domain.Bases.Interfaces;
using hexagonal.Domain.Enums;

namespace hexagonal.Application.Bases;

public class SingleResult<TEntity> : ISingleResult<TEntity> where TEntity : IEntity
{
    public SingleResult()
    {
        Code = (int) EnumResponse.Ok;
        Success = true;
    }

    public SingleResult(IEnumerable<string> messages)
    {
        Code = (int) EnumResponse.BadRequest;
        Success = false;
        Messages = messages;
        Message = messages?.ToList().FirstOrDefault();
    }

    public SingleResult(int code, IEnumerable<string> messages)
    {
        Code = code;
        Success = false;
        Messages = messages;
        Message = messages?.ToList().FirstOrDefault();
    }


    public SingleResult(int code, string? message)
    {
        Code = code;
        Success = false;
        Message = message ?? string.Empty;
    }

    public SingleResult(TEntity? data)
    {
        Code = data == null ? (int) EnumResponse.NotFound : (int) EnumResponse.Ok;
        Success = data != null;
        Message = data == null
            ? ""
            : string.Empty;
        Data = data;
    }

    public string? ExceptionMessage { get; set; }
    public IEnumerable<string>? Messages { get; set; }
    public int Code { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }
    public TEntity? Data { get; set; }
}