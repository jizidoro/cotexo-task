using hexagonal.Application;
using hexagonal.Domain;
using Moq;

namespace hexagonal.Tests;

public class BookServiceTests
{
    private readonly Mock<IBookService> _bookServiceMock;
    private readonly Book _testBook;

    public BookServiceTests()
    {
        // Arrange
        _bookServiceMock = new Mock<IBookService>();
        _testBook = new Book
        {
            Id = 1,
            Livro = "Book 1",
            TotalPaginas = 100
        };
    }

    [Fact]
    public async Task AddBookAsync_AddsBookSuccessfully()
    {
        // Arrange
        _bookServiceMock.Setup(p => p.AddBookAsync(_testBook)).ReturnsAsync(_testBook);

        // Act
        var result = await _bookServiceMock.Object.AddBookAsync(_testBook);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_testBook, result);
        _bookServiceMock.Verify(p => p.AddBookAsync(_testBook), Times.Once);
    }
}