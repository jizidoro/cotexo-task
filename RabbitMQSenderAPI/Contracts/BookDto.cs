namespace RabbitMQSenderAPI.Contracts;

public class BookDto
{
    public int? Id { get; set; }

    public string? Livro { get; set; }

    public string? Autor { get; set; }

    public int? CategoryId { get; set; }
    public string? CategoryName { get; set; }

    public int? TotalPaginas { get; set; }

    public bool? IsActive { get; set; }
}