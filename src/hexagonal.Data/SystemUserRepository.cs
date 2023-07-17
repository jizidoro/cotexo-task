using hexagonal.Data.Bases;
using hexagonal.Data.DataAccess;
using hexagonal.Domain;

namespace hexagonal.Data;

public class SystemUserRepository : Repository<SystemUser>, ISystemUserRepository
{
    private readonly HexagonalContext _context;

    public SystemUserRepository(HexagonalContext context)
        : base(context)
    {
        _context = context ??
                   throw new ArgumentNullException(nameof(context));
    }
}