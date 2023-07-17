﻿using hexagonal.Data.Bases;
using hexagonal.Data.DataAccess;
using hexagonal.Domain;

namespace hexagonal.Data;

public class BookRepository : Repository<Book>, IBookRepository
{
    private readonly HexagonalContext _context;

    public BookRepository(HexagonalContext context)
        : base(context)
    {
        _context = context ??
                   throw new ArgumentNullException(nameof(context));
    }
}