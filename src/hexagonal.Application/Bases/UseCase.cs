using System.ComponentModel.DataAnnotations;
using hexagonal.Application.Bases.Interfaces;
using hexagonal.Domain.Bases;
using hexagonal.Domain.Bases.Interfaces;

namespace hexagonal.Application.Bases;

public class UseCase : IUseCase
{
    public static ISingleResult<Entity> ValidateEntity<T>(T entity) where T : IEntity
    {
        var context = new ValidationContext(entity, null, null);
        ICollection<ValidationResult> validationResults = new List<ValidationResult>();
        var valid = Validator.TryValidateObject(entity, context, validationResults, true);
        if (!valid)
        {
            var listErrors = validationResults.Select(x => x.ErrorMessage);
            return new SingleResult<Entity>(listErrors!);
        }

        return new SingleResult<Entity>();
    }
}