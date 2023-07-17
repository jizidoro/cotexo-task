using System.ComponentModel.DataAnnotations;
using hexagonal.Domain.Bases.Interfaces;

namespace hexagonal.Domain.Bases;

public abstract class Entity : IEntity
{
    [Key] public int Id { get; set; }

    public virtual int Key => Id;

    public virtual string Value => ToString()!;
}