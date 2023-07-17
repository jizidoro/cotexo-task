using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using hexagonal.Domain.Bases;

namespace hexagonal.Domain;

[Table("book_book")]
public class Book : Entity
{
    [Column("book_tx_livro", TypeName = "varchar")]
    [MaxLength(50)]
    [Required(ErrorMessage = "Livro is required")]
    public string? Livro { get; set; }

    [Column("book_tx_autor", TypeName = "varchar")]
    [MaxLength(50)]
    [Required(ErrorMessage = "Autor is required")]
    public string? Autor { get; set; }

    // This represents the foreign key in the database.
    [ForeignKey("Category")] public int? CategoryId { get; set; }

    // This is the navigation property.
    public Category? Category { get; set; }

    [Column("book_qt_totalpaginas", TypeName = "int")]
    [Required(ErrorMessage = "Total de Paginas is required")]
    public int? TotalPaginas { get; set; }

    [Column("book_bt_isactive", TypeName = "bit")]
    [Required(ErrorMessage = "Is Active is required")]
    public bool IsActive { get; set; }
}