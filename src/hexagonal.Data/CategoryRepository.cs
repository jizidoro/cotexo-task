using hexagonal.Data.Bases;
using hexagonal.Data.DataAccess;
using hexagonal.Domain;

namespace hexagonal.Data;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly HexagonalContext _context;

    public CategoryRepository(HexagonalContext context)
        : base(context)
    {
        _context = context ??
                   throw new ArgumentNullException(nameof(context));
    }
}