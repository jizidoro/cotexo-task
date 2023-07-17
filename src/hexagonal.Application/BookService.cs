using hexagonal.Data;
using hexagonal.Domain;

namespace hexagonal.Application;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;

    public BookService(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<IQueryable<Book>> GetAllBooksAsync()
    {
        return _repository.GetAll();
    }

    public async Task<Book?> GetBookAsync(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task<Book> AddBookAsync(Book book)
    {
        await _repository.BeginTransactionAsync().ConfigureAwait(false);
        await _repository.Add(book).ConfigureAwait(false);
        await _repository.CommitTransactionAsync().ConfigureAwait(false);
        return book;
    }

    public async Task<Book?> UpdateBookAsync(Book book)
    {
        var existingBook = await _repository.GetById(book.Id);
        if (existingBook != null)
        {
            existingBook.Livro = book.Livro;
            existingBook.TotalPaginas = book.TotalPaginas;

            await _repository.BeginTransactionAsync().ConfigureAwait(false);
            _repository.Update(existingBook);
            await _repository.CommitTransactionAsync().ConfigureAwait(false);
        }

        return existingBook;
    }

    public async Task DeleteBookAsync(int id)
    {
        var book = await _repository.GetById(id);
        if (book != null)
        {
            await _repository.BeginTransactionAsync().ConfigureAwait(false);
            _repository.Remove(book.Id);
            await _repository.CommitTransactionAsync().ConfigureAwait(false);
        }
    }
}