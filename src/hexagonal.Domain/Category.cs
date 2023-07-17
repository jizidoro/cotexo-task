using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using hexagonal.Domain.Bases;

namespace hexagonal.Domain;

[Table("cate_category")]
public class Category : Entity
{
    [Column("cate_tx_name", TypeName = "varchar")]
    [MaxLength(255)]
    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}